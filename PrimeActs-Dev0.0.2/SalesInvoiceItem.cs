using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;

namespace PrimeActs.Domain
{
    public partial class SalesInvoiceItem : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SalesInvoiceItem()
        {
        }

        public System.Guid SalesInvoiceItemID { get; set; }
        public System.Guid SalesInvoiceID { get; set; }
        public string SalesInvoiceItemDescription { get; set; }
        public decimal SalesInvoiceItemLineTotal { get; set; }
        public System.Guid TicketItemID { get; set; }
        public decimal SalesInvoiceItemVAT { get; set; }
        public Nullable<decimal> CurrencyAmount { get; set; }
        public virtual SalesInvoice SalesInvoice { get; set; }
        public virtual TicketItem TicketItem { get; set; }
    }
}
