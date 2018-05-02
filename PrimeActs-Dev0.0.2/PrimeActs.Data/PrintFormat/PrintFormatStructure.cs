using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.PrintFormatStructure
{
    //class PrintFormatStructure
    //{
    //}

    public class Counterparty
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string HundredThousands { get; set; }
        public string TenThousands { get; set; }
        public string Thousands { get; set; }
        public string Hundreds { get; set; }
        public string Tens { get; set; }
        public string Units { get; set; }
    }

    public class TestLevel1
    {
        public TestLevel1()
        {
            Level2List = new List<TestLevel2>();
        }
        public string Level1 { get; set; }
        public List<TestLevel2> Level2List { get; set; }

    }

    public class TestLevel0
    {
        public TestLevel0()
        {
            Level1List = new List<TestLevel1>();
        }
        public string Level0 { get; set; }
        public List<TestLevel1> Level1List { get; set; }
    }

    public class TestLevel2
    {
        public string Level2 { get; set; }
    }

    public class DailySalesLine
    {
        public string ConsignmentReference { get; set; }
        public string TicketItemDescription { get; set; }
        //public double? CashQty { get; set; }
        public decimal? CashQty { get; set; }
        public decimal? CashUnit { get; set; }
        public decimal? CashTotal { get; set; }
        public decimal? CashPort { get; set; }
        //public double? CreditQty { get; set; }
        public decimal? CreditQty { get; set; }
        public decimal? CreditUnit { get; set; }
        public decimal? CreditTotal { get; set; }
        public decimal? CreditPort { get; set; }
        public string TicketReference { get; set; }
        public string CustomerCompanyName { get; set; }
        public string Brand { get; set; }
        public string PackType { get; set; }
        public string TicketDate { get; set; }
        public string PackSize { get; set; }
        public string SalesInitial { get; set; }
        public string DepartmentCode { get; set; }
        public string ProduceName { get; set; }
    }

    public class DailySalesGroup
    {
        public List<DailySalesLine> DailySalesLines { get; set; }
        //public double? TotalCashQty { get; set; }
        public decimal? TotalCashQty { get; set; }
        public decimal? TotalCashUnit { get; set; }
        public decimal? TotalCashTotal { get; set; }
        public decimal? TotalCashPort { get; set; }
        //public double? TotalCreditQty { get; set; }
        public decimal? TotalCreditQty { get; set; }
        public decimal? TotalCreditUnit { get; set; }
        public decimal? TotalCreditTotal { get; set; }
        public decimal? TotalCreditPort { get; set; }
    }

    public class DailySalesPrintModel
    {
        public List<Ticket> TicketLine { get; set; }
        private List<DailySalesGroup> DailySalesGroups { get; set; }
    }

    public class ProduceItemExtended : Produce
    {
        public ProduceItemExtended()
        {
            ConsignmentPrintModels = new List<ConsignmentPrintModel>();
        }

        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public List<ConsignmentPrintModel> ConsignmentPrintModels { get; set; }
    }

    public class ExtendedProduceGroup : ProduceGroup
    {
        public List<ProduceItemExtended> Produces { get; set; }
    }

    public class ChequePrintModel
    {
        public ChequePrintModel()
        {
            ChequePages = new List<ChequePage>();
        }

        public List<ChequePage> ChequePages { get; set; }
    }

    public class ChequePage
    {
        public ChequePage()
        {
            ChequePrintRemittanceItems = new List<ChequePrintRemittanceItem>();
        }

        public List<ChequePrintRemittanceItem> ChequePrintRemittanceItems { get; set; }
        public ChequePrintHeader ChequePrintHeader { get; set; }
        public bool PrintVoidCheque { get; set; }
    }

    public class ChequePrintHeader
    {

        public int LinesPerRemittance { get; set; } // control the Moustache template used for printing
        public bool PrintVoidCheque { get; set; }
        public Address ChequeAddress { get; set; }
        public string ChequePrintDate { get; set; }
        public string ChequeName { get; set; }
        public decimal? ChequeAmount { get; set; }
        public int ChequeNumber { get; set; } // more put in place for reference in future, should we also print cheque number on the Remittance
        public string ChequeWords { get; set; }
        public string AccCode { get; set; }
    }

    public class ChequePrintRemittanceItem
    {

        public string Reference { get; set; }
        public string RemittanceDate { get; set; }
        public string RemittanceType { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? Balance { get; set; }
    }

    public class CSVTemplate
    {
        public List<NatWestStdDomesticPaymentCSV> CSVList { get; set; }

        public CSVTemplate()
        {
            CSVList = new List<NatWestStdDomesticPaymentCSV>();
        }
    }

    public class ConsignmentPrintModel : ConsignmentItem
    {
        public string ReceivedDate { get; set; }
        public List<PrimeActs.PrintFormatStructure.DisectionSubLine> SubLines { get; set; }
        public DisectionTotals1 DisectionTotals1 { get; set; }
        public DisectionTotals2 DisectionTotals2 { get; set; }
        public DisectionTotals3 DisectionTotals3 { get; set; }
        public DisectionTotals4 DisectionTotals4 { get; set; }
    }

    public class Disection
    {
        public Disection()
        {
            ProduceGroups = new List<ProduceGroup>();
        }
        public DateTime Date { get; set; }
        public List<ProduceGroup> ProduceGroups { get; set; }

    }

    public class ProduceDisection
    {
        public ProduceDisection()
        {
            Produces = new List<ProduceItemExtended>();
        }
        public DateTime Date { get; set; }
        public List<ProduceItemExtended> Produces { get; set; }
        
    }

    public class DisectionSub1
    {
        public string Price { get; set; }
        public string QtySoldToday { get; set; }
        public string ValueSoldToday { get; set; }
        public string PreviousReturnsQty { get; set; }
        public string PreviousReturnsValue { get; set; }
        public string PreviousSaleQty { get; set; }
        public string PreviousSaleValue { get; set; }
        public decimal PriceNum { get; set; }
        public int QtySoldTodayNum { get; set; }
        public decimal ValueSoldTodayNum { get; set; }
        public int PreviousReturnsQtyNum { get; set; }
        public decimal PreviousReturnsValueNum { get; set; }
        public int PreviousSaleQtyNum { get; set; }
        public decimal PreviousSaleValueNum { get; set; }
        //public string PreviousSaleCode { get; set; }
    }

    public class DisectionSub2
    {
        public string SalesCode { get; set; }
        public string SalesTicket { get; set; }
        public string SalesQty { get; set; }
        public string SalesPrice { get; set; }
        public string SalesTotal { get; set; }
    }

    public class DisectionTotals1
    {
        public string QtyTotal { get; set; }
        public string ValueTotal { get; set; }
        public string QtyTotalPrevReturns { get; set; }
        public string ValueTotalPrevReturns { get; set; }
        public string QtyTotalPrevSale { get; set; }
        public string ValueTotalPrevSale { get; set; }
        public string RecdQty { get; set; }
        public string Balance { get; set; }
    }
    public class DisectionTotals2
    {
        public string ValueAve { get; set; }
        public string ValueAvePrevReturns { get; set; }
        public string ValueAvePrevSale { get; set; }
        public string BroughtForward { get; set; }
    }
    public class DisectionTotals3
    {
        public string SoldToday { get; set; }
    }

    public class DisectionTotals4
    {
        public string EstimatedPurchaseCost { get; set; }
    }

    public class DisectionSubLine
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }
        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col13 { get; set; }
        public string Col14 { get; set; }
        public string Col15 { get; set; }
        public string Col16 { get; set; }
        public string Col17 { get; set; }
    }

    public class DisectionSubPrintModel
    {
        public List<DisectionSub1> DisectionSubList { get; set; }
    }

    public class DisectionPrintModel
    {
        public DisectionPrintModel()
        {
            DisectionDetails = new Disection();
        }
        public bool RawPrinter { get; set; }
        public Disection DisectionDetails { get; set; }
        public ProduceDisection ProduceDisectionDetails { get; set; }
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string PrintSelections1 { get; set; }
        public string PrintSelections2 { get; set; }
        public string PrintSelections3 { get; set; }
        public string PrintSelections4 { get; set; }
        public string PrintSelections5 { get; set; }
        public string PrintSelections6 { get; set; }
        public string PrintSelections7 { get; set; }
        public string PrintSelections8 { get; set; }
        public string StartSalesDate { get; set; }
        public string EndSalesDate { get; set; }
        public TestLevel0 Test0 { get; set; }
    }

    public class TestRawPrintCondensed
    {
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public List<string> TestLines { get; set; }
        public List<DailySalesGroup> DailySalesGroups { get; set; }
        public string NumberOfPages { get; set; }
    }

    public class TestPrintModel
    {
        public TestPrintModel()
        {
            CounterParties = new List<Counterparty>();
        }
        public bool RawPrinter { get; set; }
        public List<Counterparty> CounterParties { get; set; }
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string PrintSelections1 { get; set; }
        public string PrintSelections2 { get; set; }
        public string PrintSelections3 { get; set; }
        public string PrintSelections4 { get; set; }
        public string PrintSelections5 { get; set; }
        public string PrintSelections6 { get; set; }
        public string PrintSelections7 { get; set; }
        public string PrintSelections8 { get; set; }
        public TestLevel0 Test0 { get; set; }
    }
    
}
