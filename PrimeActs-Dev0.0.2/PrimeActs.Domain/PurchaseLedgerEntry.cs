using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseLedgerEntry :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseLedgerEntry()
        {
           
        
            //this.PurchaseInvoices = new List<PurchaseInvoice>();
        }

        public System.Guid PurchaseLedgerEntryID { get; set; }
        public System.Guid LedgerEntryTypeID { get; set; }
        public string PurchaseLedgerEntryDescription { get; set; }
        public decimal PurchaseAmount { get; set; }
        public double CurrencyAmount { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public System.Guid PurchaseInvoiceID { get; set; }
        public System.Guid BatchNumberLogID { get; set; }
        //PE removed from table, update to code 6/1/17
        //public System.Guid SupplierDepartmentID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        //DB changes: 10/11/2016: columen renamed isallocated
        public bool IsAllocated { get; set; }
        public Nullable<decimal> TransactionTaxAmount { get; set; }
        public int AccountingYear { get; set; }
        //DB changes: 10/11/2016: columen deleted
        //public byte AccountingPeriod { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
     
        public virtual BatchNumberLog BatchNumberLog { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual LedgerEntryType LedgerEntryType { get; set; }
        public virtual Note Note { get; set; }
 
        public virtual SupplierDepartment SupplierDepartment { get; set; }
    }
}
