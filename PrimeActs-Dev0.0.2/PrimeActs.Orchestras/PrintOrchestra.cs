using Microsoft.ServiceBus;
using Mustache;
using Newtonsoft.Json;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AutoMapper;
//using AutoMapper.Internal;
using PrimeActs.Infrastructure.Extensions;
using PrimeActs.PrintFormatStructure;
using PrimeActs.Data.Service;
using PrimeActs.Infrastructure.EntityFramework;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;



namespace PrimeActs.Orchestras
{
    public interface IPrintOrchestra
    {
        void Initialize(ApplicationUser principal);

        bool PrintTest();
        bool RawPrintTest();
        dynamic DisectionPrintTest(DisectionReportViewModel disectionReportView, bool returnActionPrint = true);
        dynamic DisectionPrint(DisectionReportViewModel disectionReportView, bool returnActionPrint = true);
        //bool RawPrintCondensedTest();
        bool PrintDailySalesReport(DateTime runDate);
        bool PrintReceipt(TicketPrintViewModel receiptToPrint);        
        bool PrintReceiptTicket(ReceiptTicketViewModel receiptToPrint);
        string CSVGenericTest();
    }

    public class PrintOrchestra : IPrintOrchestra
    {
        private readonly ILogger _logger;
        private readonly ISetupLocalService _setupLocalService;
        private readonly IPrinterOrchestra _printerOrchestra;
        private readonly IPrintTaskOrchestra _printTaskOrchestra;
        

        private ApplicationUser _principal;
        private readonly string _serverCode = string.Empty;
        private IProduceGroupService _produceGroupService;
        private IConsignmentItemService _consignmentItemService;
        private ISupplierDepartmentService _supplierDepartmentService;
        private IConsignmentService _consignmentService;
        private IConsignmentItemArrivalService _consignmentItemArrivalService;
        private ITicketItemService _ticketItemService;
        private IDepartmentPrintTaskService _departmentPrintTaskService;
        private IPrintTaskService _printTaskService;
        private IPurchaseInvoiceService _purchaseInvoiceService;
        private IBankAccountService _bankAccountService;


        public PrintOrchestra(ISetupLocalService setupLocalService, IPrinterOrchestra printerOrchestra, ILogger logger, IProduceGroupService produceGroupService, IConsignmentItemService consignmentItemService, ISupplierDepartmentService supplierDepartmentService, IConsignmentService consignmentService, IConsignmentItemArrivalService consignmentItemArrivalService, ITicketItemService ticketItemService, IDepartmentPrintTaskService departmentPrintTaskService, IPurchaseInvoiceService purchaseInvoiceService, IPrintTaskService printTaskService, IPrintTaskOrchestra printTaskOrchestra, IBankAccountService bankAccountService)
        {
            
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _setupLocalService = setupLocalService;
            _printerOrchestra = printerOrchestra;
            _produceGroupService = produceGroupService;
            _consignmentItemService = consignmentItemService;
            _supplierDepartmentService = supplierDepartmentService;
            _consignmentService = consignmentService;
            _consignmentItemArrivalService = consignmentItemArrivalService;
            _ticketItemService = ticketItemService;
             _printTaskService = printTaskService;
             _printTaskOrchestra = printTaskOrchestra;
            _departmentPrintTaskService = departmentPrintTaskService;
            _purchaseInvoiceService = purchaseInvoiceService;
            _bankAccountService = bankAccountService;
            _logger = logger;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        //public class Counterparty
        //{
        //    public DateTime Date { get; set; }
        //    public string Name { get; set; }
        //    public string Amount { get; set; }
        //    public string HundredThousands { get; set; }
        //    public string TenThousands { get; set; }
        //    public string Thousands { get; set; }
        //    public string Hundreds { get; set; }
        //    public string Tens { get; set; }
        //    public string Units { get; set; }
        //}

        //public class TestLevel1
        //{
        //    public TestLevel1()
        //    {
        //        Level2List = new List<TestLevel2>();
        //    }
        //    public string Level1 { get; set; }
        //    public List<TestLevel2> Level2List { get; set; }

        //}

        //public class TestLevel0
        //{
        //    public TestLevel0()
        //    {
        //        Level1List = new List<TestLevel1>();
        //    }
        //    public string Level0 { get; set; }
        //    public List<TestLevel1> Level1List { get; set; }
        //}

        //public class TestLevel2
        //{
        //    public string Level2 { get; set; }
        //}

        //public class DailySalesLine
        //{
        //    public string ConsignmentReference { get; set; }
        //    public string TicketItemDescription { get; set; }
        //    public double? CashQty { get; set; }
        //    public decimal? CashUnit { get; set; }
        //    public decimal? CashTotal { get; set; }
        //    public decimal? CashPort { get; set; }
        //    public double? CreditQty { get; set; }
        //    public decimal? CreditUnit { get; set; }
        //    public decimal? CreditTotal { get; set; }
        //    public decimal? CreditPort { get; set; }
        //    public string TicketReference { get; set; }
        //    public string CustomerCompanyName { get; set; }
        //    public string Brand { get; set; }
        //    public string PackType { get; set; }
        //    public string TicketDate { get; set; }
        //    public string PackSize { get; set; }
        //    public string SalesInitial { get; set; }
        //}

        //public class DailySalesGroup
        //{
        //    public List<DailySalesLine> DailySalesLines { get; set; }
        //    public double? TotalCashQty { get; set; }
        //    public decimal? TotalCashUnit { get; set; }
        //    public decimal? TotalCashTotal { get; set; }
        //    public decimal? TotalCashPort { get; set; }
        //    public double? TotalCreditQty { get; set; }
        //    public decimal? TotalCreditUnit { get; set; }
        //    public decimal? TotalCreditTotal { get; set; }
        //    public decimal? TotalCreditPort { get; set; }
        //}

        //public class DailySalesPrintModel
        //{
        //    public List<Ticket> TicketLine { get; set; }
        //    private List<DailySalesGroup> DailySalesGroups { get; set; }
        //}

        //public class ProduceItemExtended : Produce
        //{
        //    public ProduceItemExtended()
        //    {
        //        ConsignmentPrintModels = new List<ConsignmentPrintModel>();
        //    }

        //    public string SupplierCode { get; set; }
        //    public string SupplierName { get; set; }
        //    public List<ConsignmentPrintModel> ConsignmentPrintModels { get; set; }
        //}

        //public class ExtendedProduceGroup : ProduceGroup
        //{
        //    public List<ProduceItemExtended> Produces { get; set; }
        //}

        //public class ChequePrintModel
        //{
        //    public ChequePrintModel()
        //    {
        //        ChequePages = new List<ChequePage>();
        //    }

        //    public List<ChequePage> ChequePages { get; set; }
        //}

        //public class ChequePage
        //{
        //    public ChequePage()
        //    {
        //        ChequePrintRemittanceItems = new List<ChequePrintRemittanceItem>();
        //    }

        //    public List<ChequePrintRemittanceItem> ChequePrintRemittanceItems { get; set; }
        //    public ChequePrintHeader ChequePrintHeader { get; set; }
        //    public bool PrintVoidCheque { get; set; }
        //}

        //public class ChequePrintHeader
        //{

        //    public int LinesPerRemittance { get; set; } // control the Moustache template used for printing
        //    public bool PrintVoidCheque { get; set; }
        //    public Address ChequeAddress { get; set; }
        //    public string ChequePrintDate { get; set; }
        //    public string ChequeName { get; set; }
        //    public decimal? ChequeAmount { get; set; }
        //    public int ChequeNumber { get; set; } // more put in place for reference in future, should we also print cheque number on the Remittance
        //    public string ChequeWords { get; set; }
        //    public string AccCode { get; set; }
        //}

        //public class ChequePrintRemittanceItem
        //{

        //    public string Reference { get; set; }
        //    public string RemittanceDate { get; set; }
        //    public string RemittanceType { get; set; }
        //    public decimal? DebitAmount { get; set; }
        //    public decimal? CreditAmount { get; set; }
        //    public decimal? Balance { get; set; }
        //}

        //public class ConsignmentPrintModel : ConsignmentItem
        //{
        //    // public ConsignmentItem ConsignmentPrintItem { get; set; }
        //    public string ReceivedDate { get; set; }
        //    public List<DisectionSubLine> SubLines { get; set; }
        //}

        //public class Disection
        //{
        //    public Disection()
        //    {
        //        ProduceGroups = new List<ProduceGroup>();
        //    }
        //    public DateTime Date { get; set; }
        //    public List<ProduceGroup> ProduceGroups { get; set; }

        //}

        //public class ProduceDisection
        //{
        //    public ProduceDisection()
        //    {
        //        Produces = new List<ProduceItemExtended>();
        //    }
        //    public DateTime Date { get; set; }
        //    public List<ProduceItemExtended> Produces { get; set; }
        //}

        //public class DisectionSub1
        //{
        //    public string Price { get; set; }
        //    public string QtySoldToday { get; set; }
        //    public string ValueSoldToday { get; set; }
        //    public string PreviousReturnsQty { get; set; }
        //    public string PreviousReturnsValue { get; set; }
        //    public string PreviousSaleQty { get; set; }
        //    public string PreviousSaleValue { get; set; }           
        //}

        //public class DisectionSub2
        //{
        //    public string SalesCode { get; set; }
        //    public string SalesTicket { get; set; }
        //    public string SalesQty { get; set; }
        //    public string SalesPrice { get; set; }
        //    public string SalesTotal { get; set; }
        //}

        //public class DisectionSubLine
        //{
        //    public string Col1 { get; set; }
        //    public string Col2 { get; set; }
        //    public string Col3 { get; set; }
        //    public string Col4 { get; set; }
        //    public string Col5 { get; set; }
        //    public string Col6 { get; set; }
        //    public string Col7 { get; set; }
        //    public string Col8 { get; set; }
        //    public string Col9 { get; set; }
        //    public string Col10 { get; set; }
        //    public string Col11 { get; set; }
        //    public string Col12 { get; set; }
        //    public string Col13 { get; set; }
        //    public string Col14 { get; set; }
        //    public string Col15 { get; set; }
        //    public string Col16 { get; set; }
        //    public string Col17 { get; set; }
        //}

        //public class DisectionSubPrintModel
        //{
        //    public List<DisectionSub1> DisectionSubList { get; set; }
        //}

        //public class TestRawPrintCondensed
        //{
        //    public string PrintDate { get; set; }
        //    public string PrintTime { get; set; }
        //    public List<string> TestLines { get; set; }
        //    public List<DailySalesGroup> DailySalesGroups { get; set; }
        //    public string NumberOfPages { get; set; }
        //}

        //public class TestPrintModel
        //{
        //    public TestPrintModel()
        //    {
        //        CounterParties = new List<Counterparty>();
        //    }
        //    public bool RawPrinter { get; set; }
        //    public List<Counterparty> CounterParties { get; set; }
        //    public string PrintDate { get; set; }
        //    public string PrintTime { get; set; }
        //    public string PrintSelections1 { get; set; }
        //    public string PrintSelections2 { get; set; }
        //    public string PrintSelections3 { get; set; }
        //    public string PrintSelections4 { get; set; }
        //    public string PrintSelections5 { get; set; }
        //    public string PrintSelections6 { get; set; }
        //    public string PrintSelections7 { get; set; }
        //    public string PrintSelections8 { get; set; }
        //    public TestLevel0 Test0 { get; set; }
        //}

        //public class DisectionPrintModel
        //{
        //    public DisectionPrintModel()
        //    {
        //        DisectionDetails = new Disection();
        //    }
        //    public bool RawPrinter { get; set; }
        //    public Disection DisectionDetails { get; set; }
        //    public ProduceDisection ProduceDisectionDetails { get; set; }
        //    public string PrintDate { get; set; }
        //    public string PrintTime { get; set; }
        //    public string PrintSelections1 { get; set; }
        //    public string PrintSelections2 { get; set; }
        //    public string PrintSelections3 { get; set; }
        //    public string PrintSelections4 { get; set; }
        //    public string PrintSelections5 { get; set; }
        //    public string PrintSelections6 { get; set; }
        //    public string PrintSelections7 { get; set; }
        //    public string PrintSelections8 { get; set; }
        //    public string StartSalesDate { get; set; }
        //    public string EndSalesDate { get; set; }
        //    public TestLevel0 Test0 { get; set; }
        //}

        public ChequePrintModel GetChequePrintTestData()
        {
            var chequePrintModel = new ChequePrintModel();
            List<ChequePrintRemittanceItem> remittanceItems = new List<ChequePrintRemittanceItem>();
            remittanceItems.Add(new ChequePrintRemittanceItem()
            {
                Balance = 10,
                CreditAmount = 10,
                DebitAmount = 0,
                RemittanceDate = "19/07/2016",
                RemittanceType = "Invoice"
            });
            ChequePrintHeader chequeHeader = new ChequePrintHeader()
            {
                ChequeAddress = new Address() { AddressLine1 = "Address Line 1", AddressLine2 = "Address Line 2", AddressLine3 = "Address Line 3", CountyCity = "London", Postcode = "WC2H 0PD"},
                AccCode = "0123456789",
                ChequeAmount = 10,
                ChequeName = "Grow Vegetables and Code C Sharp Ltd",
                ChequeNumber = 1234,
                PrintVoidCheque = false,
                ChequePrintDate = "19/07/2016",
                ChequeWords = "**NIL***NIL***NIL**FIVE***TWO**NINE*"
            };
            var chequePage = new ChequePage();
            chequePage.ChequePrintHeader = chequeHeader;
            chequePage.ChequePrintRemittanceItems = remittanceItems;
            chequePage.PrintVoidCheque = false;
            chequePrintModel.ChequePages.Add(chequePage);
            return chequePrintModel;
        }

        public bool ChequePrint()
        {
            _logger.Debug("PrintCheques: Start");

            try
            {
                var chequeTemplate = GetChequeRawTemplate();
               
                FormatCompiler compiler = new FormatCompiler();
                var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);

                Generator generatorCheque = compiler.Compile(chequeTemplate);
                var chequesToPrint = GetChequePrintTestData();

                var jsonCheques = generatorCheque.Render(chequesToPrint);
                var documentModelCheques = JsonConvert.DeserializeObject<PrintDocumentModel>(jsonCheques);
                documentModelCheques.PageWidth = 400;
                bool successChequePrint = SendPrintRequest(printers, documentModelCheques, _serverCode != "L");
                return successChequePrint; // return true if Prints successful.
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
            finally
            {
                _logger.Debug("PrintCheque: End");
            }

        }

        public dynamic DisectionPrint(DisectionReportViewModel disectionReportView, bool returnActionPrint = true) // default is to print
        {
            var rawTemplate = GetDisectionTemplate();
            var subReportTemplate = GetDisectionSubReport1Template();

            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(rawTemplate);
            Generator subgenerator = compiler.Compile(subReportTemplate);
            DateTime today = DateTime.Today;
            Guid searchDept = String.IsNullOrEmpty(disectionReportView.DepartmentId)
                ? default(Guid)
                : Guid.Parse(disectionReportView.DepartmentId);

            //var produceGroupRange =
            //    _produceGroupService.ProduceGroupIncludeProduceByNameRange(disectionReportView.ProduceGroupStartName,
            //        disectionReportView.ProduceGroupEndName, searchDept);
            var produceGroupRange = GetProduceGroupRange(disectionReportView, searchDept);


            //var json = generator.Render(receiptToPrint);
            var disectionPrintModel = InitDisectionPrintModel(today);

            List<DisectionSub1> DisectionSub1 = new List<DisectionSub1>();
            List<DisectionSub2> disectionSub2List = new List<DisectionSub2>();
            List<DisectionSub2> disectionSub3List = new List<DisectionSub2>();

            //disectionPrintModel = DisectionPrintModelForProduct(disectionReportView, DisectionSub1, disectionSub2List, disectionPrintModel);
            foreach (var pg in produceGroupRange)
            {
                var dpm = DisectionPrintModelForProduct(disectionReportView, DisectionSub1, disectionSub2List, disectionPrintModel);
                disectionPrintModel.ProduceDisectionDetails.Produces.AddRange(dpm.ProduceDisectionDetails.Produces);
            }
            
            ExtractHeadingsParametersFromPrintModel(disectionPrintModel, produceGroupRange);

            var json = generator.Render(disectionPrintModel);

            var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);

            //documentModel.RawPrinter = false;
            documentModel.RawPrinter = true;
            documentModel.Condensed = false; // test condensed DC removed 19/01/2017
            var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
            printers.Remove(printers[1]); // want to test going to xps
            printers.Remove(printers[1]); //just do one for testing
            //printers.Remove(printers[0]); //just do one for testing
            if (returnActionPrint == false)
            {
                SendDocumentToFile(@"C:\Users\Public\TestFolder\TestDisection.txt", documentModel);
                var filename = @"C:\Users\Public\TestFolder\TestDisection.txt";
                return filename;
            }

           // return SendPrintRequest(printers, documentModel, _serverCode != "L");
            SendPrintRequest(printers, documentModel, _serverCode != "L");
            return true;
        }

