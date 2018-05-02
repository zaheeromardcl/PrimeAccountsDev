using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class SupplierContactOrchestra : ISupplierContactOrchestra
    {
        private readonly ISupplierContactService _supplierContactService;
        private readonly ISupplierContactLocationService _supplierContactLocationService;
        private readonly ISupplierContactDepartmentService _supplierContactDepartmentService;
        private readonly ISupplierLocationService _supplierLocationService;
        private readonly ISupplierDepartmentService _supplierDepartmentService;
        private readonly IContactService _contactService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public SupplierContactOrchestra(ISupplierContactService supplierContactService,
            ISupplierContactDepartmentService supplierContactDepartmentService,
            ISupplierContactLocationService supplierContactLocationService,
            ISupplierDepartmentService supplierDepartmentService,
            ISupplierLocationService supplierLocationService,
            ISetupLocalService setupLocalService,
            IContactService contactService,
            INoteService noteService)
        {
            _supplierContactService = supplierContactService;
            _supplierContactLocationService = supplierContactLocationService;
            _supplierContactDepartmentService = supplierContactDepartmentService;
            _supplierLocationService = supplierLocationService;
            _supplierDepartmentService = supplierDepartmentService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _contactService = contactService;
            _noteService = noteService;
        }

        public List<SupplierContactEditModel> GetSupplierContactModels(List<Guid> supplierContactIds)
        {
            var list = _supplierContactService
                .GetSupplierContacts(supplierContactIds);
            var result = new List<SupplierContactEditModel>();
            foreach (var supplierContact in list)
            {
                result.Add(new SupplierContactEditModel()
                {
                    SupplierContactID = supplierContact.SupplierContactID,
                    ContactID = supplierContact.ContactID,
                    FirstName = supplierContact.Contact.FirstName,
                    LastName = supplierContact.Contact.LastName,
                    Title = supplierContact.Contact.Title,
                    ContactType = supplierContact.Contact.ContactType,
                    ContactReference = supplierContact.Contact.ContactReference,
                    EmailAddress = supplierContact.Contact.EmailAddress,
                    DDITelephoneNo = supplierContact.Contact.DDITelephoneNo,
                    MobileNo = supplierContact.Contact.MobileNo,
                    SupplierID = supplierContact.SupplierID,
                    SortOrder = supplierContact.SortOrder,
                });
            }
            return result;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<SupplierContactEditModel> BuildSupplierContactModels(List<SupplierContact> supplierContacts)
        {
            var list = new List<SupplierContactEditModel>();
            foreach (var item in supplierContacts)
            {
                var model = new SupplierContactEditModel();
                model.SupplierContactID = item.SupplierContactID;
                model.SupplierID = item.SupplierID;
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
                var supplierContactLocations = _supplierContactLocationService.GetSupplierContactLocationListByConId(item.SupplierContactID);
                model.SelectedLocationIds = new List<string>();
                model.LbxLocationOptions = new List<LbxViewModel>();
                foreach (var scl in supplierContactLocations)
                {
                    model.SelectedLocationIds.Add(scl.SupplierLocationID.ToString());
                    var lxbItem = new LbxViewModel();
                    lxbItem.Id = scl.SupplierLocationID.ToString();
                    lxbItem.label = _supplierLocationService.GetSupplierLocationById(scl.SupplierLocationID).SupplierLocationName;
                    lxbItem.value = scl.SupplierLocationID.ToString();
                    model.LbxLocationOptions.Add(lxbItem);
                }
                var supplierContactDepartments = _supplierContactDepartmentService.GetSupplierContactDepartmentListByConId(item.SupplierContactID);
                model.SelectedDepartmentIds = new List<string>();
                model.LbxDepartmentOptions = new List<LbxViewModel>();
                foreach (var scd in supplierContactDepartments)
                {
                    model.SelectedDepartmentIds.Add(scd.SupplierDepartmentID.ToString());
                    var lxbItem = new LbxViewModel();
                    lxbItem.Id = scd.SupplierDepartmentID.ToString();
                    lxbItem.label = _supplierDepartmentService.SupplierDepartmentById(scd.SupplierDepartmentID).SupplierDepartmentName;
                    lxbItem.value = scd.SupplierDepartmentID.ToString();
                    model.LbxDepartmentOptions.Add(lxbItem);
                }
                list.Add(model);
            }
            return list;
        }

        public SupplierContactEditModel CreateSupplierContact(SupplierContactEditModel model, List<Guid> locaGuidList, List<Guid> depaGuidList)
        {
            try
            {
                var contItem = ApplyChanges(model);
                if (model.SupplierContactID == Guid.Empty
                    && model.ItemAdding && !model.ItemDeleting)
                {
                    model.SupplierContactID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    contItem.SupplierContactID = model.SupplierContactID;
                    contItem.ObjectState = ObjectState.Added;
                    _supplierContactService.Insert(contItem);
                    InsertSupplierContactLocation(contItem, locaGuidList);
                    InsertSupplierContactDepartment(contItem, depaGuidList);
                }
                else if (!model.ItemAdding && !model.ItemDeleting)
                {
                    contItem.ObjectState = ObjectState.Modified;
                    _supplierContactService.Update(contItem);
                    //UpdateSupplierContactLocation(contItem, locaGuidList);   /////////////////
                    //UpdateSupplierContactDepartment(contItem, depaGuidList); ////////////////
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Supplier Contact failed", ex);
            }
            return model;
        }

        private void InsertSupplierContactLocation(SupplierContact contItem, List<Guid> locaGuidList)
        {
            var SCLitem = new SupplierContactLocation();
            SCLitem.SupplierContactLocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
            SCLitem.SupplierContactID = contItem.SupplierContactID;
            SCLitem.CreatedBy = contItem.CreatedBy;
            SCLitem.CreatedDate = contItem.CreatedDate;
            SCLitem.UpdatedBy = contItem.UpdatedBy;
            SCLitem.UpdatedDate = contItem.UpdatedDate;
            foreach (var locaGuid in locaGuidList)
            {
                SCLitem.SupplierLocationID = locaGuid;
                _supplierContactLocationService.Insert(SCLitem);
            }
        }

        private void InsertSupplierContactDepartment(SupplierContact contItem, List<Guid> depaGuidList)
        {
            var SCDitem = new SupplierContactDepartment();
            SCDitem.SupplierContactDepartmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
            SCDitem.SupplierContactID = contItem.SupplierContactID;
            SCDitem.CreatedBy = contItem.CreatedBy;
            SCDitem.CreatedDate = contItem.CreatedDate;
            SCDitem.UpdatedBy = contItem.UpdatedBy;
            SCDitem.UpdatedDate = contItem.UpdatedDate;
            foreach (var depaGuid in depaGuidList)
            {
                SCDitem.SupplierDepartmentID = depaGuid;
                _supplierContactDepartmentService.Insert(SCDitem);
            }
        }

        private SupplierContact ApplyChanges(SupplierContactEditModel model)
        {
            var _obj = new SupplierContact();
            _obj.SupplierContactID = model.SupplierContactID;
            _obj.SupplierID = model.SupplierID;
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

        private Guid SaveContact(SupplierContactEditModel model)
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

        private Guid CreateContact(SupplierContactEditModel model)
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

        private Guid UpdateContact(SupplierContactEditModel model)
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

        private Guid? SaveNote(SupplierContactEditModel model)
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

        private Guid CreateNote(SupplierContactEditModel model)
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

        private Guid UpdateNote(SupplierContactEditModel model)
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
/*
        private Guid SaveAddr(SupplierContactEditModel model, Address address)
        {
            if (!string.IsNullOrEmpty(model.AddressLine1)
            //&& !string.IsNullOrEmpty(model.CountyCity)
            && !string.IsNullOrEmpty(model.PostalTown)
            && !string.IsNullOrEmpty(model.Postcode))
            {
                if (model.AddressID == null || model.AddressID == Guid.Empty)
                    model.AddressID = CreateAddr(model, address);
                else model.AddressID = UpdateAddr(model, address);
            }
            return model.AddressID;
        }

        private Guid CreateAddr(SupplierContactEditModel model, Address address)
        {
            var addr = new Address
            {
                AddressID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                AddressLine3 = model.AddressLine3,
                PostalTown = model.PostalTown,
                CountyCity = model.CountyCity,
                Postcode = model.Postcode,
                ObjectState = ObjectState.Added
            };
            address = addr;
            _addressService.Insert(addr);
            return addr.AddressID;
        }

        private Guid UpdateAddr(SupplierContactEditModel model, Address address)
        {
            var addr = _addressService.Find(model.AddressID);
            addr.AddressLine1 = model.AddressLine1;
            addr.AddressLine2 = model.AddressLine2;
            addr.AddressLine3 = model.AddressLine3;
            addr.PostalTown = model.PostalTown;
            addr.CountyCity = model.CountyCity;
            addr.Postcode = model.Postcode;
            addr.ObjectState = ObjectState.Modified;
            address = addr;
            _addressService.Update(addr);
            return addr.AddressID;
        }
*/
