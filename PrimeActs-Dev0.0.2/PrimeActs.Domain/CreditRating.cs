using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CreditRating : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CreditRating()
        {
        }

        public System.Guid CreditRatingID { get; set; }
        public string CreditRatingCode { get; set; }
        public string CreditRatingDescription { get; set; }
        public System.Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}