        private static void ExtractHeadingsParametersFromPrintModel(DisectionPrintModel disectionPrintModel,
            List<ProduceGroup> produceGroupRange)
        {
            if (disectionPrintModel.ProduceDisectionDetails.Produces.Count > 0)
            {
                var firstConsignment =
                    disectionPrintModel.ProduceDisectionDetails.Produces[0].ConsignmentPrintModels.First()
                        .Consignment.ConsignmentReference;

                var lastConsignment =
                    disectionPrintModel.ProduceDisectionDetails.Produces.Last()
                        .ConsignmentPrintModels.Last()
                        .Consignment.ConsignmentReference;

                var firstSupplier =
                    disectionPrintModel.ProduceDisectionDetails.Produces[0].ConsignmentPrintModels.First()
                        .Consignment.SupplierDepartment.Supplier.SupplierCode;

                var lastSupplier = disectionPrintModel.ProduceDisectionDetails.Produces.Last()
                    .ConsignmentPrintModels.Last()
                    .Consignment.SupplierDepartment.Supplier.SupplierCode;

                disectionPrintModel.PrintSelections2 = produceGroupRange.First().ProduceGroupName;
                disectionPrintModel.PrintSelections3 = firstSupplier;
                disectionPrintModel.PrintSelections4 = firstConsignment;
                disectionPrintModel.PrintSelections5 = produceGroupRange.Last().ProduceGroupName;
                disectionPrintModel.PrintSelections6 = lastSupplier;
                disectionPrintModel.PrintSelections7 = lastConsignment;
            }
            else
            {
                disectionPrintModel.PrintSelections2 = "";
                disectionPrintModel.PrintSelections3 = "";
                disectionPrintModel.PrintSelections4 = "";
                disectionPrintModel.PrintSelections5 = "";
                disectionPrintModel.PrintSelections6 = "";
                disectionPrintModel.PrintSelections7 = "";
            }
        }

        private static DisectionPrintModel InitDisectionPrintModel(DateTime today)
        {
            var disectionPrintModel = new DisectionPrintModel
            {
                PrintDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                PrintTime = DateTime.Now.ToShortTimeString(),//ToString("HH:mm:ss"),
                PrintSelections1 = "Paul Fruits".ToUpper(),
                PrintSelections2 = "A",
                PrintSelections3 = "V",
                PrintSelections4 = "192",
                PrintSelections5 = "ZWAA",
                PrintSelections6 = "503",
                PrintSelections7 = "243977",
                StartSalesDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndSalesDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            disectionPrintModel.RawPrinter = true;
            return disectionPrintModel;
        }

        private List<ProduceGroup> GetProduceGroupRange(DisectionReportViewModel disectionReportView, Guid searchDept)
        {
            List<ProduceGroup> produceGroupRange = new List<ProduceGroup>();

            if (disectionReportView.ProduceGroupStartName == String.Empty &&
                disectionReportView.ProduceGroupEndName != String.Empty)
            {
                disectionReportView.ProduceGroupStartName = disectionReportView.ProduceGroupEndName;
            }
            if (disectionReportView.ProduceGroupEndName == String.Empty &&
                disectionReportView.ProduceGroupStartName != String.Empty)
            {
                disectionReportView.ProduceGroupEndName = disectionReportView.ProduceGroupStartName;
            }

            if (disectionReportView.ProduceGroupStartName == String.Empty &&
                disectionReportView.ProduceGroupEndName == String.Empty)
            {
                produceGroupRange = _produceGroupService.ProduceGroupIncludeProduceByDepartment(searchDept);
            }
            else
            {
                produceGroupRange =
                    _produceGroupService.ProduceGroupIncludeProduceByNameRange(disectionReportView.ProduceGroupStartName,
                        disectionReportView.ProduceGroupEndName, searchDept);
            }
            return produceGroupRange;
        }

        private DisectionPrintModel DisectionPrintModelForProduct(DisectionReportViewModel disectionReportView,
            List<DisectionSub1> DisectionSub1, List<DisectionSub2> disectionSub2List, DisectionPrintModel disectionPrintModel)
        {
            var mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsignmentItem, ConsignmentPrintModel>();
                cfg.CreateMap<Produce, ProduceItemExtended>();
                cfg.CreateMap<ProduceGroup, ExtendedProduceGroup>();
                cfg.CreateMap<ExtendedProduceGroup, ExtendedProduceGroup>();
            });
            var mapper = mapperconfig.CreateMapper();

            var optDepartmentFilter = disectionReportView.DepartmentId == null
                ? default(Guid)
                : Guid.Parse(disectionReportView.DepartmentId);

            ProduceGroup sampleData = new ProduceGroup();
            // if ProduceGroupStartId is null then want the first produceid sorted by name
            if (disectionReportView.ProduceGroupStartId == null)
            {
                var firstProductGroup = _produceGroupService.GetAllProduceGroups().OrderBy(a => a.ProduceGroupName).FirstOrDefault();
                disectionReportView.ProduceGroupStartId = firstProductGroup.ProduceGroupID.ToString();
            }

            var produceGroupId = new Guid(disectionReportView.ProduceGroupStartId);
            var testdata = _produceGroupService.ProduceGroupIncludeProduceByID(produceGroupId, optDepartmentFilter);
            var produceGroupItemsDenormalised = new ExtendedProduceGroup();
            mapper.Map(testdata, produceGroupItemsDenormalised);

