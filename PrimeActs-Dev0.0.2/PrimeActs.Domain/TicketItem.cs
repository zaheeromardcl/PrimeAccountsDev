using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrimeActs.Domain
{
    public partial class TicketItem :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TicketItem()
        {
            
            this.SalesInvoiceItems = new List<SalesInvoiceItem>();
        }

        public System.Guid TicketItemID { get; set; }
        public System.Guid TicketID { get; set; }
        public string TicketItemDescription { get; set; }
        public decimal TicketItemQuantity { get; set; }
        public decimal TicketItemTotalPrice { get; set; }
        public Nullable<System.Guid> ConsignmentItemID { get; set; }
        public Nullable<System.Guid> HaulierSupplierID { get; set; }

        public Nullable<System.Guid> TransactionTaxRateID { get; set; }
        
        public Nullable<decimal> CurrencyAmount { get; set; }
        public Nullable<System.Guid> PorterageID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public decimal PorterageValue { get; set; }
        public Nullable<System.Guid> OriginalTicketItemID { get; set; }
        public Nullable<bool> IsLatest { get; set; }
        public Nullable<System.Guid> TransferTypeID { get; set; }
        [NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
         [ForeignKey("ConsignmentItemID")]
        public virtual ConsignmentItem ConsignmentItem { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual TransferType TransferType { get; set; }

        [ForeignKey("TransactionTaxRateID")]
        public virtual TransactionTaxRate TransactionTaxRate { get; set; }
        public virtual Department Department { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
