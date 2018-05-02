using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class LedgerEntryType :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public LedgerEntryType()
        {
          
          //  this.PurchaseLedgerEntries = new List<PurchaseLedgerEntry>();
            this.SalesLedgerEntries = new List<SalesLedgerEntry>();
        }

        public System.Guid LedgerEntryTypeID { get; set; }
        public string LedgerEntryTypeDescription { get; set; }
        public int LedgerEntryTypeNumber { get; set; }
        public bool IsActive { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
       // public virtual ICollection<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
        public virtual ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }
    }
}
