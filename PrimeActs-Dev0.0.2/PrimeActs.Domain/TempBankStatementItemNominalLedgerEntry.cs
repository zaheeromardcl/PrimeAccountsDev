using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class TempBankStatementItemNominalLedgerEntry : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TempBankStatementItemNominalLedgerEntry()
         {
             
         }

        public System.Guid BankStatementItemNominalLedgerEntryID { get; set; }
        public System.Guid BankStatementItemID { get; set; }
        public System.Guid NominalLedgerEntryID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
