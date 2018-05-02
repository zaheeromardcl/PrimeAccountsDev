using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerContactLocation : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid CustomerContactLocationID { get; set; }
        public System.Guid CustomerContactID { get; set; }
        public System.Guid CustomerLocationID { get; set; }
        public virtual CustomerContact CustomerContact { get; set; }
        public virtual CustomerLocation CustomerLocation { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
