using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Address : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Address()
        {
            this.Companies = new List<Company>();
            this.RegisteredAddressCompanies = new List<Company>();
            this.CustomerLocations = new List<CustomerLocation>();
            this.Departments = new List<Department>();
            this.Divisions = new List<Division>();
            this.PurchaseInvoices = new List<PurchaseInvoice>();
            this.CustomerDepartmentAddressSalesInvoices = new List<SalesInvoice>();
            this.DivisionAddressSalesInvoices = new List<SalesInvoice>();
            this.StockLocations = new List<StockLocation>();
            this.SupplierLocations = new List<SupplierLocation>();
        }

        public System.Guid AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalTown { get; set; }
        public string CountyCity { get; set; }
        public string Postcode { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Company> RegisteredAddressCompanies { get; set; }
        public virtual ICollection<CustomerLocation> CustomerLocations { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual ICollection<SalesInvoice> CustomerDepartmentAddressSalesInvoices { get; set; }
        public virtual ICollection<SalesInvoice> DivisionAddressSalesInvoices { get; set; }
        public virtual ICollection<StockLocation> StockLocations { get; set; }
        public virtual ICollection<SupplierLocation> SupplierLocations { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
