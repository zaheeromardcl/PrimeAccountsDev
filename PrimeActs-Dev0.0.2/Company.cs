using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Company : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Company()
        {
            
            this.BatchNumberLogs = new List<BatchNumberLog>();
            this.ChildCompanies = new List<Company>();
            this.CompanyNominalAccounts = new List<CompanyNominalAccount>();
            this.Countries = new List<Country>();
            this.CreditRatings = new List<CreditRating>();
            this.Currencies = new List<Currency>();
            this.Customers = new List<Customer>();
            this.CustomerTypes = new List<CustomerType>();
            this.DespatchLocations = new List<DespatchLocation>();
            this.Divisions = new List<Division>();
            this.NominalAccounts = new List<NominalAccount>();
            this.PackWtUnits = new List<PackWtUnit>();
            this.Ports = new List<Port>();
            this.Porterages = new List<Porterage>();
            this.PurchaseChargeTypes = new List<PurchaseChargeType>();
            this.PurchaseTypes = new List<PurchaseType>();
            this.RoleContexts = new List<RoleContext>();
            this.StockLocations = new List<StockLocation>();
            this.Suppliers = new List<Supplier>();
            this.TransactionTaxCodes = new List<TransactionTaxCode>();
            this.WarehouseLocations = new List<WarehouseLocation>();
            this.ApplicationUsers = new List<ApplicationUser>();
        }

        public System.Guid CompanyID { get; set; }
        public Nullable<System.Guid> ParentCompanyID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<System.Guid> RegisteredAddressID { get; set; }
        public string TransactionTaxNo { get; set; }
        public string CompanyNo { get; set; }
        public byte[] Logo { get; set; }
        public string Telephone { get; set; }
        public string FaxNo { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string InvoiceInfo { get; set; }
        public virtual Address Address { get; set; }
        public virtual Address RegisteredAddress { get; set; }
       
        public virtual ICollection<BatchNumberLog> BatchNumberLogs { get; set; }
        public virtual ICollection<Company> ChildCompanies { get; set; }
        public virtual Company ParentCompany { get; set; }
        public virtual ICollection<CompanyNominalAccount> CompanyNominalAccounts { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<CreditRating> CreditRatings { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CustomerType> CustomerTypes { get; set; }
        public virtual ICollection<DespatchLocation> DespatchLocations { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
        public virtual ICollection<NominalAccount> NominalAccounts { get; set; }
        public virtual ICollection<PackWtUnit> PackWtUnits { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<Porterage> Porterages { get; set; }
        public virtual ICollection<PurchaseChargeType> PurchaseChargeTypes { get; set; }
        public virtual ICollection<PurchaseType> PurchaseTypes { get; set; }
        public virtual ICollection<RoleContext> RoleContexts { get; set; }
        public virtual ICollection<StockLocation> StockLocations { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<TransactionTaxCode> TransactionTaxCodes { get; set; }
        public virtual ICollection<WarehouseLocation> WarehouseLocations { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
