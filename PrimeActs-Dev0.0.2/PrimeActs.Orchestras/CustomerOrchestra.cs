using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class CustomerOrchestra : ICustomerOrchestra
    {
        private readonly ICustomerService _customerService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;
        private IApplicationUserService _appUserService;
        private readonly ICustomerLocationOrchestra _locationOrchestra;
        private readonly ICustomerDepartmentOrchestra _departmentOrchestra;
        private readonly ICustomerContactOrchestra _contactOrchestra;
        private readonly ICustomerDepartmentService _customerDepartmentService;

        public CustomerOrchestra(ICustomerService customerService,
            ICustomerLocationOrchestra locationOrchestra, ICustomerDepartmentOrchestra departmentOrchestra,
            ICustomerContactOrchestra contactOrchestra, ICustomerDepartmentService customerDepartmentService,
            INoteService noteService, IApplicationUserService appUserService,
            ISetupLocalService setupLocalService)
        {
            _customerService = customerService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _departmentOrchestra = departmentOrchestra;
            _locationOrchestra = locationOrchestra;
            _noteService = noteService;
            _appUserService = appUserService;
            _contactOrchestra = contactOrchestra;
            _customerDepartmentService = customerDepartmentService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<CustomerEditModel> GetCustomerModelsForAC(string search)
        {
            var list = string.IsNullOrEmpty(search)
                ? null
                : _customerService.GetAllCustomers()
                    .Where(x =>
                        x.CustomerCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) |
                        x.CustomerCompanyName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    .Select(BuildCustomerEditModelAC)
                    .ToList();
            return list;
        }

        public List<CustomerDepartmentEditModel> GetCustomerDepartmentModelsForAC(string search)
        {

            List<CustomerDepartmentEditModel> customerDepartmentEditModels = new List<CustomerDepartmentEditModel>();
            var customers = _customerService.Query(x => x.CustomerCode.StartsWith(search) |
                          x.CustomerCompanyName.StartsWith(search))
                    .Include(inc => inc.CustomerDepartments)
                    .Select().ToList();
          foreach (var item in customers)
          {
              foreach (var itemCD in item.CustomerDepartments)
              {
                  customerDepartmentEditModels.Add(BuildCustomerDepartmentEditModelAC(itemCD));    
              }
          }
          return customerDepartmentEditModels;
          
        }

        public List<CustomerEditModel> GetCustomerForAutoComplete(string search)
        {
            return
                _customerService.GetAllCustomers()
                    .Where(inc => inc.CustomerCompanyName.StartsWith(search) | inc.CustomerCode.StartsWith(search))
                    .Select(inc => BuildCustomerEditModel(inc))
                    .ToList();
        }

        private CustomerEditModel BuildCustomerEditModelAC(Customer entity)
        {
            return new CustomerEditModel
            {
                CustomerID = entity.CustomerID,
                CustomerCode = entity.CustomerCode,
                CustomerCompanyName = entity.CustomerCompanyName
            };
        }

        public CustomerEditModel CreateCustomer(CustomerEditModel model)
        {
            try
            {
                var item = ApplyChanges(model);
                item.NoteID = SaveNote(model);
                if (model.CustomerID == Guid.Empty)
                {
                    model.CustomerID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    item.CustomerID = model.CustomerID;
                    item.ObjectState = ObjectState.Added;
                    _customerService.Insert(item);
                }
                else
                {
                    item.ObjectState = ObjectState.Modified;
                    _customerService.Update(item);
                }
                SaveCustomerDataIn3lists(model);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Customer failed", ex);
            }
            return model;
        }

        private void SaveCustomerDataIn3lists(CustomerEditModel model)
        {
            Dictionary<string, Guid> locaDic = new Dictionary<string, Guid>();
            Dictionary<string, Guid> depaDic = new Dictionary<string, Guid>();
            foreach (var item in model.CustomerLocations)
            {
                var result = _locationOrchestra.CreateCustomerLocation(item);
                //if (IsIdGuid(item.CustomerLocationID.ToString())) ///////////////////////////
                if (!item.ItemAdding && !item.ItemDeleting)
                    item.x_CustomerLocationID = item.CustomerLocationID.ToString();
                locaDic[item.x_CustomerLocationID] = result.CustomerLocationID;
            }
            foreach (var item in model.CustomerDepartments)
            {
                var locaGuidList = new List<Guid>();
                foreach (var xId in item.SelectedLocationIds)
                    locaGuidList.Add(locaDic[xId]);
                var result = _departmentOrchestra.CreateCustomerDepartment(item, locaGuidList);
                //if (IsIdGuid(item.CustomerDepartmentID.ToString())) //////////////////////////
                if (!item.ItemAdding && !item.ItemDeleting)
                    item.x_CustomerDepartmentID = item.CustomerDepartmentID.ToString();
                depaDic[item.x_CustomerDepartmentID] = result.CustomerDepartmentID;
            }
            foreach (var item in model.CustomerContacts)
            {
                var locaGuidList = new List<Guid>();
                foreach (var xId in item.SelectedLocationIds)
                    locaGuidList.Add(locaDic[xId]);
                var depaGuidList = new List<Guid>();
                foreach (var xId in item.SelectedDepartmentIds)
                    depaGuidList.Add(depaDic[xId]);
                _contactOrchestra.CreateCustomerContact(item, locaGuidList, depaGuidList);
            }
        }

        private bool IsIdGuid(string expression)
        {
            if (expression != null)
            {
                Regex guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                return guidRegEx.IsMatch(expression);
            }
            return false;
        }

        private Customer ApplyChanges(CustomerEditModel model)
        {
            var item = new Customer();
            item.CustomerID = model.CustomerID == Guid.Empty
                ? PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]) : model.CustomerID;
            CustomerItemsOf3lists(model, item.CustomerID);
            item.CustomerCode = model.CustomerCode;
            item.CustomerCompanyName = model.CustomerCompanyName;
            item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate) ? DateTime.Parse(model.CreatedDate) : DateTime.Now;
            item.CreatedBy = _principal.Id;
            item.UpdatedDate = DateTime.Now;
            item.UpdatedBy = _principal.Id;
            item.IsActive = true;
            item.IsTransfer = 0; // model.IsTransfer.HasValue ? model.IsTransfer.Value : <--- Paul Edwards
            item.Statements = model.Statements;
            item.ParentCustomerID = model.ParentCustomerID;
            item.CompanyID = model.CompanyID;
            return item;
        }

        private void CustomerItemsOf3lists(CustomerEditModel model, Guid id)
        {
            foreach (var item in model.CustomerContacts)
            {
                item.CustomerID = id;
                item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                    ? DateTime.Parse(model.CreatedDate).ToString() : DateTime.Now.ToString();
                item.CreatedByUserID = _principal.Id.ToString();
                item.UpdatedDate = DateTime.Now.ToString();
                item.UpdatedByUserID = _principal.Id.ToString();
                item.IsActive = true;
            }
            foreach (var item in model.CustomerDepartments)
            {
                item.CustomerID = id;
                item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                    ? DateTime.Parse(model.CreatedDate).ToString() : DateTime.Now.ToString();
                item.CreatedBy = _principal.Id.ToString();
                item.UpdatedDate = DateTime.Now.ToString();
                item.UpdatedBy = _principal.Id.ToString();
                item.IsActive = true;
            }
            foreach (var item in model.CustomerLocations)
            {
                item.CustomerID = id;
                item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                    ? DateTime.Parse(model.CreatedDate).ToString() : DateTime.Now.ToString();
                item.CreatedBy = _principal.Id.ToString();
                item.UpdatedDate = DateTime.Now.ToString();
                item.UpdatedBy = _principal.Id.ToString();
                item.IsActive = true;
            }
        }

        private CustomerDepartmentEditModel BuildCustomerDepartmentEditModelAC(CustomerDepartment cdentity)
        {         
                CustomerDepartmentEditModel strCustDeptList = new CustomerDepartmentEditModel();
                strCustDeptList.CustomerDepartmentName= cdentity.Customer.CustomerCompanyName + " - ";
                strCustDeptList.CustomerDepartmentName = strCustDeptList.CustomerDepartmentName +  cdentity.CustomerDepartmentName;
                strCustDeptList.CustomerDepartmentID = cdentity.CustomerDepartmentID;
                strCustDeptList.CustomerCompanyName = cdentity.Customer.CustomerCompanyName;
                return strCustDeptList;
        }

        private Guid? SaveNote(CustomerEditModel model)
        {
            if (string.IsNullOrEmpty(model.NoteDescription))
                model.NoteDescription = "?!";  // <--- Paul Edwards
            if (!string.IsNullOrEmpty(model.Notes))
            {
                if (model.NoteID == null || model.NoteID == Guid.Empty)
                    model.NoteID = CreateNote(model, _principal);
                else model.NoteID = UpdateNote(model, _principal);
            }
            return model.NoteID;
        }

        private Guid CreateNote(CustomerEditModel model, ApplicationUser author)
        {
            var note = new Note
            {
                NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                NoteText = model.Notes,
                NoteDescription = model.NoteDescription,
                // IsActive = true,
                ObjectState = ObjectState.Added
            };
            _noteService.Insert(note);
            return note.NoteID;
        }

        private Guid UpdateNote(CustomerEditModel model, ApplicationUser author)
        {
            var note = _noteService.Find(model.NoteID.Value);
            note.NoteText = model.Notes;
            note.NoteDescription = model.NoteDescription;
            note.ObjectState = ObjectState.Modified;
            _noteService.Update(note);
            return note.NoteID;
        }

        public CustomerEditModel GetCustomerModelByCustomerID(Guid customerID)
        {
            Customer entity = _customerService.GetCustomerByIdFromRepo(customerID);
            CustomerEditModel customerModel = entity != null
                ? BuildCustomerEditModel(entity)
                : null;
            return customerModel;
        }

        private CustomerEditModel BuildCustomerEditModel(Customer entity)
        {
            var customerEditModel = new CustomerEditModel();
            customerEditModel.CustomerID = entity.CustomerID;
            customerEditModel.CustomerCode = entity.CustomerCode;
            customerEditModel.CustomerCompanyName = entity.CustomerCompanyName;
            customerEditModel.ParentCustomerID = entity.ParentCustomerID;
            customerEditModel.ParentCustomerName = entity.ParentCustomer.CustomerCompanyName;
            customerEditModel.IsTransfer = entity.IsTransfer;
            customerEditModel.Statements = entity.Statements;
            customerEditModel.CompanyID = entity.CompanyID;
            customerEditModel.CompanyName = entity.Company.CompanyName;
            if (entity.NoteID.HasValue)
            {
                customerEditModel.NoteID = entity.NoteID;
                entity.Note = _noteService.NoteById(entity.NoteID.Value);
                customerEditModel.Notes = entity.Note.NoteText;
            }
            customerEditModel.IsActive = entity.IsActive ?? true;
            customerEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
            customerEditModel.CreatedBy = entity.CreatedBy.HasValue ? entity.CreatedBy.Value.ToString() : string.Empty;
            customerEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
            customerEditModel.UpdatedBy = entity.UpdatedBy.HasValue ? entity.UpdatedBy.Value.ToString() : string.Empty;
            customerEditModel.CustomerLocations = _locationOrchestra.BuildCustomerLocationModels(entity.CustomerLocations);
            customerEditModel.CustomerDepartments = _departmentOrchestra.BuildCustomerDepartmentModels(entity.CustomerDepartments);
            customerEditModel.CustomerContacts = _contactOrchestra.BuildCustomerContactModels(entity.CustomerContacts);
            return customerEditModel;
        }

        public CustomerPagingModel GetCustomerPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var customerPagingModel = new CustomerPagingModel();
            var items = _customerService.GetCustomers(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<CustomerEditModel>(items.Select(BuildCustomerEditModel).ToList(), queryOptions);
            customerPagingModel.CustomerEditModels = result;
            customerPagingModel.SearchObject = new SearchObject
            {
                CustomerCode = searchObject.CustomerCode,
                CustomerCompanyName = searchObject.CustomerCompanyName,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null,
                RecordsInDays = searchObject.RecordsInDays
            };
            return customerPagingModel;
        }
    }
}

//private CustomerViewModel BuildCustomerViewModel(Customer customer, List<Customer> customers)
//{
//    var customerViewModel = new CustomerViewModel();
//    customerViewModel.CustomerEditModel = customer == null
//        ? new CustomerEditModel()
//        : BuildCustomerEditModel(customer);
//    customerViewModel.CustomerEditModels = customers != null
//        ? customers.Select(x => BuildCustomerEditModel(x)).ToList()
//        : null;
//    return customerViewModel;
//}

//public CustomerViewModel GetCustomerViewModels(int page, int pageSize, string searchString)
//{
//    throw new NotImplementedException();
//}

//public CustomerViewModel GetCustomerViewModel()
//{
//    throw new NotImplementedException();
//}
