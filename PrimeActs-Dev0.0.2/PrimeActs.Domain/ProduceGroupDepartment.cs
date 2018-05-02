using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class ProduceGroupDepartment : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ProduceGroupDepartment()
        {
            this.StockBoardProduceGroups = new List<StockBoardProduceGroup>();
            
        }

        public System.Guid ProduceGroupDepartmentID { get; set; }
        public System.Guid ProduceGroupID { get; set; }
        public System.Guid DepartmentID { get; set; }
        
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


        [NotMapped]
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        [ForeignKey("ProduceGroupID")]
        public virtual ProduceGroup ProduceGroup { get; set; }//grandparent
        

        [NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

        [ForeignKey("ProduceGroupDepartmentID")]
        public virtual ICollection<StockBoardProduceGroup> StockBoardProduceGroups { get; set; }

    }
}
