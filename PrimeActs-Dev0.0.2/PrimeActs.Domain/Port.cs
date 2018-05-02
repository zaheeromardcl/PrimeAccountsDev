using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Port : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Port()
        {
            
            this.Consignments = new List<Consignment>();
        }

        public System.Guid PortID { get; set; }
        public string PortName { get; set; }
        public string PortCode { get; set; }
        public System.Guid CompanyID { get; set; }
        
        public virtual Company Company { get; set; }
        public virtual ICollection<Consignment> Consignments { get; set; }
    }
}
