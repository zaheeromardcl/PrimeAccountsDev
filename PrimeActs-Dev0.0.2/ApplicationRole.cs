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
            this.ApplicationUsers = new List<ApplicationUser>();
            this.Permissions = new List<Permission>();
          
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Discriminator { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}