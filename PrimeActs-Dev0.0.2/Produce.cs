using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Produce : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Produce()
        {
           
            this.ConsignmentItems = new List<ConsignmentItem>();
            this.ProduceIntrastats = new List<ProduceIntrastat>();
        }

        public System.Guid ProduceID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public string ProduceCode { get; set; }
        public string ProduceName { get; set; }
        public System.Guid ProduceGroupID { get; set; }
        public System.Guid MasterGroupID { get; set; }
        public Nullable<System.Guid> TransactionTaxCodeID { get; set; }
      
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
        public virtual Division Division { get; set; }
        public virtual MasterGroup MasterGroup { get; set; }
        public virtual ProduceGroup ProduceGroup { get; set; }
        public virtual TransactionTaxCode TransactionTaxCode { get; set; }
        public virtual ICollection<ProduceIntrastat> ProduceIntrastats { get; set; }
    }
}
