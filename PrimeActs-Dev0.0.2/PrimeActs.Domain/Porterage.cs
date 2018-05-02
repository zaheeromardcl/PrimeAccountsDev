using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Porterage : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Porterage()
        {
           
            this.ConsignmentItems = new List<ConsignmentItem>();
        }

        public System.Guid PorterageID { get; set; }
        public string PorterageCode { get; set; }
        public decimal UnitPrice { get; set; }
        public Nullable<decimal> MinimumAmount { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public System.Guid CompanyID { get; set; }


        public virtual Company Company { get; set; }
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
    }
}
