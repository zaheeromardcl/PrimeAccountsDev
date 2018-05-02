using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerContactDepartment : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid CustomerContactDepartmentID { get; set; }
        public System.Guid CustomerContactID { get; set; }
        public Nullable<System.Guid> CustomerDepartmentID { get; set; }
        public virtual CustomerContact CustomerContact { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
