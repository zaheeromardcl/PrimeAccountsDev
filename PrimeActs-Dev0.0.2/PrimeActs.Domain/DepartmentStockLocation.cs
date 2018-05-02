using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class DepartmentStockLocation :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid DepartmentStockLocationID { get; set; }
        public System.Guid DepartmentID { get; set; }
        public System.Guid StockLocationID { get; set; }





        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual StockLocation StockLocation { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
