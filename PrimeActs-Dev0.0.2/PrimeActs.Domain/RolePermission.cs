using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class RolePermission :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public RolePermission()
        {
        }
        public System.Guid RolePermissionID { get; set; }
        public System.Guid PermissionID { get; set; }
        public System.Guid RoleID { get; set; }

        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        
        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("RoleID")]
        public virtual ApplicationRole Role { get; set; }
        
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
