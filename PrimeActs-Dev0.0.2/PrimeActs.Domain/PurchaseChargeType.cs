using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseChargeType : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseChargeType()
        {
            
        }

        public System.Guid PurchaseChargeTypeID { get; set; }
        public string PurchaseChargeTypeCode { get; set; }
        public string PurchaseChargeTypeName { get; set; }
        public System.Guid NominalAccountID { get; set; }
        public System.Guid CompanyID { get; set; }
                                                 
        public virtual Company Company { get; set; }
    }
}
