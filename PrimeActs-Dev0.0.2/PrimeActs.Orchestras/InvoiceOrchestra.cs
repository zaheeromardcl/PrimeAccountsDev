using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Invoice;
using PrimeActs.Orchestras;
using PrimeActs.Data.Contexts.NonEntityDataAccess;
using PrimeActs.Infrastructure.BaseEntities;
using System.Drawing;
using System.IO;
using System.Text;

namespace PrimeActs.Orchestra
{
    public interface IInvoiceOrchestra
    {
        void Initialize(ApplicationUser principal);
        InvoiceModel ExecuteInvoice(InvoiceModel invoiceModel);
        InvoiceViewModel GetInvoiceViewModel(Guid id);
        InvoiceViewModel GetInvoiceViewModelbyRef(string salesReferenceNumber);
        InvoiceDetailModel GetInvoiceDetailViewModel(Guid id);
        InvoiceDetailModel GetInvoiceDetailViewModelByRef(string salesReferenceNumber);
        byte[] GetLogo(Guid id);
        InvoicePagingModel GetInvoicesPagingModel(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Invoice.SearchObject searchObject);
        ResultList<InvoiceEditModel> GetInvoices(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Invoice.SearchObject searchObject);
        List<SalesInvoice> GetCustomerDepartmentSalesInvoices(Guid customerDepartmentID);
        void TestInvoiceTransactions();
    }

    public class InvoiceOrchestra : IInvoiceOrchestra
    {
        private readonly ISetupGlobalService _setupGlobalService;
        private readonly IEventOrchestra _eventOrchestra;
        private readonly IDivisionService _divisionService;
        private readonly INonEntityDataService _nonEntityDataService;
        private readonly ILedgerEntryTypeService _ledgerEntryTypeService;
        private readonly ITicketService _ticketService;
        private readonly ICustomerDepartmentService _customerDepartmentService;
        private readonly ISalesLedgerEntryService _salesLedgerEntryService;
        private readonly ISalesInvoiceItemService _salesInvoiceItemService;
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly IBatchNumberLogService _batchNumberLogService;
        private readonly INominalAccountService _nominalAccountService;
        private readonly INominalLedgerEntryService _nominalLedgerEntryService;
        private readonly IAddressService _addressService;
        private readonly ITicketItemService _ticketItemService;
        private readonly ICompanyService _companyService;
        private readonly IFileService _fileService;
        private readonly ICustomerService _customerService;
        private readonly IConsignmentItemService _consignmentItemService;
        private readonly IConsignmentService _consignmentService;
       
        private readonly IPrinterService _printerService;
        private readonly IAuditService _auditService;
        private readonly string _serverCode;
        private ApplicationUser _principal;
    
        private BatchNumberLog _batchNumber = null;
        private List<string> _salesInvoiceNumbers = new List<string>();

        public InvoiceOrchestra(ISetupGlobalService setupGlobalService
            , ISetupLocalService setupLocalService
            , IEventOrchestra eventOrchestra
            , IDivisionService divisionService
            , INonEntityDataService nonEntityDataService
            , ICustomerDepartmentService customerDepartmentService
            , ITicketService ticketService
            , ILedgerEntryTypeService ledgerEntryTypeService
            , ISalesLedgerEntryService salesLedgerEntryService
            , ISalesInvoiceItemService salesInvoiceItemService
            , ISalesInvoiceService salesInvoiceService
            , IBatchNumberLogService batchNumberLogService
            , INominalAccountService nominalAccountService
            , INominalLedgerEntryService nominalLedgerEntryService
            , ITicketItemService ticketItemService
            , IAddressService addressService
            , ICompanyService companyService
            , IFileService fileService
            , IConsignmentService consignmentService
            , IConsignmentItemService consignmentItemService
            , ICustomerService customerService
         
            , IPrinterService printerService
            , IAuditService auditService
            )
        {

            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _setupGlobalService = setupGlobalService;
            _eventOrchestra = eventOrchestra;
            _divisionService = divisionService;
            _nonEntityDataService = nonEntityDataService;
            _customerDepartmentService = customerDepartmentService;
            _customerService = customerService;
            _ledgerEntryTypeService = ledgerEntryTypeService;
            _ticketService = ticketService;
            _salesLedgerEntryService = salesLedgerEntryService;
            _salesInvoiceItemService = salesInvoiceItemService;
            _salesInvoiceService = salesInvoiceService;
            _batchNumberLogService = batchNumberLogService;
            _nominalAccountService = nominalAccountService;
            _nominalLedgerEntryService = nominalLedgerEntryService;
            _ticketItemService = ticketItemService;
            _addressService = addressService;
            _companyService = companyService;
            _consignmentItemService = consignmentItemService;
            _consignmentService = consignmentService;
            
            _printerService = printerService;
            _auditService = auditService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;

        }
        
        public byte[] GetLogo(Guid id)
        {
            var q = _companyService.Query()
                    .Select().Where(x => x.CompanyID == id).FirstOrDefault()
                    ;

            byte[] pic = q.Logo;

            return pic;
        }

        public InvoiceViewModel GetInvoiceViewModelbyRef(string salesReferenceNumber)
        {
            Guid id =
                _salesInvoiceService.Query(x => x.SalesInvoiceReference == salesReferenceNumber)
                    .Select()
                    .SingleOrDefault()
                    .SalesInvoiceID;
            return GetInvoiceViewModel(id);
        }
        
        public InvoiceViewModel GetInvoiceViewModel(Guid id)
        {
            var invoiceViewModel = new InvoiceViewModel
            {
                InvoiceEditModel =
                    id == Guid.Empty
                        ? new InvoiceEditModel
                        {
                            SalesInvoiceReference = RandomString(),
                            SalesInvoiceDate = DateTime.Today.ToShortDateString(),
                            InvoiceItemEditModels = new List<InvoiceItemEditModel> { new InvoiceItemEditModel() }
                        } : BuildInvoiceEditModel(_salesInvoiceService.SalesInvoiceById(id))
            };
            return invoiceViewModel;
        }

        public InvoiceDetailModel GetInvoiceDetailViewModel(Guid id)
        {
            var invoiceDetailViewModel = BuildInvoiceDetailViewModel(_salesInvoiceService.SalesInvoiceById(id));
            return invoiceDetailViewModel;
        }

        public InvoiceDetailModel GetInvoiceDetailViewModelByRef(string salesReferenceNumber)
        {
            Guid id =
                _salesInvoiceService.Query(x => x.SalesInvoiceReference == salesReferenceNumber)
                    .Select()
                    .SingleOrDefault()
                    .SalesInvoiceID;
            return GetInvoiceDetailViewModel(id);
        }

        public void TestInvoiceTransactions()
        {
            // DC - Test Commitment Control Methodologies
            Printer p = new Printer()
            {
                PrinterID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                PrinterName = "TEST",
                NetworkName = "TEST",
                DefaultOrder = 99,
                IsActive = false,
                IsColour = false,
                IsRaw = false,
                HasTractor = false
            };
            _printerService.Insert(p);           

            Audit ad = new Audit()
            {
                AuditID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                JsonDataAfter = "TEST",
                JsonDataBefore = "TEST",
                ContentType = "TEST",
                UserID = _principal.Id,
                EditDate = DateTime.Now,
                CompanyID = Guid.Parse("EA3CDB19-D647-4E87-AA83-3A7E523F16C8"),
                DivisionID = Guid.Parse("8510D575-22BB-4E12-B71C-1487B722FE35"),
                DepartmentID = Guid.Parse("00760000-0000-0002-0006-830109511300"),
                ReferenceID = Guid.Parse("00760000-0000-0006-8367-096886203610"),
                Reference = "TEST"
               
            };
            _auditService.Insert(ad);
           
        }

