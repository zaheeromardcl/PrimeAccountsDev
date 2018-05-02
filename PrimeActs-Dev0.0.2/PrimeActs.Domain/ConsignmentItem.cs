using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class ConsignmentItem :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ConsignmentItem()
        {
            
            this.ConsignmentItemArrivals = new List<ConsignmentItemArrival>();
            this.PurchaseInvoiceItems = new List<PurchaseInvoiceItem>();
            this.TicketItems = new List<TicketItem>();
            this.ConsignmentItemPriceReturns = new List<ConsignmentItemPriceReturn>();
        }

        public System.Guid ConsignmentItemID { get; set; }
        public Nullable<System.Guid> ConsignmentID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public Nullable<System.DateTime> BestBeforeDate { get; set; }
        public System.Guid ProduceID { get; set; }
        public string Brand { get; set; }
        public string Rotation { get; set; }
        public string PackType { get; set; }
        public Nullable<System.Guid> PackWtUnitID { get; set; }
        public decimal PackWeight { get; set; }
        public string PackSize { get; set; }
        public Nullable<int> PackPall { get; set; }
       
        public Nullable<decimal> EstimatedProfit { get; set; }
        public Nullable<decimal> EstimatedChargeCost { get; set; }
        public Nullable<decimal> RetReduce { get; set; }
        public Nullable<decimal> EstimatedPurchaseCost { get; set; }
        public Nullable<int> ItemStatus { get; set; }
        public System.Guid PorterageID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<System.Guid> FK1 { get; set; }
        public Nullable<System.Guid> FK2 { get; set; }
        public Nullable<bool> Bit1 { get; set; }
        public Nullable<bool> Bit2 { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public Nullable<decimal> Numeric1 { get; set; }
        public Nullable<decimal> Numeric2 { get; set; }
        public Nullable<int> Int1 { get; set; }
        public Nullable<int> Int2 { get; set; }
        public Nullable<System.Guid> OriginCountryID { get; set; }
        
        public virtual Consignment Consignment { get; set; }
        public virtual Country Country { get; set; }
        public virtual Department Department { get; set; }
        public virtual Note Note { get; set; }
        public virtual PackWtUnit PackWtUnit { get; set; }
        public virtual Porterage Porterage { get; set; }
        public virtual Produce Produce { get; set; }

        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


        public int QuantityExpected { get; set; } 
        
        [NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        public virtual ICollection<ConsignmentItemArrival> ConsignmentItemArrivals { get; set; }
        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        [ForeignKey("TicketItemID")]
        public virtual ICollection<TicketItem> TicketItems { get; set; }

        public virtual ICollection<ConsignmentItemPriceReturn> ConsignmentItemPriceReturns { get; set; }
    }
}
