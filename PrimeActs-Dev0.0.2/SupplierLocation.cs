using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;

namespace PrimeActs.Domain
{
    
    public partial class SupplierLocation: AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SupplierLocation()
        {
            
            this.SupplierBankAccounts = new List<SupplierBankAccount>();
            this.SupplierContactLocations = new List<SupplierContactLocation>();
            this.SupplierDepartments = new List<SupplierDepartment>();
        }

        public System.Guid SupplierLocationID { get; set; }
        public System.Guid SupplierID { get; set; }
        public string SupplierLocationName { get; set; }
        public System.Guid AddressID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public virtual Address Address { get; set; }
        public virtual Note Note { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public virtual ICollection<SupplierContactLocation> SupplierContactLocations { get; set; }
        public virtual ICollection<SupplierDepartment> SupplierDepartments { get; set; }
    }
}
