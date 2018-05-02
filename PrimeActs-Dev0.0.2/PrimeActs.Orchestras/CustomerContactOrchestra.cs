using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Customer;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class CustomerContactOrchestra : ICustomerContactOrchestra
    {
        private readonly ICustomerContactService _customerContactService;
        private readonly ICustomerContactLocationService _customerContactLocationService;
        private readonly ICustomerContactDepartmentService _customerContactDepartmentService;
        private readonly ICustomerLocationService _customerLocationService;
        private readonly ICustomerDepartmentService _customerDepartmentService;
        private readonly IContactService _contactService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public CustomerContactOrchestra(ICustomerContactService customerContactService,
            ICustomerContactDepartmentService customerContactDepartmentService,
            ICustomerContactLocationService customerContactLocationService,
            ICustomerLocationService customerLocationService,
            ICustomerDepartmentService customerDepartmentService,
            ISetupLocalService setupLocalService,
            IContactService contactService,
            INoteService noteService)
        {
            _customerContactService = customerContactService;
            _customerContactLocationService = customerContactLocationService;
            _customerContactDepartmentService = customerContactDepartmentService;
            _customerLocationService = customerLocationService;
            _customerDepartmentService = customerDepartmentService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _contactService = contactService;
            _noteService = noteService;
        }

        public List<CustomerContactModel> GetCustomerContactModels(List<Guid> customerContactIds)
        {
            var list = _customerContactService
                .GetCustomerContacts(customerContactIds);
            var result = new List<CustomerContactModel>();
            foreach (var customerContact in list)
            {
                result.Add(new CustomerContactModel()
                {
                    CustomerContactID = customerContact.CustomerContactID,
                    ContactID = customerContact.ContactID,
                    FirstName = customerContact.Contact.FirstName,
                    LastName = customerContact.Contact.LastName,
                    Title = customerContact.Contact.Title,
                    ContactType = customerContact.Contact.ContactType,
                    ContactReference = customerContact.Contact.ContactReference,
                    EmailAddress = customerContact.Contact.EmailAddress,
                    DDITelephoneNo = customerContact.Contact.DDITelephoneNo,
                    MobileNo = customerContact.Contact.MobileNo,
                    CustomerID = customerContact.CustomerID,
                    SortOrder = customerContact.SortOrder,
                });
            }
            return result;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<CustomerContactModel> BuildCustomerContactModels(List<CustomerContact> customerContacts)
        {
            var list = new List<CustomerContactModel>();
            foreach (var item in customerContacts)
            {
                var model = new CustomerContactModel();
                model.CustomerContactID = item.CustomerContactID;
                model.CustomerID = item.CustomerID;
                model.SortOrder = item.SortOrder.HasValue ? item.SortOrder.Value : Guid.Empty;
                model.ContactID = item.ContactID;
                var contact = _contactService.GetContactById(item.ContactID);
                model.EmailAddress = contact.EmailAddress;
                model.FirstName = contact.FirstName;
                model.LastName = contact.LastName;
                model.Title = contact.Title;
                model.ContactType = contact.ContactType;
                model.ContactReference = contact.ContactReference;
                model.DDITelephoneNo = contact.DDITelephoneNo;
                model.MobileNo = contact.MobileNo;
                model.NoteID = contact.NoteID;
                model.Notes = contact.NoteID.HasValue ? _noteService.Find(contact.NoteID.Value).NoteText : string.Empty;
                // setup LxbViewModel of ListBox
                var customerContactLocations = _customerContactLocationService.GetCustomerContactLocationListByConId(item.CustomerContactID);
                model.SelectedLocationIds = new List<string>();
                model.LbxLocationOptions = new List<LbxViewModel>();
                foreach (var ccl in customerContactLocations)
                {
                    model.SelectedLocationIds.Add(ccl.CustomerLocationID.ToString());
                    var lxbItem = new LbxViewModel();
                    lxbItem.Id = ccl.CustomerLocationID.ToString();
                    lxbItem.label = _customerLocationService.CustomerLocationById(ccl.CustomerLocationID).CustomerLocationName;
                    lxbItem.value = ccl.CustomerLocationID.ToString();
                    model.LbxLocationOptions.Add(lxbItem);
                }
                var customerContactDepartments = _customerContactDepartmentService.GetCustomerContactDepartmentListByConId(item.CustomerContactID);
                model.SelectedDepartmentIds = new List<string>();
                model.LbxDepartmentOptions = new List<LbxViewModel>();
                foreach (var ccd in customerContactDepartments)
                {
                    model.SelectedDepartmentIds.Add(ccd.CustomerDepartmentID.ToString());
                    var lxbItem = new LbxViewModel();
                    lxbItem.Id = ccd.CustomerDepartmentID.ToString();
                    lxbItem.label = _customerDepartmentService.CustomerDepartmentById(ccd.CustomerDepartmentID.Value).CustomerDepartmentName;
                    lxbItem.value = ccd.CustomerDepartmentID.ToString();
                    model.LbxDepartmentOptions.Add(lxbItem);
                }
                list.Add(model);
            }
            return list;
        }

        public CustomerContactModel CreateCustomerContact(CustomerContactModel model, List<Guid> locaGuidList, List<Guid> depaGuidList)
        {
            try
            {
                var contItem = ApplyChanges(model);
                if (model.CustomerContactID == Guid.Empty
                    && model.ItemAdding && !model.ItemDeleting)
                {
                    model.CustomerContactID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    contItem.CustomerContactID = model.CustomerContactID;
                    contItem.ObjectState = ObjectState.Added;
                    _customerContactService.Insert(contItem);
                    InsertCustomerContactLocation(contItem, locaGuidList);
                    InsertCustomerContactDepartment(contItem, depaGuidList);
                }
                else if (!model.ItemAdding && !model.ItemDeleting)
                {
                    contItem.ObjectState = ObjectState.Modified;
                    _customerContactService.Update(contItem);
                    //UpdateSupplierContactLocation(contItem, locaGuidList);   /////////////////
                    //UpdateSupplierContactDepartment(contItem, depaGuidList); ////////////////
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Customer Contact failed", ex);
            }
            return model;
        }

        private void InsertCustomerContactLocation(CustomerContact contItem, List<Guid> locaGuidList)
        {
            var item = new CustomerContactLocation();
            item.CustomerContactLocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
            item.CustomerContactID = contItem.CustomerContactID;
            item.CreatedBy = contItem.CreatedBy;
            item.CreatedDate = contItem.CreatedDate;
            item.UpdatedBy = contItem.UpdatedBy;
            item.UpdatedDate = contItem.UpdatedDate;
            foreach (var locaGuid in locaGuidList)
            {
                item.CustomerLocationID = locaGuid;
                _customerContactLocationService.Insert(item);
            }
        }

        private void InsertCustomerContactDepartment(CustomerContact contItem, List<Guid> depaGuidList)
        {
            var item = new CustomerContactDepartment();
            item.CustomerContactDepartmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
            item.CustomerContactID = contItem.CustomerContactID;
            item.CreatedBy = contItem.CreatedBy;
            item.CreatedDate = contItem.CreatedDate;
            item.UpdatedBy = contItem.UpdatedBy;
            item.UpdatedDate = contItem.UpdatedDate;
            foreach (var depaGuid in depaGuidList)
            {
                item.CustomerDepartmentID = depaGuid;
                _customerContactDepartmentService.Insert(item);
            }
        }

        private CustomerContact ApplyChanges(CustomerContactModel model)
        {
            var _obj = new CustomerContact();
            _obj.CustomerContactID = model.CustomerContactID;
            _obj.CustomerID = model.CustomerID;
            _obj.ContactID = SaveContact(model);
            _obj.SortOrder = model.SortOrder; /////////////--- ??? --- !!!
            _obj.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                ? DateTime.Parse(model.CreatedDate)
                : DateTime.Now;
            _obj.CreatedBy = _principal.Id;
            _obj.UpdatedDate = DateTime.Now;
            _obj.UpdatedBy = _principal.Id;
            _obj.IsActive = true;
            return _obj;
        }

        private Guid SaveContact(CustomerContactModel model)
        {
            if (!string.IsNullOrEmpty(model.FirstName)
            && !string.IsNullOrEmpty(model.LastName))
            {
                if (model.ContactID == null || model.ContactID == Guid.Empty)
                    model.ContactID = CreateContact(model);
                else model.ContactID = UpdateContact(model);
            }
            return model.ContactID;
        }

        private Guid CreateContact(CustomerContactModel model)
        {
            var _noteId = SaveNote(model);
            var _obj = new Contact
            {
                ContactID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Title = model.Title,
                ContactType = model.ContactType,
                ContactReference = model.ContactReference,
                EmailAddress = model.EmailAddress,
                DDITelephoneNo = model.DDITelephoneNo,
                MobileNo = model.MobileNo,
                NoteID = _noteId,
                ObjectState = ObjectState.Added
            };
            _contactService.Insert(_obj);
            return _obj.ContactID;
        }

        private Guid UpdateContact(CustomerContactModel model)
        {
            var _noteId = SaveNote(model);
            var item = _contactService.Find(model.ContactID);
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Title = model.Title;
            item.ContactType = model.ContactType;
            item.ContactReference = model.ContactReference;
            item.EmailAddress = model.EmailAddress;
            item.DDITelephoneNo = model.DDITelephoneNo;
            item.MobileNo = model.MobileNo;
            item.NoteID = _noteId;
            item.ObjectState = ObjectState.Modified;
            _contactService.Update(item);
            return item.ContactID;
        }

        private Guid? SaveNote(CustomerContactModel model)
        {
            if (string.IsNullOrEmpty(model.NoteDescription))
                model.NoteDescription = "?!";  // <--- Paul Edwards
            if (!string.IsNullOrEmpty(model.Notes))
            {
                if (model.NoteID == null || model.NoteID == Guid.Empty)
                    model.NoteID = CreateNote(model);
                else model.NoteID = UpdateNote(model);
            }
            return model.NoteID;
        }

        private Guid CreateNote(CustomerContactModel model)
        {
            var note = new Note
            {
                NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                NoteText = model.Notes,
                NoteDescription = model.NoteDescription,
                ObjectState = ObjectState.Added
            };
            _noteService.Insert(note);
            return note.NoteID;
        }

        private Guid UpdateNote(CustomerContactModel model)
        {
            var note = _noteService.Find(model.NoteID.Value);
            note.NoteText = model.Notes;
            note.NoteDescription = model.NoteDescription;
            note.ObjectState = ObjectState.Modified;
            _noteService.Update(note);
            return note.NoteID;
        }
    }
}
