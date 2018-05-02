using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PackWtUnit : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PackWtUnit()
        {
            
            this.ConsignmentItems = new List<ConsignmentItem>();
        }

        public System.Guid PackWtUnitID { get; set; }
        public string WtUnit { get; set; }
        public Nullable<decimal> KgMultiple { get; set; }
        public System.Guid CompanyID { get; set; }
        
        public virtual Company Company { get; set; }
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
    }
}
