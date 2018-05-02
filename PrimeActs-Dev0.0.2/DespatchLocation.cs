using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class DespatchLocation : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public DespatchLocation()
        {
           
            this.Consignments = new List<Consignment>();
        }

        public System.Guid DespatchLocationID { get; set; }
        public string DespatchLocationCode { get; set; }
        public string DespatchLocationName { get; set; }
        public System.Guid CompanyID { get; set; }
   
        public virtual Company Company { get; set; }
        public virtual ICollection<Consignment> Consignments { get; set; }
    }
}
