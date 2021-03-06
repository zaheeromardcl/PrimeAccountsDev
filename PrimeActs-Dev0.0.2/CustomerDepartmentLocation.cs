using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerDepartmentLocation : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerDepartmentLocation()
        {
          
        }

        public System.Guid CustomerDepartmentLocationID { get; set; }
        public System.Guid CustomerDepartmentID { get; set; }
        public Nullable<System.Guid> CustomerLocationID { get; set; }
       
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual CustomerLocation CustomerLocation { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
