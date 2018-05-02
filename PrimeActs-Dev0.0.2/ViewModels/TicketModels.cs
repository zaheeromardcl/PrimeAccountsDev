#region

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Company;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class TicketEditModel
    {
        public TicketEditModel()
        {
            TicketItems = new List<TicketItemEditModel>();
        }

        public Guid TicketID { get; set; }
        public string PONumber { get; set; }
        public string TicketReference { get; set; }
        public string CustomerCompanyName { get; set; }
        public Guid CustomerDepartmentID { get; set; }
        public Guid? NoteID { get; set; }
        public string Notes { get; set; }
        public string TicketDate { get; set; }
        public Guid CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyRate { get; set; }
        public Guid CustomerCurrencyID { get; set; }
        public Guid SalesPersonUserID { get; set; }
        public string SalesPersonName { get; set; }
        public Guid? SalesPersonDepartmentID { get; set; }
        public string SalesPersonDepartmentName { get; set; }
        public string SalesPersonDepartmentCode { get; set; }
        public bool IsCashSale { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
         public bool IsSaved { get; set; }
        public decimal TicketTotalPrice { get; set; }
        public decimal TicketTotalPorterage { get; set; }
        public decimal TicketSubTotal { get; set; }
        public double TicketVATTotal { get; set; }
        public bool IsHistory { get; set; }
        public string ServerCode { get; set; }
        public Guid DivisionID { get; set; }
        public decimal? AmountReceived { get; set; }

        public List<TicketItemEditModel> TicketItems { get; set; }
        public PrintDocument PrintDoc {get; set;}
    }

    public class TicketViewModel
    {
        public TicketEditModel TicketEditModel { get; set; }
        // public TicketItemEditModel TicketItemEditModel { get; set; }
        public string TempTicketID { get; set; }
        public Guid TicketNoteID { get; set;  }
        public List<TicketItemEditModel> TicketItems { get; set; }
      
    }

    public class CreateTicketViewModel
    {
        public List<PaymentType> PaymentTypeList { get; set; }
        public TicketEditModel TicketCreateModel { get; set; }
    }

    public class TicketPrintViewModel
    {
        public CompanyViewModel Company { get; set; }
        public TicketEditModel Ticket { get; set; }
    }

    public class ReceiptTicketViewModel
    {
        public Guid TicketID { get; set; }
        public string TicketReference { get; set; }
        public DateTime TicketDate { get; set; }
        public Guid CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public Guid CustomerDepartmentID { get; set; }
        public string CustomerCompanyName { get; set; }
        public Guid SalesPersonUserID { get; set; }
        public string SalesPersonName { get; set; }
        public Guid? NoteID { get; set; }
        public string Notes { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? AmountReceived { get; set; }
        public CompanyViewModel Company { get; set; }
    }

    public class SearchObject
    {
        public string TicketReference { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCompanyName { get; set; }
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
                    }
                }

            }
        }
    }

    public class TicketPagingModel
    {
        public ResultList<TicketEditModel> TicketEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }
   
    public class TicketItemPagingModel
    {
        public ResultList<TicketItemEditModel> TicketItemEditModels { get; set; }
    }

    public class TicketItemEditModel
    {
        public Guid TicketItemID { get; set; }
        public Guid TicketID { get; set; }
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string TicketItemDescription { get; set; }
        // public decimal Currency { get; set; }
        public decimal CurrencyAmount { get; set; }
        public double TicketItemQuantity { get; set; }
        public string TicketItemBrand { get; set; }
        public string TicketItemWeight { get; set; }
        public Guid TicketItemPorterageID { get; set; }
        public decimal? TicketItemMinPorterage { get; set; }
        public decimal TicketItemPorterageValue { get; set; }
        public decimal TicketItemPorterage { get; set; }
        public string TicketItemSize { get; set; }
        public double TicketItemUnitPrice { get; set; }
        public decimal? TicketItemTotalPrice { get; set; }
        public Guid ConsignmentItemID { get; set; }
        public string ConsignmentReference { get; set; }
        public Guid ProduceID { get; set; }
        public string Produce { get; set; }
        public string ProduceDescription { get; set; }
        public Guid SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
      
        public bool IsLatest { get; set; }
        public Guid OriginalTicketItemID { get; set; }
        public Guid? TransferTypeID { get; set; }
        public string TransferTypeName { get; set; }

        public bool IsDirty { get; set; }
    }
}