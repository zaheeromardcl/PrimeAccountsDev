using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseType : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseType()
        {
            
            this.Consignments = new List<Consignment>();
        }

        public System.Guid PurchaseTypeID { get; set; }
        public string PurchaseTypeName { get; set; }
        public string PurchaseTypeCode { get; set; }
        public System.Guid CompanyID { get; set; }
                                              
        public virtual Company Company { get; set; }
        public virtual ICollection<Consignment> Consignments { get; set; }
    }
}
