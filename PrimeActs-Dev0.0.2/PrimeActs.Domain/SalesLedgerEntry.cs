using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
namespace PrimeActs.Domain
{
    public partial class SalesLedgerEntry : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SalesLedgerEntry()
        {
            //this.SalesInvoices = new List<SalesInvoice>();
            this.SalesLedgerInvoiceAllocations = new List<SalesLedgerInvoiceAllocation>();
        }

        public System.Guid SalesLedgerEntryID { get; set; }
        public System.Guid LedgerEntryTypeID { get; set; }
        public string SalesLedgerEntryDescription { get; set; }
        public decimal SaleAmount { get; set; }
        public Nullable<decimal> CurrencyAmount { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public System.Guid CustomerDepartmentID { get; set; }
        public System.Guid BatchNumberLogID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }

        //DB changes: 10/11/2016: columen deleted
        //public bool IsHistory { get; set; }
        public int AccountingYear { get; set; }
        //DB changes: 10/11/2016: columen deleted
        //public int AccountingPeriod { get; set; }
        public Guid? SalesPersonUserID { get; set; }
        
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        public virtual BatchNumberLog BatchNumberLog { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual ApplicationUser SalesPerson { get; set; }
        public virtual LedgerEntryType LedgerEntryType { get; set; }
        public virtual Note Note { get; set; }
        public ICollection<SalesLedgerInvoiceAllocation> SalesLedgerInvoiceAllocations { get; set; }

        //public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
    }
}
