using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ProduceIntrastat : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ProduceIntrastat()
        {
            
        }

        public System.Guid ProduceIntrastatID { get; set; }
        public System.Guid ProduceID { get; set; }
        public int IntrastatCode { get; set; }
        public System.DateTime EndDate { get; set; }
        
        public virtual Produce Produce { get; set; }
    }
}
