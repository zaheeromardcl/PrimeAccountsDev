#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Infrastructure.Extensions;

#endregion

namespace PrimeActs.Orchestras
{
    public class TicketOrchestra : ITicketOrchestra
    {
        private readonly ISetupGlobalService _setupGlobalService;
        private readonly INoteService _noteService;
        private readonly ITicketItemService _ticketItemService;
        private readonly ITicketService _ticketService;
        private ICustomerCurrencyService _currencyCustomerService;
        private ICurrencyService _currencyService;
        private ICustomerService _customerService;
        private ICustomerDepartmentService _customerDeptService;
        private IApplicationUserService _applicationUserService;
        private IApplicationRoleService _applicationRoleService;
        private ApplicationUser _principal;
        private ICompanyService _companyService;
        private IDivisionService _divisionService;
        private IAddressService _addressService;
        private IConsignmentItemService _consignmentItemService;
        
        private IValidationDictionary _validationDictionary;
        private ITransactionTaxCodeService _transactionTaxCodeService;
        private ITransactionTaxRateService _transactionTaxRateService;

        private IPaymentTypeService _paymentTypeService;
        private ISalesLedgerEntryService _salesLedgerEntryService;
        private ILedgerEntryTypeService _ledgerEntryTypeService;
        private IBatchNumberLogService _batchNumberLogService;
        private INominalLedgerEntryService _nominalLedgerEntryService;

      
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly ISalesInvoiceItemService _salesInvoiceItemService;
        private readonly ITransferTypeService _transferTypeService;
        private IDepartmentService _departmentService;
        private IProduceService _produceService;
        private ITransactionTaxLocationService _transactionTaxLocationService;
        private ISalesLedgerInvoiceAllocationService _salesLedgerInvoiceAllocationService;

        private readonly string _serverCode;
        private ICashTicketService _cashTicketService;


        public TicketOrchestra(ISetupGlobalService setupGlobalService, ISetupLocalService setupLocalService, 
            ITicketService ticketService, 
            ITicketItemService ticketItemService,
            IConsignmentItemService consignmentItemService,
            ICurrencyService currencyService, 
            ICustomerService customerService,
            ICustomerCurrencyService currencyCustomerService, 
            ITransactionTaxCodeService transactionTaxCodeService, 
            INoteService noteService, 
            IApplicationUserService aspNetUserService,
            IDivisionService divisionService, 
            ICompanyService companyService, 
            IAddressService addressService, 
            ICustomerDepartmentService customerDeptService,
            IPaymentTypeService paymentTypeService,
            ISalesLedgerEntryService salesLedgerEntryService, 
            ILedgerEntryTypeService ledgerEntryTypeService,
            IBatchNumberLogService batchNumberLogService,
            
            ISalesInvoiceService salesInvoiceService,
            ISalesInvoiceItemService salesInvoiceItemService,
            ITransferTypeService transferTypeService, 
            IApplicationRoleService applicationRoleService, 
            IDepartmentService departmentService, 
            INominalLedgerEntryService nominalLedgerEntryService,
            ITransactionTaxRateService transactionTaxRateService,
            IProduceService produceService,
            ITransactionTaxLocationService transactionTaxLocationService,
            ISalesLedgerInvoiceAllocationService salesLedgerInvoiceAllocationService,
            ICashTicketService cashTicketService
            )
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _setupGlobalService = setupGlobalService;

            _ticketItemService = ticketItemService;
            _ticketService = ticketService;
            _currencyService = currencyService;
            _customerService = customerService;
            _currencyCustomerService = currencyCustomerService;
            _applicationUserService = aspNetUserService;
            _transactionTaxCodeService = transactionTaxCodeService;
            _noteService = noteService;
            _divisionService = divisionService;
            _companyService = companyService;
            _addressService = addressService;
            _customerDeptService = customerDeptService;
            _paymentTypeService = paymentTypeService;
            _salesLedgerEntryService = salesLedgerEntryService;
            _ledgerEntryTypeService = ledgerEntryTypeService;
            _batchNumberLogService = batchNumberLogService;
            
            _salesInvoiceService = salesInvoiceService;
            _salesInvoiceItemService = salesInvoiceItemService;
            _transferTypeService = transferTypeService;
            _applicationRoleService = applicationRoleService;
            _departmentService = departmentService;
            _nominalLedgerEntryService = nominalLedgerEntryService;
            _transactionTaxRateService = transactionTaxRateService;
            _consignmentItemService = consignmentItemService;
            _produceService = produceService;
            _transactionTaxLocationService = transactionTaxLocationService;
            _salesLedgerInvoiceAllocationService = salesLedgerInvoiceAllocationService;
            _cashTicketService = cashTicketService;
        }

        public void Initialize(ApplicationUser principal)
        {       
            _principal = principal;
        }

