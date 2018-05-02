using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class StockLocation : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public StockLocation()
        {
           this.DepartmentStockLocations = new List<DepartmentStockLocation>();
        }

        public System.Guid StockLocationID { get; set; }
        public string StockLocationName { get; set; }
        public string StockLocationCode { get; set; }
        public System.Guid CompanyID { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public virtual Address Address { get; set; }
       public virtual Company Company { get; set; }
        public virtual ICollection<DepartmentStockLocation> DepartmentStockLocations { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