            GetProduceRelatedItems(ref produceGroupItemsDenormalised, mapper, ref DisectionSub1, ref disectionSub2List, disectionReportView,
                optDepartmentFilter );
            produceGroupItemsDenormalised.Produces.RemoveAll(a => a.ConsignmentPrintModels.Count == 0);

            // 2nd Pass to segment by Supplier
            var segementedProduceItems = new List<ProduceItemExtended>();
            int testerrcount = 0;

            foreach (var produceItem in produceGroupItemsDenormalised.Produces.OrderBy(a => a.ProduceCode))
            {
                if (produceItem.ConsignmentItems != null)
                {
                    SegmentProduceItems(produceItem, mapper, ref segementedProduceItems);
                }
                else
                {
                    testerrcount++;
                }
            }

            disectionPrintModel.ProduceDisectionDetails = new ProduceDisection();
            disectionPrintModel.ProduceDisectionDetails.Produces.AddRange(segementedProduceItems);
            ReorderProduceForPrint(ref disectionPrintModel);

            int test1 = 0;
            var t1 = new DisectionTotals1();
            foreach (var p in disectionPrintModel.ProduceDisectionDetails.Produces)
            {
                foreach (var c in p.ConsignmentPrintModels)
                {
                    FormatDisectionTotals(c);
                }
            }
            return disectionPrintModel;
        }


        public dynamic DisectionPrintTest(DisectionReportViewModel disectionReportView, bool returnActionPrint = true) // default is to print
        {
            var rawTemplate = GetDisectionTemplate();
            var subReportTemplate = GetDisectionSubReport1Template();
           
            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(rawTemplate);
            Generator subgenerator = compiler.Compile(subReportTemplate);
            DateTime today = DateTime.Today;

            //var json = generator.Render(receiptToPrint);
            var disectionPrintModel = new DisectionPrintModel
            {
                PrintDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                PrintTime = today.ToString("HH:mm:ss"),
                PrintSelections1 = "Paul Fruits".ToUpper(),
                PrintSelections2 = "A",
                PrintSelections3 = "V",
                PrintSelections4 = "192",
                PrintSelections5 = "ZWAA",
                PrintSelections6 = "503",
                PrintSelections7 = "243977",
                StartSalesDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndSalesDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            List<DisectionSub1> DisectionSub1 = new List<DisectionSub1>();
            List<DisectionSub2> disectionSub2List = new List<DisectionSub2>();
            List<DisectionSub2> disectionSub3List = new List<DisectionSub2>();

            DisectionTestData(ref DisectionSub1, ref disectionSub2List, ref disectionSub3List); // test data for disection report

            DisectionSubPrintModel disectionSubPrintModel = new DisectionSubPrintModel();
            disectionSubPrintModel.DisectionSubList = DisectionSub1;
            var jsonsub1 = subgenerator.Render(disectionSubPrintModel);

            disectionPrintModel.RawPrinter = true;
            
            var mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsignmentItem, ConsignmentPrintModel>();
                cfg.CreateMap<Produce, ProduceItemExtended>();
                cfg.CreateMap<ProduceGroup, ExtendedProduceGroup>();
                cfg.CreateMap<ExtendedProduceGroup, ExtendedProduceGroup>();
            });
            var mapper = mapperconfig.CreateMapper();
            //var optDepartmentFilter = Guid.Parse("76000400-0000-0070-9204-000068336078"); // optional parameter to filter by Department
            
            var optDepartmentFilter = disectionReportView.DepartmentId == null
                ? default(Guid)
                : Guid.Parse(disectionReportView.DepartmentId);

            ProduceGroup sampleData = new ProduceGroup();
            //var testdata = _produceGroupService.ProduceGroupIncludeProduceByName("APPLES", optDepartmentFilter);
            var produceGroupId = new Guid(disectionReportView.ProduceGroupStartId);
            var testdata = _produceGroupService.ProduceGroupIncludeProduceByID(produceGroupId, optDepartmentFilter);
            //var testdata = _produceGroupService.ProduceGroupIncludeProduceByNameRange("APPLES","SALADS (D53)", optDepartmentFilter);
            //var testdata = _produceGroupService.GetAllProduceGroups();
            var produceGroupItemsDenormalised = new ExtendedProduceGroup();
            mapper.Map(testdata, produceGroupItemsDenormalised);

            
            GetProduceRelatedItems(ref produceGroupItemsDenormalised, mapper, ref DisectionSub1, ref  disectionSub2List, disectionReportView, optDepartmentFilter);
           
           // produceGroupItemsDenormalised.Produces.RemoveAll(a => a.ConsignmentItems.Count == 0); // for testing remove those with zero consignments
            produceGroupItemsDenormalised.Produces.RemoveAll(a => a.ConsignmentPrintModels.Count == 0);

            // 2nd Pass to segment by Supplier
            var segementedProduceItems = new List<ProduceItemExtended>();
            int testerrcount = 0;

            foreach (var produceItem in produceGroupItemsDenormalised.Produces.OrderBy(a => a.ProduceCode))
            {
                if (produceItem.ConsignmentItems != null)
                {
                    SegmentProduceItems(produceItem, mapper, ref segementedProduceItems);
                }
                else
                {
                    testerrcount++;
                }
            }
          
            disectionPrintModel.ProduceDisectionDetails = new ProduceDisection();
            disectionPrintModel.ProduceDisectionDetails.Produces.AddRange(segementedProduceItems);
            ReorderProduceForPrint(ref disectionPrintModel);
    
            int test1 = 0;
            var t1 = new DisectionTotals1();
            foreach (var p in disectionPrintModel.ProduceDisectionDetails.Produces)
            {
                foreach (var c in p.ConsignmentPrintModels)
                {
                    FormatDisectionTotals(c);
                }
            }
    
            var json = generator.Render(disectionPrintModel);
           
