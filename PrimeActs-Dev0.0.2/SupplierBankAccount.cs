using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class SupplierBankAccount : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SupplierBankAccount()
        {
        }

        public System.Guid SupplierBankAccountID { get; set; }
        public System.Guid BankAccountID { get; set; }
        public Nullable<System.Guid> SupplierID { get; set; }
        public Nullable<System.Guid> SupplierDepartmentID { get; set; }
        public Nullable<System.Guid> SupplierLocationID { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual SupplierDepartment SupplierDepartment { get; set; }
        public virtual SupplierLocation SupplierLocation { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}

