using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;

namespace PrimeActs.Domain
{
    public partial class SalesInvoiceItem :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SalesInvoiceItem()
        {
        }

        public System.Guid SalesInvoiceItemID { get; set; }
        public System.Guid SalesInvoiceID { get; set; }
        public string SalesInvoiceItemDescription { get; set; }
        public decimal SalesInvoiceItemLineTotal { get; set; }
      
        //public decimal TransactionTaxRatePercentage { get; set; }
        public System.Guid TransactionTaxRateID { get; set; }
        public System.Guid TicketItemID { get; set; }
        
        public Nullable<decimal> CurrencyAmount { get; set; }
        public virtual SalesInvoice SalesInvoice { get; set; }
        public virtual TicketItem TicketItem { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
             [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
