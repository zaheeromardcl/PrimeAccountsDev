using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Price : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Price()
        {
            
        }

        public System.Guid PriceID { get; set; }
        public string CurrentPrice { get; set; }
        public System.DateTime PriceDateTime { get; set; }
        
    }
}
