using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerCurrency : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerCurrency()
        {
            
        }

        public System.Guid CustomerCurrencyID { get; set; }
        public System.Guid CustomerID { get; set; }
        public System.Guid CurrencyID { get; set; }
        public Nullable<int> SortOrder { get; set; }
      
        public virtual Currency Currency { get; set; }
        public virtual Customer Customer { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
