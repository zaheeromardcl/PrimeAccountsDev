using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class NominalLedgerEntry :  PrimeActs.Infrastructure.BaseEntities.IObjectState
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
        public System.Guid LedgerEntryTypeID {get;set;}
        public decimal CurrencyAmount {get;set;}
        public Nullable<System.Guid> CurrencyID { get; set; }
        public decimal ExchangeRate {get;set;}

        //DB changes 10/11/2016:renamed column:
        //IsHistory -- renamed to IsReconciled
        public bool IsReconciled { get; set; }

        public int AccountingYear { get; set; }
        //public byte AccountingPeriod { get; set; }
        //DB changes 10/11/2016:deleted column
        //public int? AccountingPeriod { get; set; } 
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
     
        public virtual ICollection<BankReconciliation> BankReconciliations { get; set; }
        public virtual BatchNumberLog BatchNumberLog { get; set; }
        public virtual NominalAccount NominalAccount { get; set; }
    }
}