            var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);

            //documentModel.RawPrinter = false;
            documentModel.RawPrinter = true;
            documentModel.Condensed = false; // test condensed DC removed 19/01/2017
            var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
            printers.Remove(printers[1]); // want to test going to xps
            printers.Remove(printers[1]); //just do one for testing
            //printers.Remove(printers[0]); //just do one for testing
            if (returnActionPrint == false)
            {
                SendDocumentToFile(@"C:\Users\Public\TestFolder\TestDisection.txt", documentModel);
                var filename = @"C:\Users\Public\TestFolder\TestDisection.txt";
                return filename;
            }

            return SendPrintRequest(printers, documentModel, _serverCode != "L");
            
        }

        private void SendDocumentToFile(string filename, PrintDocumentModel document)
        {
            using (System.IO.StreamWriter testfile = new StreamWriter(@"C:\Users\Public\TestFolder\TestDisection.txt"))
            {
                foreach (var section in document.Sections)
                {
                    foreach (var line in section.Lines)
                    {
                        var lineout = FormatLineForFile(line);
                        testfile.WriteLine(lineout);
                    }
                }
            }
        }

        private string FormatLineForFile(LineModel line)
        {
            string outputstring = "";
            foreach (var lineItem in line.LineItems)
            {
                if (lineItem.Image == null)
                {
                    var formatString = formatAlignRawLineItem(lineItem);
                    outputstring += formatString;
                }
            }
            //outputstring += Environment.NewLine;
            return outputstring;
        }

        private static string formatAlignRawLineItem(LineItemModel lineItem)
        {
            var formatArgs = "{0}";
            string lineItemAdjust = lineItem.Text;

            if (lineItem.Width.HasValue)
            {
                if (lineItem.Align == StringAlignment.Center)
                {
                    if (lineItem.Text.Length < lineItem.Width)
                    {
                        lineItemAdjust = lineItem.Text.CenterString(lineItem.Width.Value);
                    }
                }
                if (lineItem.Align == StringAlignment.Near)
                {
                    formatArgs = "{0,-" + lineItem.Width.Value.ToString() + "}";
                }
                if (lineItem.Align == StringAlignment.Far)
                {
                    formatArgs = "{0," + lineItem.Width.Value.ToString() + "}";
                }
            }

            var formatString = string.Format(formatArgs, lineItemAdjust);
            return formatString;
        }

        private static void FormatDisectionTotals(ConsignmentPrintModel c)
        {
            var tqty = c.SubLines.Sum(a => a.Col2 == null ? 0 : decimal.Parse(a.Col2));
            var tvalue = c.SubLines.Sum(a => a.Col3 == null ? 0 : decimal.Parse(a.Col3));
            var tqtyrtn = c.SubLines.Sum(a => a.Col4 == null ? 0 : decimal.Parse(a.Col4));
            var tqtyvalue = c.SubLines.Sum(a => a.Col5 == null ? 0 : decimal.Parse(a.Col5));
            var tqtyprv = c.SubLines.Sum(a => a.Col6 == null ? 0 : decimal.Parse(a.Col6));
            var tvalueprv = c.SubLines.Sum(a => a.Col7 == null ? 0 : decimal.Parse(a.Col7));
            var qtyrcvd = c.ConsignmentItemArrivals.Sum(a => a.QuantityReceived);
            var qtybfwd = qtyrcvd - (int) tqtyprv;
            var qtybalance = qtybfwd - tqty;
            var dt1 = new DisectionTotals1();
            dt1.QtyTotal = tqty.ToString();
            dt1.ValueTotal = tvalue.ToString();
            dt1.QtyTotalPrevReturns = tqtyrtn.ToString();
            dt1.ValueTotalPrevReturns = tqtyvalue.ToString();
            dt1.QtyTotalPrevSale = tqtyprv.ToString();
            dt1.ValueTotalPrevSale = tvalueprv.ToString();
            dt1.RecdQty = String.Format("{0:0.##}", qtyrcvd);
            dt1.Balance = String.Format("{0:0.##}", qtybalance);
            c.DisectionTotals1 = dt1;
            var avgtoday = tqty == 0 ? 0 : tvalue/tqty;
            var avgreturn = tqtyrtn == 0 ? 0 : tqtyvalue/tqtyrtn;
            var avprev = tqtyprv == 0 ? 0 : tvalueprv/tqtyprv;
            var dt2 = new DisectionTotals2();
            dt2.ValueAve = avgtoday == 0 ? "******" : String.Format("{0:0.00}", avgtoday);
            dt2.ValueAvePrevSale = avprev == 0 ? "******" : String.Format("{0:0.00}", avprev);
            dt2.ValueAvePrevReturns = avgreturn == 0 ? "******" : String.Format("{0:0.00}", avgreturn);
            dt2.BroughtForward = String.Format("{0:0.##}", qtybfwd);
            c.DisectionTotals2 = dt2;
            var dt3 = new DisectionTotals3();
            dt3.SoldToday = dt1.QtyTotal;
            c.DisectionTotals3 = dt3;
            var dt4 = new DisectionTotals4();
            dt4.EstimatedPurchaseCost = String.Format("{0:0.00}", c.EstimatedPurchaseCost);
            c.DisectionTotals4 = dt4;
        }

        private static void DisectionTestData(ref List<DisectionSub1> DisectionSub1, ref List<DisectionSub2> disectionSub2List, ref  List<DisectionSub2> disectionSub3List)
        {
            DisectionSub1.Add(new DisectionSub1()
            {
                Price = "12.30",
                QtySoldToday = "12",
                ValueSoldToday = "145.60",
                PreviousReturnsQty = "1",
                PreviousReturnsValue = "12.30",
                PreviousSaleQty = "10",
                PreviousSaleValue = "123.00"
            });
            DisectionSub1.Add(new DisectionSub1()
            {
                Price = "11.30",
                QtySoldToday = "12",
                ValueSoldToday = "145.60",
                PreviousReturnsQty = "1",
                PreviousReturnsValue = "12.30",
                PreviousSaleQty = "10",
                PreviousSaleValue = "123.00"
            });
            DisectionSub1.Add(new DisectionSub1()
            {
                Price = "11.30",
                QtySoldToday = "12",
                ValueSoldToday = "145.60",
                PreviousReturnsQty = "1",
                PreviousReturnsValue = "12.30",
                PreviousSaleQty = "10",
                PreviousSaleValue = "123.00"
            });
            DisectionSub1.Add(new DisectionSub1()
            {
                Price = "11.30",
                QtySoldToday = "12",
                ValueSoldToday = "145.60",
                PreviousReturnsQty = "1",
                PreviousReturnsValue = "12.30",
                PreviousSaleQty = "10",
                PreviousSaleValue = "123.00"
            });
            DisectionSub1.Add(new DisectionSub1()
            {
                Price = "11.30",
                QtySoldToday = "12",
                ValueSoldToday = "145.60",
                PreviousReturnsQty = "1",
                PreviousReturnsValue = "12.30",
                PreviousSaleQty = "10",
                PreviousSaleValue = "123.00"
            });
            //Sub2 sample data
            disectionSub2List.Add(new DisectionSub2()
            {
                SalesCode = "TOMF",
                SalesQty = "3",
                SalesPrice = "12.40",
                SalesTicket = "439714",
                SalesTotal = "37.20"
            });
            disectionSub2List.Add(new DisectionSub2()
            {
                SalesCode = "PRES",
                SalesQty = "10",
                SalesPrice = "12.40",
                SalesTicket = "439721",
                SalesTotal = "119.00"
            });
            disectionSub2List.Add(new DisectionSub2()
            {
                SalesCode = "C",
                SalesQty = "12",
                SalesPrice = "12.40",
                SalesTicket = "431839",
                SalesTotal = "120.00"
            });
            disectionSub2List.Add(new DisectionSub2()
            {
                SalesCode = "SUN2",
                SalesQty = "30",
                SalesPrice = "12.40",
                SalesTicket = "431839",
                SalesTotal = "120.00"
            });
            disectionSub2List.Add(new DisectionSub2()
            {
                SalesCode = "PH",
                SalesQty = "50",
                SalesPrice = "15.60",
                SalesTicket = "439674",
                SalesTotal = "120.00"
            });
            //Sub3 sample data
            disectionSub3List.Add(new DisectionSub2()
            {
                SalesCode = "TOMF2",
                SalesQty = "0",
                SalesPrice = "0.00",
                SalesTicket = "439714",
                SalesTotal = "-1.50"
            });
            disectionSub3List.Add(new DisectionSub2()
            {
                SalesCode = "PRES",
                SalesQty = "10",
                SalesPrice = "12.40",
                SalesTicket = "439724",
                SalesTotal = "119.00"
            });
            disectionSub3List.Add(new DisectionSub2()
            {
                SalesCode = "C",
                SalesQty = "12",
                SalesPrice = "12.40",
                SalesTicket = "431839",
                SalesTotal = "120.00"
            });
            disectionSub3List.Add(new DisectionSub2()
            {
                SalesCode = "SUN2",
                SalesQty = "30",
                SalesPrice = "12.40",
                SalesTicket = "431839",
                SalesTotal = "120.00"
            });
            disectionSub3List.Add(new DisectionSub2()
            {
                SalesCode = "PH",
                SalesQty = "50",
                SalesPrice = "15.60",
                SalesTicket = "439674",
                SalesTotal = "120.00"
            });
            disectionSub3List.Add(new DisectionSub2()
            {
                SalesCode = "LAST",
                SalesQty = "50",
                SalesPrice = "15.60",
                SalesTicket = "439674",
                SalesTotal = "120.00"
            });
            disectionSub2List.AddRange(disectionSub3List);
        }

        private static void ReorderProduceForPrint(ref DisectionPrintModel disectionPrintModel)
        {
            foreach (var produce in disectionPrintModel.ProduceDisectionDetails.Produces)
            {
                var reorderedConsignmentItems = produce.ConsignmentPrintModels.OrderBy(a => a.Consignment.ConsignmentReference);
                produce.ConsignmentPrintModels = reorderedConsignmentItems.ToList();
            }
        }

        private void ProduceItemCountersCreate()
        {
            if (!PerformanceCounterCategory.Exists("DisectionCounter"))
            {
                CounterCreationDataCollection counters = new CounterCreationDataCollection();

                // 1. counter for counting totals: PerformanceCounterType.NumberOfItems32
                CounterCreationData totalOps = new CounterCreationData();
                totalOps.CounterName = "# operations executed";
                totalOps.CounterHelp = "Total number of operations executed";
                totalOps.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(totalOps);

                // 2. counter for counting operations per second:
                //        PerformanceCounterType.RateOfCountsPerSecond32
                CounterCreationData opsPerSecond = new CounterCreationData();
                opsPerSecond.CounterName = "# operations / sec";
                opsPerSecond.CounterHelp = "Number of operations executed per second";
                opsPerSecond.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                counters.Add(opsPerSecond);

                // 3. counter for counting average time per operation:
                //                 PerformanceCounterType.AverageTimer32
                CounterCreationData avgDuration = new CounterCreationData();
                avgDuration.CounterName = "average time per operation";
                avgDuration.CounterHelp = "Average duration per operation execution";
                avgDuration.CounterType = PerformanceCounterType.AverageTimer32;
                counters.Add(avgDuration);

                // 4. base counter for counting average time
                //         per operation: PerformanceCounterType.AverageBase
                CounterCreationData avgDurationBase = new CounterCreationData();
                avgDurationBase.CounterName = "average time per operation base";
                avgDurationBase.CounterHelp = "Average duration per operation execution base";
                avgDurationBase.CounterType = PerformanceCounterType.AverageBase;
                counters.Add(avgDurationBase);


                // create new category with the counters above
                PerformanceCounterCategory.Create("DisectionCounter",
                        "Test Get Produce Related Items", counters);
            }
        }

        private void GetProduceRelatedItems(ref ExtendedProduceGroup produceGroupItemsDenormalised, IMapper mapper, ref List<DisectionSub1> disectionSub1, ref List<DisectionSub2> disectionSub2, DisectionReportViewModel disectionReportViewModel, Guid optDepartment = default(Guid))
        {
            // create Performance Counters if not exist.
           // ProduceItemCountersCreate();
            List<DateTime> timestorun = new List<DateTime>();
           
            //foreach (var produceItem in produceGroupItemsDenormalised.Produces.OrderBy(a => a.ProduceCode).Where(s => s.ProduceCode == "APBR" || s.ProduceCode == "APGS"))
            foreach (var produceItem in produceGroupItemsDenormalised.Produces.OrderBy(a => a.ProduceCode))
            {
               // var consignmentItems = _consignmentItemService.ConsignmentItemsByProduceForDateRange(produceItem.ProduceID); add order by 16/02
                var consignmentItems = _consignmentItemService.ConsignmentItemsByProduceForDateRange(produceItem.ProduceID, optDepartment);
                timestorun.Add(DateTime.Now);
                var testcount = consignmentItems.Count;

                produceItem.ConsignmentItems = consignmentItems.OrderBy(a => a.ConsignmentID).ToList();
                //mapper.Map(consignmentItems, produceItem.ConsignmentPrintModels);  // map consignment to produceitems.consignments

                Consignment previousconsignment = null;
                var produceItemConsignments = produceItem.ConsignmentItems.ToList(); // DC 28/02/2017 testing performance


                //foreach (var ci in produceItem.ConsignmentItems)
                foreach (var ci in produceItem.ConsignmentItems)
                {
                    if (previousconsignment == null || ci.ConsignmentID != previousconsignment.ConsignmentID)
                    {
                        var consignment =
                            _consignmentService.ConsignmentAndSupplierDepartmentById(ci.ConsignmentID.Value);
                        produceItem.SupplierName = consignment.SupplierDepartment.Supplier.SupplierCompanyName + "-" +
                                                   consignment.SupplierDepartment.SupplierDepartmentName;
                        produceItem.SupplierCode = consignment.SupplierDepartment.Supplier.SupplierCode;
                        ci.Consignment = consignment;
                        previousconsignment = consignment;
                    }
                    else
                    {
                        ci.Consignment = previousconsignment;
                    }
                }
                
                if (produceItem.ProduceCode == "APBR")
                {
                    int n = 0;
                }

                


                //flatten
                if (consignmentItems.Any() )
                //if (consignmentItems.Any() && produceItem.ProduceCode == "APBR")
                {
                    var filteredConsignments =
                     consignmentItems.Where(a => a.Consignment.DivisionID == Guid.Parse(disectionReportViewModel.SelectedDivisionId));
                    //mapper.Map(consignmentItems, produceItem.ConsignmentPrintModels);  // map consignment to produceitems.consignments
                    mapper.Map(filteredConsignments, produceItem.ConsignmentPrintModels);  // map consignment to produceitems.consignments

                    foreach (var consignmentItem in produceItem.ConsignmentPrintModels)
                    {
                        //var consignment = _consignmentService.ConsignmentAndSupplierDepartmentById(consignmentItem.ConsignmentID.Value);
                        var lastconsignmentid = consignmentItem.ConsignmentID;
                        //var testq = _consignmentItemService.ConsignmentItemsReceivedQuantity(consignmentItem.ConsignmentItemID);
                       
                        try
                        {
                          //  var consignment = _consignmentService.ConsignmentById(consignmentItem.ConsignmentID.Value);
                           // var consignment = _consignmentService.ConsignmentAndSupplierDepartmentById(consignmentItem.ConsignmentID.Value);
                            //produceItem.SupplierName = consignment.SupplierDepartment.Supplier.SupplierCompanyName + "-" +
                            //                      consignment.SupplierDepartment.SupplierDepartmentName;
                            //produceItem.SupplierCode = consignment.SupplierDepartment.Supplier.SupplierCode;
                          //  consignmentItem.Consignment = consignment;



                        var consignmentItemArrivalsUnsorted =
                              _consignmentItemArrivalService.ConsignmentItemArrivalsOnlyByConsignmentItemIDSQL(
                                consignmentItem.ConsignmentItemID);

                        var consignmentItemArrivals =
                                consignmentItemArrivalsUnsorted.OrderBy(a => a.ConsignmentItemArrivalDate);
                            //_consignmentItemArrivalService.ConsignmentItemArrivalsOnlyByConsignmentItemID(
                            //    consignmentItem.ConsignmentItemID).OrderBy(a => a.ConsignmentArrivalDate).ToList();
                        
                            consignmentItem.ConsignmentItemArrivals = new List<ConsignmentItemArrival>();

                        if (consignmentItemArrivals.Any()) 
                        {
                            ConsignmentItemArrival firstArrival = new ConsignmentItemArrival();
                            firstArrival = consignmentItemArrivals.First();
                            consignmentItem.ReceivedDate = firstArrival.ConsignmentItemArrivalDate.ToString("dd/MM/yyyy",
                                CultureInfo.InvariantCulture);
                            consignmentItem.ConsignmentItemArrivals.AddRange(consignmentItemArrivals);
                        }
                        
                        // Disection Report Sub Lines
                        List<PrimeActs.PrintFormatStructure.DisectionSubLine> disectionSubLines = new List<PrimeActs.PrintFormatStructure.DisectionSubLine>();
                        // TODO: Build Disection Sub Lines use live data, write queries
                            var soldToday = GetConsignmentsSoldToday(consignmentItem.ConsignmentItemID);
                            var returns = GetConsignmentReturns(consignmentItem.ConsignmentItemID);
                            var soldPrevious = GetConsignmentsSoldPriorToday(consignmentItem.ConsignmentItemID);
                            var ticketsToday = GetConsignmentTicketsToday(consignmentItem.ConsignmentItemID);
                            

                            disectionSub1.Clear();
                            disectionSub2.Clear();
                            var max1 = Math.Max(soldToday.Count, returns.Count);
                            var max2 = Math.Max(max1, soldPrevious.Count);

                            for (var i = 0; i < max2 ; i++)
                            {
                                disectionSub1.Add(new DisectionSub1 {
                                    //Price = "0.00",
                                    QtySoldToday = "0",
                                    ValueSoldToday = "0.00",
                                    PreviousSaleQty = "0",
                                    PreviousSaleValue = "0.00",
                                    PreviousReturnsQty = "0.00",
                                    PreviousReturnsValue = "0.00",
                                    Price = null,
                                    //QtySoldToday = null,
                                    //ValueSoldToday = null,
                                    //PreviousSaleQty = null,
                                    //PreviousSaleValue = null,
                                    //PreviousReturnsQty = null,
                                    //PreviousReturnsValue = null,
                                    PriceNum = 0,
                                    QtySoldTodayNum = 0,
                                    ValueSoldTodayNum = 0,
                                    PreviousSaleQtyNum = 0,
                                    PreviousSaleValueNum = 0,
                                    PreviousReturnsQtyNum = 0,
                                    PreviousReturnsValueNum = 0
                                });
                            }

                            //int nextbucket = 0;
                            foreach (var s in soldToday)
                            {
                                // is there a column for price
                                int f = disectionSub1.FindIndex(a => a.PriceNum == s.UnitPrice);
                                if (f == -1)
                                {
                                    f = disectionSub1.FindIndex(a => a.Price == null);
                                    if (f == -1) f = disectionSub1.Count - 1;
                                }
                               
                                disectionSub1[f].PriceNum = s.UnitPrice;
                                disectionSub1[f].QtySoldTodayNum = disectionSub1[f].QtySoldTodayNum + (int)s.TotalQuantity;
                                disectionSub1[f].ValueSoldTodayNum = disectionSub1[f].ValueSoldTodayNum + s.TotalPrice;
                                disectionSub1[f].Price = String.Format("{0:0.00}", disectionSub1[f].PriceNum);
                                disectionSub1[f].QtySoldToday = String.Format("{0:0.##}", disectionSub1[f].QtySoldTodayNum);
                                disectionSub1[f].ValueSoldToday = String.Format("{0:0.00}", disectionSub1[f].ValueSoldTodayNum);
                            }

                            foreach (var p in soldPrevious)
                            {
                                // is there a column for price
                                int f = disectionSub1.FindIndex(a => a.PriceNum == p.UnitPrice);
                                if (f == -1) {
                                    f = disectionSub1.FindIndex(a => a.Price == null);
                                    if (f == -1) f = disectionSub1.Count - 1;
                                }
                                if (f < disectionSub1.Count)
                                {
                                    disectionSub1[f].PriceNum = p.UnitPrice;
                                    disectionSub1[f].Price = String.Format("{0:0.00}", p.UnitPrice);
                                    disectionSub1[f].PreviousSaleQtyNum = disectionSub1[f].PreviousSaleQtyNum + (int) p.TotalQuantity;
                                    disectionSub1[f].PreviousSaleValueNum = disectionSub1[f].PreviousSaleValueNum + p.TotalPrice;
                                    disectionSub1[f].PreviousSaleQty = String.Format("{0:0.##}", disectionSub1[f].PreviousSaleQtyNum);
                                    disectionSub1[f].PreviousSaleValue = String.Format("{0:0.00}", disectionSub1[f].PreviousSaleValueNum);
                                }
                                else
                                {
                                    var test = f;
                                }
                            }

                            foreach (var r in returns)
                            {
                                // is there a column for price
                                int f = disectionSub1.FindIndex(a => a.PriceNum == r.ReturnUnitPrice);
                                if (f == -1)
                                {
                                    f = disectionSub1.FindIndex(a => a.Price == null);
                                    if (f == -1) f = disectionSub1.Count - 1;
                                }

                                disectionSub1[f].PriceNum = r.ReturnUnitPrice;
                                disectionSub1[f].Price = String.Format("{0:0.00}", r.ReturnUnitPrice);
                                disectionSub1[f].PreviousReturnsQty = String.Format("{0:0.##}", r.TotalQuantity);
                                disectionSub1[f].PreviousReturnsValue = String.Format("{0:0.00}", r.ReturnUnitPrice);
                                disectionSub1[f].PreviousReturnsQtyNum = r.TotalQuantity;
                                disectionSub1[f].PreviousReturnsValueNum = (int)r.ReturnUnitPrice;
                            }

                            int c = 0;
                           
                            disectionSub1.RemoveAll(a => a.Price == null);
                            var disectionSub1Sorted = disectionSub1.OrderByDescending(a => a.PriceNum).ToList();

                            foreach (var t in ticketsToday)
                            {
                                disectionSub2.Add(new DisectionSub2
                                {
                                    SalesCode = t.CustomerCode,
                                    SalesTotal = String.Format("{0:0.00}",t.TicketItemTotalPrice),
                                    SalesQty = String.Format("{0:0.##}",t.TicketItemQuantity),
                                    SalesPrice = String.Format("{0:0.00}",t.UnitPrice),
                                    SalesTicket = t.ShowTicketReference.Substring(2)
                                });
                            }

                            BuildDisectionSubLines(ref disectionSubLines, ref disectionSub1Sorted, ref disectionSub2);

                            consignmentItem.SubLines = disectionSubLines;
                            // Get Associated Ticket Items
                            //var ticketItems =
                            //    _ticketItemService.GetAllTicketItemsByConsignmentItemID(
                            //        consignmentItem.ConsignmentItemID);
                            var ticketItems =
                                _ticketItemService.GetAllTicketItemsByConsignmentItemIDSQL(
                                    consignmentItem.ConsignmentItemID);
                            if (ticketItems.Any()) { consignmentItem.TicketItems.AddRange(ticketItems); }

                           
                        }
                        catch (Exception ex)
                        {
                            var test = lastconsignmentid;
                        }
                    }
                }
            }
        }

        private List<vwConsignmentTicketsByDate> GetConsignmentsSoldToday(Guid id)
        {
            var today = DateTime.Now;
            var soldToday = _ticketItemService.GetConsignmentsSoldToday(id, today);
            return soldToday;
                    }

        private List<vwConsignmentTicketsByDate> GetConsignmentsSoldPriorToday(Guid id)
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);
            var soldPriorToday = _ticketItemService.GetConsignmentsSoldPriorToday(id, today);
            return soldPriorToday;
                }

        private List<vwConsignmentReturns> GetConsignmentReturns(Guid id)
        {
            var returns = _ticketItemService.GetConsignmentReturns(id);
            return returns;
            }

        private List<vwConsignmentTicketsSingleByDate> GetConsignmentTicketsToday(Guid id)
        {
            var today = DateTime.Now;
            var returns = _ticketItemService.GetConsignmentTicketsSingleByDate(id, today);
            return returns;
        }

        private void BuildDisectionSubLines(ref List<PrimeActs.PrintFormatStructure.DisectionSubLine> disectionSubLines, ref List<DisectionSub1> disectionSub1, ref List<DisectionSub2> disectionSub2)
        {
            decimal dis2CountDecimal = disectionSub2.Count;
            dis2CountDecimal = dis2CountDecimal / 2;
            int dis2Count = Convert.ToInt32(Math.Ceiling(dis2CountDecimal));
            var sublineCount = disectionSub1.Count > dis2Count ? disectionSub1.Count : dis2Count;

            PrimeActs.PrintFormatStructure.DisectionSubLine[] subLineArray = InitializeArray<PrimeActs.PrintFormatStructure.DisectionSubLine>(sublineCount);

            int index_array = 0;
            //try
            //{
                foreach (var sub1 in disectionSub1)
                {
                    subLineArray[index_array].Col1 = sub1.Price;
                    subLineArray[index_array].Col2 = sub1.QtySoldToday;
                    subLineArray[index_array].Col3 = sub1.ValueSoldToday;
                    subLineArray[index_array].Col4 = sub1.PreviousReturnsQty;
                    subLineArray[index_array].Col5 = sub1.PreviousReturnsValue;
                    subLineArray[index_array].Col6 = sub1.PreviousSaleQty;
                    subLineArray[index_array].Col7 = sub1.PreviousSaleValue;
                    index_array++;
                }

                index_array = 0;
                var countSub2Added = 0;
                var oddDone = false;
                foreach (var sub2 in disectionSub2)
                {
                    if (countSub2Added % 2 == 0)
                    {
                        subLineArray[index_array].Col8 = sub2.SalesCode;
                        subLineArray[index_array].Col9 = sub2.SalesTicket;
                        subLineArray[index_array].Col10 = sub2.SalesQty;
                        subLineArray[index_array].Col11 = sub2.SalesPrice;
                        subLineArray[index_array].Col12 = sub2.SalesTotal;
                    }
                    else
                    {
                        subLineArray[index_array].Col13 = sub2.SalesCode;
                        subLineArray[index_array].Col14 = sub2.SalesTicket;
                        subLineArray[index_array].Col15 = sub2.SalesQty;
                        subLineArray[index_array].Col16 = sub2.SalesPrice;
                        subLineArray[index_array].Col17 = sub2.SalesTotal;
                        oddDone = true;
                    }
                    if (oddDone)
                    {
                        index_array++;
                        oddDone = false;
                    }
                    countSub2Added++;
                }
           // }
           // catch (Exception ex) { }
            disectionSubLines = subLineArray.ToList();
        }

        T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        private static void SegmentProduceItems(ProduceItemExtended produceItem, IMapper mapper, ref List<ProduceItemExtended> segementedProduceItems)
        {
            var lastSupplierCode = "";
           
            //foreach (var consignmentItem in
            //    produceItem.ConsignmentPrintModels.Where(
            //        s => s.Consignment.SupplierDepartment.Supplier.SupplierCode == null))
                foreach (var consignmentItem in produceItem.ConsignmentItems) {
                    if (consignmentItem.Consignment == null)
                    {
                        int notnull = 0;
                    }
            }

            // Trap for those that flagged null DC 16022017

            //foreach (var consignmentItem in produceItem.ConsignmentItems.OrderBy(s => s.Consignment.SupplierDepartment.Supplier.SupplierCode))   
            try
            {
                foreach (
                    var consignmentItem in
                     //   produceItem.ConsignmentPrintModels.OrderBy(
                        produceItem.ConsignmentItems.OrderBy(
                            s => s.Consignment.SupplierDepartment.Supplier.SupplierCode))
                {
                    var proditem = produceItem.ProduceCode;

                    var SupplierName = consignmentItem.Consignment.SupplierDepartment.Supplier.SupplierCompanyName;
                    var SupplierCode = consignmentItem.Consignment.SupplierDepartment.Supplier.SupplierCode;
                    if (consignmentItem.Consignment.SupplierDepartment.Supplier.SupplierCode != lastSupplierCode)
                    {
                        ProduceItemExtended pex = new ProduceItemExtended();
                        mapper.Map(produceItem, pex);
                        
                        var mappedConsignmentItems =
                            produceItem.ConsignmentPrintModels.Where(
                        //    produceItem.ConsignmentItems.Where(
                                a => a.Consignment.SupplierDepartment.Supplier.SupplierCode == SupplierCode);
                        //pex.ConsignmentItems.RemoveAll(a => a.Consignment.SupplierDepartment.Supplier.SupplierCode != SupplierCode);
                        pex.SupplierCode = SupplierCode;
                        pex.SupplierName = SupplierName;
                        pex.ConsignmentPrintModels.AddRange(mappedConsignmentItems);
                        
                        segementedProduceItems.Add(pex);
                        lastSupplierCode = SupplierCode;
                    }
                }
            }
            catch (Exception ex) { }
        }

        public string CSVGenericTest()
        {
            // Get purchase Invoices To Transform
            var purchaseInvoices = _purchaseInvoiceService.GetPurchaseInvoicesByStatus(2);
            Dictionary<Guid, SupplierBankAccount> sbaDict = new Dictionary<Guid, SupplierBankAccount>();

            // Get Bank Account Details for Each Invoice, through testing if null then default to hard coded
            foreach (var p in purchaseInvoices)
            {
                // Get Supplier Department 
                var supplierDepartmentGraph = _supplierDepartmentService.SupplierDepartmentById(p.SupplierDepartmentID);

                // Get Bank Accounts for Supplier Department
                if (supplierDepartmentGraph.SupplierBankAccounts == null ||
                    supplierDepartmentGraph.SupplierBankAccounts.Count() == 0)
                {
                    var bankAccounts = _bankAccountService.BankAccountBySupplierId(p.SupplierDepartment.SupplierID);
                    SupplierBankAccount sba = new SupplierBankAccount();
                    sba.BankAccount = bankAccounts.FirstOrDefault();

                    if (!sbaDict.ContainsKey(p.SupplierDepartment.SupplierDepartmentID))
                    {
                        sbaDict.Add(p.SupplierDepartment.SupplierDepartmentID, sba);
                    }

                    List<SupplierBankAccount> sbaList = new List<SupplierBankAccount>();
                    sbaList.Add(sba);
                    p.SupplierDepartment.SupplierBankAccounts = sbaList; // should be only one
                }
                else
                {
                    if (!sbaDict.ContainsKey(p.SupplierDepartment.SupplierDepartmentID))
                    {
                        var sba = supplierDepartmentGraph.SupplierBankAccounts.FirstOrDefault();
                        sbaDict.Add(p.SupplierDepartment.SupplierDepartmentID, sba);
                    }
                }

                p.SupplierDepartment = supplierDepartmentGraph;

            }

            // Group and Sum Invoices for the same Supplier Departments
            var groupedInvoices = purchaseInvoices.GroupBy(a => a.SupplierDepartmentID)
                .Select(lg => new
                {
                    Dept = lg.Key,
                    Total = lg.Sum(s => s.Total),
                    Data = lg
                });

            var companyBankAccount = "32149689"; // Will need Company Bank Account
            var companyBankSortCode = "604005"; //
            var paymentDate = new DateTime();
            paymentDate = DateTime.Today.AddDays(7);

            CSVTemplate csvTemplate = new CSVTemplate();

            //apply data to Natwest Class used by Moustache to substitute, empty values will just be comma separated

            foreach (var gi in groupedInvoices)
            {
                SupplierBankAccount supplierBankAccount;
                if (sbaDict.TryGetValue(gi.Dept, out supplierBankAccount))
                {
                    var sortcode = supplierBankAccount.BankAccount.BankCode.ToString();
                    NatWestStdDomesticPaymentCSV NatWestCsv = new NatWestStdDomesticPaymentCSV();
                    NatWestCsv.T001 = 1;
                    NatWestCsv.T010 = companyBankAccount;
                    NatWestCsv.T014 = gi.Total;
                    NatWestCsv.T016 = paymentDate.ToString("ddMMyyyy");
                    NatWestCsv.T022 = sortcode;
                    NatWestCsv.T028 = supplierBankAccount.BankAccount.AccountNumber.ToString();
                    NatWestCsv.T030 = supplierBankAccount.BankAccount.AccountName.Replace(",", " ");
                    NatWestCsv.T034 = "Test Invoice Generation";
                    csvTemplate.CSVList.Add(NatWestCsv);
                }
            }

            // Transform using Moustache
            var moustacheTemplate = GetTemplate("NatWestCSVFormat");

            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(moustacheTemplate);
            var json = generator.Render(csvTemplate);

            //string newline = Environment.NewLine;
            string csvString = json.Replace("CR/LF", Environment.NewLine);

            return csvString;
        }


       

        public bool RawPrintTest()
        {
            var rawTemplate = GetTestRawTemplate();

            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(rawTemplate);
            DateTime today = DateTime.Today;
            
            //var json = generator.Render(receiptToPrint);
            var testPrintModel = new TestPrintModel
            {
                CounterParties = new List<Counterparty>{
                    new Counterparty{
                    Date = today,
                    Name = "Joe Bloggs",
                    Amount = "200",
                    HundredThousands = "---",
                    TenThousands = "---",
                    Thousands = "---",
                    Hundreds = "TWO",
                    Tens = "---",
                    Units = "---",
                },
                new Counterparty{
                    Date = today,
                    Name = "Bob Berry",
                    Amount = "350",
                    HundredThousands = "---",
                    TenThousands = "---",
                    Thousands = "---",
                    Hundreds = "THREE",
                    Tens = "FIVE",
                    Units = "---",
                }
            },
            PrintDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
            PrintTime = today.ToString("HH:mm:ss"),
            PrintSelections1 = "Paul Fruits".ToUpper(),
            PrintSelections2 = "A",
            PrintSelections3 = "V",
            PrintSelections4 = "192",
            PrintSelections5 = "ZWAA",
            PrintSelections6 = "503",
            PrintSelections7 = "243977"
        };
            testPrintModel.RawPrinter = true;
            var t0 = new TestLevel0();
            var TestLevel1 = new TestLevel1();
            var t2 = new TestLevel2();
            var t2a = new TestLevel2();

            t2.Level2 = "1";
            t2a.Level2 = "2";
            TestLevel1.Level1 = "This is level 1";

            t0.Level1List.Add(TestLevel1);
            testPrintModel.Test0 = t0;

            var json = generator.Render(testPrintModel);
            var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);
            documentModel.RawPrinter = true;
            var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
            return SendPrintRequest(printers, documentModel, _serverCode != "L");
        }

        //public bool RawPrintCondensedTest()
        public bool PrintDailySalesReport(DateTime dailySalesDate)
        {
            var rawTemplate = GetTestCondensedRawTemplate();

            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(rawTemplate);
            DateTime today = DateTime.Today;
            List<string> range_list = Enumerable.Range(1, 130).Select(n => n.ToString()).ToList();
            var dailySalesTickets = GetDailySalesGroups(dailySalesDate);

            //var json = generator.Render(receiptToPrint);
            var testPrintModel = new TestRawPrintCondensed()
            {
                TestLines = new List<string>(),
                PrintDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                PrintTime = DateTime.Now.ToLongTimeString()
            };
            
            testPrintModel.TestLines.AddRange(range_list); // test data to test headings
            testPrintModel.DailySalesGroups = dailySalesTickets;

            var json = generator.Render(testPrintModel);
            var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);
            documentModel.PageLength = 50; // Lines to print per page
            RawCondensedPageHeadings(ref documentModel); // Do Page Headings - Section 1 Contains Headings

            documentModel.RawPrinter = true;
            documentModel.Condensed = true;
            var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
            var printersByFunction = FilterPrintersByFunction("DailySalesReport"); // Current Report is only setup for RAW
            if(printersByFunction.Count > 0) {printers = printersByFunction;}

            return SendPrintRequest(printers, documentModel, _serverCode != "L");
        }

        public string GetDailySalesPageCount(int pageCount)
        {
            StringBuilder pageCountSB = new StringBuilder();
            pageCountSB.Insert(0, "Total Pages ");
            int pageCountPos = 12;
            pageCountSB.Append(pageCount.ToString());
            int lenStr = pageCount.ToString().Length;
            switch (lenStr)
            {
                case 1:
                    pageCountSB.Append("   ");
                    break;
                case 2:
                    pageCountSB.Append("  ");
                    break;
                //case 3:
                //    pageCountSB.Insert(pageCountPos + 3, " ");
                    break;
            }
           
            return pageCountSB.ToString();
        }

        public List<DailySalesGroup> GetDailySalesGroups(DateTime dailySalesDate)
        {
            //var testdate = new DateTime(2016, 04, 20);
            //var testdate = new DateTime(2016, 09, 09);
            var testdate = dailySalesDate;
            List<DailySalesGroup> dailySalesGroups = new List<DailySalesGroup>();
            var dailySalesTickets = _ticketItemService.TicketItemsForDailySalesReports(testdate);
            var uniqueTickets = dailySalesTickets.Select(a => a.TicketReference).Distinct().ToList();

            foreach (var ticket in uniqueTickets) // create the top grouping line
            {
               // if (ticket == "245702" || ticket == "518514" || ticket == "520377" ) 
                //if (ticket == "520880" || ticket == "520876" || ticket == "520876" || ticket == "520878" || ticket == "520879" || ticket == "520990" || ticket == "520890" || ticket == "520894" || ticket == "520896" || ticket == "520899" || ticket == "520979" || ticket == "520995" || ticket == "520996" || ticket == "521367" || ticket == "521368" || ticket == "521371" || ticket == "521375") // just one for test
                //{
                    DailySalesGroup dailySalesGroup = new DailySalesGroup();
                    var ticketItems = dailySalesTickets.Where(a => a.TicketReference == ticket).ToList();
                    List<DailySalesLine> dailySalesLines = new List<DailySalesLine>();

                    GetDailySalesLinesForTicket(ref dailySalesLines, ref ticketItems);
                    GetDailySalesTicketTotals(ref dailySalesGroup, ref dailySalesLines);
                    dailySalesGroup.DailySalesLines = dailySalesLines;
                    dailySalesGroups.Add(dailySalesGroup);
               // }
            }

            return dailySalesGroups;
        }

        private void GetDailySalesTicketTotals(ref DailySalesGroup dailySalesGroup, ref List<DailySalesLine> dailySalesLines)
        {
            //dailySalesGroup.TotalCashPort = NullIfZero(dailySalesLines.Sum(a => a.CashPort).GetValueOrDefault());
            //dailySalesGroup.TotalCashQty = NullIfZero(dailySalesLines.Sum(a => a.CashQty).GetValueOrDefault());
            //dailySalesGroup.TotalCashUnit = NullIfZero(dailySalesLines.Sum(a => a.CashUnit).GetValueOrDefault());
            //dailySalesGroup.TotalCashTotal = NullIfZero(dailySalesLines.Sum(a => a.CashTotal).GetValueOrDefault());
            dailySalesGroup.TotalCashPort = NullIfZero(dailySalesLines.Sum(a => a.CashPort));
            dailySalesGroup.TotalCashQty = NullIfZero(dailySalesLines.Sum(a => a.CashQty));
            dailySalesGroup.TotalCashUnit = NullIfZero(dailySalesLines.Sum(a => a.CashUnit));
            dailySalesGroup.TotalCashTotal = NullIfZero(dailySalesLines.Sum(a => a.CashTotal));
            dailySalesGroup.TotalCreditPort = NullIfZero(dailySalesLines.Sum(a => a.CreditPort));
            dailySalesGroup.TotalCreditQty = NullIfZero(dailySalesLines.Sum(a => a.CreditQty));
            dailySalesGroup.TotalCreditUnit = NullIfZero(dailySalesLines.Sum(a => a.CreditUnit));
            dailySalesGroup.TotalCreditTotal = NullIfZero(dailySalesLines.Sum(a => a.CreditTotal));
        }

        private static decimal? NullIfZero(decimal? iValue)
        {
            if (iValue == 0) return null;
            return iValue;
        }
        private static double? NullIfZero(double? iValue)
        {
            if (iValue == 0) return null;
            return iValue;
        }

        private void GetDailySalesLinesForTicket(ref List<DailySalesLine> dailySalesLines, ref List<DailySalesReport> ticketItems)
        {
            foreach (var ticketItem in ticketItems)
            {
                DailySalesLine dailySalesLine = new DailySalesLine();
                dailySalesLine.TicketReference = ticketItem.TicketReference;
                dailySalesLine.Brand = ticketItem.Brand;
                dailySalesLine.PackType = ticketItem.PackType;
                dailySalesLine.PackSize = ticketItem.PackSize;
                dailySalesLine.DepartmentCode = ticketItem.DepartmentCode;
                dailySalesLine.ConsignmentReference = ticketItem.ConsignmentReference;
                dailySalesLine.CustomerCompanyName = ticketItem.CustomerCompanyName.Length < 21 ? ticketItem.CustomerCompanyName : ticketItem.CustomerCompanyName.Substring(0, 20);
                dailySalesLine.TicketItemDescription = ticketItem.TicketItemDescription.Length < 21 ? ticketItem.TicketItemDescription : ticketItem.TicketItemDescription.Substring(0, 20);           
                dailySalesLine.ProduceName = ticketItem.ProduceName.Length < 21 ? ticketItem.ProduceName : ticketItem.ProduceName.Substring(0, 20);

                if (ticketItem.IsCashSale.Value == true)
                {
                    dailySalesLine.CashPort = NullIfZero(ticketItem.PorterageValue); // if zero then change to null to prevent printing
                    dailySalesLine.CashQty = ticketItem.TicketItemQuantity;
                    dailySalesLine.CashTotal = ticketItem.TicketItemTotalPrice;
                    if (ticketItem.TicketItemTotalPrice != 0 && ticketItem.TicketItemQuantity != 0)
                    {
                        dailySalesLine.CashUnit = ticketItem.TicketItemTotalPrice /
                                                  (decimal)ticketItem.TicketItemQuantity;
                    }
                }
                else
                {
                    dailySalesLine.CreditPort = NullIfZero(ticketItem.PorterageValue);
                    dailySalesLine.CreditQty = ticketItem.TicketItemQuantity;
                    dailySalesLine.CreditTotal = ticketItem.TicketItemTotalPrice;
                    if (ticketItem.TicketItemTotalPrice != 0 && ticketItem.TicketItemQuantity != 0)
                    {
                        dailySalesLine.CreditUnit = ticketItem.TicketItemTotalPrice /
                                                    (decimal)ticketItem.TicketItemQuantity;
                    }
                }
                dailySalesLine.TicketDate = String.Format("{0:dd/MM}", ticketItem.TicketDate);
                if (!string.IsNullOrEmpty(ticketItem.Nickname))
                {
                    dailySalesLine.SalesInitial = ticketItem.Nickname.Length > 7 ? ticketItem.Nickname.Substring(0, 7) : ticketItem.Nickname;
                }
                else { dailySalesLine.SalesInitial = "  "; }
               
                dailySalesLines.Add(dailySalesLine);
            }
        }

        private void RawCondensedPageHeadings(ref PrintDocumentModel documentModel)
        {
            int headingsAdded = 0;
            int line_index = 0;
            
            var headingLines = documentModel.Sections[0].Lines.ToList(); // assumes section 1 is headings
            int pageCountPos = headingLines[1].LineItems[0].Text.LastIndexOf("Page");
            headingLines[1].LineItems[0].Text = headingLines[1].LineItems[0].Text.Replace("Page  ", "Page 1"); // first Page only, remainder pageinated by RawPaginator

            int breakcount = documentModel.Sections[1].Lines.Count / documentModel.PageLength.Value; // how many page breaks required
            headingLines[1].LineItems[0].Text = headingLines[1].LineItems[0].Text.Replace("Total_Pages____", GetDailySalesPageCount(breakcount)); // first Page only, remainder pageinated by RawPaginator

            var formLine = GetFFLineModel(); // Form Feed

            while (headingsAdded <= breakcount)
            {
                if (headingsAdded > 0) // first already added, in Section 1, add headings from 2nd page onwards
                {
                    line_index = line_index + documentModel.PageLength.Value;
                    documentModel.Sections[1].Lines.Insert(line_index, formLine);
                    line_index++;
                    var headingLinesPaginated = RawPaginator(1, headingsAdded + 1, pageCountPos, headingLines);
                    documentModel.Sections[1].Lines.InsertRange(line_index, headingLinesPaginated);
                    line_index = line_index + documentModel.Sections[0].Lines.Count; // add length of header
                } 
                headingsAdded++;
            }
        }

        private List<LineModel> RawPaginator(int line, int pageNumber, int pageCountPos, List<LineModel> headingLines)
        {
          List<LineModel> newHeadingLinesList = new List<LineModel>();
            foreach (var headline in headingLines)
            {
                var newLine = new LineModel();
                newLine.LineItems = new List<LineItemModel>();
                foreach (var lineItem in headline.LineItems)
                {
                    LineItemModel lim = new LineItemModel();
                    lim = (LineItemModel)lineItem.Clone(); // must do Deep Clone or bug where highest page number on each page
                    newLine.LineItems.Add(lim);
                }
                newHeadingLinesList.Add(newLine);
            }

            if (pageCountPos > 0)
            {
                pageCountPos = pageCountPos + 5;
                var stringB = new StringBuilder(newHeadingLinesList[line].LineItems[0].Text);
                stringB.Remove(pageCountPos, 4); // space for up to 9999 pages, should be enough!
                stringB.Insert(pageCountPos, pageNumber);
                int lenStr = pageNumber.ToString().Length;
                switch (lenStr)
                {
                    case 1:
                        stringB.Insert(pageCountPos + 1, "   ");
                        break;
                    case 2:
                        stringB.Insert(pageCountPos + 2, "  ");
                        break;
                    case 3:
                        stringB.Insert(pageCountPos + 3, " ");
                        break;
                }
                newHeadingLinesList[line].LineItems[0].Text = stringB.ToString();
            }
            return newHeadingLinesList;
        }

        private LineModel GetFFLineModel()
        {
            LineItemModel formFeedLineItem = new LineItemModel();
            formFeedLineItem.Text = "\f";
            LineModel formLine = new LineModel();
            formLine.LineItems = new List<LineItemModel>();
            formLine.LineItems.Add(formFeedLineItem);
            return formLine;
        }

        private string GetTestRawTemplate()
        {
            var settingReceiptTemplate = _setupLocalService.Find("TestRawTemplate");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("TestRaw template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        private string GetChequeRawTemplate()
        {
            var settingChequeTemplate = _setupLocalService.Find("ChequeRawTemplate");
            if (settingChequeTemplate == null)
            {
                throw new Exception("Cheque template not setup in database.");
            }
            return settingChequeTemplate.SetupValueNvarchar;
        }

        private string GetTestCondensedRawTemplate()
        {
            var settingReceiptTemplate = _setupLocalService.Find("DailySalesTemplate");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("Daily Sales template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        private string GetDisectionTemplate()
        {
            var settingReceiptTemplate = _setupLocalService.Find("DisectionReportTemplate");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("Disection template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        private string GetDisectionSubReport1Template()
        {
            var settingReceiptTemplate = _setupLocalService.Find("DisectionSub1");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("Disection template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        
        public bool PrintTest()
        {
            const string ESC = "\u001B";
            const string TOF = "\u00FF";
            var documentModel = new PrintDocumentModel();
            documentModel.PageWidth = 400;
            documentModel.RawPrinter = false;
            documentModel.Sections = new List<SectionModel>
            {
                    new SectionModel
                    {
                        Lines = new List<LineModel>
                        {
                            new LineModel
                            {
                                LineItems = new List<LineItemModel>
                                {
                                    new LineItemModel
                                    {
                                        Text = "This is test page",
                                        Align = StringAlignment.Center,
                                        FontSize = 12,
                                        FontStyle = FontStyle.Bold
                                    }
                                }
                            },                            
                             new LineModel
                            {
                                LineItems = new List<LineItemModel>
                                {
                                    new LineItemModel
                                    {
                                    //Text = ESC + TOF + "Top of next page",
                                    Text = "Top of next page",
                                        Align = StringAlignment.Near,
                                        FontSize = 14,
                                        FontStyle = FontStyle.Bold
                                    }
                                }
                            },
                             new LineModel
                            {
                                LineItems = new List<LineItemModel>
                                {
                                    new LineItemModel
                                    {
                                        Text = "01234567890123456789012345678901234567890123456789012345678901234567890123456789",
                                        Align = StringAlignment.Near,
                                    FontSize = 6,
                                    FontStyle = FontStyle.Regular,
                                    X = 200
                                }
                            }
                        }
                        ,
                        new LineModel
                        {
                            LineItems = new List<LineItemModel>
                            {
                                new LineItemModel
                                {
                                    Text = "01234567890123456789012345678901234567890123456789",
                                    Align = StringAlignment.Near,
                                    FontSize = 10,
                                        FontStyle = FontStyle.Regular,
                                        Width = 400
                                    //FontName = GenericFontFamilies.SansSerif.
                                    
                                }
                            }
                        },
                        new LineModel
                        {
                            LineItems = new List<LineItemModel>
                            {
                                new LineItemModel
                                {
                                    Text = "01234567890123456789012345678901234567890123456789",
                                    Align = StringAlignment.Near,
                                    //FontSize = 10,
                                        FontStyle = FontStyle.Regular
                                    //FontName = GenericFontFamilies.SansSerif.
                                    
                                }
                            }
                        }
                        ,
                        new LineModel
                        {
                            LineItems = new List<LineItemModel>
                            {
                                new LineItemModel
                                {
                                    Text = "0000000000000000",
                                    Align = StringAlignment.Near,
                                    //FontSize = 10,
                                        FontStyle = FontStyle.Regular
                                    //FontName = GenericFontFamilies.SansSerif.
                                    
                                }
                            }
                        },
                        new LineModel
                        {
                            LineItems = new List<LineItemModel>
                            {
                                new LineItemModel
                                {
                                    Text = "01234567890123456789012345678901234567890123456789",
                                    Align = StringAlignment.Near,
                                    //FontSize = 10,
                                        FontStyle = FontStyle.Regular
                                    //FontName = GenericFontFamilies.SansSerif.
                                    
                                }
                            }
                        }
                    }
                }
            };
            var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
            return SendPrintRequest(printers, documentModel, _serverCode != "L");
        }

        private string GetReceiptTemplate()
        {
            var settingReceiptTemplate = _setupLocalService.Find("ReceiptTemplate");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("Receipt template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        private string GetTemplate(string TemplateName)
        {
            var settingTemplate = _setupLocalService.Find(TemplateName);
            if (settingTemplate == null)
            {
                throw new Exception("Template not setup in database.");
            }
            return settingTemplate.SetupValueNvarchar;
        }

        private string GetCashReceiptTemplate()
        {
            var settingReceiptTemplate = _setupLocalService.Find("CashReceiptTemplate");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("Cash Receipt template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        private string GetDeliveryNoteTemplate()
        {
            var settingDeliveryNoteTemplate = _setupLocalService.Find("DeliveryNoteTemplate");
            if (settingDeliveryNoteTemplate == null)
            {
                throw new Exception("Delivery Note template not setup in database.");
            }
            return settingDeliveryNoteTemplate.SetupValueNvarchar;
        }

        private string GetReceiptTicketTemplate()
        {
            var settingReceiptTemplate = _setupLocalService.Find("ReceiptTicketTemplate");
            if (settingReceiptTemplate == null)
            {
                throw new Exception("ReceiptTicket template not setup in database.");
            }
            return settingReceiptTemplate.SetupValueNvarchar;
        }

        protected List<Printer> FilterPrintersByFunction(string function)
        {
            List<Printer> printersByFunction = new List<Printer>();
            //var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
            var printers = _printerOrchestra.GetPrintersForDepartmentNoDefaults(_principal.SelectedDepartmentId);
            
            if (printers.Count > 0)
            {
                var printTasks = _printTaskOrchestra.GetPrintTaskForName(function);
                foreach (var p in printTasks)
                {
                    foreach (var deptPrint in p.DepartmentPrintTasks)
                    {
                        Printer deptPrinter = printers.Find(t => t.PrinterID == deptPrint.DepartmentPrinter.PrinterID);
                        //if (!printersByFunction.Contains(deptPrinter)) // DC code removed, now want to allow duplicates, for business reasons, if 2 entries setup on DeptPrinters table
                        //{
                            printersByFunction.Add(deptPrinter);
                        //}
                    }
                }
            }

            return printersByFunction;
        }

        public bool PrintReceipt(TicketPrintViewModel receiptToPrint)
        {
            _logger.Debug("PrintReceipt: Start");

            try
            {
                
                string receiptTemplate;
                
                List<string>  EmptyLines = new List<string>();
                string placeholder = "empty";
                int fixedlines = 22;
                int startfixedloop = receiptToPrint.Ticket.TicketItems.Count*2;
                for (int s = startfixedloop; s < fixedlines; s++)
                {
                    EmptyLines.Add(placeholder);
                }
                receiptToPrint.EmptyLines = receiptToPrint.Ticket.TicketItems.Count < 11 ? EmptyLines : null;

                receiptTemplate = receiptToPrint.Ticket.IsCashSale == false ? GetReceiptTemplate() : GetCashReceiptTemplate(); // Credit or Cash Receipt Template

                var deliveryNoteTemplate = GetDeliveryNoteTemplate();
                FormatCompiler compiler = new FormatCompiler();
                //var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
                var printers = _printerOrchestra.GetPrintersForDepartmentNoDefaults(_principal.SelectedDepartmentId);

                var printersByFunction = receiptToPrint.Ticket.IsCashSale == true ? FilterPrintersByFunction("DeliveryNotePrint") : FilterPrintersByFunction("CreditDeliveryNotePrint"); // if Department Printer assigned to a function
               
                if (printersByFunction.Count > 0)
                {
                    printers = printersByFunction;
                }

                Generator generatorDeliveryNote = compiler.Compile(deliveryNoteTemplate);
                var jsonDeliveryNote = generatorDeliveryNote.Render(receiptToPrint);
                var documentModelDeliveryNote = JsonConvert.DeserializeObject<PrintDocumentModel>(jsonDeliveryNote);
                documentModelDeliveryNote.PageWidth = 400;
                bool successDeliveryNotePrint = SendPrintRequest(printers, documentModelDeliveryNote, _serverCode != "L");

                //Receipt
                //printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
                printers = _printerOrchestra.GetPrintersForDepartmentNoDefaults(_principal.SelectedDepartmentId);

                string printerDepartmentFunction = receiptToPrint.Ticket.IsCashSale == false ? "ReceiptPrint": "CashTicket";
                //printersByFunction = FilterPrintersByFunction("ReceiptPrint"); // if Department Printer assigned to a function
                printersByFunction = FilterPrintersByFunction(printerDepartmentFunction); // if Department Printer assigned to a function
                if (printersByFunction.Count > 0)
                {
                    printers = printersByFunction;
                }

                Generator generator = compiler.Compile(receiptTemplate);
                var json = generator.Render(receiptToPrint);
                var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);
                documentModel.PageWidth = 400;
                
                bool successReceiptPrint = SendPrintRequest(printers, documentModel, _serverCode != "L");
               
                return successReceiptPrint & successDeliveryNotePrint; // return true if both successful. else false
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
            finally
            {
                _logger.Debug("PrintReceipt: End");
            }
        }

        //public bool PrintCashReceipt(TicketPrintViewModel receiptToPrint)
        //{
        //    _logger.Debug("PrintCashReceipt: Start");

        //    try
        //    {
        //        var receiptTemplate = GetCashReceiptTemplate();
        //        var deliveryNoteTemplate = GetDeliveryNoteTemplate();
        //        FormatCompiler compiler = new FormatCompiler();
        //        var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);

        //        Generator generatorDeliveryNote = compiler.Compile(deliveryNoteTemplate);
        //        var jsonDeliveryNote = generatorDeliveryNote.Render(receiptToPrint);
        //        var documentModelDeliveryNote = JsonConvert.DeserializeObject<PrintDocumentModel>(jsonDeliveryNote);
        //        documentModelDeliveryNote.PageWidth = 400;
        //        bool successDeliveryNotePrint = SendPrintRequest(printers, documentModelDeliveryNote, _serverCode != "L");

        //        Generator generator = compiler.Compile(receiptTemplate);
        //        var json = generator.Render(receiptToPrint);
        //        var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);
        //        documentModel.PageWidth = 400;

        //        bool successReceiptPrint = SendPrintRequest(printers, documentModel, _serverCode != "L");
        //        //return SendPrintRequest(printers, documentModel, _serverCode != "L");

        //        return successReceiptPrint & successDeliveryNotePrint; // return true if both Prints successful.
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex);
        //        return false;
        //    }
        //    finally
        //    {
        //        _logger.Debug("PrintReceipt: End");
        //    }
        //}

        public bool PrintReceiptTicket(ReceiptTicketViewModel receiptToPrint)
        {
            _logger.Debug("PrintReceiptTicket: Start");

            try
            {
                var receiptTemplate = GetReceiptTicketTemplate();
                //var deliveryNoteTemplate = GetDeliveryNoteTemplate();
                FormatCompiler compiler = new FormatCompiler();
                //var printers = _printerOrchestra.GetPrintersForDepartment(_principal.DepartmentId.Value);
                var printers = _printerOrchestra.GetPrintersForDepartmentNoDefaults(_principal.SelectedDepartmentId);
                var printersByFunction = FilterPrintersByFunction("TicketReceiptPrint"); // if Department Printer assigned to a function
                if (printersByFunction.Count > 0)
                {
                    printers = printersByFunction;
                }

                Generator generator = compiler.Compile(receiptTemplate);
                var json = generator.Render(receiptToPrint);
                var documentModel = JsonConvert.DeserializeObject<PrintDocumentModel>(json);
                documentModel.PageWidth = 400;

                bool successReceiptPrint = SendPrintRequest(printers, documentModel, _serverCode != "L");

                return successReceiptPrint;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
            finally
            {
                _logger.Debug("PrintReceipt: End");
            }
        }

        private bool SendPrintRequest(List<Printer> printers, PrintDocumentModel documentModel, bool remote)
        {
            try
            {
                if (printers == null || printers.Count == 0)
                {
                    throw new Exception("No printers configured.");
                }

                Uri endPointUri;
                ChannelFactory<IPrintServiceChannel> channelFactory;
                if (remote)
                {
                    var settingRelayServiceName = _setupLocalService.Find("RelayServiceName");
                    var serviceNamespace = settingRelayServiceName != null ? settingRelayServiceName.SetupValueNvarchar : null;
                    var settingRelayServiceSecret = _setupLocalService.Find("RelayServiceSecret");
                    var serviceSecret = settingRelayServiceSecret != null ? settingRelayServiceSecret.SetupValueNvarchar : null;

                    endPointUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, "print");

                    var relayBinding = new NetTcpRelayBinding
                    {
                        TransferMode = TransferMode.Buffered,
                        MaxBufferPoolSize = 2147483647,
                        MaxBufferSize = 2147483647,
                        MaxReceivedMessageSize = 2147483647
                    };
                    channelFactory = new ChannelFactory<IPrintServiceChannel>(
                        relayBinding,
                        new EndpointAddress(endPointUri));

                    channelFactory.Endpoint.Behaviors.Add(
                        new TransportClientEndpointBehavior
                        {
                            TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", serviceSecret)
                        });
                }
                else
                {
                    var settingServerName = _setupLocalService.Find("PrintServiceServerName");
                    var serverName = "localhost";// settingServerName != null ? settingServerName.SetupValueNvarchar : "localhost";
                   // var serverName =  settingServerName != null ? settingServerName.SetupValueNvarchar : "localhost";
                    var settingServerPort = _setupLocalService.Find("PrintServiceServerPort");
                    var serverPort = settingServerPort != null ? settingServerPort.SetupValueInt : 49501;

                    endPointUri = new Uri(string.Format("net.tcp://{0}:{1}/print", serverName, serverPort));

                    var binding = new NetTcpBinding
                    {
                        TransferMode = TransferMode.Buffered,
                        MaxBufferPoolSize = 2147483647,
                        MaxBufferSize = 2147483647,
                        MaxReceivedMessageSize = 2147483647
                    };
                    channelFactory = new ChannelFactory<IPrintServiceChannel>(
                        binding,
                        new EndpointAddress(endPointUri));
                }

                var printerNames = printers.Select(p => p.NetworkName).ToArray();

                var client = channelFactory.CreateChannel();
                try
                {
                    client.PrintDocument(printerNames, documentModel);
                    client.Close();
                    return true;
                }
                catch (TimeoutException ex)
                {
                    _logger.Error(ex);
                    client.Abort();
                    return false;
                }
                catch (CommunicationException ex)
                {
                    _logger.Error(ex);
                    client.Abort();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
       
    }

    
}
