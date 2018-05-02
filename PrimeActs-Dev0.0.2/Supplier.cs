using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
namespace PrimeActs.Domain
{
    public partial class Supplier : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Supplier()
        {
            this.ChildSuppliers = new List<Supplier>();
            this.SupplierBankAccounts = new List<SupplierBankAccount>();
            this.SupplierContacts = new List<SupplierContact>();
            this.SupplierDepartments = new List<SupplierDepartment>();
            this.SupplierLocations = new List<SupplierLocation>();
            this.TicketItems = new List<TicketItem>();
        }

        public System.Guid SupplierID { get; set; }
        public string SupplierCompanyName { get; set; }
        public Nullable<System.Guid> ParentSupplierID { get; set; }
        public string SupplierCode { get; set; }
        public Nullable<bool> IsHaulier { get; set; }
        public Nullable<bool> IsFactor { get; set; }
        public Nullable<System.Guid> CompanyID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public string TransactionTaxNo { get; set; }
        public Nullable<bool> IsEUVATExempt { get; set; }
        public virtual Company Company { get; set; }
        public virtual Note Note { get; set; }
        public virtual ICollection<Supplier> ChildSuppliers { get; set; }
        public virtual Supplier ParentSupplier { get; set; }
        public virtual ICollection<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }
        public virtual ICollection<SupplierDepartment> SupplierDepartments { get; set; }
        public virtual ICollection<SupplierLocation> SupplierLocations { get; set; }
        public virtual ICollection<TicketItem> TicketItems { get; set; }
    }
}
