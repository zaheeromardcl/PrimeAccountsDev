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
            this.SupplierContacts = new List<SupplierContact>();
            this.SupplierLocations = new List<SupplierLocation>();
            this.SupplierDepartments = new List<SupplierDepartment>();
            this.SupplierBankAccounts = new List<SupplierBankAccount>();
            this.TicketItems = new List<TicketItem>();
            //this.SupplierItems = new List<SupplierItem>();
        }

        public Guid SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierCompanyName { get; set; }
        public Nullable<Guid> ParentSupplierID { get; set; }
        public Nullable<Guid> CompanyID { get; set; }
        public Nullable<Guid> NoteID { get; set; }
        public Nullable<bool> IsHaulier { get; set; }
        public Nullable<bool> IsFactor { get; set; }

        public virtual Note Note { get; set; }
        public virtual Company Company { get; set; }
        public virtual Supplier ParentSupplier { get; set; }
        public virtual List<Supplier> ChildSuppliers { get; set; }
        public virtual List<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public virtual List<SupplierContact> SupplierContacts { get; set; }
        public virtual List<SupplierDepartment> SupplierDepartments { get; set; }
        public virtual List<SupplierLocation> SupplierLocations { get; set; }
        public virtual List<TicketItem> TicketItems { get; set; }
        //public virtual ICollection<SupplierItem> SupplierItems { get; set; }
    }
}