        public InvoiceModel ExecuteInvoice(InvoiceModel invoiceModel)
        {
            var primeEvent = new PrimeEvent { EventName = "Invoice" };
            List<Division> divisions = new List<Division>();
            foreach (var item in invoiceModel.InvoiceRunModel.SelectedDivisions)
            {
                divisions.Add(_divisionService.DivisionById(Guid.Parse(item)));

            }
            primeEvent.Events.AddRange(divisions.Select(div => new InvoiceEvent { Division = div, Username = _principal.UserName }).ToList());
            if (_eventOrchestra.IsRunning(primeEvent))
            {
                invoiceModel.ShowRunningInvoice = true;
                invoiceModel.ShowCompletedInvoice = false;
                invoiceModel.ShowRunInvoice = false;
                var currentEvent = _eventOrchestra.GetCurrentRunning(primeEvent);
                invoiceModel.InvoiceStatusModels.Clear();
                foreach (var item in currentEvent.Events)
                {

                    invoiceModel.InvoiceStatusModels.Add(new InvoiceStatus { Username = item.Username, DivisionName = item.Division.DivisionName.ToString(), Period = "Daily" });
                }
                invoiceModel.InvoiceCompletedModels = null;
                invoiceModel.InvoiceRunModel = null;
                return invoiceModel;
            }

            _eventOrchestra.StartEvent(primeEvent);

            if (invoiceModel.InvoiceRunModel.IsInvoiceRun)
            {
                var batchNumber = InvoiceRun(primeEvent, invoiceModel.InvoiceRunModel.Period, invoiceModel.InvoiceRunModel.IsCashSale);
                if (batchNumber != null)
                {
                    foreach (var item in _salesInvoiceNumbers)
                    {

                        invoiceModel.InvoiceCompletedModels.Add(new InvoiceCompletedModel { Username = _principal.UserName, SalesInvoiceReference = item.Split('*')[1].ToString(), DivisionName = _divisionService.DivisionById(Guid.Parse(item.Split('*')[0])).DivisionName, BatchNumber = item.Split('*')[2].ToString(), InvoiceType = "Invoice" });
                    }
                    _salesInvoiceNumbers.Clear();
                }
            }

            if (invoiceModel.InvoiceRunModel.IsCreditNoteRun)
            {
                var batchNumber = CreditNotesRun(primeEvent, invoiceModel.InvoiceRunModel.Period, invoiceModel.InvoiceRunModel.IsCashSale);
                if (batchNumber != null)
                {
                    foreach (var item in _salesInvoiceNumbers)
                    {
                        invoiceModel.InvoiceCompletedModels.Add(new InvoiceCompletedModel { Username = _principal.UserName, SalesInvoiceReference = item.Split('*')[1].ToString(), DivisionName = _divisionService.DivisionById(Guid.Parse(item.Split('*')[0])).DivisionName, BatchNumber = item.Split('*')[2].ToString(), InvoiceType = "Credit Notes" });
                    }
                    _salesInvoiceNumbers.Clear();
                }
            }
            _eventOrchestra.EndEvent(primeEvent);
            invoiceModel.ShowCompletedInvoice = true;
            invoiceModel.ShowRunInvoice = false;
            invoiceModel.InvoiceRunModel = null;
            invoiceModel.InvoiceStatusModels = null;
            invoiceModel.ShowRunningInvoice = false;
            return invoiceModel;


        }
        
