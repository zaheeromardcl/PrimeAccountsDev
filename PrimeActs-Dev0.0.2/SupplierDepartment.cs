using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;

namespace PrimeActs.Domain
{
    public partial class SupplierDepartment : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SupplierDepartment()
        {
            this.Consignments = new List<Consignment>();
            this.PurchaseInvoices = new List<PurchaseInvoice>();
            this.PurchaseLedgerEntries = new List<PurchaseLedgerEntry>();
            this.SupplierBankAccounts = new List<SupplierBankAccount>();
            this.SupplierContactDepartments = new List<SupplierContactDepartment>();
            this.SupplierDepartment1 = new List<SupplierDepartment>();
            this.SupplierLocations = new List<SupplierLocation>();
        }

        public System.Guid SupplierDepartmentID { get; set; }
        public System.Guid SupplierID { get; set; }
        public Nullable<System.Guid> CountryID { get; set; }
        public string SupplierDepartmentName { get; set; }
        public Nullable<int> CreditTerms { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public Nullable<decimal> Handling { get; set; }
        public Nullable<System.Guid> FactorSupplierDepartmentID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<decimal> Rebate { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<byte> EDIType { get; set; }
        public string EDINumber { get; set; }
        public string EDIIdent { get; set; }
        
        public virtual ICollection<Consignment> Consignments { get; set; }
        public virtual Note Note { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual ICollection<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public virtual ICollection<SupplierContactDepartment> SupplierContactDepartments { get; set; }
        public virtual ICollection<SupplierDepartment> SupplierDepartment1 { get; set; }
        public virtual SupplierDepartment SupplierDepartment2 { get; set; }
        public virtual ICollection<SupplierLocation> SupplierLocations { get; set; }
    }
}
