using System.Collections.Generic;
using System;

namespace PrimeActs.Domain
{
    public partial class BankStatementItemNominalLedgerEntry : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public BankStatementItemNominalLedgerEntry()
        {

        }

        public System.Guid BankStatementItemNominalLedgerEntryID { get; set; }
        public System.Guid BankStatementItemID { get; set; }
        public System.Guid NominalLedgerEntryID { get; set; }
        public Nullable<System.Guid> UpdatedByUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
