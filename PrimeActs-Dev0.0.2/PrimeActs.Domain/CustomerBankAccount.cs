using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerBankAccount : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerBankAccount()
        {
           
        }

        public System.Guid CustomerBankAccountID { get; set; }
        public System.Guid BankAccountID { get; set; }
        public System.Guid CustomerID { get; set; }
        public Nullable<System.Guid> CustomerDepartmentID { get; set; }
        public Nullable<System.Guid> CustomerLocationID { get; set; }
  
      
        public virtual BankAccount BankAccount { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual CustomerLocation CustomerLocation { get; set; }

    }
}
