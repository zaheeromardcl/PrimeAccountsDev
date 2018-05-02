using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class StockBoardProduceGroup : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public StockBoardProduceGroup()
        {
           
         }

        public System.Guid StockBoardProduceGroupID { get; set; }
        public System.Guid ProduceGroupDepartmentID { get; set; }
        
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        //[ForeignKey("StockBoardID")]
        public virtual StockBoard StockBoard { get; set; }
        public virtual ProduceGroupDepartment ProduceGroupDepartment { get; set; }
        
        [NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

        //relationship
        public System.Guid StockBoardID { get; set; } //stockboardproducegroup relates back to stockboard.
       //public virtual ICollection<ProduceGroup> ProduceGroups { get; set; }//grandchildren
    }
}