        public InvoicePagingModel GetInvoicesPagingModel(QueryOptions queryOptions, Domain.ViewModels.Invoice.SearchObject searchObject)
        {
            var totalCount = 0;
            var inoviocePagingModel = new InvoicePagingModel();
            var invoices = _salesInvoiceService.GetSalesInvoices(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<InvoiceEditModel>(invoices.Select(BuildInvoiceEditModel).ToList(), queryOptions);
            inoviocePagingModel.InvoiceEditModels = result;
            inoviocePagingModel.SearchObject = new PrimeActs.Domain.ViewModels.Invoice.SearchObject
            {
                SalesInvoiceReference = searchObject.SalesInvoiceReference,
                CustomerDepartmentName = searchObject.CustomerDepartmentName,
                ConsignmentReference = searchObject.ConsignmentReference,
                TicketReference = searchObject.TicketReference,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null,
                RecordsInDays = searchObject.RecordsInDays
            };
            return inoviocePagingModel;
        }

        public ResultList<InvoiceEditModel> GetInvoices(QueryOptions queryOptions, Domain.ViewModels.Invoice.SearchObject searchObject)
        {
            var totalCount = 0;
            var inoviocePagingModel = new InvoicePagingModel();
            var invoices = _salesInvoiceService.GetSalesInvoices(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<InvoiceEditModel>(
                    invoices != null ? invoices.Select(BuildInvoiceEditModel).ToList() : null, queryOptions);
        }

        public List<SalesInvoice> GetCustomerDepartmentSalesInvoices(Guid customerDepartmentID) 
        {

            var customerDepartmentInvoices = _salesInvoiceService.GetSalesInvoicesByCustomerDepartment(customerDepartmentID);
            return customerDepartmentInvoices;
        
        }

        private InvoiceEditModel BuildInvoiceEditModel(SalesInvoice entity)
        {
            var invoiceEditModel = new InvoiceEditModel();

            if (entity.SalesInvoiceItems.Count > 0)
            {
                invoiceEditModel.InvoiceItemEditModels = entity.SalesInvoiceItems.Select(BuildInvoiceItemEditModel).ToList();
                invoiceEditModel.CompanyDivisionName = entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.Company.CompanyName + "-" + entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.DivisionName;
                //invoiceEditModel.CompanyVATRegistrationNumber = entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.Company.T;
                invoiceEditModel.CompanyNumber = entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.Company.CompanyNo;
            }

            invoiceEditModel.SalesInvoiceID = entity.SalesInvoiceID;
            invoiceEditModel.SalesInvoiceReference = entity.SalesInvoiceReference.ToString();
            invoiceEditModel.SalesInvoiceDate = entity.SalesInvoiceDate.ToShortDateString();
            invoiceEditModel.CustomerDepartmentID = entity.CustomerDepartmentID;
            invoiceEditModel.CustomerDepartmentName = entity.CustomerDepartment.CustomerDepartmentName;

            invoiceEditModel.CustomerDepartmentAddress1 = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.AddressLine1 : string.Empty;
            invoiceEditModel.CustomerDepartmentAddress2 = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.AddressLine2 : string.Empty;
            invoiceEditModel.CustomerDepartmentAddress3 = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.AddressLine3 : string.Empty;
            invoiceEditModel.customerDepartmentPostalTown = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.PostalTown : string.Empty;
            invoiceEditModel.customerDepartmentPostcode = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.Postcode : string.Empty;

            invoiceEditModel.DivisionAddress1 = entity.DivisionAddress != null ? entity.DivisionAddress.AddressLine1 : string.Empty;
            invoiceEditModel.DivisionAddress2 = entity.DivisionAddress != null ? entity.DivisionAddress.AddressLine2 : string.Empty;
            invoiceEditModel.DivisionAddress3 = entity.DivisionAddress != null ? entity.DivisionAddress.AddressLine3 : string.Empty;
            invoiceEditModel.DivisionPostalTown = entity.DivisionAddress != null ? entity.DivisionAddress.PostalTown : string.Empty;
            invoiceEditModel.DivisionPostCode = entity.DivisionAddress != null ? entity.DivisionAddress.Postcode : string.Empty;
            invoiceEditModel.Currency = entity.Currency != null ? entity.Currency.CurrencyName.ToString() : string.Empty;
            invoiceEditModel.NoteText = entity.Note != null ? entity.Note.NoteText.ToString() : null;
            invoiceEditModel.ExchangeRate = entity.ExchangeRate.ToString();

            invoiceEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
            invoiceEditModel.CreatedBy = _principal.Id;
            invoiceEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
            invoiceEditModel.UpdatedBy = _principal.Id;

            return invoiceEditModel;
        }



        private InvoiceDetailModel BuildInvoiceDetailViewModel(SalesInvoice entity)
        {
            var invoiceDetailViewModel = new InvoiceDetailModel();

            if (entity.SalesInvoiceItems.Count > 0)
            {
                var invoiceItemEditModels = entity.SalesInvoiceItems.Select(BuildInvoiceItemEditModel).ToList();
                invoiceDetailViewModel.InvoiceTicketItems = invoiceItemEditModels.GroupBy(x => x.TicketNumber)
                    .Select(g => new InvoiceTicketItemModel
                    {
                        TicketNumber = g.Key,
                        PorterageValue = g.Sum(x => Convert.ToDecimal(x.porterageValue)).ToString(),
                        InvoiceItemModels = g.ToList()
                    })
                    .ToList();
                invoiceDetailViewModel.CompanyDivisionName = entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.Company.CompanyName + "-" + entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.DivisionName;
               // invoiceDetailViewModel.CompanyVATRegistrationNumber = entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.Company.TransactionTaxNo;
                invoiceDetailViewModel.CompanyNumber = entity.SalesInvoiceItems.First().TicketItem.Ticket.Division.Company.CompanyNo;
            }

            invoiceDetailViewModel.SalesInvoiceID = entity.SalesInvoiceID;
            invoiceDetailViewModel.SalesInvoiceReference = entity.SalesInvoiceReference.ToString();
            invoiceDetailViewModel.SalesInvoiceDate = entity.SalesInvoiceDate.ToShortDateString();
            invoiceDetailViewModel.CustomerDepartmentID = entity.CustomerDepartmentID;
            invoiceDetailViewModel.CustomerDepartmentName = entity.CustomerDepartment.CustomerDepartmentName;

            invoiceDetailViewModel.CustomerDepartmentAddress1 = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.AddressLine1 : string.Empty;
            invoiceDetailViewModel.CustomerDepartmentAddress2 = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.AddressLine2 : string.Empty;
            invoiceDetailViewModel.CustomerDepartmentAddress3 = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.AddressLine3 : string.Empty;
            invoiceDetailViewModel.CustomerDepartmentPostalTown = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.PostalTown : string.Empty;
            invoiceDetailViewModel.CustomerDepartmentPostcode = entity.CustomerDepartmentAddress != null ? entity.CustomerDepartmentAddress.Postcode : string.Empty;

            invoiceDetailViewModel.DivisionAddress1 = entity.DivisionAddress != null ? entity.DivisionAddress.AddressLine1 : string.Empty;
            invoiceDetailViewModel.DivisionAddress2 = entity.DivisionAddress != null ? entity.DivisionAddress.AddressLine2 : string.Empty;
            invoiceDetailViewModel.DivisionAddress3 = entity.DivisionAddress != null ? entity.DivisionAddress.AddressLine3 : string.Empty;
            invoiceDetailViewModel.DivisionPostalTown = entity.DivisionAddress != null ? entity.DivisionAddress.PostalTown : string.Empty;
            invoiceDetailViewModel.DivisionPostCode = entity.DivisionAddress != null ? entity.DivisionAddress.Postcode : string.Empty;
            invoiceDetailViewModel.Currency = entity.Currency != null ? entity.Currency.CurrencyName.ToString() : string.Empty;
            invoiceDetailViewModel.NoteText = entity.Note != null ? entity.Note.NoteText.ToString() : null;
            invoiceDetailViewModel.ExchangeRate = entity.ExchangeRate.ToString();

            invoiceDetailViewModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
            invoiceDetailViewModel.CreatedBy = _principal.Id;
            invoiceDetailViewModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
            invoiceDetailViewModel.UpdatedBy = _principal.Id;

            return invoiceDetailViewModel;
        }
        
        private BatchNumberLog InvoiceRun(PrimeEvent primeEvent, string period, bool isCashSale)
        {
            int accountingYear;
            byte accountingPeriod;
            Guid salesNominalAccountId;

            InvoiceRunSetAccounting(out accountingYear, out accountingPeriod, out salesNominalAccountId);
            var ledgerEntryType = _ledgerEntryTypeService.LedgerEntryTypeByNumber(2);
            BatchNumberLog batchNumber = null;

            DateTime startDateTime = StartDate(period);
            DateTime endDateTime = EndDate(period, startDateTime);
            foreach (var division in primeEvent.Events.Select(x => x.Division))
            {
                List<TicketItem> ticketItems = GetInvoiceRunTicketItems(isCashSale, startDateTime, endDateTime, division);
                
                if (ticketItems == null || ticketItems.Count <= 0)
                    continue;

                batchNumber = GetInvoiceRunBatchNumber(batchNumber, division);

                _batchNumber = batchNumber;
                GetInvoiceRunSalesInvoiceNumber(division);

                var currencyIds = ticketItems.Select(x => x.Ticket.CurrencyID).Distinct();

                foreach (var currency in currencyIds)
                {
                    var customerDepartmentIds = ticketItems.Where(x => x.Ticket.CurrencyID == currency).Select(x => x.Ticket.CustomerDepartmentID.Value).Distinct();
                    foreach (var customerDepartmentId in customerDepartmentIds.AsParallel())
                    {
                        
                        //salesInvoiceNumber.SalesInvoiceNumber = (int.Parse(salesInvoiceNumber.SalesInvoiceNumber) + 1).ToString();
                       // _salesInvoiceNumbers.Add(division.DivisionID + "*" + salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix + "*" + batchNumber.BatchNumber);
                       
                        var salesLedgerEntry = BuildSalesLedgerEntry(accountingYear, accountingPeriod, ledgerEntryType, batchNumber, customerDepartmentId);
                        var salesLedgerEntrySLC = BuildSalesLedgerEntrySLC(accountingYear, accountingPeriod, ledgerEntryType, batchNumber, customerDepartmentId);

                        var salesInvoice = BuildSalesInvoice(division, customerDepartmentId);

                        decimal totalPorteragePrice = 0.0m;
                        decimal totalPrice = 0.0m;
                        decimal totalVat = 0.0m;

                        InvoiceRunAddSalesInvoiceItems(ticketItems, salesInvoice, ref totalPorteragePrice, ref totalPrice);

                        salesLedgerEntry.SaleAmount = totalPrice  + totalPorteragePrice;
                        salesLedgerEntrySLC.SaleAmount = -1 * totalPrice + totalVat + totalPorteragePrice;
                     
                        _salesLedgerEntryService.Insert(salesLedgerEntry);
                        _salesLedgerEntryService.Insert(salesLedgerEntrySLC);
                        _salesInvoiceService.Insert(salesInvoice);

                        PostNominalLedgerAll(accountingYear, accountingPeriod, salesNominalAccountId, batchNumber, totalPorteragePrice, totalPrice, totalVat);
                        PostNominalLedgerPorterage(accountingYear, accountingPeriod, batchNumber, totalPorteragePrice);
                        PostNominalLedgerTotal(accountingYear, accountingPeriod, batchNumber, totalPrice);
                        PostNominalLedgerVat(accountingYear, accountingPeriod, batchNumber, totalVat);
                    }
                }
                var description = "";
                //if (salesInvoiceNumber != null)
                //{
                //    description = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix;
                //    _salesInvoiceNumberService.Update(salesInvoiceNumber);
                //}

                RebateRunWorker(division, ticketItems, ledgerEntryType.LedgerEntryTypeID, accountingYear, accountingPeriod, salesNominalAccountId, description);
            }
            return batchNumber;
        }

        private void PostNominalLedgerAll(int accountingYear, byte accountingPeriod, Guid salesNominalAccountId, BatchNumberLog batchNumber, decimal totalPorteragePrice, decimal totalPrice, decimal totalVat)
        {
            var nominalLedgerEntryAll = BuildNominalLedgerEntry(accountingYear, accountingPeriod, salesNominalAccountId, batchNumber, totalPorteragePrice, totalPrice, totalVat);
            _nominalLedgerEntryService.Insert(nominalLedgerEntryAll);
        }

        private void PostNominalLedgerVat(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalVat)
        {
            var nominalAccountTotalVat = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "VAT");
            var nominalLedgerEntryVat = BuildNominalLedgerEntryVat(accountingYear, accountingPeriod, batchNumber, totalVat, nominalAccountTotalVat);
            _nominalLedgerEntryService.Insert(nominalLedgerEntryVat);
        }

        private void PostNominalLedgerTotal(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalPrice)
        {
            var nominalAccountTotalSales = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "SLC");
            var nominalLedgerEntrytotal = BuildNominalLedgerEntryTotal(accountingYear, accountingPeriod, batchNumber, totalPrice, nominalAccountTotalSales);
            _nominalLedgerEntryService.Insert(nominalLedgerEntrytotal);
        }

