using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Country : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Country()
        {
            this.ConsignmentItems = new List<ConsignmentItem>();
        }

        public System.Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public System.Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
    }
}
