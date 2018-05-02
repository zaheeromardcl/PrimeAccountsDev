using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class ApplicationRole :  PrimeActs.Infrastructure.BaseEntities.IObjectState, IRole<Guid>
    {
        public ApplicationRole()
        {            
            this.ApplicationUserRoles = new List<ApplicationUserRole>(); 
            this.ApplicationUsers = new List<ApplicationUser>();
            //this.Permissions = new List<Permission>();
            this.RolePermissions = new List<RolePermission>();          
        }

        public System.Guid Id { get; set; }
       // public System.Guid RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        //public virtual ICollection<Permission> Permissions { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}