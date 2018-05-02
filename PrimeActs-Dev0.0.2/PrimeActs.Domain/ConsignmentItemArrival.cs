using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ConsignmentItemArrival :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ConsignmentItemArrival()
        {
           
        }

        public System.Guid ConsignmentItemArrivalID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<System.Guid> ConsignmentItemID { get; set; }
        //public System.DateTime ConsignmentArrivalDate { get; set; }
        public System.DateTime ConsignmentItemArrivalDate { get; set; }
        public Nullable<System.Guid> StockLocationID { get; set; }
        public int QuantityReceived { get; set; }
        
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        public virtual ConsignmentItem ConsignmentItem { get; set; }
        public virtual Note Note { get; set; }
    }
}
