using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class tgenConsignmentNumber :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid ConsignmentID { get; set; }
        public int ConsignmentNumber { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
