using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class SupplierLocationOrchestra : ISupplierLocationOrchestra
    {
        private readonly ISupplierLocationService _supplierLocationService;
        private readonly IAddressService _addressService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public SupplierLocationOrchestra(ISupplierLocationService supplierService,
            ISetupLocalService setupLocalService,
            IAddressService addressService,
            INoteService noteService)
        {
            _supplierLocationService = supplierService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _addressService = addressService;
            _noteService = noteService;
        }

        public List<SupplierLocationModel> GetSupplierLocationModels(List<Guid> supplierLocationIds)
        {
            var list = _supplierLocationService
                .GetSupplierLocations(supplierLocationIds);
            var result = new List<SupplierLocationModel>();
            foreach (var supplierLocation in list)
            {
                result.Add(new SupplierLocationModel()
                {
                    SupplierLocationID = supplierLocation.SupplierLocationID,
                    SupplierLocationName = supplierLocation.SupplierLocationName,
                    Telephone = supplierLocation.TelephoneNumber,
                    FaxNumber = supplierLocation.FaxNumber,
                    /*
                    Address = supplierLocation.Address == null
                    ? new AddressModels()
                    : new AddressModels()
                    {
                        AddressLine1 = supplierLocation.Address.AddressLine1 ?? "",
                        AddressLine2 = supplierLocation.Address.AddressLine2 ?? "",
                        AddressLine3 = supplierLocation.Address.AddressLine3 ?? "",
                        CountyCity = supplierLocation.Address.CountyCity ?? "",
                        PostalTown = supplierLocation.Address.PostalTown ?? "",
                        Postcode = supplierLocation.Address.Postcode ?? "",
                    }
                    */
                    AddressID = supplierLocation.AddressID,
                    AddressLine1 = supplierLocation.Address.AddressLine1,
                    AddressLine2 = supplierLocation.Address.AddressLine2,
                    AddressLine3 = supplierLocation.Address.AddressLine3,
                    Postcode = supplierLocation.Address.Postcode,
                    PostalTown = supplierLocation.Address.PostalTown,
                    CountyCity = supplierLocation.Address.CountyCity,
                });
            }
            return result;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<SupplierLocationModel> BuildSupplierLocationModels(List<SupplierLocation> supplierLocations)
        {
            var list = new List<SupplierLocationModel>();
            foreach (var item in supplierLocations)
            {
                var model = new SupplierLocationModel();
                model.SupplierLocationID = item.SupplierLocationID;
                model.SupplierLocationName = item.SupplierLocationName;
                model.SupplierID = item.SupplierID;
                model.Telephone = item.TelephoneNumber;
                model.FaxNumber = item.FaxNumber;
                // get Address object
                item.Address = _addressService.AddressById(item.AddressID);
                model.AddressID = item.Address.AddressID;
                model.AddressLine1 = item.Address.AddressLine1;
                model.AddressLine2 = item.Address.AddressLine2;
                model.AddressLine3 = item.Address.AddressLine3;
                model.Postcode = item.Address.Postcode;
                model.PostalTown = item.Address.PostalTown;
                model.CountyCity = item.Address.CountyCity;
                // Notes
                model.NoteID = item.NoteID;
                model.Notes = item.NoteID.HasValue ? _noteService.Find(item.NoteID.Value).NoteText : string.Empty;
                list.Add(model);
            }
            return list;
        }

        public SupplierLocationModel CreateSupplierLocation(SupplierLocationModel model)
        {
            try
            {
                var item = ApplyChanges(model);
                item.NoteID = SaveNote(model);
                item.AddressID = SaveAddr(model, item.Address);
                if (model.SupplierLocationID == Guid.Empty
                    && model.ItemAdding && !model.ItemDeleting)
                {
                    model.SupplierLocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    item.SupplierLocationID = model.SupplierLocationID;
                    item.ObjectState = ObjectState.Added;
                    _supplierLocationService.Insert(item);
                }
                else if (!model.ItemAdding && !model.ItemDeleting)
                {
                    item.ObjectState = ObjectState.Modified;
                    _supplierLocationService.Update(item);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Supplier Location failed", ex);
            }
            return model;
        }

        private SupplierLocation ApplyChanges(SupplierLocationModel model)
        {
            var sl = new SupplierLocation();
            sl.SupplierLocationID = model.SupplierLocationID;
            sl.SupplierLocationName = model.SupplierLocationName;
            sl.SupplierID = model.SupplierID;
            sl.TelephoneNumber = model.Telephone;
            sl.FaxNumber = model.FaxNumber;
            sl.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                ? DateTime.Parse(model.CreatedDate)
                : DateTime.Now;
            sl.CreatedBy = _principal.Id;
            sl.UpdatedDate = DateTime.Now;
            sl.UpdatedBy = _principal.Id;
            sl.IsActive = true;
            return sl;
        }

        private Guid SaveAddr(SupplierLocationModel model, Address address)
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

        private Guid CreateAddr(SupplierLocationModel model, Address address)
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

        private Guid UpdateAddr(SupplierLocationModel model, Address address)
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

        private Guid? SaveNote(SupplierLocationModel model)
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

        private Guid CreateNote(SupplierLocationModel model)
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

        private Guid UpdateNote(SupplierLocationModel model)
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
