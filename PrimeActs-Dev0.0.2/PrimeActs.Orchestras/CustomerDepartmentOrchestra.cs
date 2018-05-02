using PrimeActs.Data.Service;
using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Customer;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class CustomerDepartmentOrchestra : ICustomerDepartmentOrchestra
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerDepartmentService _customerDepartmentService;
        private readonly ICustomerDepartmentLocationService _customerDepartmentLocationService;
        private readonly ICustomerLocationService _customerLocationService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public CustomerDepartmentOrchestra(ICustomerDepartmentService customerDepartmentService,
            ICustomerDepartmentLocationService customerDepartmentLocationService,
            ICustomerLocationService customerLocationService,
            ISetupLocalService setupLocalService, INoteService noteService,
            ICustomerService customerService)
        {
            _customerDepartmentService = customerDepartmentService;
            _customerService = customerService;
            _customerDepartmentLocationService = customerDepartmentLocationService;
            _customerLocationService = customerLocationService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _noteService = noteService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<CustomerDepartmentEditModel> BuildCustomerDepartmentModels(List<CustomerDepartment> customerDepartments)
        {
            var list = new List<CustomerDepartmentEditModel>();
            foreach (var item in customerDepartments)
            {
                var model = new CustomerDepartmentEditModel();
                model.CustomerDepartmentID = item.CustomerDepartmentID;
                model.CustomerDepartmentName = item.CustomerDepartmentName;
                model.CustomerID = item.CustomerID;
                model.EmailAddress = item.EmailAddress;
                model.Commission = item.Commission.HasValue ? item.Commission.Value : 0;
                model.Handling = item.Handling.HasValue ? item.Handling.Value : 0;
                model.RebateType = item.RebateType;
                model.RebateRate = item.RebateRate.HasValue ? item.RebateRate.Value : 0;
                model.RebateCustomerDepartmentID = item.RebateCustomerDepartmentID; //.HasValue ? item.RebateCustomerDepartmentID.Value : Guid.Empty
                model.RebateCustomerCompany_DepartmentName = item.RebateCustomerDepartmentID.HasValue
                    ? _customerDepartmentService.CustomerDepartmentByCustomerDepartmentId(item.RebateCustomerDepartmentID.Value).CustomerDepartmentName
                    : string.Empty;
                model.SalesPersonUserID = item.SalesPersonUserID;
                //model.SalesPersonName = __get_name__(item.SalesPersonUserID); ////////////////////
                model.InvoiceCustomerLocationID = item.InvoiceCustomerLocationID;
                model.InvoiceCustomerLocation_Name = _customerDepartmentLocationService
                    .GetCustomerDepartmentLocationByLocId(item.InvoiceCustomerLocationID)
                    .CustomerLocation.CustomerLocationName;
                model.InvoiceFrequency = item.InvoiceFrequency.ToString();
                model.TransactionTaxReference = item.TransactionTaxReference;
                model.CreditTerms = item.CreditTerms.HasValue ? item.CreditTerms.Value : 0;
                model.CreditLimit = item.CreditLimit.HasValue ? item.CreditLimit.Value : 0;
                model.NoteID = item.NoteID;
                model.Notes = item.NoteID.HasValue ? _noteService.Find(item.NoteID.Value).NoteText : string.Empty;
                // setup LxbViewModel of ListBox
                var customerDepartmentLocations = _customerDepartmentLocationService.GetCustomerDepartmentLocationListByDepId(item.CustomerDepartmentID);
                model.SelectedLocationIds = new List<string>();
                model.LbxLocationOptions = new List<LbxViewModel>();
                foreach (var cdl in customerDepartmentLocations)
                {
                    model.SelectedLocationIds.Add(cdl.CustomerLocationID.ToString());
                    var lxbItem = new LbxViewModel();
                    lxbItem.Id = cdl.CustomerLocationID.ToString();
                    lxbItem.label = _customerLocationService.CustomerLocationById(cdl.CustomerLocationID.Value).CustomerLocationName;
                    lxbItem.value = cdl.CustomerLocationID.ToString();
                    model.LbxLocationOptions.Add(lxbItem);
                }
                list.Add(model);
            }
            return list;
        }

        public CustomerDepartmentEditModel CreateCustomerDepartment(CustomerDepartmentEditModel model, List<Guid> locaGuidList)
        {
            try
            {
                var item = ApplyChanges(model);
                item.NoteID = SaveNote(model);
                if (model.CustomerDepartmentID == Guid.Empty
                    && model.ItemAdding && !model.ItemDeleting)
                {
                    model.CustomerDepartmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    item.CustomerDepartmentID = model.CustomerDepartmentID;
                    item.ObjectState = ObjectState.Added;
                    _customerDepartmentService.Insert(item);
                    InsertCustomerDepartmentLocation(item, locaGuidList);
                }
                else if (!model.ItemAdding && !model.ItemDeleting)
                {
                    item.ObjectState = ObjectState.Modified;
                    _customerDepartmentService.Update(item);
                    //UpdateCustomerDepartmentLocation(item, locaGuidList); //nem
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Customer Department failed", ex);
            }
            return model;
        }

        private void InsertCustomerDepartmentLocation(CustomerDepartment depaItem, List<Guid> locaGuidList)
        {
            var item = new CustomerDepartmentLocation();
            item.CustomerDepartmentLocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
            item.CustomerDepartmentID = depaItem.CustomerDepartmentID;
            item.CreatedBy = depaItem.CreatedBy;
            item.CreatedDate = depaItem.CreatedDate;
            item.UpdatedBy = depaItem.UpdatedBy;
            item.UpdatedDate = depaItem.UpdatedDate;
            foreach (var locaGuid in locaGuidList)
            {
                item.CustomerLocationID = locaGuid;
                _customerDepartmentLocationService.Insert(item);
            }
        }

        private CustomerDepartment ApplyChanges(CustomerDepartmentEditModel model)
        {
            return new CustomerDepartment()
            {
                CustomerDepartmentID = model.CustomerDepartmentID,
                Commission = model.Commission,
                Handling = model.Handling,
                CustomerDepartmentName = model.CustomerDepartmentName,
                CustomerID = model.CustomerID,
                CustomerTypeID = model.CustomerTypeID,
                RebateType = model.RebateType,
                RebateRate = model.RebateRate,
                FactorRef = model.FactorRef,
                TransactionTaxReference = model.TransactionTaxReference,
                CreditTerms = model.CreditTerms,
                CreditLimit = model.CreditLimit,
                EmailAddress = model.EmailAddress,
                NoteID = model.NoteID,
                CreatedDate = !string.IsNullOrEmpty(model.CreatedDate) ? DateTime.Parse(model.CreatedDate) : DateTime.Now,
                CreatedBy = _principal.Id,
                UpdatedDate = DateTime.Now,
                UpdatedBy = _principal.Id,
                IsActive = true,
            };
        }

        private Guid? SaveNote(CustomerDepartmentEditModel model)
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

        private Guid CreateNote(CustomerDepartmentEditModel model, ApplicationUser author)
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

        private Guid UpdateNote(CustomerDepartmentEditModel model, ApplicationUser author)
        {
            var note = _noteService.Find(model.NoteID.Value);
            note.NoteText = model.Notes;
            note.NoteDescription = model.NoteDescription;
            note.ObjectState = ObjectState.Modified;
            _noteService.Update(note);
            return note.NoteID;
        }

        public CustomerDepartmentEditModel GetCustomerDepartmentEditModel(Guid id)
        {
            var entity = _customerDepartmentService.CustomerDepartmentById(id);
            var customerCompanyName = _customerService.CustomerById(entity.CustomerID).CustomerCompanyName;
            var model = new CustomerDepartmentEditModel()
            {
                CustomerDepartmentID = entity.CustomerDepartmentID,
                CustomerDepartmentName = entity.CustomerDepartmentName,
                RebateCustomerDepartmentID = entity.RebateCustomerDepartmentID,
                RebateRate = entity.RebateRate,
                RebateType = entity.RebateType,
                RebateCustomerCompany_DepartmentName = customerCompanyName + " - " + entity.CustomerDepartmentName
            };
            return model;
        }

        public List<CustomerDepartmentViewModel> GetAllCustomerDepartments()
        {
            List<CustomerDepartment> listOfAll = _customerDepartmentService.GetAllCustomerDepartments();
            var cdvmList = new List<CustomerDepartmentViewModel>();
            foreach (var entity in listOfAll)
            {
                var customerCompanyName = _customerService.CustomerById(entity.CustomerID).CustomerCompanyName;
                var model = new CustomerDepartmentViewModel()
                {
                    CustomerDepartmentID = entity.CustomerDepartmentID,
                    CustomerDepartmentName = entity.CustomerDepartmentName,
                    RebateCustomerDepartmentID = entity.RebateCustomerDepartmentID,
                    RebateRate = entity.RebateRate,
                    RebateType = entity.RebateType,
                    CustomerDepartmentEditModels = entity.Customer.CustomerDepartments
                    .Select(cd => new CustomerDepartmentEditModel()
                    {
                        CustomerDepartmentID = cd.CustomerDepartmentID,
                        CustomerDepartmentName = cd.CustomerDepartmentName,
                        RebateCustomerCompany_DepartmentName = customerCompanyName
                                               + " - " + cd.CustomerDepartmentName
                    }).ToList()
                };
                cdvmList.Add(model);
            }
            return cdvmList;
        }

        public CustomerDepartmentViewModel GetCustomerDepartment(Guid id)
        {
            var entity = _customerDepartmentService.CustomerDepartmentById(id);
            var customerCompanyName = _customerService.CustomerById(entity.CustomerID).CustomerCompanyName;
            var model = new CustomerDepartmentViewModel()
            {
                CustomerDepartmentID = entity.CustomerDepartmentID,
                CustomerDepartmentName = entity.CustomerDepartmentName,
                RebateCustomerDepartmentID = entity.RebateCustomerDepartmentID,
                RebateRate = entity.RebateRate,
                RebateType = entity.RebateType,
                CustomerDepartmentEditModels = entity.Customer.CustomerDepartments.Select(cd => new CustomerDepartmentEditModel()
                {
                    CustomerDepartmentID = cd.CustomerDepartmentID,
                    CustomerDepartmentName = cd.CustomerDepartmentName,
                    RebateCustomerCompany_DepartmentName = customerCompanyName
                                           + " - " + cd.CustomerDepartmentName
                }).ToList()
            };
            return model;
        }

        public CustomerDepartmentViewModel UpdateRebate(CustomerDepartmentViewModel model)
        {
            var entity = _customerDepartmentService.CustomerDepartmentById(model.CustomerDepartmentID);
            entity.RebateType = (byte)model.RebateType;
            entity.RebateRate = model.RebateRate;
            entity.RebateCustomerDepartmentID = model.RebateCustomerDepartmentID;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = _principal.Id;
            _customerDepartmentService.Update(entity);
            return model;
        }

        public List<CustomerDepartment> GetCustomerDepartments(Guid customerDepartmentID)
        {
            return _customerDepartmentService.CustomerDepartmentsByCustomerDepartmentID(customerDepartmentID);
        }
    }
}
