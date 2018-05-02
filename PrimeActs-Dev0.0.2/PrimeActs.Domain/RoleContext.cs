using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class RoleContext :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid RoleContextID { get; set; }
        public System.Guid UserRoleID { get; set; }
        public Nullable<System.Guid> CompanyID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public System.Guid DivisionID { get; set; }
        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
