using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class tgenDeliveryNoteNumber :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid DeliveryNoteID { get; set; }
        public int DeliveryNoteNumber { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

}
}
