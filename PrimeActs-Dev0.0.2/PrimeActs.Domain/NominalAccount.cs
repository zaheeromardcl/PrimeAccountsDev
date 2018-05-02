using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class NominalAccount : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public NominalAccount()
        {
          
            this.CompanyNominalAccounts = new List<CompanyNominalAccount>();
            this.NominalLedgerEntries = new List<NominalLedgerEntry>();
        }

        public System.Guid NominalAccountID { get; set; }
        public string NominalAccountName { get; set; }
        public string NominalCode { get; set; }
        public bool IsPandL { get; set; }
        public bool IsBroughtForward { get; set; }
        public bool IsCurrent { get; set; }
        public Nullable<System.Guid> BankAccountID { get; set; }
        public bool IsPettyCashAccount { get; set; }
        public bool IsSystem { get; set; }
        public System.Guid CompanyID { get; set; }
         public virtual BankAccount BankAccount { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<CompanyNominalAccount> CompanyNominalAccounts { get; set; }
        public virtual ICollection<NominalLedgerEntry> NominalLedgerEntries { get; set; }
    }
}
