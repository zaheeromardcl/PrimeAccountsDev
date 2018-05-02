using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class StockBoard :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public StockBoard()
        {
            this.StockBoardProduceGroups = new List<StockBoardProduceGroup>();
            
        }

        public System.Guid StockBoardID {get;set;}
        public string StockBoardName {get;set;}
        public Nullable<System.Guid> DepartmentID {get;set;}
        public System.Guid CompanyID {get;set;}
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        
        [NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        [ForeignKey("StockBoardID")]
        public virtual ICollection<StockBoardProduceGroup> StockBoardProduceGroups { get; set; }//children
        
        //
        
       
    }
}
