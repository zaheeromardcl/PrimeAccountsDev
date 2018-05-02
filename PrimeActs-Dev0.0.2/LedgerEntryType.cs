using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class LedgerEntryType : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public LedgerEntryType()
        {
          
          //  this.PurchaseLedgerEntries = new List<PurchaseLedgerEntry>();
            this.SalesLedgerEntries = new List<SalesLedgerEntry>();
        }

        public System.Guid LedgerEntryTypeID { get; set; }
        public string LedgerEntryTypeDescription { get; set; }
        public byte LedgerEntryTypeNumber { get; set; }
     
       // public virtual ICollection<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
        public virtual ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }
    }
}
