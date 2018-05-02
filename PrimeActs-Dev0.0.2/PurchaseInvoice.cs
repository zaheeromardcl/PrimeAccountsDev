using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseInvoice : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseInvoice()
        {


            this.PurchaseInvoiceItems = new List<PurchaseInvoiceItem>();
        }

        public System.Guid PurchaseInvoiceID { get; set; }
        public System.Guid SupplierDepartmentID { get; set; }
        public System.Guid AddressID { get; set; }
        public string PurchaseInvoiceNumber { get; set; }
        public string ServerCode { get; set; }
        //public System.Guid PurchaseLedgerEntryID { get; set; }
        public System.DateTime PurchaseInvoiceDate { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public virtual Address Address { get; set; }

        public virtual Note Note { get; set; }

        //public virtual PurchaseLedgerEntry PurchaseLedgerEntry { get; set; }
        public virtual SupplierDepartment SupplierDepartment { get; set; }
        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public bool IsSaved { get; set; }
        public decimal? Total { get; set; }
        public int? Status { get; set; }
    }
}
