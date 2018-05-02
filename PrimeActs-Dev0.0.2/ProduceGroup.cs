using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ProduceGroup : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ProduceGroup()
        {
        
            this.Produces = new List<Produce>();
        }

        public System.Guid ProduceGroupID { get; set; }
        public System.Guid DivisionID { get; set; }
        public string ProduceGroupCode { get; set; }
        public string ProduceGroupName { get; set; }
        
        public virtual Division Division { get; set; }
        public virtual ICollection<Produce> Produces { get; set; }
    }
}
