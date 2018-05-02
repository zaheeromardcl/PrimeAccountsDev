using PrimeActs.Infrastructure.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class Permission :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Permission()
        {
            this.ApplicationRoles = new List<ApplicationRole>();
        }

        [Key]
        public System.Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public int PermissionContext { get; set; }
        public string PermissionDescription { get; set; }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }

    }
}