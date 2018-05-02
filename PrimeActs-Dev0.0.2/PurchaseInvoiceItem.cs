using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseInvoiceItem : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseInvoiceItem()
        {
            
        }

        public System.Guid PurchaseInvoiceItemID { get; set; }
        public Nullable<System.Guid> ConsignmentItemID { get; set; }
        public decimal TotalPrice { get; set; }
        public Nullable<decimal> FXTotalPrice { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public System.Guid PurchaseInvoiceID { get; set; }
        public decimal? Quantity { get; set; }
                 
        public virtual ConsignmentItem ConsignmentItem { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
    }
}
