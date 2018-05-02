#region

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

#endregion

namespace PrimeActs.Domain.ViewModels.PurchaseInvoice
{
    public class PurchaseInvoiceModel
    {
        public Guid PurchaseInvoiceID { get; set; }

        public Guid SupplierDepartmentId { get; set; }

        public string PurchaseInvoiceNumber { get; set; }

        public string PurchaseInvoiceDate { get; set; }

        public string SupplierDepartmentName { get; set; }

        public AddressModels Address { get; set; }

        public decimal Total { get; set; }

        // it's a Guid generated at the time of creating the create purchaseinvoice tab serves as the purchaseInvoiceID
        public string UploadFolder { get; set; }

        public string ServerCode { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public bool IsSaved { get; set; }

        public bool? IsActive { get; set; }

        public HttpFileCollection Files { get; set; }
        public string Status { get; set; }
        public string PreviousStatus { get; set; }
        public decimal? SupplierInvoiceAmount { get; set; }
        public decimal SubTotal { get; set; }
        public string SupplierCode { get; set; }
    }

    public class PurchaseInvoicePagingModel
    {
        public ResultList<PurchaseInvoiceModel> PurchaseInvoiceEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public enum PurchaseInvoiceStatus
    {
        OK = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public class SearchObject
    {
        public string PurchaseInvoiceNumber { get; set; }
        public string SupplierDepartmentId { get; set; }
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
                        case "LASTMONTH":
                            this.FromDate = DateTime.Today.AddMonths(-1);
                            this.ToDate = DateTime.Today.AddDays(1);
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
}