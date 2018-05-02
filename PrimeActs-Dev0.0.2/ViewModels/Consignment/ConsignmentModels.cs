#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain.ViewModels.Consignment
{
    public class ConsignmentEditModel
    {
        public ConsignmentEditModel()
        {
            ConsignmentItemEditModels = new List<ConsignmentItemEditModel>();
            ConsignmentFileEditModels = new List<ConsignmentFileEditModel>();
            FileEditModels = new List<FileEditModel>();
        }

        public Guid ConsignmentID { get; set; }
        public string ConsignmentDescription { get; set; }
        public string DespatchDate { get; set; }
        public string ContractDate { get; set; }
        public string ConsignmentReference { get; set; }
        public string FileName { get; set; }
        public string FileID { get; set; }
        public byte[] FileContent { get; set; }
        public decimal Handling { get; set; }
        public decimal Commission { get; set; }
        public bool ShowVehicleOnInvoice { get; set; }
        public string Vehicle { get; set; }
        public string VehicleDetail { get; set; }
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }        
        public string SupplierReference { get; set; }
        public Guid SupplierDepartmentID { get; set; }
        public string SupplierDepartmentName { get; set; }
        public Guid SupplierID { get; set; }
        public string SupplierCompanyName { get; set; }
        public Guid? PortID { get; set; }
        public string PortName { get; set; }
        public Guid PurchaseTypeID { get; set; }
        public string PurchaseTypeName { get; set; }
        public string ReceivedDate { get; set; }
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public Guid? DespatchLocationID { get; set; }
        public string DespatchName { get; set; }
        public Guid? NoteID { get; set; }
        public string NoteText { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int ItemCount { get; set; }
        public int FileCount { get; set; }
        public bool IsSaved { get; set; }
        public bool DisplayPort { get; set; }
        public bool DisplayDespatchLocation { get; set; }
        public string SelectPurchaseType { get; set; }
        public List<PurchaseType> purchaseTypeList { get; set; }
        public List<FileEditModel> FileEditModels { get; set; }
        public List<ConsignmentItemEditModel> ConsignmentItemEditModels { get; set; }
        public List<ConsignmentFileEditModel> ConsignmentFileEditModels { get; set; }
        public string UserName { get; set; }
        public bool MultipleConsignmentItems { get; set; }
        public Guid DefaultDepartmentID { get; set; }
        public string DefaultDepartmentName { get; set; }
    }

    public class SearchObject
    {
        public string ConsignmentReference { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
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

                        case "PREVIOUSYEAR":
                            this.FromDate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);
                            this.ToDate = DateTime.Parse(new DateTime(FromDate.Value.Year, 12, 31).ToString("yyyy-MM-dd 23:59:59.000"));
                            break;
                        case "LASTMONTH":
                            this.FromDate = DateTime.Today.AddMonths(-1);
                            this.ToDate = DateTime.Today;
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

    public class ConsignmentPagingModel
    {
        public ResultList<ConsignmentEditModel> ConsignmentEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class ConsignmentUserNameModel
    {
        public List<ConsignmentEditModel> ConsignmentEditModels { get; set; }
    }

    public class ConsignmentViewModel
    {
        public ConsignmentEditModel ConsignmentEditModel { get; set; }
    }

    public class ConsignmentItemViewModel
    {
        public List<Porterage> Porterage { get; set; }
        public List<PackWtUnit> PackWtUnit { get; set; }
        public ConsignmentItemEditModel ConsignmentItemEditModel  { get; set; }
        public List<ConsignmentItemEditModel> ConsignmentItemEditModels { get; set; }
        public ConsignmentEditModel ConsignmentEditModel { get; set; }
    }

    public class ConsignmentItemArrivalEditModel
    {
        public Guid ConsignmentItemArrivalID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Guid ConsignmentItemID { get; set; }
        public DateTime ConsignmentArrivalDate { get; set; }
        public Nullable<System.Guid> StockLocationID { get; set; }
        public int Quantity { get; set; }
        public bool IsExpected { get; set; }
        public bool IsActive { get; set; }
        
    }

    public class ConsignmentDetailsViewModel
    {
        public List<FileEditModel> FileEditModels { get; set; }
        public ConsignmentItemEditModel ConsignmentItemEditModel { get; set; }
        public List<ConsignmentItemEditModel> ConsignmentItemEditModels { get; set; }
        public ConsignmentEditModel ConsignmentEditModel { get; set; }
    }

    public class ConsignmentItemEditModel
    {
        public ConsignmentEditModel Consignment { get; set; }
        public Guid ConsignmentItemID { get; set; }
        public Guid ConsignmentID { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public string Brand { get; set; }
        public string Rotation { get; set; }
        public string Pack { get; set; }
        public decimal PackWeight { get; set; }
        public string PackSize { get; set; }
        public int? PackPall { get; set; }
        public Guid PackWtUnitID { get; set; }        
        public string WtUnit { get; set; }
        public string PackType { get; set; }
        public Guid PorterageID { get; set; }
        public string PorterageCode { get; set; }
        //public virtual Porterage Porterage { get; set; }
        public Guid ProduceID { get; set; }
        public string SelectProduce { get; set; }
        public string Produce { get; set; }
        public Guid DepartmentID { get; set; }
        //public Department 
        public string SelectDept { get; set; }
        public string DepartmentName { get; set; }
        public Guid OriginCountryID { get; set; }
        public string CountryName { get; set; }
        //public decimal ExpectedQuantity { get; set; } // to be removed when ItemArrival working
        //public decimal ReceivedQuantity { get; set; } // to be removed when ItemArrival working
        public decimal? EstimatedPercentageProfit { get; set; }
        public decimal? EstimatedChargeCostPerPack { get; set; }
        public decimal? Returns { get; set; }
        public decimal? EstimatedPurchaseCostPerPack { get; set; }
        public byte Status { get; set; }
        public Guid NoteID { get; set; }
        public string NoteText { get; set; }
        public virtual Note Note { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public List<ConsignmentItemArrivalEditModel> ConsignmentItemArrivals { get; set; }
    }


    public class ConsignmentFileEditModel
    {
        public ConsignmentEditModel Consignment { get; set; }
        public Guid ConsignmentID { get; set; }
        public Guid FileID { get; set; }
        public Guid ConsignmentFileID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class FileEditModel
    {
        public ConsignmentFileEditModel ConsignmentFile { get; set; }
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class OrderViewModel {

        public ConsignmentEditModel OrderEditModel { get; set; }
        
    }

     public class OrderPagingModel
    {
        public ResultList<Consignment.ConsignmentEditModel> Orders { get; set; }
        public SearchObject SearchObject { get; set; }
    }
     public class OrderSearchObject
     {
         public string ConsignmentReference { get; set; }
         public string SupplierCode { get; set; }
         public string SupplierName { get; set; }
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
}