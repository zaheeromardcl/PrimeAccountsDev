using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
   // public partial class TransactionTaxLocation : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    public partial class TransactionTaxLocation : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TransactionTaxLocation() { }

        public System.Guid TransactionTaxLocationID { get; set; }
        public string TransactionTaxLocationName { get; set; }
        public System.Guid TransactionTaxLocationNominalAccountID { get; set; }
        public string TransactionTaxDisplayName { get; set; }
        public string TransactionTaxReference { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public System.Guid CreatedByUserID { get; set; }
        public System.Guid CompanyID { get; set; }

          [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