        public bool Validate(TicketEditModel model)
        {
            var validator = new TicketEditModelValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var failer in result.Errors)
                    _validationDictionary.AddError(failer.PropertyName, failer.ErrorMessage);
            }
            return result.IsValid;
        }

        public bool Validate(TicketItemEditModel model)
        {
            return true;
        }
        public Guid TicketType(Guid tickettypeid)
        {
            return tickettypeid;

        }

        public CreateTicketViewModel GetCreateTicketViewModel(Guid id)
        {
            var ticket = id != Guid.Empty ? BuildTicketEditModel(_ticketService.TicketById(id)) :
                new TicketEditModel
                {
                    TicketReference = RandomString(),
                    TicketDate = DateTime.Now.ToString(),
                    TicketItems = new List<TicketItemEditModel>()
                };
            if (id == Guid.Empty)
            {
                var currency = _currencyService.GetByCurrencyCode("GB");
                ticket.CurrencyID = currency.CurrencyID;
                ticket.CurrencyName = currency.CurrencyCode + " - " + currency.CurrencyName;
            }
            var paymentTypeList = _paymentTypeService.GetAllActivePaymentTypes();

            var salesRole = _applicationRoleService.FindByName("Sales");
            //var users = _applicationUserOrchestra.GetSalesUsersForAutoComplete(search, currentLoggedInUser.DepartmentId.Value, salesRole.Id);
            var salesRolesIncludeUsers = _applicationRoleService.FindById(salesRole.Id, true, false);
            var salesRolesForUserDepartment = salesRolesIncludeUsers.ApplicationUsers.Where(dep => dep.DepartmentId == _principal.DepartmentId);
            //var transactiontaxcodeidrate = _transactionTaxCodeService.t
            var viewModel = new CreateTicketViewModel
            {
                PaymentTypeList = paymentTypeList,
                TicketCreateModel = ticket
            };
            
            SetDefaultSalesPerson(salesRolesForUserDepartment, ref viewModel);
            return viewModel;
        }

        private void SetDefaultSalesPerson(IEnumerable<ApplicationUser> salesRolesForUserDepartment, ref CreateTicketViewModel viewModel)
        {
            if (salesRolesForUserDepartment.Count() == 1) // Default Sales Person in ViewModel if there is a one to one relationship: User to Sales Person
            {
                var defaultSalesPerson = salesRolesForUserDepartment.First();
                viewModel.TicketCreateModel.SalesPersonUserID = defaultSalesPerson.Id;
                viewModel.TicketCreateModel.SalesPersonName = string.Format("{0} {1}", defaultSalesPerson.Firstname, defaultSalesPerson.Lastname);
                viewModel.TicketCreateModel.SalesPersonDepartmentID = defaultSalesPerson.DepartmentId;
                var departmentName = _departmentService.DepartmentById(defaultSalesPerson.DepartmentId.Value).DepartmentName;
                viewModel.TicketCreateModel.SalesPersonDepartmentName = departmentName;
            }
        }

        public TicketEditModel GetTicketEditModel(Guid id)
        {
            var ticketEditModel =
                    id == Guid.Empty
                        ? new TicketEditModel
                        {
                            TicketReference = RandomString(),
                            TicketDate = DateTime.Now.ToString(),
                            CreatedDate = DateTime.Now.ToString(),
                            UpdatedDate = DateTime.Now.ToString(),
                            TicketItems = new List<TicketItemEditModel> { new TicketItemEditModel() }
                        }
                        : BuildTicketEditModel(_ticketService.TicketById(id));
            return ticketEditModel;
        }

        public TicketViewModel GetTicketViewModel(Guid id)
        {
            var ticketViewModel = new TicketViewModel
            {
                TicketEditModel = GetTicketEditModel(id)
            };
            return ticketViewModel;
        }

        public TicketPrintViewModel GetTicketPrintViewModel(Guid id)
        {
            var company = _companyService.CompanyWithAddress(_principal.CompanyId.Value);
            var ticket = _ticketService.TicketById(id);

            var ticketPrintViewModel = BuildTicketPrintViewModel(company, ticket);
            return ticketPrintViewModel;
        }

        private TicketPrintViewModel BuildTicketPrintViewModel(Company company, Ticket ticket)
        {
            var transactionTaxDetails =
               _transactionTaxLocationService.TransactionTaxLocationByCompanyID(company.CompanyID);
            TicketPrintViewModel model = new TicketPrintViewModel
            {
                Company = BuildCompanyModel(company),
                Ticket = BuildTicketEditModel(ticket),
                TransactionTaxReference = transactionTaxDetails.TransactionTaxReference
            };
            return model;
        }

        private CompanyViewModel BuildCompanyModel(Company company)
        {
            var model = new CompanyViewModel
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
              
                CompanyNo = company.CompanyNo,
                Logo = company.Logo,
            };

            if (company.Address != null)
            {
                model.Address = new AddressViewModel
                {
                    AddressID = company.Address.AddressID,
                    AddressLine1 = company.Address.AddressLine1,
                    AddressLine2 = company.Address.AddressLine2,
                    AddressLine3 = company.Address.AddressLine3,
                    Postcode = (company.Address.Postcode ?? "").RemoveSpecialCharacters()
                };
            }
            else
            {
                model.Address = new AddressViewModel();
            }

            return model;
        }


        public List<Ticket> GetAllCustomerDepartmentTickets(Guid customerDepartmentID)
        {
            var customerDepartmentTickets = _ticketService.GetAllTicketsByCustomerDepartment(customerDepartmentID);
            return customerDepartmentTickets;
        }

        public ResultList<TicketEditModel> GetTickets(QueryOptions queryOptions, SearchObject searchObject)
        {
            int totalCount;
            var tickets = _ticketService.GetTickets(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<TicketEditModel>(tickets != null ? tickets.Select(BuildTicketEditModel).ToList() : null,
                    queryOptions);
        }

        public ResultList<vwCashTicket> GetDailyCashTicketsAllViewModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var tickets = _cashTicketService.GetDailyCashTicketsAll(queryOptions, searchObject);
            
           // var temp1 = tickets.Select(BuildDailyCashTicketModel).ToList();
            //var temp2 = tickets.ToList();

            var resultList = new ResultList<vwCashTicket>(tickets != null ? tickets.ToList() : null,
              queryOptions);

           
            return resultList;
        }

        public ResultList<vwCashTicketViewModel> GetDailyCashTicketsAllViewModelvw(QueryOptions queryOptions, SearchObject searchObject)
        {
            var tickets = _cashTicketService.GetDailyCashTicketsAll(queryOptions, searchObject);

            // var temp1 = tickets.Select(BuildDailyCashTicketModel).ToList();
            //var temp2 = tickets.ToList();

            //var resultList = new ResultList<vwCashTicket>(tickets != null ? tickets.ToList() : null,
            //  queryOptions);

            var resultList = new ResultList<vwCashTicketViewModel>(tickets != null ? tickets.Select(BuildDailyCashTicketModelVw).ToList() : null,
              queryOptions); ;
            return resultList;
        }

        public ResultList<DailyCashTicketModel> GetDailyCashTicketsAll(QueryOptions queryOptions, SearchObject searchObject)
        {
            var tickets = _cashTicketService.GetDailyCashTicketsAll(queryOptions, searchObject);
            //return
            //new ResultList<DailyCashTicketModel>(tickets != null ? tickets.Select(BuildDailyCashTicketModel).ToList() : null,
            //  queryOptions);
            var temp1 = tickets.Select(BuildDailyCashTicketModel).ToList();
            var temp2 = tickets.ToList();

            var resultList = new ResultList<DailyCashTicketModel>(tickets != null ? tickets.Select(BuildDailyCashTicketModel).ToList() : null,
              queryOptions);
            return resultList;
        }

        public ResultList<DailyCashTicketModel> GetDailyCashTickets(QueryOptions queryOptions, SearchObject searchObject)
        {
            int totalCount;
            var tickets = _cashTicketService.GetDailyCashTickets(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return 
            new ResultList<DailyCashTicketModel>(tickets != null ? tickets.Select(BuildDailyCashTicketModel).ToList() : null,
              queryOptions);
        }

        // before we created the view for daily cash we used the method below
        public ResultList<TicketEditModel> GetDailyCashTicketsOld(QueryOptions queryOptions, SearchObject searchObject)
        {
            int totalCount;
            var tickets = _ticketService.GetDailyCashTickets(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<TicketEditModel>(tickets != null ? tickets.Select(BuildTicketEditModel).ToList() : null,
                    queryOptions);
        }

        public ResultList<SalesLedgerEntryViewModel> GetDailySalesLedgerEntries(QueryOptions queryOptions, SearchObject searchObject)
        {
            int totalCount;
            var entries = _salesLedgerEntryService.GetDailySalesLedgerEntries(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            //return
            //    new ResultList<SalesLedgerEntryViewModel>(entries != null ? entries.Select(BuildSalesLedgerEntryModel).ToList() : null,
            //        queryOptions);

            var resultList = new ResultList<SalesLedgerEntryViewModel>(entries != null ? entries.Select(BuildSalesLedgerEntryModel).ToList() : null,
                    queryOptions);

            return resultList;
        }
        
        public TicketEditModel GetTicketOnly(Guid id)
        {
            
            Ticket ticket = _ticketService.GetTicketOnly(id);
            ApplicationUser soldBy = _applicationUserService.UserById(Guid.Parse(ticket.SalesPersonUserID.ToString()));
            ticket.SalesPersonName = soldBy.Firstname + " " + soldBy.Lastname;
            Currency currency = _currencyService.CurrencyById(Guid.Parse(ticket.CurrencyID.ToString()));
            //ticket.CreatedBy = 
            ticket.CurrencyName = currency.CurrencyName;
            ticket.CurrencyRate = currency.DefaultExchangeRate;
            return BuildTicketEditModel(ticket);
        }

        public List<TicketItemEditModel> GetTicketItemsOnly(Guid id)
        {
            var ticketItems = _ticketItemService.GetTicketItemsOnly(id);
            return ticketItems.Select(BuildTicketItemEditModel).ToList();
        }

        public TicketPagingModel GetTicketPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var ticketPagingModel = new TicketPagingModel();
            var tickets = _ticketService.GetTickets(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<TicketEditModel>(tickets.Select(BuildTicketEditModel).ToList(), queryOptions);
            ticketPagingModel.TicketEditModels = result;
            ticketPagingModel.SearchObject = new SearchObject
            {
                TicketReference = searchObject.TicketReference,
                CustomerCode = searchObject.CustomerCode,
                CustomerCompanyName = searchObject.CustomerCompanyName,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null,
                RecordsInDays = searchObject.RecordsInDays
            };
            return ticketPagingModel;
        }


        public TicketItemPagingModel GetTicketItemPagingModel(Guid id, QueryOptions queryOptions)
        {
            var totalCount = 0;
            var ticketItemPagingModel = new TicketItemPagingModel();
            var ticketitems = _ticketItemService.TicketItemsByTicketID(id);
            queryOptions.TotalPages = totalCount;
            var result = new ResultList<TicketItemEditModel>(ticketitems.Select(BuildTicketItemEditModel).ToList(),
                queryOptions);
            //var result = new ResultList<TicketItemEditModel>(ticketitems., queryOptions);
            ticketItemPagingModel.TicketItemEditModels = result;
            return ticketItemPagingModel;
        }

        private Guid CreateNote(string noteText, string description, ApplicationUser author)
        {
            var note = new Note
            {
                NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                NoteText = noteText,
                NoteDescription = description,
               
               // IsActive = true,
                ObjectState = ObjectState.Added
            };
            _noteService.Insert(note);
            return note.NoteID;
        }

        private Guid UpdateNote(Guid noteId, string noteText, string description, ApplicationUser author)
        {
            var note = _noteService.Find(noteId);
            note.NoteText = noteText;
            note.NoteDescription = description;
            note.ObjectState = ObjectState.Modified;
            _noteService.Update(note);

            return noteId;
        }

        public SalesLedgerEntry CreateSalesLedgerEntry(TicketEditModel model)
        {
            try
            {
                var ledgerEntryType = _ledgerEntryTypeService.LedgerEntryTypeByNumber(4);
                var batchNumberLogID = _batchNumberLogService.GetBatchNumberLogIDByDivisionID(_principal.SelectedDivisionId);

                Guid? noteId = null;
                if (!string.IsNullOrEmpty(model.Notes))
                {
                    if (model.NoteID == null || model.NoteID == Guid.Empty)
                    {
                        noteId = CreateNote(model.Notes, model.TicketReference, _principal);
                    }
                    else
                    {
                        noteId = UpdateNote(model.NoteID.Value, model.Notes, model.TicketReference, _principal);
                    }
                }
             //   SetupGlobal vartext = _setupGlobalService.Find("AccountingYear");
                List<SetupGlobal> AllSetupGlobalList = _setupGlobalService.GetAllSetupValuesBySetupName("AccountingYear");
                var accountingYear = AllSetupGlobalList[0].SetupValueInt;
                //var settingAccountingYear = _setupGlobalService.Find("AccountingYear");
                //var accountingYear = settingAccountingYear != null ? settingAccountingYear.SetupValueInt.Value : DateTime.Now.Year;

                List<SetupGlobal> AllSetupGlobalListPeriod = _setupGlobalService.GetAllSetupValuesBySetupName("AccountingPeriod");
                var accountingPeriod = AllSetupGlobalListPeriod[0].SetupValueInt;
               
                //var settingAccountingPeriod = _setupGlobalService.Find("AccountingPeriod");
              //  var accountingPeriod = Convert.ToByte(settingAccountingPeriod != null ? settingAccountingPeriod.SetupValueInt.Value : DateTime.Now.Month);

                var saleAmount = 0-(model.AmountReceived ?? 0);
                var salesLedgerEntry = new SalesLedgerEntry
                {
                    SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                    SalesLedgerEntryDescription = "Cash " + model.TicketReference,
                    SaleAmount = saleAmount,
                    CurrencyAmount = 0.0m,
                    CurrencyID = model.CurrencyID,
                    ExchangeRate = 0.0m,
                    CustomerDepartmentID = model.CustomerDepartmentID,
                    BatchNumberLogID = batchNumberLogID,
                    NoteID = noteId,
                    CreatedBy = _principal.Id,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    //IsActive = true,
                    ObjectState = ObjectState.Added,
                    //AccountingYear = accountingYear,
                   // AccountingPeriod = accountingPeriod,
                    SalesPersonUserID = model.SalesPersonUserID
                };

                _salesLedgerEntryService.Insert(salesLedgerEntry);
                return salesLedgerEntry;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CreateSalesInvoice(Guid ticketId, decimal amountReceived)
        {
            var ticket = _ticketService.TicketById(ticketId);

            var ledgerEntryType = _ledgerEntryTypeService.LedgerEntryTypeByNumber(4);
            var batchNumberLogID = _batchNumberLogService.GetBatchNumberLogIDByDivisionID(_principal.SelectedDivisionId);
            //Code not required - as tgenSalesInvoicenumber is obsolete.
            //var salesInvoiceNumber = _salesInvoiceNumberService.Query(x => x.DivisionID == ticket.DivisionID).Select().SingleOrDefault();
            //if (salesInvoiceNumber == null)
            //{
            //    Code not required - as tgenSalesInvoicenumber is obsolete.
            //    salesInvoiceNumber = new tgenSalesInvoiceNumber
            //    {
            //        SalesInvoiceID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
            //        SalesInvoiceNumber = "10000001",
            //        Prefix = "PI",
            //        Suffix = "A",
            //        DivisionID = ticket.DivisionID.Value,
            //        ObjectState = ObjectState.Added
            //    };
            //    _salesInvoiceNumberService.Insert(salesInvoiceNumber);
            //    salesInvoiceNumber = "10001";
            //}
            //else
            //{
            //    salesInvoiceNumber.SalesInvoiceNumber = (int.Parse(salesInvoiceNumber.SalesInvoiceNumber) + 1).ToString();
            //    _salesInvoiceNumberService.Update(salesInvoiceNumber);
            //}
            List<SetupGlobal> AllSetupGlobalList = _setupGlobalService.GetAllSetupValuesBySetupName("AccountingYear");
            var accountingYear = AllSetupGlobalList[0].SetupValueInt;
           
            List<SetupGlobal> AllSetupGlobalListPeriod = _setupGlobalService.GetAllSetupValuesBySetupName("AccountingPeriod");
            var accountingPeriod = AllSetupGlobalListPeriod[0].SetupValueInt;

            //var settingAccountingYear = _setupGlobalService.Find("AccountingYear");
            //var accountingYear = settingAccountingYear != null ? settingAccountingYear.SetupValueInt.Value : DateTime.Now.Year;

            //var settingAccountingPeriod = _setupGlobalService.Find("AccountingPeriod");
            //var accountingPeriod = settingAccountingPeriod != null ? settingAccountingPeriod.SetupValueInt.Value : DateTime.Now.Month;

            var salesLedgerEntryPaid = new SalesLedgerEntry
            {
                SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                SalesLedgerEntryDescription = "Cash " + ticket.TicketReference,//+ (salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix),
                SaleAmount = -amountReceived,
                CurrencyAmount = 0.0m,
                CurrencyID = ticket.CurrencyID,
                ExchangeRate = 0.0m,
                CustomerDepartmentID = ticket.CustomerDepartmentID.Value,
                BatchNumberLogID = batchNumberLogID,
                NoteID = null,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                SalesPersonUserID = ticket.SalesPersonUserID,
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear.GetValueOrDefault()//,
                //DB changes: 10/11/2016: column deleted
                //AccountingPeriod = accountingPeriod.GetValueOrDefault()
            };

            _salesLedgerEntryService.Insert(salesLedgerEntryPaid);

            var salesInvoice = new SalesInvoice
            {
                SalesInvoiceID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                CustomerDepartmentID = ticket.CustomerDepartmentID.Value,
                ServerCode = _serverCode,
                SalesInvoiceReference = "Hardcodedvalue",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                SalesInvoiceDate = DateTime.Now,
                
                DivisionAddressID = _divisionService.DivisionById(ticket.DivisionID.Value).AddressID,
                CurrencyID = ticket.CurrencyID,
                ExchangeRate = 0.0m,
                NoteID = null,
                CreatedDate = DateTime.Now,
                CreatedBy = _principal.Id,
               UpdatedDate = DateTime.Now,
                ObjectState = ObjectState.Added
            };

            decimal transactionTaxPercentage = 0.0m;
            foreach (var ticketItem in ticket.TicketItems)
            {
                var salesInvoiceItemLineTotal = ticketItem.TicketItemTotalPrice + ticketItem.PorterageValue;
                var salesInvoiceItem = new SalesInvoiceItem
                {
                    SalesInvoiceItemID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                    SalesInvoiceID = salesInvoice.SalesInvoiceID,
                    SalesInvoiceItemDescription = ticketItem.TicketItemDescription,
                    SalesInvoiceItemLineTotal = salesInvoiceItemLineTotal,
                    TicketItemID = ticketItem.TicketItemID,

                    TransactionTaxRateID = Guid.Parse("76000800-0000-0070-9204-000068336078"),
                   
                    //CurrencyID = null,
                    CurrencyAmount = 0.0m,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = _principal.Id,
                    ObjectState = ObjectState.Added,
                };
                //totalPorteragePrice = totalPorteragePrice + ticketItem.PorterageValue;
                //totalPrice = totalPrice + (ticketItem.TicketItemTotalPrice - ticketItem.PorterageValue);
               // transactionTaxRatePercentage = transactionTaxRatePercentage + salesInvoiceItem.TransactionTaxRatePercentage;
                _salesInvoiceItemService.Insert(salesInvoiceItem);
            }
            //salesLedgerEntry.SaleAmount = totalPrice + totalVat + totalPorteragePrice;
            //salesInvoice.TransactionTaxAmount = 0;

            _salesInvoiceService.Insert(salesInvoice);

            var saleAmountTotal = salesInvoice.SalesInvoiceItems.Sum(x => x.SalesInvoiceItemLineTotal);

            var salesLedgerEntryTotal = new SalesLedgerEntry
            {
                SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                SalesLedgerEntryDescription = "Cash " + ticket.TicketReference,//+ (salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix),
                SaleAmount = saleAmountTotal,
                CurrencyAmount = 0.0m,
                CurrencyID = ticket.CurrencyID,
                ExchangeRate = 0.0m,
                CustomerDepartmentID = ticket.CustomerDepartmentID.Value,
                BatchNumberLogID = batchNumberLogID,
                NoteID = null,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                SalesPersonUserID = ticket.SalesPersonUserID,
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear.GetValueOrDefault()
            };

            _salesLedgerEntryService.Insert(salesLedgerEntryTotal);

            SalesLedgerInvoiceAllocation salesLedgerInvoiceAllocationTotal = new SalesLedgerInvoiceAllocation()
            {
                SalesLedgerInvoiceAllocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                SalesLedgerEntryID = salesLedgerEntryTotal.SalesLedgerEntryID,
                SalesInvoiceID = salesInvoice.SalesInvoiceID,
                SaleAmount = saleAmountTotal,
                CreatedDate = DateTime.Now,
                ObjectState = ObjectState.Added,
                CreatedBy = _principal.Id
            };

            _salesLedgerInvoiceAllocationService.Insert(salesLedgerInvoiceAllocationTotal);

            SalesLedgerInvoiceAllocation salesLedgerInvoiceAllocationPaid = new SalesLedgerInvoiceAllocation()
            {
                SalesLedgerInvoiceAllocationID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                SalesLedgerEntryID = salesLedgerEntryPaid.SalesLedgerEntryID,
                SalesInvoiceID = salesInvoice.SalesInvoiceID,
                SaleAmount = salesLedgerEntryPaid.SaleAmount,
                CreatedDate = DateTime.Now,
                ObjectState = ObjectState.Added,
                CreatedBy = _principal.Id
            };
            
            _salesLedgerInvoiceAllocationService.Insert(salesLedgerInvoiceAllocationPaid);
        }

        public TicketEditModel CreateTicket(TicketEditModel model)
        {           
            var ticket = ApplyChanges(model);
            
            model.TicketID = ticket.TicketID;
            model.ServerCode = _serverCode;
            ticket.ObjectState = ObjectState.Added;
            ticket.CustomerDepartmentID = model.CustomerDepartmentID;
            //ticket.TicketDate = DateTime.Now;
            //ticket.IsActive = true;
          
            model.IsHistory = false;
            model.DivisionID = _principal.DivisionId.Value;
            model.CreatedBy = _principal.Id;
            model.ServerCode = _serverCode;
            //  ticket.CreatedBy = _principal.Firstname;
            ticket.CreatedDate = DateTime.Now;
            model.CreatedDate = ticket.CreatedDate.ToString();
            model.UpdatedDate = ticket.CreatedDate.ToString();

            if (!string.IsNullOrEmpty(model.Notes))
            {
                if (model.NoteID == null || model.NoteID == Guid.Empty)
                {
                    model.NoteID = CreateNote(model.Notes, model.TicketReference, _principal);
                }
                else
                {
                    model.NoteID = UpdateNote(model.NoteID.Value, model.Notes, model.TicketReference, _principal);
                }
            }
            ticket.NoteID = model.NoteID;
            _ticketService.Insert(ticket);

            return model;
        }

        public TicketEditModel UpdateTicket(TicketEditModel model)
        {
            var ticket = ApplyChanges(model);
            ticket.ObjectState = ObjectState.Modified;
            
            
            ticket.IsHistory = model.IsHistory;
            ticket.UpdatedDate = DateTime.Now;
            ticket.IsCashSale = model.IsCashSale;
            ticket.ServerCode = _serverCode;
            ticket.ServerCode = _serverCode;
            ticket.CreatedBy = model.CreatedBy;
            ticket.CreatedDate = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Notes))
            {
                if (model.NoteID == null || model.NoteID == Guid.Empty)
                {
                    model.NoteID = CreateNote(model.Notes, model.TicketReference, _principal);
                }
                else
                {
                    model.NoteID = UpdateNote(model.NoteID.Value, model.Notes, model.TicketReference, _principal);
                }
            }
            else if (model.NoteID != null && model.NoteID != Guid.Empty)
            {
                _noteService.Delete(model.NoteID);
                model.NoteID = null;
            }

            ticket.NoteID = model.NoteID;
            _ticketService.Update(ticket);
            
            return model;
        }

        public void SaveTicket(TicketEditModel model)
        {
            // Create/Update TicketItems
            foreach (var ticketItemModel in model.TicketItems)
            {
                if (ticketItemModel.IsDirty)
                {
                    if (Guid.Empty == ticketItemModel.TicketItemID)
                    {
                        ticketItemModel.CreatedDate = DateTime.Now;
                        ticketItemModel.UpdatedDate = DateTime.Now;

                        CreateTicketItem(ticketItemModel);
                    }
                    else
                    {
                        ticketItemModel.CreatedDate = DateTime.Now;
                        ticketItemModel.UpdatedDate = DateTime.Now;
                        UpdateTicketItem(ticketItemModel);
                    }
                }
            }

            var ticket = ApplyChanges(model);
            ticket.ObjectState = ObjectState.Modified;
           // ticket.IsActive = true;
            
            ticket.UpdatedDate = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Notes))
            {
                if (model.NoteID == null || model.NoteID == Guid.Empty)
                {
                    model.NoteID = CreateNote(model.Notes, model.TicketReference, _principal);
                }
                else
                {
                    model.NoteID = UpdateNote(model.NoteID.Value, model.Notes, model.TicketReference, _principal);
                }
            }
            else if (model.NoteID != null && model.NoteID != Guid.Empty)
            {
                _noteService.Delete(model.NoteID);
                model.NoteID = null;
            }

            ticket.NoteID = model.NoteID;
            _ticketService.Update(ticket);

            if (ticket.IsCashSale.Value && model.AmountReceived.HasValue)
            {
                CreateSalesInvoice(ticket.TicketID, model.AmountReceived.Value);
            }

            
            //04/08/2016
            //AK: COMMENTED OUT REBATE - NOT SAVING TO NOMINAL LEDGER CORRECTLY
            //TO DO: CORRECT LOGIC FOR ENTRY INTO VARIOUS LEDGER IF CUSTOMER HAS REBATE SET UP

            //if (ticket.CustomerDepartmentID != null) 
            //{
            //    var fullTicket = _ticketService.GetTicket(ticket.TicketID);
            //    if (fullTicket.CustomerDepartment.RebateCustomerDepartmentID != null)
            //    {
            //        CreateRebate(fullTicket);
            //    }
            //}
        }

        private void CreateRebate(Ticket ticket)
        {
            var rate = ticket.CustomerDepartment.RebateRate ?? 0;

            var rebate = Decimal.Zero;

            if (ticket.CustomerDepartment.RebateType == 1)
            {
                // percentage %
                decimal totalOfItems = ticket.TicketItems.Sum(x => x.TicketItemTotalPrice);
                rebate = totalOfItems * rate / 100;
            }
            else
            {
                // flat rate in pennies
                rebate = ticket.TicketItems.Count * rate;
            }

            // save into nominal ledger entry
            try
            {                
                var batchNumber = _batchNumberLogService.GiveLatestBatchNumber(
                    new BatchNumberLog
                    {
                        BatchNumberLogID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        ServerPrefix = _serverCode,
                        CreatedBy = _principal.Id,
                        CreatedDate = DateTime.Now,
                       // IsActive = true,
                        CompanyID = _principal.CompanyId.Value,
                        TransactionDateTime = DateTime.Today
                    });

                var settingAccountingYear = _setupGlobalService.Find("AccountingYear");
                var accountingYear = settingAccountingYear != null ? settingAccountingYear.SetupValueInt.Value : DateTime.Now.Year;

                var settingAccountingPeriod = _setupGlobalService.Find("AccountingPeriod");
                var accountingPeriod = Convert.ToByte(settingAccountingPeriod != null ? settingAccountingPeriod.SetupValueInt.Value : DateTime.Now.Month);

                var nominalAccountId = System.Guid.Parse("00760000-0000-0193-0006-828813158487");

                var nominalLedgerEntryReference = string.Format("Rebate {0}", ticket.TicketReference);
                var nominalLedgerEntryDescription = string.Format("Rebate {0}", ticket.TicketReference);
                var nominalLedgerEntryDate = ticket.TicketDate;
                var batchNumberLogId = batchNumber.BatchNumberLogID;

                var nominalLedgerEntry = new NominalLedgerEntry
                {
                    NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    NominalAccountID = nominalAccountId,
                    NominalLedgerEntryReference = nominalLedgerEntryReference,
                    NominalLedgerEntryDescription = nominalLedgerEntryDescription,
                    NominalLedgerEntryAmount = rebate,                    
                    BatchNumberLogID = batchNumberLogId,                    
                    CreatedBy = _principal.Id,
                    CreatedDate = DateTime.Now,
                  //  IsActive = true,
                    ObjectState = ObjectState.Added,
                    AccountingYear = accountingYear,
                    //AccountingPeriod = accountingPeriod,
                    NominalLedgerEntryDate = nominalLedgerEntryDate
                };

                _nominalLedgerEntryService.Insert(nominalLedgerEntry);                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Create Rebate failed", ex);
            }
        }

        public TicketItemEditModel CreateTicketItem(TicketItemEditModel model)
        {

            var myTicketItem = ApplyChanges(model);

            myTicketItem.ObjectState = ObjectState.Added;
            myTicketItem.CreatedBy = _principal.Id;
            myTicketItem.CreatedDate = DateTime.Now;
            myTicketItem.UpdatedBy = _principal.Id;
            myTicketItem.UpdatedDate = DateTime.Now;
            myTicketItem.PorterageID = null;
            myTicketItem.PorterageValue = Convert.ToDecimal(model.TicketItemPorterageValue);
            myTicketItem.TransactionTaxRateID = myTicketItem.TransactionTaxRateID;
            
            myTicketItem.TicketID = model.TicketID;
            myTicketItem.OriginalTicketItemID = myTicketItem.TicketItemID;
            _ticketItemService.Insert(myTicketItem);

            model.TicketItemID = myTicketItem.TicketItemID;
            //model.OriginalTicketItemID = myTicketItem.OriginalTicketItemID;
            return model;
        }

        public TicketItemEditModel UpdateTicketItem(TicketItemEditModel model)
        {
            var TicketItem = ApplyChanges(model);
            TicketItem.ObjectState = ObjectState.Modified;
            TicketItem.UpdatedBy = _principal.Id;
            TicketItem.UpdatedDate = DateTime.Today;
            TicketItem.TransactionTaxRateID = model.TransactionTaxRateID;
            
           // TicketItem.IsActive = true;
            _ticketItemService.Update(TicketItem);
            return model;
        }

        public void RemoveTicketItem(Guid id)
        {
            //var ticketItem = _ticketItemService.Find(id);
            //ticketItem.IsActive = false;
            //ticketItem.ObjectState = ObjectState.Modified;
            _ticketItemService.Delete(id);
        }

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        public TicketViewModel CreateTicket(TicketViewModel model)
        {
            var ticket = ApplyChanges(model.TicketEditModel);

            model.TempTicketID = ticket.TicketID.ToString();
            ticket.ObjectState = ObjectState.Added;
            ticket.SalesPersonUserID = model.TicketEditModel.SalesPersonUserID;
            ticket.TicketDate = DateTime.Now;
            ticket.CreatedBy = _principal.Id;
            ticket.DivisionID = model.TicketEditModel.DivisionID;
         //   ticket.IsActive = true;
            ticket.IsHistory = false;
            
            ticket.CreatedDate = DateTime.Now;
            ticket.UpdatedDate = DateTime.Now;
            //ticket.CustomerCompanyName = model.TicketEditModel.CustomerCompanyName;
            if (!string.IsNullOrEmpty(model.TicketEditModel.Notes))
            {
                if (model.TicketEditModel.NoteID == null || model.TicketEditModel.NoteID == Guid.Empty)
                {
                    model.TicketEditModel.NoteID = CreateNote(model.TicketEditModel.Notes, model.TicketEditModel.TicketReference, _principal);
                }
                else
                {
                    model.TicketEditModel.NoteID = UpdateNote(model.TicketEditModel.NoteID.Value, model.TicketEditModel.Notes, model.TicketEditModel.TicketReference, _principal);
                }
            }
            
            _ticketService.Insert(ticket);
            return model;
        }

        public ReceiptTicketViewModel GetReceiptTicketViewModel(Guid id)
        {
            var salesLedgerEntry = _salesLedgerEntryService.FindById(id);
            var company = _companyService.CompanyWithAddress(_principal.CompanyId.Value); // DC 06/07
            var receiptTicketViewModel = BuildReceiptTicketViewModel(salesLedgerEntry, company);
            return receiptTicketViewModel;
        }

        public CreateTransferPageViewModel GetCreateTransferPageViewModel()
        {
            var createTransferPageViewModel = new CreateTransferPageViewModel
            {
                TransferTypeList = _transferTypeService.GetAllActiveTransferTypes(),
                TransferCreateModel = new TransferCreateViewModel
                {
                    TicketReference = RandomString(),
                    TicketDate = DateTime.Now.ToString(),
                }
            };
            var currency = _currencyService.GetByCurrencyCode("GB");
            createTransferPageViewModel.TransferCreateModel.CurrencyID = currency.CurrencyID;
            createTransferPageViewModel.TransferCreateModel.CurrencyName = currency.CurrencyCode + " - " + currency.CurrencyName;

            return createTransferPageViewModel;
        }

        public Guid CreateTransferTicket(TransferCreateViewModel model)
        {
            var customer = _customerService.GetCustomerByCode("TFRS");
            var customerDepartmentId = customer.CustomerDepartments.First().CustomerDepartmentID;

            var ticketEditModel = new TicketEditModel
            {
                TicketID = model.TicketID,
                TicketReference = model.TicketReference,
                TicketDate = model.TicketDate.ToString(),
                CurrencyID = model.CurrencyID,
                CustomerDepartmentID = customerDepartmentId,
                SalesPersonUserID = model.SalesPersonUserID,
                NoteID = model.NoteID,
                Notes = model.Notes
            };

            var createdTicket = CreateTicket(ticketEditModel);
            return createdTicket.TicketID;
        }

        #region Private Helpers

        private Ticket ApplyChanges(TicketViewModel model)
        {
            var Ticket = new Ticket
            {
                TicketID =
                    Guid.Empty != model.TicketEditModel.TicketID ? model.TicketEditModel.TicketID : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                PONumber = model.TicketEditModel.PONumber,
                TicketReference = model.TicketEditModel.TicketReference,
                ////CustomerCurrencyI = Guid.Parse(model.CustomerCurrencyID.ToString()),
                CurrencyID = Guid.Parse(model.TicketEditModel.CurrencyID.ToString()),
                //SalesPersonID = Guid.Parse(model.TicketID.ToString()),
                //NoteID = Guid.Parse(model.TicketID.ToString()),
                IsCashSale = model.TicketEditModel.IsCashSale,
                TicketDate = DateTime.Parse(model.TicketEditModel.TicketDate),
                CustomerDepartmentID = model.TicketEditModel.CustomerDepartmentID,
                //CurrencyRate = decimal.Parse(model.CurrencyRate),
             
                IsHistory = false,
             
                CreatedDate = DateTime.Now,
                CreatedBy = model.TicketEditModel.CreatedBy,
                UpdatedDate = DateTime.Now,
                UpdatedBy = model.TicketEditModel.UpdatedBy,
            };
            model.TempTicketID = Ticket.TicketID.ToString();

            return Ticket;
        }
   
        private Ticket ApplyChanges(TicketEditModel model)
        {
            var Ticket = new Ticket
            {
                TicketID = Guid.Empty != model.TicketID ? model.TicketID : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                PONumber = model.PONumber,
                TicketReference = model.TicketReference,
                //CustomerCurrencyI = Guid.Parse(model.CustomerCurrencyID.ToString()),
                CurrencyID = model.CurrencyID,
                //CustomerCompanyName = model.CustomerCompanyName,
                SalesPersonUserID = model.SalesPersonUserID,
                SalesPersonName = model.SalesPersonName,
                NoteID = model.NoteID,
                IsCashSale = model.IsCashSale,
                TicketDate = DateTime.Parse(model.TicketDate),
                CustomerDepartmentID = model.CustomerDepartmentID,
                ServerCode = _serverCode,
                // CurrencyRate = decimal.Parse(model.CurrencyRate),
                DivisionID = _principal.DivisionId.Value,
              //  IsActive = true,
               
                IsHistory = false,
                CreatedDate = string.IsNullOrEmpty(model.UpdatedDate) ? DateTime.Now : DateTime.Parse(model.UpdatedDate),
                CreatedBy = _principal.Id,
                UpdatedDate = string.IsNullOrEmpty(model.UpdatedDate) ? DateTime.Now : DateTime.Parse(model.UpdatedDate),
                UpdatedBy = _principal.Id,
            };

            model.CreatedDate = Ticket.CreatedDate.ToString();
            
            return Ticket;
        }

        private TicketItem ApplyChanges(TicketItemEditModel model)
        {
          
            //AK: amended to populate correct description in ticketitem table
            var description = string.Format("{0} - {1} - {2} - {3}", model.TicketItemBrand, model.Produce, model.TicketItemWeight, model.TicketItemSize).Trim();
          //  var description = string.Format("{0} - {1} - {2} - {3}", model.TicketItemBrand, model.Produce, model.TicketItemWeight, model.TicketItemSize).Trim();
            if (!string.IsNullOrEmpty(description) && string.IsNullOrEmpty(model.TicketItemDescription) && model.TicketItemDescription != description)
            {
                model.TicketItemDescription = description;
            }


            //1. Find the transactiontaxcode id from tblProduceTransactionTaxCode for that produce
            //2. Identify the transactiontaxrateid by using the produceid, taxcodeid, and taxlocationid and date.

            //   ConsignmentItem Consigntmentitemproduce = new ConsignmentItem();
            //Consigntmentitemproduce = _consignmentItemService.ConsignmentItemByID(model.ConsignmentItemID);
            //var produceid = Consigntmentitemproduce.ProduceID;
            //Produce producetax = new Produce();
            //producetax = _produceService.ProduceById(produceid);
            
            //TransactionTaxCode vartransactiontaxcode= _transactionTaxCodeService.TransactionTaxCodeById
            //producetax.varTransactionTaxCode//-- table incorrectdata in UAT;

                     
            
                TicketItem newTicketItem = new TicketItem();
                newTicketItem.TicketItemID = Guid.Empty != model.TicketItemID ? model.TicketItemID : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                newTicketItem.TicketID = model.TicketID;
                newTicketItem.TicketItemDescription = model.TicketItemDescription;
                newTicketItem.DepartmentID = model.DepartmentID;
                newTicketItem.TicketItemQuantity = model.TicketItemQuantity;
                newTicketItem.OriginalTicketItemID = model.TicketItemID;
                newTicketItem.TransactionTaxRateID = Guid.Parse("76000800-0000-0070-9204-000068336078");//hardcoded taxrateid to be used until understood.
                
                newTicketItem.TicketItemTotalPrice = model.TicketItemTotalPrice ?? 0;
                newTicketItem.ConsignmentItemID = model.ConsignmentItemID; //model.ConsignmentItemID,
            newTicketItem.IsLatest = true;
                newTicketItem.TransferTypeID = model.TransferTypeID;
                newTicketItem.CreatedDate = DateTime.Now;
                newTicketItem.CreatedBy = model.CreatedBy;
                newTicketItem.UpdatedDate = DateTime.Now;
                newTicketItem.UpdatedBy = model.UpdatedBy;
            return newTicketItem;

        }

        private ReceiptTicketViewModel BuildReceiptTicketViewModel(SalesLedgerEntry entity, Company company)
        {
            var receiptTicketViewModel = new ReceiptTicketViewModel
            {
                TicketID = Guid.Empty,
                //UpdatedBy = entity.UpdatedBy.Value,
                UpdatedBy = entity.UpdatedBy.GetValueOrDefault(),
                UpdatedDate = entity.UpdatedDate != null ? entity.UpdatedDate.ToString() : string.Empty,
                CreatedBy = entity.CreatedBy.Value,
                CreatedDate = entity.CreatedDate != null ? entity.UpdatedDate.ToString() : string.Empty,
                AmountReceived = -entity.SaleAmount
            };

            receiptTicketViewModel.TicketReference = entity.SalesLedgerEntryDescription.Replace("Cash ", "");
            receiptTicketViewModel.TicketDate = entity.CreatedDate != null ? entity.UpdatedDate.ToString() : string.Empty;
            receiptTicketViewModel.SalesPersonUserID = entity.SalesPersonUserID ?? Guid.Empty;
            if (receiptTicketViewModel.SalesPersonUserID != Guid.Empty)
            {
                var salesPerson = _applicationUserService.Find(receiptTicketViewModel.SalesPersonUserID);
                receiptTicketViewModel.SalesPersonName = string.Format("{0} {1}", salesPerson.Firstname, salesPerson.Lastname);
            }

            if (entity.CurrencyID != null)
            {
                receiptTicketViewModel.CurrencyID = entity.CurrencyID.Value;
                receiptTicketViewModel.CurrencyName = entity.Currency.CurrencyCode + " - " + entity.Currency.CurrencyName;
                receiptTicketViewModel.CurrencySymbol = GetCurrencySymbol(entity.Currency.CurrencyCode);
            }

            if (entity.CustomerDepartment != null)
            {
                receiptTicketViewModel.CustomerDepartmentID = entity.CustomerDepartmentID;
                receiptTicketViewModel.CustomerCompanyName = entity.CustomerDepartment.Customer.CustomerCompanyName + " - " + entity.CustomerDepartment.CustomerDepartmentName;
            }

            if (entity.Note != null)
            {
                receiptTicketViewModel.NoteID = entity.Note.NoteID;
                receiptTicketViewModel.Notes = entity.Note.NoteText;
            }

            //receiptTicketViewModel.Company = new CompanyViewModel();
            receiptTicketViewModel.Company = BuildCompanyModel(company);

            var transactionTaxDetails =
                _transactionTaxLocationService.TransactionTaxLocationByCompanyID(company.CompanyID);
            receiptTicketViewModel.TransactionTaxReference = transactionTaxDetails.TransactionTaxReference;

            return receiptTicketViewModel;
        }

        private SalesLedgerEntryViewModel BuildSalesLedgerEntryModel(SalesLedgerEntry entity)
        {
            var entryModel = new SalesLedgerEntryViewModel()
            {
                SalesLedgerEntryID = entity.SalesLedgerEntryID.ToString(),
                CreatedDate = entity.CreatedDate.ToString(),
                SaleAmount = entity.SaleAmount,
                SalesPersonName = entity.SalesPerson == null ? "" : entity.SalesPerson.UserName,
                CustomerDepartment = entity.CustomerDepartment.CustomerDepartmentName
            };

            return entryModel;
        }

        private DailyCashTicketModel BuildDailyCashTicketModel(vwCashTicket entity)
        {
            var vwCashTicketModel = new DailyCashTicketModel()
            {
                TicketReference = entity.TicketReference,
                TicketID = entity.TicketID,
                SalesPersonName = entity.SalesPersonName,
                AmountPaid = entity.AmountPaid,
                BalanceOwed = entity.BalanceOwed,
                TicketTotal = entity.TicketTotal,
                CustomerDepartmentID = entity.CustomerDepartmentID,
                CreatedDate = entity.CreatedDate.ToString(),
                SalesInvoiceID = entity.SalesInvoiceID,
                DivisionID = entity.DivisionID
            };

            return vwCashTicketModel;
        }

        private vwCashTicketViewModel BuildDailyCashTicketModelVw(vwCashTicket entity)
        {
            var vwCashTicketViewModel = new vwCashTicketViewModel()
            {
                TicketReference = entity.TicketReference,
                TicketID = entity.TicketID,
                SalesPersonName = entity.SalesPersonName,
                AmountPaid = entity.AmountPaid,
                BalanceOwed = entity.BalanceOwed,
                TicketTotal = entity.TicketTotal,
                CustomerDepartmentID = entity.CustomerDepartmentID,
                CreatedDate = entity.CreatedDate.ToString(),
                //CreatedDate = entity.CreatedDate,
                SalesInvoiceID = entity.SalesInvoiceID,
                DivisionID = entity.DivisionID
            };

            return vwCashTicketViewModel;
        }

        private TicketEditModel BuildTicketEditModel(Ticket entity)
        {
            var ticketEditModel = new TicketEditModel();

            ticketEditModel.TicketID = entity.TicketID;
            ticketEditModel.PONumber = entity.PONumber;
            ticketEditModel.TicketReference = entity.TicketReference;
            ticketEditModel.CustomerDepartmentID = entity.CustomerDepartmentID.Value;
            if (entity.CurrencyID != null)
            {
                ticketEditModel.CurrencyID = entity.CurrencyID.Value;
                ticketEditModel.Currency = _currencyService.CurrencyById(Guid.Parse(entity.CurrencyID.ToString()));
                ticketEditModel.CurrencyName = ticketEditModel.Currency.CurrencyCode + " - " + ticketEditModel.Currency.CurrencyName;
                ticketEditModel.CurrencySymbol = GetCurrencySymbol(ticketEditModel.Currency.CurrencyCode);
            }

            ticketEditModel.SalesPersonUserID = entity.SalesPersonUserID ?? Guid.Empty;
            if (ticketEditModel.SalesPersonUserID != Guid.Empty)
            {
                var salesPerson = _applicationUserService.FindById(ticketEditModel.SalesPersonUserID);
                if (salesPerson != null)
                {
                    ticketEditModel.SalesPersonName = string.Format("{0} {1}", salesPerson.Firstname, salesPerson.Lastname);
                    if (salesPerson.Department != null)
                    {
                        ticketEditModel.SalesPersonDepartmentID = salesPerson.Department.DepartmentID;
                        ticketEditModel.SalesPersonDepartmentName = salesPerson.Department.DepartmentName;
                        ticketEditModel.SalesPersonDepartmentCode = salesPerson.Department.DepartmentCode;
                    }
                }
            }

            if (entity.Note != null)
            {
                ticketEditModel.NoteID = entity.Note.NoteID;
                ticketEditModel.Notes = entity.Note.NoteText;
            }

            ticketEditModel.IsCashSale = entity.IsCashSale ?? false;
            ticketEditModel.TicketDate = entity.TicketDate.ToString();
            ticketEditModel.CustomerDepartmentID = entity.CustomerDepartmentID.Value;
            var isNullMsg = entity.CustomerDepartment == null ? "CustomerDepartment is NULL"
                : entity.CustomerDepartment.Customer == null ? "CustomerDepartment.Customer is NULL" : null;
            ticketEditModel.CustomerCompanyName = isNullMsg == null
                ?
                entity.CustomerDepartment.Customer.CustomerCompanyName + " - " + entity.CustomerDepartment.CustomerDepartmentName
                : isNullMsg
                ;
            ticketEditModel.CurrencyRate = entity.CurrencyRate.ToString();
            //ticketEditModel.IsActive = entity.IsActive ?? true;           
            ticketEditModel.IsHistory = entity.IsHistory;
            ticketEditModel.CreatedDate = entity.CreatedDate != null ? entity.CreatedDate.ToString() : string.Empty;
            ticketEditModel.CreatedBy = entity.CreatedBy.Value;
            ticketEditModel.UpdatedDate = entity.UpdatedDate != null ? entity.UpdatedDate.ToString() : string.Empty;
            ticketEditModel.UpdatedBy = entity.UpdatedBy.Value;

            ticketEditModel.SalesLedgerEntries = new List<SalesLedgerEntryViewModel>();

            foreach (var ticketItem in entity.TicketItems)
            {
                ticketEditModel.TicketItems.Add(BuildTicketItemEditModel(ticketItem));

                //gathering salesledgerentries

                foreach (var invoiceItem in ticketItem.SalesInvoiceItems)
                {
                    var salesLedgerEntryViewModels = invoiceItem.SalesInvoice.SalesLedgerInvoiceAllocations.Select(al =>
                        new SalesLedgerEntryViewModel()
                        {
                            SalesLedgerEntryID = al.SalesLedgerEntry.SalesLedgerEntryID.ToString(),
                            SaleAmount = al.SalesLedgerEntry.SaleAmount,
                            CreatedDate = al.SalesLedgerEntry.CreatedDate.ToString(),
                            Allocated = al.SaleAmount // this amount was allocated to this invoice from this salesledgerentry
                        }
                        );

                    // add only distinct entries to the list

                    foreach (var salesLedgerEntryViewModel in salesLedgerEntryViewModels)
                    {
                        if (
                            ticketEditModel.SalesLedgerEntries.All(s => s.SalesLedgerEntryID != salesLedgerEntryViewModel.SalesLedgerEntryID))
                        {
                            ticketEditModel.SalesLedgerEntries.Add(salesLedgerEntryViewModel);
                        }
                    }
                }
            }

            ticketEditModel.TicketSubTotal = ticketEditModel.TicketItems.Sum(x => x.TicketItemTotalPrice ?? 0);
            ticketEditModel.TicketTotalPorterage = ticketEditModel.TicketItems.Sum(x => x.TicketItemPorterageValue);
            ticketEditModel.TicketTotalPrice = ticketEditModel.TicketSubTotal + ticketEditModel.TicketTotalPorterage;
            ticketEditModel.PaidAmount = ticketEditModel.TicketTotalPrice - ticketEditModel.SalesLedgerEntries.Sum(x => x.Allocated);
            ticketEditModel.UnpaidAmount = ticketEditModel.SalesLedgerEntries.Sum(x => x.Allocated);
            // Try to get Amount Received from Sales Invoice
            //if (ticketEditModel.IsCashSale)
            //{
            //    var salesLedgerEntry = _salesLedgerEntryService.FindForTicket(ticketEditModel.TicketID);
            //    if (salesLedgerEntry != null)
            //    {
            //        ticketEditModel.AmountReceived = salesLedgerEntry.SaleAmount;
            //    }
            //}
            //-ki
            ticketEditModel.CustomerDepartmentName = _customerDeptService.CustomerDepartmentByCustomerDepartmentId(ticketEditModel.CustomerDepartmentID).CustomerDepartmentName;
            ticketEditModel.CreatedUserName = _applicationUserService.FindById(ticketEditModel.CreatedBy).UserName;
            //-ki
            return ticketEditModel;
        }

        private string GetCurrencySymbol(string currencyCode)
        {
            switch (currencyCode.ToLower())
            {
                case "us": return "$";
                case "eu": return "€";
                case "gb": return "£";
                default: return "£";
            }
        }

        private TicketItemEditModel BuildTicketItemEditModel(TicketItem entity)
        {
            var ticketItemEditModel = new TicketItemEditModel();

            ticketItemEditModel.TicketID = entity.TicketID;
            ticketItemEditModel.TicketItemID = entity.TicketItemID;
            if (entity.DepartmentID != null)
            {
                ticketItemEditModel.DepartmentID = entity.DepartmentID.Value;
                ticketItemEditModel.DepartmentName = entity.Department != null ? entity.Department.DepartmentName : "";
            }
            ticketItemEditModel.TicketItemDescription = entity.TicketItemDescription;
            ticketItemEditModel.TicketItemQuantity = entity.TicketItemQuantity;

            if (entity.ConsignmentItem != null)
            {
                if (entity.ConsignmentItem.Produce != null)
                {
                    ticketItemEditModel.ProduceID = entity.ConsignmentItem.Produce.ProduceID;
                    ticketItemEditModel.Produce = entity.ConsignmentItem.Produce.ProduceName;
                    ticketItemEditModel.ProduceDescription =
                        //string.Format("{0} - ({1}) [{2}] {3} {4} {5},{6},{7}",
                        string.Format("{0} - ({1}) [{2}] {3} {4} {5}",
                            entity.ConsignmentItem.Produce.ProduceName,
                            entity.ConsignmentItem.Produce.ProduceCode,
                            "", // remaining quantity
                            entity.ConsignmentItem.Brand,
                            entity.ConsignmentItem.PackSize,
                        //AK: inserted producecode & supplier code ({6})
                        //entity.ConsignmentItem.PackType);
                            entity.ConsignmentItem.PackType);
                            //entity.ConsignmentItem.Produce.ProduceCode,
                            //entity.ConsignmentItem.Consignment.SupplierDepartment.Supplier.SupplierCode);
                }

                ticketItemEditModel.TicketItemPorterageID = entity.ConsignmentItem.PorterageID;
                if (entity.ConsignmentItem.Porterage != null)
                {
                    ticketItemEditModel.TicketItemPorterage = entity.ConsignmentItem.Porterage.UnitPrice;
                    ticketItemEditModel.TicketItemMinPorterage = entity.ConsignmentItem.Porterage.MinimumAmount;
                }
                ticketItemEditModel.TicketItemBrand = entity.ConsignmentItem.Brand;
                ticketItemEditModel.TicketItemSize = entity.ConsignmentItem.PackSize;
                ticketItemEditModel.TicketItemWeight = entity.ConsignmentItem.PackType;
            }

            if (entity.TransferType != null)
            {
                ticketItemEditModel.TransferTypeID = entity.TransferType.TransferTypeID;
                ticketItemEditModel.TransferTypeName = entity.TransferType.TransferTypeName;
            }

            ticketItemEditModel.TicketItemUnitPrice = entity.TicketItemTotalPrice / entity.TicketItemQuantity;
            ticketItemEditModel.TicketItemTotalPrice = entity.TicketItemTotalPrice;
            ticketItemEditModel.TransactionTaxRateID = entity.TransactionTaxRateID.Value;
            ticketItemEditModel.CreatedBy = entity.CreatedBy.Value;
            ticketItemEditModel.TicketItemPorterageValue = entity.PorterageValue;

            return ticketItemEditModel;
        }

        private string RandomString()
        {
            return DateTime.Today.DayOfWeek.ToString().Substring(0, 2).ToUpper() + new Random().Next(100000, 999999);
        }

        #endregion
    }
}