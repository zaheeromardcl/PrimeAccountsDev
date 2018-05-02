
using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class ApplicationUserRole :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {

        public ApplicationUserRole()
        {
           

        }

        public System.Guid UserRoleID { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid RoleID { get; set; }

        public Nullable<System.Guid> DepartmentId { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }
        public Nullable<System.Guid> DivisionId { get; set; }

        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


        
        public virtual Department Department { get; set; }
        public virtual Company Company { get; set; }
        public virtual Division Division { get; set; }
        
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("RoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; }
        
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
