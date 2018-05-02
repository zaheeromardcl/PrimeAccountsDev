using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class TransactionTaxRate :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TransactionTaxRate()
        {
           
        }

        public System.Guid TransactionTaxRateID { get; set; }
        public decimal TransactionTaxRatePercentage { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.Guid> TransactionTaxCodeID { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal MinimumUnitCost { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
      //  public virtual TransactionTaxCode TransactionTaxCode { get; set; }
    }
}
