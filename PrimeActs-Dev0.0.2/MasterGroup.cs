using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class MasterGroup : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public MasterGroup()
        {
          
            this.Produces = new List<Produce>();
        }

        public System.Guid MasterGroupID { get; set; }
        public System.Guid DivisionID { get; set; }
        public string MasterGroupCode { get; set; }
        public string MasterGroupName { get; set; }
       
        public virtual Division Division { get; set; }
        public virtual ICollection<Produce> Produces { get; set; }
    }
}
