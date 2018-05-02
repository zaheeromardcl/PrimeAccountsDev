using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class WarehouseLocation : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public WarehouseLocation()
        {
            
        }

        public System.Guid WarehouseLocationID { get; set; }
        public string WarehouseLocationName { get; set; }
                                                public System.Guid CompanyID { get; set; }
        
        public virtual Company Company { get; set; }
    }
}
