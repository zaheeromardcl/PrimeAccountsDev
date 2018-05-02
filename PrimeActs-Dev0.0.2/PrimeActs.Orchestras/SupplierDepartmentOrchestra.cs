using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.BankAccount;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class SupplierDepartmentOrchestra : ISupplierDepartmentOrchestra
    {
        private readonly ISupplierDepartmentService _supplierDepartmentService;
        private readonly ISupplierDepartmentLocationService _supplierDepartmentLocationService;
        private readonly ISupplierLocationService _supplierLocationService;
        private readonly IConsignmentService _consignmentService;
        private readonly ICountryService _countryService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public SupplierDepartmentOrchestra(ISupplierDepartmentService supplierService,
            ISupplierDepartmentLocationService supplierDepartmentLocationService,
            ISupplierLocationService supplierLocationService,
            ISetupLocalService setupLocalService,
            IConsignmentService consignmentService,
            ICountryService countryService,
            INoteService noteService)
        {
            _supplierDepartmentService = supplierService;
            _supplierDepartmentLocationService = supplierDepartmentLocationService;
            _supplierLocationService = supplierLocationService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _consignmentService = consignmentService;
            _countryService = countryService;
            _noteService = noteService;
        }

        public SupplierDepartmentViewModel GetSupplierDepartmentModel(Guid supplierDepartmentId)
        {
            var supplierDepartment = _supplierDepartmentService
                                    .SupplierDepartmentById(supplierDepartmentId);
            SupplierDepartmentViewModel supplierDepartmentEditModel = new SupplierDepartmentViewModel()
            {
                SupplierDepartmentID = supplierDepartment.SupplierDepartmentID,
                SupplierDepartmentName = supplierDepartment.SupplierDepartmentName,
                Commission = supplierDepartment.Commission ?? 0,
                Handling = supplierDepartment.Handling ?? 0,
                SupplierLocations = supplierDepartment.SupplierLocations.Select(
                    sdl => new SupplierLocationModel()
                    {
                        SupplierLocationID = sdl.SupplierLocationID,
                        SupplierLocationName = sdl.SupplierLocationName,
                        Telephone = sdl.TelephoneNumber,
                        FaxNumber = sdl.FaxNumber,
                        /*
                        Address = sdl.Address == null
                        ? new AddressModels()
                        : new AddressModels()
                        {
                            AddressLine1 = sdl.Address.AddressLine1 ?? "",
                            AddressLine2 = sdl.Address.AddressLine2 ?? "",
                            AddressLine3 = sdl.Address.AddressLine3 ?? "",
                            CountyCity = sdl.Address.CountyCity ?? "",
                            PostalTown = sdl.Address.PostalTown ?? "",
                            Postcode = sdl.Address.Postcode ?? "",
                        },
                        */
                        AddressID = sdl.AddressID,
                        AddressLine1 = sdl.Address.AddressLine1,
                        AddressLine2 = sdl.Address.AddressLine2,
                        AddressLine3 = sdl.Address.AddressLine3,
                        Postcode = sdl.Address.Postcode,
                        PostalTown = sdl.Address.PostalTown,
                        CountyCity = sdl.Address.CountyCity,
                    }),
                SupplierContacts = supplierDepartment.Supplier.SupplierContacts.Select(
                    sc => new SupplierContactModel()
                    {
                        Contact = new ContactModel()
                        {
                            FirstName = sc.Contact.FirstName,
                            LastName = sc.Contact.LastName,
                            Title = sc.Contact.Title,
                            EmailAddress = sc.Contact.EmailAddress
                        }
                    }),
                BankAccounts = supplierDepartment.SupplierBankAccounts.Select(
                    sba => new BankAccountModel()
                    {
                        AccountName = sba.BankAccount.AccountName,
                        BankCode = sba.BankAccount.BankCode.ToString(),
                        AccountNumber = sba.BankAccount.AccountNumber.ToString(),

                    })

            };
            return supplierDepartmentEditModel;
        }

        public SupplierDepartmentWithConsigmentViewModel GetSupplierDepartmentWithConsignmentsModel(Guid id, SupplierDepartmentSearch supplierDepartmentSearch)
        {
            var supplierDepartment = _supplierDepartmentService.SupplierDepartmentBasicById(id);
            var consignments = _consignmentService.GetConsignmentsBySupplierDepartmentID(id, supplierDepartmentSearch);
            SupplierDepartmentWithConsigmentViewModel model = new SupplierDepartmentWithConsigmentViewModel()
            {
                SupplierDepartmentID = supplierDepartment.SupplierDepartmentID,
                SupplierDepartmentName = supplierDepartment.SupplierDepartmentName,
                Commission = supplierDepartment.Commission ?? 0,
                Handling = supplierDepartment.Handling ?? 0,

                Consignments = consignments.Select(
                    sba => new ConsignmentBasicViewModel()
                    {
                        ConsignmentReference = sba.ConsignmentReference,
                        TotalEstitamedPurcahseCost = sba.ConsignmentItems.Sum(ci => ci.EstimatedPurchaseCost),
                        DepatchedDate = sba.DespatchDate.ToString(),
                        ConsignmentID = sba.ConsignmentID
                    })

            };
            return model;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<SupplierDepartmentEditModel> BuildSupplierDepartmentModels(List<SupplierDepartment> supplierDepartments)
        {
            var list = new List<SupplierDepartmentEditModel>();
            foreach (var item in supplierDepartments)
            {
                var model = new SupplierDepartmentEditModel();
                model.SupplierDepartmentID = item.SupplierDepartmentID;
                model.SupplierDepartmentName = item.SupplierDepartmentName;
                model.SupplierID = item.SupplierID;
                model.EmailAddress = item.EmailAddress;
                model.Commission = item.Commission.HasValue ? item.Commission.Value : 0;
                model.Handling = item.Handling.HasValue ? item.Handling.Value : 0;
                model.GivesRebate = item.GivesRebate;
                model.RebateAmount = item.Rebate.HasValue ? item.Rebate.Value : 0;
                model.CountryID = item.CountryID.HasValue ? item.CountryID.Value : Guid.Empty;
                model.CountryName = item.CountryID.HasValue ? _countryService.CountryById(item.CountryID.Value).CountryName : string.Empty;
                model.IsTransactionTaxExempt = item.IsTransactionTaxExempt;
                model.TransactionTaxReference = item.TransactionTaxReference;
                model.CreditTerm = item.CreditTerm.HasValue ? item.CreditTerm.Value : 0;
                model.CreditLimit = item.CreditLimit.HasValue ? item.CreditLimit.Value : 0;
                model.NoteID = item.NoteID;
                model.Notes = item.NoteID.HasValue ? _noteService.Find(item.NoteID.Value).NoteText : string.Empty;
                // setup LxbViewModel of ListBox
                var supplierDepartmentLocations = _supplierDepartmentLocationService.GetSupplierDepartmentLocationListByDepId(item.SupplierDepartmentID);
                model.SelectedLocationIds = new List<string>();
                model.LbxLocationOptions = new List<LbxViewModel>();
                foreach (var sdl in supplierDepartmentLocations)
                {
                    model.SelectedLocationIds.Add(sdl.SupplierLocationID.ToString());
                    var lxbItem = new LbxViewModel();
                    lxbItem.Id = sdl.SupplierLocationID.ToString();
                    lxbItem.label = _supplierLocationService.GetSupplierLocationById(sdl.SupplierLocationID).SupplierLocationName;
                    lxbItem.value = sdl.SupplierLocationID.ToString();
                    model.LbxLocationOptions.Add(lxbItem);
                }
                list.Add(model);
            }
            return list;
        }

        public SupplierDepartmentEditModel CreateSupplierDepartment(SupplierDepartmentEditModel model, List<Guid> locaGuidList)
        {
            try
            {
                var depaItem = ApplyChanges(model);
                depaItem.NoteID = SaveNote(model);
                if (model.SupplierDepartmentID == Guid.Empty
                    && model.ItemAdding && !model.ItemDeleting)
                {
                    model.SupplierDepartmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
                    depaItem.SupplierDepartmentID = model.SupplierDepartmentID;
                    depaItem.ObjectState = ObjectState.Added;
                    _supplierDepartmentService.Insert(depaItem);
                    InsertSupplierDepartmentLocation(depaItem, locaGuidList);
                }
                else if (!model.ItemAdding && !model.ItemDeleting)
                {
                    depaItem.ObjectState = ObjectState.Modified;
                    _supplierDepartmentService.Update(depaItem);
                    //UpdateSupplierDepartmentLocation(depaItem, locaGuidList); //nem
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Supplier Department failed", ex);
            }
            return model;
        }

        private void InsertSupplierDepartmentLocation(SupplierDepartment depaItem, List<Guid> locaGuidList)
        {
            var SDLitem = new SupplierDepartmentLocation();
            SDLitem.SupplierDepartmentLocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]);
            SDLitem.SupplierDepartmentID = depaItem.SupplierDepartmentID;
            SDLitem.CreatedBy = depaItem.CreatedBy;
            SDLitem.CreatedDate = depaItem.CreatedDate;
            SDLitem.UpdatedBy = depaItem.UpdatedBy;
            SDLitem.UpdatedDate = depaItem.UpdatedDate;
            foreach (var locaGuid in locaGuidList)
            {
                SDLitem.SupplierLocationID = locaGuid;
                _supplierDepartmentLocationService.Insert(SDLitem);
            }
        }

        private SupplierDepartment ApplyChanges(SupplierDepartmentEditModel model)
        {
            return new SupplierDepartment()
            {
                SupplierDepartmentID = model.SupplierDepartmentID,
                Commission = model.Commission,
                Handling = model.Handling,
                SupplierDepartmentName = model.SupplierDepartmentName,
                SupplierID = model.SupplierID,
                CountryID = model.CountryID,
                GivesRebate = model.GivesRebate,
                Rebate = model.RebateAmount,
                FactorSupplierDepartmentID = model.FactorSupplierDepartmentID,
                IsTransactionTaxExempt = model.IsTransactionTaxExempt,
                TransactionTaxReference = model.TransactionTaxReference,
                CreditTerm = model.CreditTerm,
                CreditLimit = model.CreditLimit,
                EmailAddress = model.EmailAddress,
                //NoteID = model.NoteID,
                CreatedDate = !string.IsNullOrEmpty(model.CreatedDate) ? DateTime.Parse(model.CreatedDate) : DateTime.Now,
                CreatedBy = _principal.Id,
                UpdatedDate = DateTime.Now,
                UpdatedBy = _principal.Id,
                IsActive = true,
            };
        }

        private Guid? SaveNote(SupplierDepartmentEditModel model)
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

        private Guid CreateNote(SupplierDepartmentEditModel model, ApplicationUser author)
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

        private Guid UpdateNote(SupplierDepartmentEditModel model, ApplicationUser author)
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
