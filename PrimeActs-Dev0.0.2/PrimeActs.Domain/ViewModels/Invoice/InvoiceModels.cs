using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace PrimeActs.Domain.ViewModels.Invoice
{
    public class InvoiceRunModel
    {
        public bool IsInvoiceRun { get; set; }
        public bool IsCreditNoteRun { get; set; }
        public bool IsCashSale { get; set; }
        public List<string> SelectedDivisions { get; set; }
        public string Period { get; set; }
        public bool RunAll { get; set; }
        public string Username { get; set; }
    }

    public class InvoiceEditModel
    {

        public InvoiceEditModel()
        {
            InvoiceItemEditModels = new List<InvoiceItemEditModel>();
        }

        public Guid SalesInvoiceID { get; set; }                
        
        public Guid CurrencyID { get; set; }
        public Guid NoteID { get; set; }
        public string CompanyDivisionName { get; set; }
        public string SalesInvoiceReference { get; set; }
        public Guid CustomerDepartmentID { get; set; }
        public string CustomerDepartmentName { get; set; }
        public Guid CustomerDepartmentAddressID { get; set; }
        public string CustomerDepartmentAddress1 { get; set; }
        public string CustomerDepartmentAddress2 { get; set; }
        public string CustomerDepartmentAddress3 { get; set; }
        public string customerDepartmentPostalTown { get; set; }
        public string customerDepartmentPostcode { get; set; }
        public string ServerCode { get; set; }
        public string SalesInvoiceDate { get; set; }
        public Guid DivisionAddressID { get; set; }
        public string DivisionAddress1 { get; set; }
        public string DivisionAddress2 { get; set; }
        public string DivisionAddress3 { get; set; }
        public string DivisionPostalTown { get; set; }
        public string DivisionPostCode { get; set; }
        public string Currency { get; set; }
        public string ExchangeRate { get; set; }
        public string NoteText { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsSaved { get; set; }
        public string CompanyVATRegistrationNumber { get; set; }
        public string TransactionTaxAmount { get; set; }
        public string CompanyNumber { get; set; }
        public Image Logo { get; set; }
        public List<InvoiceItemEditModel> InvoiceItemEditModels { get; set; }

    }

    public class SearchObject
    {
        public string SalesInvoiceReference { get; set; }
        public string CustomerDepartmentName { get; set; }
        public string TicketReference { get; set; }
        public string ConsignmentReference { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        private string _recordsInDays { get; set; }
        public string RecordsInDays
        {
            get { return _recordsInDays; }
            set
            {
                _recordsInDays = value;
                if (!String.IsNullOrEmpty(value))
                {
                    switch (value)
                    {
                        case "CURRENTMONTH":
                            this.FromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            this.ToDate = DateTime.Parse(FromDate.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        case "PREVIOUSMONTH":
                            DateTime tempDate = DateTime.Today.AddMonths(-1);
                            this.FromDate = new DateTime(tempDate.Year, tempDate.Month, 1);
                            this.ToDate = DateTime.Parse(FromDate.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        case "CURRENTYEAR":
                            this.FromDate = new DateTime(DateTime.Today.Year, 1, 1);
                            this.ToDate = DateTime.Parse(new DateTime(DateTime.Today.Year, 12, 31).ToString("yyyy-MM-dd 23:59:59.000"));

                            break;

                        case "PREVIOUSYEAR":
                            this.FromDate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);
                            this.ToDate = DateTime.Parse(new DateTime(FromDate.Value.Year, 12, 31).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        default:
                            this.FromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            this.ToDate = DateTime.Parse(FromDate.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;

                            break;

                    }
                }

            }
        }

    }

    public class InvoicePagingModel
    {
        public ResultList<InvoiceEditModel> InvoiceEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class InvoiceViewModel
    {
        public InvoiceEditModel InvoiceEditModel { get; set; }
        public string TempInvoiceID { get; set; }
        public List<InvoiceItemEditModel> InvoiceItems { get; set; }
    }

    public class InvoiceDetailModel
    {
        public Guid SalesInvoiceID { get; set; }
       
        public Guid CurrencyID { get; set; }
        public Guid NoteID { get; set; }
        public string CompanyDivisionName { get; set; }
        public string SalesInvoiceReference { get; set; }
        public Guid CustomerDepartmentID { get; set; }
        public string CustomerDepartmentName { get; set; }
        public Guid CustomerDepartmentAddressID { get; set; }
        public string CustomerDepartmentAddress1 { get; set; }
        public string CustomerDepartmentAddress2 { get; set; }
        public string CustomerDepartmentAddress3 { get; set; }
        public string CustomerDepartmentPostalTown { get; set; }
        public string CustomerDepartmentPostcode { get; set; }
        public string ServerCode { get; set; }
        public string SalesInvoiceDate { get; set; }
        public Guid DivisionAddressID { get; set; }
        public string DivisionAddress1 { get; set; }
        public string DivisionAddress2 { get; set; }
        public string DivisionAddress3 { get; set; }
        public string DivisionPostalTown { get; set; }
        public string DivisionPostCode { get; set; }
        public string Currency { get; set; }
        public string ExchangeRate { get; set; }
        public string NoteText { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsSaved { get; set; }
        public string CompanyVATRegistrationNumber { get; set; }
        public string TransactionTaxAmount { get; set; }
        public string CompanyNumber { get; set; }
        public Image Logo { get; set; }
        public List<InvoiceTicketItemModel> InvoiceTicketItems { get; set; }

        public InvoiceDetailModel()
        {
            InvoiceTicketItems = new List<InvoiceTicketItemModel>();
        }
    }

    public class InvoiceTicketItemModel
    {
        public string TicketNumber { get; set; }
        public string PorterageValue { get; set; }
        public List<InvoiceItemEditModel> InvoiceItemModels { get; set; }

        public InvoiceTicketItemModel()
        {
            InvoiceItemModels = new List<InvoiceItemEditModel>();
        }
    }

    public class InvoiceItemEditModel
    {
        public InvoiceEditModel Invoice { get; set; }
        public Guid SalesInvoiceItemID { get; set; }
        public Guid SalesInvoiceID { get; set; }
        public Guid TicketItemID { get; set; }
        public Guid CurrencyID { get; set; }
        public string Currency { get; set; }
        public string SalesInvoiceItemDescription { get; set; }
        public string SalesInvoiceItemLineTotal { get; set; }

        //public string SalesInvoiceItemVAT { get; set; }        
        public string ExchangeRate { get; set; }
        public string porterageValue { get; set; }
        public string Brand { get; set; }
        public string TicketNumber { get; set; }
        public string TicketItemQty { get; set; }
        public string TicketItemTotalPrice { get; set; }
    }

    public class InvoiceModel
    {
        public InvoiceModel()
        {
            DivisionList = new List<ItemViewModel>();
            InvoiceStatusModels = new List<InvoiceStatus>();            
            InvoiceCompletedModels = new List<InvoiceCompletedModel>();
        }
        public List<InvoiceCompletedModel> InvoiceCompletedModels { get; set; }
        public InvoiceRunModel InvoiceRunModel { get; set; }
        public List<InvoiceStatus> InvoiceStatusModels { get; set; }
        public List<ItemViewModel> DivisionList { get; set; }

        public bool  ShowRunInvoice  {get;set;}
        public bool  ShowRunningInvoice {get;set;}
        public bool  ShowCompletedInvoice {get;set;}
    }
    public class InvoiceStatus
    {
        public string Username { get; set; }
        public string DivisionName { get; set; }
        public string Period { get; set; }
    }
    public class InvoiceCompletedModel
    {
        public string DivisionName { get; set; }
        public string Username { get; set; }
        public string BatchNumber { get; set; }
        public string InvoiceType { get; set; }
        public string SalesInvoiceReference { get; set; }
    }
}