        private void PostNominalLedgerPorterage(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalPorteragePrice)
        {
            var nominalAccountPorterage = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "PORT");
            var nominalLedgerEntryPorterage = BuildNominalLedgerEntryPorterage(accountingYear, accountingPeriod, batchNumber, totalPorteragePrice, nominalAccountPorterage);
            _nominalLedgerEntryService.Insert(nominalLedgerEntryPorterage);
        }

        private void InvoiceRunSetAccounting(out int accountingYear, out byte accountingPeriod, out Guid salesNominalAccountId)
        {
            var settingAccountingYear = _setupGlobalService.Find("AccountingYear");
            accountingYear = settingAccountingYear != null ? settingAccountingYear.SetupValueInt.Value : DateTime.Now.Year;

            var settingAccountingPeriod = _setupGlobalService.Find("AccountingPeriod");
            accountingPeriod = Convert.ToByte(settingAccountingPeriod != null ? settingAccountingPeriod.SetupValueInt.Value : DateTime.Now.Month);

            var settingSlcNominalAccountId = _setupGlobalService.Find("SlcNominalAccountId");
            var slcNominalAccountId = Guid.Parse(settingSlcNominalAccountId != null ? settingSlcNominalAccountId.SetupValueNvarchar : "00760000-0000-0001-0006-817528069450");

            var settingSalesNominalAccountId = _setupGlobalService.Find("SalesNominalAccountId");
            salesNominalAccountId = Guid.Parse(settingSalesNominalAccountId != null ? settingSalesNominalAccountId.SetupValueNvarchar : "00760000-0000-0005-0006-817528069450");

        }

        private void InvoiceRunAddSalesInvoiceItems(List<TicketItem> ticketItems, SalesInvoice salesInvoice, ref decimal totalPorteragePrice, ref decimal totalPrice)
        {
            foreach (var ticketItem in ticketItems)
            {
                var salesInvoiceItemLineTotal = ticketItem.TicketItemTotalPrice + ticketItem.PorterageValue;
                var salesInvoiceItem = BuildSalesInvoiceItem(salesInvoice, ticketItem, salesInvoiceItemLineTotal);
                totalPorteragePrice = totalPorteragePrice + ticketItem.PorterageValue;
                totalPrice = totalPrice + salesInvoiceItemLineTotal;
                //   totalVat = totalVat + salesInvoiceItem.SalesInvoiceItemVAT;
                _salesInvoiceItemService.Insert(salesInvoiceItem);
            }
        }

        private void GetInvoiceRunSalesInvoiceNumber(Division division)
        {
            //if (salesInvoiceNumber == null)
            //{

            //    //todo : SOLUTION FOR SALES INVOICE NUMBER
            //    salesInvoiceNumber "1233";//= _salesInvoiceNumberService.Query(x => x.DivisionID == division.DivisionID).Select().SingleOrDefault();
            //}
            //if (salesInvoiceNumber.DivisionID != division.DivisionID)
            //{
            //    //todo : SOLUTION FOR SALES INVOICE NUMBER
            //    salesInvoiceNumber = "123";//_salesInvoiceNumberService.Query(x => x.DivisionID == division.DivisionID).Select().SingleOrDefault();
            //}
        }

        private BatchNumberLog GetInvoiceRunBatchNumber(BatchNumberLog batchNumber, Division division)
        {            
            if (_batchNumber == null)
            {
                batchNumber = _batchNumberLogService.GiveLatestBatchNumber(new BatchNumberLog
                {
                    BatchNumberLogID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    ServerPrefix = _serverCode,
                    CreatedBy = _principal.Id,
                    CreatedDate = DateTime.Now,
                    //IsActive = true,
                    CompanyID = division.CompanyID,
                    TransactionDateTime = DateTime.Today
                });
            }
            else
            {
                batchNumber = new BatchNumberLog
                {
                    BatchNumberLogID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    ServerPrefix = _serverCode,
                    CreatedBy = _principal.Id,
                    CreatedDate = DateTime.Now,
                    //IsActive = true,
                    CompanyID = division.CompanyID,
                    TransactionDateTime = DateTime.Today,
                    BatchNumber = _batchNumber.BatchNumber + 1

                };
                _batchNumberLogService.Insert(batchNumber);
            }
            return batchNumber;
        }

        private List<TicketItem> GetInvoiceRunTicketItems(bool isCashSale, DateTime startDateTime, DateTime endDateTime, Division division)
        {
            List<TicketItem> ticketItems = _ticketItemService.GetTicketItemsForNewInvoice(division.DivisionID, startDateTime, endDateTime, isCashSale);
            ticketItems.AddRange(_ticketItemService.GetEditedTicketItemsForInvoice(division.DivisionID, startDateTime, endDateTime, isCashSale));
            return ticketItems;
        }

        private SalesLedgerEntry BuildSalesLedgerEntry(int accountingYear, byte accountingPeriod, LedgerEntryType ledgerEntryType, BatchNumberLog batchNumber, Guid customerDepartmentId)
        {
            var salesLedgerEntry = new SalesLedgerEntry
            {
                SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                CustomerDepartmentID = customerDepartmentId,
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                SalesLedgerEntryDescription = "HARDCODED FOR NOW",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                CurrencyAmount = 0.0m,
                CurrencyID = null,
                ExchangeRate = 0.0m,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,               
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear//,
                //DB changes: 10/11/2016: columen deleted
                //AccountingPeriod = accountingPeriod
            };
            return salesLedgerEntry;
        }

