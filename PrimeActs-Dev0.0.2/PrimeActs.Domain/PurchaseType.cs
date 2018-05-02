using PrimeActs.Infrastructure.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class PurchaseType :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseType()
        {
            
            this.Consignments = new List<Consignment>();
        }

        public System.Guid PurchaseTypeID { get; set; }
        public string PurchaseTypeName { get; set; }
        public string PurchaseTypeCode { get; set; }
        public System.Guid CompanyID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }


        [NotMapped]
        public ObjectState ObjectState { get; set; }
        
                                              
        public virtual Company Company { get; set; }
             [ForeignKey("PurchaseTypeID")]
        public virtual ICollection<Consignment> Consignments { get; set; }


    }
}
