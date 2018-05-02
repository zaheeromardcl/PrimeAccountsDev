using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseInvoiceItem :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseInvoiceItem()
        {
            
        }

        public System.Guid PurchaseInvoiceItemID { get; set; }
        public Nullable<System.Guid> ConsignmentItemID { get; set; }
        public decimal TotalPrice { get; set; }
        public Nullable<decimal> CurrencyAmount { get; set; }
        
        
        public System.Guid PurchaseInvoiceID { get; set; }
        public Nullable<decimal> PurchaseInvoiceItemQuantity { get; set; }
        public Nullable<DateTime> PurchaseDate { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public System.Guid TransactionTaxRateID { get; set; }
        //public Nullable<System.Guid> BankAccountID { get; set; }

        public string PurchaseInvoiceItemDescription { get; set; }

        public virtual ConsignmentItem ConsignmentItem { get; set; }
        
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

        public Nullable<System.Guid> PurchaseInvoiceItemChargeTypeID { get; set; }
    }
}