        private SalesLedgerEntry BuildSalesLedgerEntrySLC(int accountingYear, byte accountingPeriod, LedgerEntryType ledgerEntryType, BatchNumberLog batchNumber, Guid customerDepartmentId)
        {
            var salesLedgerEntrySLC = new SalesLedgerEntry
            {
                SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                CustomerDepartmentID = customerDepartmentId,
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                SalesLedgerEntryDescription = "HALSDFS",// salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                CurrencyAmount = 0.0m,
                CurrencyID = null,
                ExchangeRate = 0.0m,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,               
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear//,
                //DB changes: 10/11/2016: columen deleted
                //AccountingPeriod = accountingPeriod

            };
            return salesLedgerEntrySLC;
        }

        private SalesInvoice BuildSalesInvoice(Division division, Guid customerDepartmentId)
        {
            var salesInvoice = new SalesInvoice
            {
                SalesInvoiceID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                CustomerDepartmentID = customerDepartmentId,
                ServerCode = _serverCode,
                SalesInvoiceReference = "ASDFASDF",// salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                SalesInvoiceDate = DateTime.Now,

                DivisionAddressID = division.AddressID,
                CurrencyID = null,
                ExchangeRate = 0.0m,
                NoteID = null,
                CreatedDate = DateTime.Now,
                CreatedBy = _principal.Id,
                ObjectState = ObjectState.Added
            };
            return salesInvoice;
        }

        private SalesInvoiceItem BuildSalesInvoiceItem(SalesInvoice salesInvoice, TicketItem ticketItem, decimal salesInvoiceItemLineTotal)
        {
            var salesInvoiceItem = new SalesInvoiceItem
            {
                SalesInvoiceItemID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                SalesInvoiceID = salesInvoice.SalesInvoiceID,
                SalesInvoiceItemDescription = ticketItem.TicketItemDescription,
                SalesInvoiceItemLineTotal = salesInvoiceItemLineTotal,
                TicketItemID = ticketItem.TicketItemID,
                //  SalesInvoiceItemVAT = 0,//salesInvoiceItemLineTotal * (ticketItem.TransactionTaxRatePercentage ?? 0 / 100),
                //CurrencyID = null,
                CurrencyAmount = 0.0m,
                CreatedDate = DateTime.Now,
                CreatedBy = _principal.Id,
                ObjectState = ObjectState.Added
            };
            return salesInvoiceItem;
        }

        private NominalLedgerEntry BuildNominalLedgerEntryVat(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalVat, NominalAccount nominalAccountTotalVat)
        {
            var nominalLedgerEntryVat = new NominalLedgerEntry
            {
                NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                //DivisionID = divisionId,
                NominalAccountID = nominalAccountTotalVat.NominalAccountID,
                NominalLedgerEntryReference = "REF",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryDescription = "DESC",// salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryAmount = totalVat,
                NominalLedgerEntryDate = DateTime.Now,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                // IsActive = true,
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear
               // AccountingPeriod = accountingPeriod

            };
            return nominalLedgerEntryVat;
        }

        private NominalLedgerEntry BuildNominalLedgerEntryTotal(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalPrice, NominalAccount nominalAccountTotalSales)
        {
            var nominalLedgerEntrytotal = new NominalLedgerEntry
            {
                NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                //DivisionID = divisionId,
                NominalAccountID = nominalAccountTotalSales.NominalAccountID,
                NominalLedgerEntryReference = "REF",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryDescription = "DESC",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryAmount = totalPrice,
                NominalLedgerEntryDate = DateTime.Now,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                AccountingYear = accountingYear,
                //AccountingPeriod = accountingPeriod,
                // IsActive = true,
                ObjectState = ObjectState.Added
            };
            return nominalLedgerEntrytotal;
        }

        private NominalLedgerEntry BuildNominalLedgerEntryPorterage(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalPorteragePrice, NominalAccount nominalAccountPorterage)
        {
            var nominalLedgerEntryPorterage = new NominalLedgerEntry
            {
                NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                //DivisionID = divisionId,
                NominalAccountID = nominalAccountPorterage.NominalAccountID,
                NominalLedgerEntryReference = "REF",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryDescription = "DESC",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryAmount = totalPorteragePrice,
                NominalLedgerEntryDate = DateTime.Now,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                IsReconciled= false,
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear
                //,
                //AccountingPeriod = accountingPeriod

            };
            return nominalLedgerEntryPorterage;
        }

        private NominalLedgerEntry BuildNominalLedgerEntry(int accountingYear, byte accountingPeriod, Guid salesNominalAccountId, BatchNumberLog batchNumber, decimal totalPorteragePrice, decimal totalPrice, decimal totalVat)
        {
            var nominalLedgerEntryAll = new NominalLedgerEntry
            {
                NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                //DivisionID = divisionId,
                NominalAccountID = salesNominalAccountId,
                NominalLedgerEntryReference = "REF",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryDescription = "DESC",//salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryAmount = -1 * totalPrice + totalVat + totalPorteragePrice,
                NominalLedgerEntryDate = DateTime.Now,                
                CreatedBy = _principal.Id,
                ObjectState = ObjectState.Added,
                CreatedDate = DateTime.Now,
                AccountingYear = accountingYear
                //,
                //AccountingPeriod = accountingPeriod

            };
            return nominalLedgerEntryAll;
        }

