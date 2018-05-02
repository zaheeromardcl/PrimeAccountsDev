using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class ProduceTransactionTaxCode : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ProduceTransactionTaxCode()
        {

        }

        public System.Guid ProduceTransactionTaxCodeID { get; set; }
        public System.Guid ProduceID { get; set; }
        public System.Guid TransactionTaxCodeID { get; set; }
        public System.Guid TransactionTaxLocationID { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
