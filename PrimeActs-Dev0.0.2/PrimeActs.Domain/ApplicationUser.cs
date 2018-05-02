using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.Infrastructure.BaseEntities;


namespace PrimeActs.Domain
{
    public partial class ApplicationUser :  PrimeActs.Infrastructure.BaseEntities.IObjectState, IUser<Guid>
    {
        public ApplicationUser()
        {
            this.ApplicationRoles = new List<ApplicationRole>();
            this.ApplicationUserRoles = new List<ApplicationUserRole>();
            this.Consignments = new List<Consignment>();
        }

        public System.Guid Id { get; set; }
       // public System.Guid UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public bool IsActive { get; set; }
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
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Consignment> Consignments { get; set; }

        [NotMapped]
        public Guid SelectedDepartmentId { get; set; }
        [NotMapped]
        public Guid SelectedCompanyId { get; set; }
        [NotMapped]
        public Guid SelectedDivisionId { get; set; }

         
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        [ForeignKey("UserID")]
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        
        public virtual Department Department { get; set; }
        public virtual Company Company { get; set; }
        public virtual Division Division { get; set; }

        [NotMapped]
        public UserContextModel ContextOptions { get; set; }
        [NotMapped]
        public virtual ICollection<ApplicationUserRoleModel> ApplicationUserRoleModels { get; set; }

        [NotMapped]
        public List<PermissionShort> Permissions { get; set; }

        [NotMapped]
        public ICollection<ApplicationRoleModel> UserRoles { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public string AdminPasswordHash { get; set; }

        [NotMapped]
        public bool IsInvoiceAdminAuthenticated { get; set; }

        public ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("PrimeActs:ApplicationUser", JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
            return userIdentity;
        }

        public Claim GenerateClaim()
        {
            return new Claim("PrimeActs:ApplicationUser", JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }

    public class ApplicationRoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ApplicationUserRoleModel
    {
        public System.Guid UserRoleID { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid RoleID { get; set; }
        public Nullable<System.Guid> DepartmentId { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }
        public Nullable<System.Guid> DivisionId { get; set; }
    }
}
