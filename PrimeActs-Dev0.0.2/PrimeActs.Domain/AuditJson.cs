using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.AuditJson
{
    // C# Representations of Json, not the same as base objects, so held in seperate namespace, to help use http://json2csharp.com/# 
    
    public class ConsignmentItem
    {
        public List<object> serverErrors { get; set; }
        public int Id { get; set; }
        public string ConsignmentID { get; set; }
        public string ConsignmentItemID { get; set; }
        public string CountryName { get; set; }
        public string OriginCountryID { get; set; }
        public string BestBeforeDate { get; set; }
        public string Brand { get; set; }
        public string Produce { get; set; }
        public string Department { get; set; }
        public string Rotation { get; set; }
        public string PackType { get; set; }
        public string PackWtUnitID { get; set; }
        public string PackWeight { get; set; }
        public string PackWtUnit { get; set; }
        public string PackSize { get; set; }
        public string PackPall { get; set; }
        public string ExpectedQuantity { get; set; }
        public string ReceivedQuantity { get; set; }
        public string EstimatedPercentageProfit { get; set; }
        public string EstimatedChargeCostPerPack { get; set; }
        public string Returns { get; set; }
        public string EstimatedPurchaseCostPerPack { get; set; }
        public string PorterageID { get; set; }
        public string Porterage { get; set; }
        public string ProduceID { get; set; }
        public string ProduceName { get; set; }
        public string Consignment { get; set; }
        public string NoteID { get; set; }
        public string NoteText { get; set; }
        public bool IsCountry { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public bool DepartmentNameFocused { get; set; }
        public List<ConsignmentItemArrival> ConsignmentItemArrivals { get; set; }
        public List<object> errors { get; set; }
        public List<object> Errors { get; set; }
        public bool ShowErrors { get; set; }
        public bool isUK { get; set; }
        public bool hasPackWeight { get; set; }
    }

    public class ConsignmentItemArrival
    {
        public string ConsignmentItemArrivalID { get; set; }
        public string NoteID { get; set; }
        public string ConsignmentItemID { get; set; }
        public string ConsignmentArrivalDate { get; set; }
        public int QuantityReceived { get; set; }
    }

    public class ConsignmentRootObject
    {
        public string ConsignmentReference { get; set; }
        public string ConsignmentDescription { get; set; }
        public string ConsignmentID { get; set; }
        public string ContractDate { get; set; }
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        //public string DepartmentID { get; set; }
        //public string DepartmentName { get; set; }
        //public string DepartmentCode { get; set; }
        public string DivisionID { get; set; }
        public string DivisionName { get; set; }
        public string DespatchDate { get; set; }
        public string DespatchID { get; set; }
        public string DespatchName { get; set; }
        public bool DisplayDespatchLocation { get; set; }
        public bool DisplayPort { get; set; }
        public string Handling { get; set; }
        public bool IsSaved { get; set; }
        public string NoteID { get; set; }
        public string NoteText { get; set; }
        public string PortID { get; set; }
        public string PortName { get; set; }
        public string PurchaseTypeDescription { get; set; }
        public string PurchaseTypeID { get; set; }
        public string ReceivedDate { get; set; }
        public string SelectPurchaseType { get; set; }
        public string SupplierCompanyName { get; set; }
        public string SupplierDepartmentID { get; set; }
        public string SupplierDepartmentName { get; set; }
        public string SupplierID { get; set; }
        public string SupplierReference { get; set; }
        public string Vehicle { get; set; }
        public string VehicleDetail { get; set; }
        public string Commission { get; set; }
        public List<ConsignmentItem> ConsignmentItems { get; set; }
        public bool MultipleConsignmentItems { get; set; }
        public string DefaultDepartmentID { get; set; }
        public string DefaultDepartmentName { get; set; }
        public bool tedit { get; set; }
        public bool pedit { get; set; }
        public List<object> serverErrors { get; set; }
        public List<object> Errors { get; set; }
        public bool ShowErrors { get; set; }
        public bool SupplierHasFocus { get; set; }
    }

    public class TicketItem
    {
        public List<object> serverErrors { get; set; }
        public string TicketID { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DefaultDepartmentID { get; set; }
        public string DefaultDepartmentCode { get; set; }
        public string DepartmentCode { get; set; }
        public string ConsignmentItemID { get; set; }
        public bool IncludeZeroCheckBox { get; set; }
        public bool IsSaving { get; set; }
        public bool IsDirty { get; set; }
        public bool FocusDepartment { get; set; }
        public bool FocusProduce { get; set; }
        public bool FocusQty { get; set; }
        public bool FocusUnitPrice { get; set; }
        public bool DepartmentVisited { get; set; }
        public bool ConsignmentItemVisited { get; set; }
        public bool QuantityVisited { get; set; }
        public bool UnitPriceVisited { get; set; }
        public bool DepartmentInputDone { get; set; }
        public bool ConsignmentItemInputDone { get; set; }
        public bool QuantityInputDone { get; set; }
        public bool UnitPriceInputDone { get; set; }
        public bool TicketItemInputDone { get; set; }
        public List<string> errors { get; set; }
        public List<string> Errors { get; set; }
        public bool ShowErrors { get; set; }
    }

    public class TicketRootObject
    {
        public List<TicketItem> TicketItems { get; set; }
        public string TicketID { get; set; }
        public object PONumber { get; set; }
        public string TicketReference { get; set; }
        public string CustomerCompanyName { get; set; }
        public string CustomerDepartmentID { get; set; }
        public object NoteID { get; set; }
        public object Notes { get; set; }
        public string TicketDate { get; set; }
        public string CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyRate { get; set; }
        public string CustomerCurrencyID { get; set; }
        public string SalesPersonUserID { get; set; }
        public string SalesPersonName { get; set; }
        public string SalesPersonDepartmentID { get; set; }
        public string SalesPersonDepartmentName { get; set; }
        public string SalesPersonDepartmentCode { get; set; }
        public bool IsCashSale { get; set; }
        public object UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public List<object> serverErrors { get; set; }
        public string SelectedPaymentType { get; set; }
        public bool FocusTicketReference { get; set; }
        public List<object> errors { get; set; }
        public List<object> Errors { get; set; }
        public bool ShowErrors { get; set; }
    }

    public class PurchaseInvoiceItem
    {
        public string PurchaseInvoiceID { get; set; }
        public string PurchaseInvoiceItemID { get; set; }
        public string ConsignmentItemID { get; set; }
        public string Description { get; set; }
        public object ExchangeRate { get; set; }
        public string PurchaseDate { get; set; }
        public object Currency { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public object CurrencyAmount { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public object UpdatedDate { get; set; }
        public string PreviousConsignmentItemID { get; set; }
        public string PreviousDescription { get; set; }
        public bool IsSaving { get; set; }
        public bool IsDirty { get; set; }
        public bool IncludeZeroCheckBox { get; set; }
        public decimal TotalPriceReadOnly { get; set; }
        public decimal TotalPrice { get; set; }
        public bool FocusConsignment { get; set; }
        public List<object> errors { get; set; }
        public List<object> Errors { get; set; }
        public bool ShowErrors { get; set; }
    }

    public class PurchaseInvoiceRootObject
    {
        public List<object> serverErrors { get; set; }
        public bool ChargesIsVisible { get; set; }
        public string PurchaseInvoiceReference { get; set; }
        public string fileName { get; set; }
        public string UploadFolder { get; set; }
        public List<object> UploadedFileNames { get; set; }
        public bool Created { get; set; }
        public bool AttachFilesVisible { get; set; }
        public bool NoMoreAttachments { get; set; }
        public int SupplierInvoiceAmount { get; set; }
        public string PurchaseInvoiceDate { get; set; }
        public string SupplierDepartmentName { get; set; }
        public string SupplierDepartmentID { get; set; }
        public string PurchaseInvoiceId { get; set; }
        public Address Address { get; set; }
        public List<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public bool HasItems { get; set; }
        public int VAT { get; set; }
        public string PreviousSupplierDepartmentID { get; set; }
        public string PreviousSupplierDepartmentName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public object UpdatedDate { get; set; }
        public bool IsSaved { get; set; }
        public bool IsSaving { get; set; }
        public bool IsDirty { get; set; }
        public bool FocusAddItems { get; set; }
        public int TotalOfItems { get; set; }
        public int Total { get; set; }
        public List<object> errors { get; set; }
        public List<object> Errors { get; set; }
        public bool ShowErrors { get; set; }
        public bool SupplierHasFocus { get; set; }
    }
}
