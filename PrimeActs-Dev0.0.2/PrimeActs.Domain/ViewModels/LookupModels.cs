#region

using System;
using System.Collections.Generic;
using System.Globalization;
using PrimeActs.Domain.ViewModels.Produce;
using PrimeActs.Domain.ViewModels.Users;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    class LookupModels
    {
    }
    public class LookupLists
    {
        //Default values for drop down boxes
        public string DefaultCompanyID { get; set; }
        public string DefaultDivisionID { get; set; }
        public string DefaultDepartmentID { get; set; }
        //Actual lookup tables for reuse
        public List<Country> Country { get; set; }
        public List<CreditRating> CreditRating { get; set; }
        public List<Currency> Currency { get; set; }
        public List<CustomerType> CustomerType { get; set; }
        public List<DespatchLocation> DespatchLocation { get; set; }
        public List<PackWtUnit> PackWtUnit { get; set; }
        public List<Port> Port { get; set; }
        public List<Porterage> Porterage { get; set; }
        public List<PurchaseChargeType> PurchaseChargeType { get; set; }
        public List<PurchaseType> PurchaseType { get; set; }
        public List<StockLocation> StockLocation { get; set; }
        public List<TransactionTaxLocation> TransactionTaxLocation { get; set; }
        public List<WarehouseLocation> WarehouseLocation { get; set; }
        public List<vwPermissionDetail> PermissionDetail { get; set; }
        public List<CompanyDropDown> Company { get; set; }
        public List<DivisionDropDown> Division { get; set; }
        public List<DepartmentDropDown> Department { get; set; }
    }
    public class CompanyDropDown
    {
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
    }
    public class DivisionDropDown
    {
        public string DivisionID { get; set; }
        public string CompanyID { get; set; }
        public string DivisionName { get; set; }
        public bool IsActive { get; set; }
        public bool AutogenerateConsignnmentReference { get; set; }
    }
    public class DepartmentDropDown
    {
        public string DepartmentID { get; set; }
        public string DivisionID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public bool IsActive { get; set; }
    }
}
