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
    public class CustomerLocationOrchestra : ICustomerLocationOrchestra
    {
        private readonly ICustomerDepartmentService _departmentService;
        private readonly ICustomerLocationService _locationService;
        private readonly IAddressService _addressService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;
        public CustomerLocationOrchestra(ICustomerLocationService locationService,
            ICustomerDepartmentService departmentService,
            ISetupLocalService setupLocalService,
            IAddressService addressService,
            INoteService noteService)
        {
            _locationService = locationService;
            _departmentService = departmentService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _addressService = addressService;
            _noteService = noteService;
        }

        public List<CustomerLocationModel> GetCustomerLocationModels(Guid cdId)
        {
            var customerDepartment = _departmentService
                .CustomerDepartmentByCustomerDepartmentId(cdId);
            var list = _locationService
                .GetAllCustomerLocationsByCustomerID(customerDepartment.CustomerID);
            var result = new List<CustomerLocationModel>();
            foreach (var customerLocation in list)
            {
                var address = _addressService.AddressById(customerLocation.AddressID);
                result.Add(new CustomerLocationModel()
                {
                    // one CustomerDepartmentName
                    CustomerDepartmentName = customerDepartment.CustomerDepartmentName,
                    // several customerLocation
                    CustomerLocationID = customerLocation.CustomerLocationID,
                    CustomerLocationName = customerLocation.CustomerLocationName,
                    Telephone = customerLocation.TelephoneNumber,
                    FaxNumber = customerLocation.FaxNumber,
                    Address = address == null ? new AddressModels() : new AddressModels()
                    {
                        AddressLine1 = address.AddressLine1 ?? "",
                        AddressLine2 = address.AddressLine2 ?? "",
                        AddressLine3 = address.AddressLine3 ?? "",
                        CountyCity = address.CountyCity ?? "",
                        PostalTown = address.PostalTown ?? "",
                        Postcode = address.Postcode ?? "",
                    }
                    /*
                    Address = customerLocation.Address == null ? new AddressModels() : new AddressModels()
                    {
                        AddressLine1 = customerLocation.Address.AddressLine1 ?? "",
                        AddressLine2 = customerLocation.Address.AddressLine2 ?? "",
                        AddressLine3 = customerLocation.Address.AddressLine3 ?? "",
                        CountyCity = customerLocation.Address.CountyCity ?? "",
                        PostalTown = customerLocation.Address.PostalTown ?? "",
                        Postcode = customerLocation.Address.Postcode ?? "",
                    }
                    */
                });
            }
            return result;
        }

        public List<CustomerLocationModel> GetCustomerLocationModelsForDDLinvc(Guid cdId)
        {
            var customerDepartment = _departmentService
                .CustomerDepartmentByCustomerDepartmentId(cdId);
            var listCusLoc = _locationService
                .GetAllCustomerLocationsByCustomerID(customerDepartment.CustomerID);
            List<CustomerLocationModel> result;
            if (listCusLoc != null && listCusLoc.Count > 0)
                result = new List<CustomerLocationModel>();
            else result = null;
            foreach (var customerLocation in listCusLoc)
            {
                result.Add(new CustomerLocationModel()
                {
                    // one CustomerDepartmentName
                    CustomerDepartmentName = customerDepartment.CustomerDepartmentName,
                    // several customerLocation
                    CustomerLocationID = customerLocation.CustomerLocationID,
                    CustomerLocationName = customerLocation.CustomerLocationName,
                    Telephone = customerLocation.TelephoneNumber,
                    FaxNumber = customerLocation.FaxNumber,
                    Address = new AddressModels()
                });
            }
            return result;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<CustomerLocationModel> BuildCustomerLocationModels(List<CustomerLocation> customerLocations)
        {
            var list = new List<CustomerLocationModel>();
            foreach (var item in customerLocations)
            {
                var model = new CustomerLocationModel();
                model.CustomerLocationID = item.CustomerLocationID;
                model.CustomerLocationName = item.CustomerLocationName;
                model.CustomerID = item.CustomerID;
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

        public CustomerLocationModel CreateCustomerLocation(CustomerLocationModel model)
        {
            try
            {
                var item = ApplyChanges(model);
                item.NoteID = SaveNote(model);
                item.AddressID = SaveAddr(model, item.Address);
                if (model.CustomerLocationID == Guid.Empty
                    && model.ItemAdding && !model.ItemDeleting)
                {
                    model.CustomerLocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    item.CustomerLocationID = model.CustomerLocationID;
                    item.ObjectState = ObjectState.Added;
                    _locationService.Insert(item);
                }
                else if (!model.ItemAdding && !model.ItemDeleting)
                {
                    item.ObjectState = ObjectState.Modified;
                    _locationService.Update(item);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Supplier Location failed", ex);
            }
            return model;
        }

        private CustomerLocation ApplyChanges(CustomerLocationModel model)
        {
            var cl = new CustomerLocation();
            cl.CustomerLocationID = model.CustomerLocationID;
            cl.CustomerLocationName = model.CustomerLocationName;
            cl.CustomerID = model.CustomerID;
            cl.TelephoneNumber = model.Telephone;
            cl.FaxNumber = model.FaxNumber;
            cl.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                ? DateTime.Parse(model.CreatedDate)
                : DateTime.Now;
            cl.CreatedBy = _principal.Id;
            cl.UpdatedDate = DateTime.Now;
            cl.UpdatedBy = _principal.Id;
            cl.IsActive = true;
            return cl;
        }

        private Guid SaveAddr(CustomerLocationModel model, Address address)
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

        private Guid CreateAddr(CustomerLocationModel model, Address address)
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

        private Guid UpdateAddr(CustomerLocationModel model, Address address)
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

        private Guid? SaveNote(CustomerLocationModel model)
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

        private Guid CreateNote(CustomerLocationModel model)
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

        private Guid UpdateNote(CustomerLocationModel model)
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
