using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class TransferType : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TransferType()
        {
            this.TicketItems = new List<TicketItem>();
        }

        public System.Guid TransferTypeID { get; set; }
        public string TransferTypeName { get; set; }
        public bool IsActive { get; set; }
        public string TransferTypeCode { get; set; }
        public virtual ICollection<TicketItem> TicketItems { get; set; }
        
[System.ComponentModel.DataAnnotations.Schema.NotMapped]
public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
