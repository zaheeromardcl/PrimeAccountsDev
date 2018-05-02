using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class BankReconciliation : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid BankReconciliationID { get; set; }
        public System.DateTime BankReconcilitationDate { get; set; }
        public System.Guid NominalLedgerEntryID { get; set; }
        public int TransactionID { get; set; }
        public double TransactionAmount { get; set; }
        public System.Guid UserID { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual NominalLedgerEntry NominalLedgerEntry { get; set; }
    }
}
