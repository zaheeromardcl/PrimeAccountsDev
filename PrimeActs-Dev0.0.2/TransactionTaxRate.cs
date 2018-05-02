using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class TransactionTaxRate : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TransactionTaxRate()
        {
           
        }

        public System.Guid TransactionTaxRateID { get; set; }
        public decimal TransactionTaxRatePercentage { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.Guid> TransactionTaxCodeID { get; set; }
        
        public virtual TransactionTaxCode TransactionTaxCode { get; set; }
    }
}
