using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class BatchNumberLog : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public BatchNumberLog()
        {
            
            this.NominalLedgerEntries = new List<NominalLedgerEntry>();
           // this.PurchaseLedgerEntries = new List<PurchaseLedgerEntry>();
          this.SalesLedgerEntries = new List<SalesLedgerEntry>();
        }

        public System.Guid BatchNumberLogID { get; set; }
        public System.Guid CompanyID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public string ServerPrefix { get; set; }
        public int BatchNumber { get; set; }
        public Nullable<System.DateTime> TransactionDateTime { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        
        public virtual Company Company { get; set; }
        public virtual Division Division { get; set; }
        public virtual ICollection<NominalLedgerEntry> NominalLedgerEntries { get; set; }
  //      public virtual ICollection<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
       public virtual ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }
    }
}
