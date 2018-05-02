using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class TempBankNominalLedgerEntry : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TempBankNominalLedgerEntry()
        {
            
        }

        public System.Guid TempBankNominalLedgerEntryID { get; set; }
        public System.Guid BankStatementID { get; set; }
        public DateTime BankReconciliationDate { get; set; }
        public System.Guid NominalLedgerEntryID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TempDescriptionn { get; set; }
        public string TransactionType { get; set; }
        public bool? IsReconciled { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
