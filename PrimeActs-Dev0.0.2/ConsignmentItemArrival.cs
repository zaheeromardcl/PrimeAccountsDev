using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ConsignmentItemArrival : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ConsignmentItemArrival()
        {
           
        }

        public System.Guid ConsignmentItemArrivalID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<System.Guid> ConsignmentItemID { get; set; }
        public System.DateTime ConsignmentArrivalDate { get; set; }
        public Nullable<System.Guid> StockLocationID { get; set; }
        public int Quantity { get; set; }
        public bool IsExpected { get; set; }
       
        public virtual ConsignmentItem ConsignmentItem { get; set; }
        public virtual Note Note { get; set; }
    }
}
