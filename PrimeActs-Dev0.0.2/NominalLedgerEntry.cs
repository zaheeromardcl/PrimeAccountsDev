using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class NominalLedgerEntry : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public NominalLedgerEntry()
        {
          
            this.BankReconciliations = new List<BankReconciliation>();
        }

        public System.Guid NominalLedgerEntryID { get; set; }
        public System.Guid BatchNumberLogID { get; set; }
        public System.Guid NominalAccountID { get; set; }
        public string NominalLedgerEntryReference { get; set; }
        public decimal NominalLedgerEntryAmount { get; set; }
        public System.DateTime NominalLedgerEntryDate { get; set; }
        public string NominalLedgerEntryDescription { get; set; }
        public bool IsHistory { get; set; }
        public int AccountingYear { get; set; }
        public byte AccountingPeriod { get; set; }
        public Nullable<bool> IsActive { get; set; }
     
        public virtual ICollection<BankReconciliation> BankReconciliations { get; set; }
        public virtual BatchNumberLog BatchNumberLog { get; set; }
        public virtual NominalAccount NominalAccount { get; set; }
    }
}
