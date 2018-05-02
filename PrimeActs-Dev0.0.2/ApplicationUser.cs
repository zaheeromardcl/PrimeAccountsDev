using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeActs.Infrastructure.BaseEntities;


namespace PrimeActs.Domain
{
    public partial class ApplicationUser :  PrimeActs.Infrastructure.BaseEntities.IObjectState, IUser<Guid>
    {
        public ApplicationUser()
        {           
            this.ApplicationRoles = new List<ApplicationRole>();
        }

        public System.Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public bool Userstatus { get; set; }
        public Nullable<System.DateTime> LastLoggedOn { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public Nullable<System.Guid> DepartmentId { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }
        public Nullable<System.Guid> DivisionId { get; set; }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        public virtual Department Department { get; set; }
        public virtual Company Company { get; set; }
        public virtual Division Division { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("PrimeActs:ApplicationUser", JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
            return userIdentity;
        }
    }
}
