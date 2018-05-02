using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class SalesInvoice : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SalesInvoice()
        {
      
            this.SalesInvoiceItems = new List<SalesInvoiceItem>();
        }

        public System.Guid SalesInvoiceID { get; set; }
        public System.Guid CustomerDepartmentID { get; set; }
        public Nullable<System.Guid> CustomerDepartmentAddressID { get; set; }
        public string ServerCode { get; set; }
        public string SalesInvoiceReference { get; set; }
        public System.DateTime SalesInvoiceDate { get; set; }
        public Nullable<System.Guid> SalesLedgerEntryID { get; set; }
        public Nullable<System.Guid> DivisionAddressID { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<decimal> TransactionTaxAmount { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Address DivisionAddress { get; set; }
        public virtual Address CustomerDepartmentAddress { get; set; }
         public virtual Currency Currency { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual Division Division { get; set; }
        public virtual Note Note { get; set; }
      
        public virtual SalesLedgerEntry SalesLedgerEntry { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }
    }
}
