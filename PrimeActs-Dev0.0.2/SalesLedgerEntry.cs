using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
namespace PrimeActs.Domain
{
    public partial class SalesLedgerEntry : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SalesLedgerEntry()
        {
            this.SalesInvoices = new List<SalesInvoice>();
        }

        public System.Guid SalesLedgerEntryID { get; set; }
        public System.Guid LedgerEntryTypeID { get; set; }
        public string SalesLedgerEntryDescription { get; set; }
        public decimal SaleAmount { get; set; }
        public Nullable<decimal> FXSaleAmount { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public System.Guid CustomerDepartmentID { get; set; }
        public System.Guid BatchNumberLogID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public bool IsHistory { get; set; }
        public int AccountingYear { get; set; }
        public byte AccountingPeriod { get; set; }
        public Guid? SalesPersonUserID { get; set; }
        public DateTime? SaleDate { get; set; }

        public virtual BatchNumberLog BatchNumberLog { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual LedgerEntryType LedgerEntryType { get; set; }
        public virtual Note Note { get; set; }
    
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
    }
}