        private void RebateRunWorker(Division division, List<TicketItem> ticketItems, 
            Guid ledgerEntryTypeID, int accountingYear, byte accountingPeriod, 
            Guid nominalAccountId, string description)
        {
            var ticketItemsByCurrencyId = ticketItems.GroupBy(x => x.Ticket.CurrencyID)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var currencyIdTicketItemPair in ticketItemsByCurrencyId)
            {
                var currencyId = currencyIdTicketItemPair.Key;
                var ticketItemsForCurrency = currencyIdTicketItemPair.Value;

                var batchNumber = _batchNumberLogService.GiveLatestBatchNumber(new BatchNumberLog
                {
                    BatchNumberLogID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    ServerPrefix = _serverCode,
                    CreatedBy = _principal.Id,
                    CreatedDate = DateTime.Now,
                    CompanyID = division.CompanyID,
                    TransactionDateTime = DateTime.Today
                });

                var ticketItemsByCustomerDepartment = ticketItemsForCurrency
                    .Where(x => x.Ticket.CustomerDepartment.RebateType != 0)
                    .GroupBy(x => x.Ticket.CustomerDepartment)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var customerDeparmentIdTicketItemPair in ticketItemsByCustomerDepartment)
                {
                    var customerDepartment = customerDeparmentIdTicketItemPair.Key;
                    var ticketItemsToRebate = customerDeparmentIdTicketItemPair.Value;

                    var rebateSalesLedgerEntry = PostRebateSalesLedgerEntry(ledgerEntryTypeID, accountingYear, accountingPeriod, description, batchNumber, customerDepartment, ticketItemsToRebate);

                    PostRebateNominalLedger(accountingYear, accountingPeriod, nominalAccountId, description, batchNumber, rebateSalesLedgerEntry);
                }
            }

            //var customerDepartment = _customerDepartmentService.CustomerDepartmentById(customerDepartmentId);
            //if (customerDepartment.RebateType != 0)
            //{
            //    var rebateSalesLedgerEntry = new SalesLedgerEntry
            //    {
            //        SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
            //        LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
            //        CustomerDepartmentID = customerDepartment.RebateCustomerDepartmentID.Value,
            //        BatchNumberLogID = batchNumber.BatchNumberLogID,
            //        SalesLedgerEntryDescription = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
            //        FXSaleAmount = 0.0m,
            //        CurrencyID = null,
            //        ExchangeRate = 0.0m,
            //        CreatedBy = _principal.UserName,
            //        CreatedDate = DateTime.Now,
            //        IsActive = true,
            //        ObjectState = ObjectState.Added,
            //        AccountingYear = accountingYear,
            //        AccountingPeriod = accountingPeriod
            //    };

            //    if (customerDepartment.RebateType == 1) // Rebate value per ticket
            //    {
            //        var ticketCount = ticketItems.Select(x => x.TicketID).Distinct().Count();
            //        rebateSalesLedgerEntry.SaleAmount = ticketCount * customerDepartment.RebateRate.Value;
            //    }
            //    else if (customerDepartment.RebateType == 2) // Rebate would be percentage of sale
            //    {
            //        rebateSalesLedgerEntry.SaleAmount = salesLedgerEntry.SaleAmount * customerDepartment.RebateRate.Value;
            //    }
            //    _salesLedgerEntryService.Insert(rebateSalesLedgerEntry);
            //}
        }

        private SalesLedgerEntry PostRebateSalesLedgerEntry(Guid ledgerEntryTypeID, int accountingYear, byte accountingPeriod, string description, BatchNumberLog batchNumber, CustomerDepartment customerDepartment, List<TicketItem> ticketItemsToRebate)
        {
            var rebateSalesLedgerEntry = BuildRebateSalesLedgerEntry(ledgerEntryTypeID, accountingYear, accountingPeriod, description, batchNumber, customerDepartment);

            if (customerDepartment.RebateType == 1)
            {
                var ticketCount = ticketItemsToRebate.Select(x => x.TicketID).Distinct().Count();
                rebateSalesLedgerEntry.SaleAmount = ticketCount * customerDepartment.RebateRate.Value;
            }
            else if (customerDepartment.RebateType == 2)
            {
                var totalSaleAmount = ticketItemsToRebate.Sum(x => x.TicketItemTotalPrice + x.PorterageValue + ((x.TicketItemTotalPrice + x.PorterageValue)));
                rebateSalesLedgerEntry.SaleAmount = totalSaleAmount * customerDepartment.RebateRate.Value;
            }

            _salesLedgerEntryService.Insert(rebateSalesLedgerEntry);
            return rebateSalesLedgerEntry;
        }

        private void PostRebateNominalLedger(int accountingYear, byte accountingPeriod, Guid nominalAccountId, string description, BatchNumberLog batchNumber, SalesLedgerEntry rebateSalesLedgerEntry)
        {
            var rebateNominalLedgerEntry = BuildRebateNominalLedger(accountingYear, accountingPeriod, nominalAccountId, description, batchNumber, rebateSalesLedgerEntry);
            _nominalLedgerEntryService.Insert(rebateNominalLedgerEntry);
        }

        private NominalLedgerEntry BuildRebateNominalLedger(int accountingYear, byte accountingPeriod, Guid nominalAccountId, string description, BatchNumberLog batchNumber, SalesLedgerEntry rebateSalesLedgerEntry)
        {
            var rebateNominalLedgerEntry = new NominalLedgerEntry
            {
                NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                //DivisionID = divisionId,
                NominalAccountID = nominalAccountId,
                NominalLedgerEntryReference = description,
                NominalLedgerEntryDescription = description,
                NominalLedgerEntryAmount = -1 * rebateSalesLedgerEntry.SaleAmount,
                NominalLedgerEntryDate = DateTime.Now,
                CreatedBy = _principal.Id,
                ObjectState = ObjectState.Added,
                CreatedDate = DateTime.Now,
                AccountingYear = accountingYear//,
                //AccountingPeriod = accountingPeriod
            };
            return rebateNominalLedgerEntry;
        }

        private SalesLedgerEntry BuildRebateSalesLedgerEntry(Guid ledgerEntryTypeID, int accountingYear, byte accountingPeriod, string description, BatchNumberLog batchNumber, CustomerDepartment customerDepartment)
        {
            var rebateSalesLedgerEntry = new SalesLedgerEntry
            {
                SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                LedgerEntryTypeID = ledgerEntryTypeID,
                CustomerDepartmentID = customerDepartment.RebateCustomerDepartmentID.Value,
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                SalesLedgerEntryDescription = description,
                CurrencyAmount = 0.0m,
                CurrencyID = null,
                ExchangeRate = 0.0m,
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                ObjectState = ObjectState.Added,
                AccountingYear = accountingYear//,
                //DB changes: 10/11/2016: columen deleted                          
                //AccountingPeriod = accountingPeriod
            };
            return rebateSalesLedgerEntry;
        }

        private BatchNumberLog CreditNotesRun(PrimeEvent primeEvent, string period, bool isCashSale)
        {
            var settingAccountingYear = _setupGlobalService.Find("AccountingYear");
            var accountingYear = settingAccountingYear != null ? settingAccountingYear.SetupValueInt.Value : DateTime.Now.Year;

            var settingAccountingPeriod = _setupGlobalService.Find("AccountingPeriod");
            var accountingPeriod = Convert.ToByte(settingAccountingPeriod != null ? settingAccountingPeriod.SetupValueInt.Value : DateTime.Now.Month);

            var ticketItems = new List<TicketItem>();
            var ledgerEntryType = _ledgerEntryTypeService.LedgerEntryTypeByNumber(2);

            BatchNumberLog batchNumber = null;
            foreach (var division in primeEvent.Events.Select(x => x.Division))
            {
                DateTime startDateTime = StartDate(period);
                DateTime endDateTime = EndDate(period, startDateTime);
                ticketItems.AddRange(_ticketItemService.GetTicketItemsForCreditNote(division.DivisionID, startDateTime, endDateTime, isCashSale));
                if (ticketItems.Count <= 0)
                    continue;

                batchNumber = GetInvoiceRunBatchNumber(batchNumber, division);
                //if (_batchNumber == null)
                //{
                //    batchNumber = _batchNumberLogService.GiveLatestBatchNumber(new BatchNumberLog
                //    {
                //        BatchNumberLogID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                //        ServerPrefix = _serverCode,
                //        CreatedBy = _principal.UserName,
                //        CreatedDate = DateTime.Now,
                //        CompanyID = division.CompanyID,
                //        TransactionDateTime = DateTime.Now
                //    });
                //}
                //else
                //{
                //    batchNumber = new BatchNumberLog
                //    {
                //        BatchNumberLogID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                //        ServerPrefix = _serverCode,
                //        CreatedBy = _principal.UserName,
                //        CreatedDate = DateTime.Now,
                //        CompanyID = division.CompanyID,
                //        TransactionDateTime = DateTime.Now,
                //        BatchNumber = _batchNumber.BatchNumber + 1,
                //    };
                //    _batchNumberLogService.Insert(batchNumber);

                //}
                _batchNumber = batchNumber;
                //if (salesInvoiceNumber == null)
                //{
                //    todo : SOLUTION FOR SALES INVOICE NUMBER
                //    salesInvoiceNumber = "122";
                //    salesInvoiceNumber = _salesInvoiceNumberService.Query(x => x.DivisionID == division.DivisionID).Select().SingleOrDefault();
                //}
                //if (salesInvoiceNumber.DivisionID != division.DivisionID)
                //{
                //    todo : SOLUTION FOR SALES INVOICE NUMBER
                //    salesInvoiceNumber = "122";
                //    salesInvoiceNumber = _salesInvoiceNumberService.Query(x => x.DivisionID == division.DivisionID).Select().SingleOrDefault();
                //}

                var CurrenciesList = ticketItems.Select(x => x.Ticket.CurrencyID).Distinct();
                foreach (var currency in CurrenciesList)
                {
                    var customerDepartments = ticketItems.Where(x => x.Ticket.CurrencyID == currency).Select(x => x.Ticket.CustomerDepartmentID).Distinct();
                    foreach (var customerDepartment in customerDepartments.AsParallel())
                    {
                        //salesInvoiceNumber.SalesInvoiceNumber ="123";// (int.Parse(salesInvoiceNumber.SalesInvoiceNumber) + 1).ToString();
                       // _salesInvoiceNumbers.Add(division.DivisionID + "123");// + salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix + "*" + batchNumber.BatchNumber);

                        var salesLedgerEntry = BuildSalesLedgerEntry(accountingYear, accountingPeriod, ledgerEntryType, batchNumber, (Guid)customerDepartment);
                        var salesLedgerEntrySLC = BuildSalesLedgerEntrySLC(accountingYear, accountingPeriod, ledgerEntryType, batchNumber, (Guid)customerDepartment);
                        var salesInvoice = BuildSalesInvoice(division, (Guid)customerDepartment);

                        //var salesLedgerEntry = new SalesLedgerEntry
                        //{
                        //    SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),

                        //    LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                        //    CustomerDepartmentID = (Guid)customerDepartment,
                        //    BatchNumberLogID = batchNumber.BatchNumberLogID,
                        //    SalesLedgerEntryDescription = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    SaleAmount = 0.0m,
                        //    FXSaleAmount = 0.0m,
                        //    CurrencyID = null,
                        //    ExchangeRate = 0.0m,
                        //    CreatedBy = _principal.UserName,
                        //    CreatedDate = DateTime.Now,
                        //   // IsActive = true,
                        //    ObjectState = ObjectState.Added
                        //};
                        
                        //var salesLedgerEntrySLC = new SalesLedgerEntry
                        //{
                        //    SalesLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        //    LedgerEntryTypeID = ledgerEntryType.LedgerEntryTypeID,
                        //    CustomerDepartmentID = (Guid)customerDepartment,
                        //    BatchNumberLogID = batchNumber.BatchNumberLogID,
                        //    SalesLedgerEntryDescription = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    FXSaleAmount = 0.0m,
                        //    CurrencyID = null,
                        //    ExchangeRate = 0.0m,
                        //    CreatedBy = _principal.UserName,
                        //    CreatedDate = DateTime.Now,                       
                        //    ObjectState = ObjectState.Added
                        //};
                        
                        //var salesInvoice = new SalesInvoice
                        //{
                        //    SalesInvoiceID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        //    CustomerDepartmentID = customerDepartment.Value,
                        //    ServerCode = _serverCode,
                        //    SalesInvoiceReference = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    SalesInvoiceDate = DateTime.Now,                          
                        //    DivisionAddressID = division.AddressID,
                        //    CurrencyID = null,
                        //    ExchangeRate = 0.0m,
                        //    NoteID = null,
                        //    CreatedDate = DateTime.Now,
                        //    CreatedBy = _principal.UserName,
                        //    ObjectState = ObjectState.Added
                        //};
                        
                        decimal totalPorteragePrice = 0.0m;
                        decimal totalPrice = 0.0m;
                        decimal totalVat = 0.0m;
                        InvoiceRunAddSalesInvoiceItems(ticketItems, salesInvoice, ref totalPorteragePrice, ref totalPrice);
                        //foreach (var ticketItem in ticketItems)
                        //{
                        //    var salesInvoiceItemLineTotal = ticketItem.TicketItemTotalPrice + ticketItem.PorterageValue;
                        //    var salesInvoiceItem = new SalesInvoiceItem
                        //    {
                        //        SalesInvoiceItemID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        //        SalesInvoiceID = salesInvoice.SalesInvoiceID,
                        //        SalesInvoiceItemDescription = ticketItem.TicketItemDescription,
                        //        SalesInvoiceItemLineTotal = salesInvoiceItemLineTotal,
                        //        TicketItemID = ticketItem.TicketItemID,
                        //      // SalesInvoiceItemVAT = salesInvoiceItemLineTotal * (ticketItem.TransactionTaxRatePercentage ?? 0 / 100),
                        //        //CurrencyID = null,
                        //        CurrencyAmount = 0.0m,
                        //        CreatedDate = DateTime.Now,
                        //        CreatedBy = _principal.UserName,
                        //        ObjectState = ObjectState.Added
                        //    };

                        //    totalPorteragePrice = totalPorteragePrice + ticketItem.PorterageValue;
                        //    totalPrice = totalPrice + ticketItem.TicketItemTotalPrice;
                        //    totalVat = totalVat;// + salesInvoiceItem.SalesInvoiceItemVAT;
                        //    _salesInvoiceItemService.Insert(salesInvoiceItem);
                        //}

                        salesLedgerEntry.SaleAmount = totalPrice + totalVat + totalPorteragePrice;
                        salesLedgerEntrySLC.SaleAmount = -1 * totalPrice + totalVat + totalPorteragePrice;
                       
                        _salesLedgerEntryService.Insert(salesLedgerEntry);
                        _salesLedgerEntryService.Insert(salesLedgerEntrySLC);
                        _salesInvoiceService.Insert(salesInvoice);

                        PostNominalLedgerCreditNoteAll(accountingYear, accountingPeriod, batchNumber, totalPorteragePrice, totalPrice, totalVat);
                        PostNominalLedgerPorterage(accountingYear, accountingPeriod, batchNumber, totalPorteragePrice);
                        PostNominalLedgerTotal(accountingYear, accountingPeriod, batchNumber, totalPrice);
                        PostNominalLedgerVat(accountingYear, accountingPeriod, batchNumber, totalVat);

                        //var nominalAccountPorterage = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "PORT");
                        //var nominalLedgerEntryPorterage = new NominalLedgerEntry
                        //{
                        //    NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        //    BatchNumberLogID = batchNumber.BatchNumberLogID,
                        //    //DivisionID = divisionId,
                        //    NominalAccountID = nominalAccountPorterage.NominalAccountID,
                        //    NominalLedgerEntryReference = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    NominalLedgerEntryDescription = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    NominalLedgerEntryAmount = totalPorteragePrice,
                        //    NominalLedgerEntryDate = DateTime.Now,
                        //    CreatedBy = _principal.UserName,
                        //    CreatedDate = DateTime.Now,
                        //    IsHistory = true,
                        //    ObjectState = ObjectState.Added,
                        //    AccountingYear = accountingYear,
                        //    AccountingPeriod = accountingPeriod
                        //};
                        //_nominalLedgerEntryService.Insert(nominalLedgerEntryPorterage);
                       


                        //var nominalAccountTotalSales = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "SLC");
                        //var nominalLedgerEntrytotal = new NominalLedgerEntry
                        //{
                        //    NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        //    BatchNumberLogID = batchNumber.BatchNumberLogID,
                        //    //DivisionID = divisionId,
                        //    NominalAccountID = nominalAccountTotalSales.NominalAccountID,
                        //    NominalLedgerEntryReference = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    NominalLedgerEntryDescription = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    NominalLedgerEntryAmount = totalPrice,
                        //    NominalLedgerEntryDate = DateTime.Now,
                        //    CreatedBy = _principal.UserName,
                        //    CreatedDate = DateTime.Now,
                        //    AccountingYear = accountingYear,
                        //    AccountingPeriod = accountingPeriod,
                        //    ObjectState = ObjectState.Added
                        //};
                        //_nominalLedgerEntryService.Insert(nominalLedgerEntrytotal);
                        

                        //var nominalAccountTotalVat = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "VAT");
                        //var nominalLedgerEntryVat = new NominalLedgerEntry
                        //{
                        //    NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        //    BatchNumberLogID = batchNumber.BatchNumberLogID,
                        //    //DivisionID = divisionId,
                        //    NominalAccountID = nominalAccountTotalVat.NominalAccountID,
                        //    NominalLedgerEntryReference = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    NominalLedgerEntryDescription = salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                        //    NominalLedgerEntryAmount = totalVat,
                        //    NominalLedgerEntryDate = DateTime.Now,
                        //    CreatedBy = _principal.UserName,
                        //    CreatedDate = DateTime.Now,
                        //    AccountingYear = accountingYear,
                        //    AccountingPeriod = accountingPeriod,
                        //    ObjectState = ObjectState.Added
                        //};
                        //_nominalLedgerEntryService.Insert(nominalLedgerEntryVat);                       

                    }
                }
                //if (salesInvoiceNumber != null)
                //   // _salesInvoiceNumberService.Update(salesInvoiceNumber);
            }
            return batchNumber;
        }

        private void PostNominalLedgerCreditNoteAll(int accountingYear, byte accountingPeriod, BatchNumberLog batchNumber, decimal totalPorteragePrice, decimal totalPrice, decimal totalVat)
        {
            var nominalAccountSales = _nominalAccountService.GetAllNominalAccounts().SingleOrDefault(x => x.NominalCode == "SALES");
            var nominalLedgerEntryAll = new NominalLedgerEntry
            {
                NominalLedgerEntryID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                BatchNumberLogID = batchNumber.BatchNumberLogID,
                //DivisionID = divisionId,
                NominalAccountID = nominalAccountSales.NominalAccountID,
                NominalLedgerEntryReference ="ref",// salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryDescription = "DESC",// salesInvoiceNumber.Prefix + salesInvoiceNumber.SalesInvoiceNumber + salesInvoiceNumber.Suffix,
                NominalLedgerEntryAmount = -1 * totalPrice + totalVat + totalPorteragePrice,
                NominalLedgerEntryDate = DateTime.Now,
                CreatedBy = _principal.Id,
                ObjectState = ObjectState.Added,
                CreatedDate = DateTime.Now,
                AccountingYear = accountingYear
                //,AccountingPeriod = accountingPeriod
            };
            _nominalLedgerEntryService.Insert(nominalLedgerEntryAll);
        }
        
        private InvoiceItemEditModel BuildInvoiceItemEditModel(SalesInvoiceItem entity)
        {
            var invoiceItemEditModel = new InvoiceItemEditModel();

            invoiceItemEditModel.SalesInvoiceID = entity.SalesInvoiceID;
            invoiceItemEditModel.SalesInvoiceItemID = entity.SalesInvoiceItemID;
            invoiceItemEditModel.SalesInvoiceItemDescription = entity.SalesInvoiceItemDescription;
            invoiceItemEditModel.SalesInvoiceItemLineTotal = entity.SalesInvoiceItemLineTotal.ToString();
           // invoiceItemEditModel.SalesInvoiceItemVAT = entity.SalesInvoiceItemVAT.ToString();

            invoiceItemEditModel.Currency = entity.SalesInvoice.Currency != null ? entity.SalesInvoice.Currency.CurrencyName.ToString() : String.Empty;
            invoiceItemEditModel.CurrencyID = entity.SalesInvoice.Currency != null ? entity.SalesInvoice.Currency.CurrencyID : Guid.Empty;
            invoiceItemEditModel.TicketItemID = entity.TicketItemID;
            invoiceItemEditModel.ExchangeRate = entity.CurrencyAmount.ToString();
            invoiceItemEditModel.porterageValue = entity.TicketItem != null ? entity.TicketItem.PorterageValue.ToString() : string.Empty;// _ticketItemService.TicketItemByID(entity.TicketItemID).PorterageValue.ToString();
            invoiceItemEditModel.Brand = entity.TicketItem != null && entity.TicketItem.ConsignmentItem != null ? entity.TicketItem.ConsignmentItem.Brand : string.Empty;//_ticketItemService.Query(x => x.TicketItemID == entity.TicketItemID).Include(inc => inc.ConsignmentItem).Select(x => x.ConsignmentItem.Brand).SingleOrDefault();
            invoiceItemEditModel.TicketNumber = entity.TicketItem != null && entity.TicketItem.Ticket != null ? entity.TicketItem.Ticket.TicketReference : string.Empty;//_ticketItemService.Query(x => x.TicketItemID == entity.TicketItemID).Include(inc => inc.Ticket).Select(x => x.Ticket.TicketReference).SingleOrDefault().ToString();
            invoiceItemEditModel.TicketItemQty = entity.TicketItem != null ? entity.TicketItem.TicketItemQuantity.ToString() : string.Empty;//_ticketItemService.Query(x => x.TicketItemID == entity.TicketItemID).Select(x => x.TicketItemQuantity).SingleOrDefault().ToString();
            invoiceItemEditModel.TicketItemTotalPrice = entity.TicketItem != null ? entity.TicketItem.TicketItemTotalPrice.ToString() : string.Empty;// _ticketItemService.Query(x => x.TicketItemID == entity.TicketItemID).Select(x => x.TicketItemTotalPrice).SingleOrDefault().ToString();
            return invoiceItemEditModel;
        }

        private string RandomString()
        {
            return DateTime.Today.DayOfWeek.ToString().Substring(0, 2).ToUpper() + new Random().Next(100000, 999999);
        }

        private DateTime StartDate(string Period)
        {
            switch (Period)
            {
                case "Daily":
                    return DateTime.Parse(DateTime.Today.ToShortDateString());
                    break;
                case "Weekly":
                    DateTime day = DateTime.Today;
                    DateTime rv;
                    int offset = 0;

                    switch (day.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            offset = 5;
                            break;
                        case DayOfWeek.Monday:
                            offset = 4;
                            break;
                        case DayOfWeek.Tuesday:
                            offset = 3;
                            break;
                        case DayOfWeek.Wednesday:
                            offset = 2;
                            break;
                        case DayOfWeek.Thursday:
                            offset = 1;
                            break;
                        case DayOfWeek.Friday:
                            offset = 0;
                            break;
                        case DayOfWeek.Saturday:
                            offset = -1;
                            break;
                    }

                    rv = day.AddDays(-7 + offset);
                    return DateTime.Parse(rv.ToShortDateString());

                    break;
                case "Monthly":
                    DateTime firstFriday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                    while (firstFriday.DayOfWeek != DayOfWeek.Friday)
                    {
                        firstFriday = firstFriday.AddDays(1);
                    }
                    return DateTime.Parse(firstFriday.ToShortDateString());
                    break;
            }
            return DateTime.Parse(DateTime.Today.ToShortDateString());
        }

        private DateTime EndDate(string Period, DateTime startdate)
        {

            switch (Period)
            {
                case "Daily":
                    return DateTime.Parse(startdate.ToString("yyyy-MM-dd 23:59:59.000"));
                    break;
                case "Weekly":
                    return DateTime.Parse(startdate.AddDays(6).ToString("yyyy-MM-dd 23:59:59.000"));
                    break;
                case "Monthly":
                    return DateTime.Parse(startdate.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                    break;
            }
            return DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd 23:59:59.000"));
        }

    }
}