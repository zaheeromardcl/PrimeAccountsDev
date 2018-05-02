using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface IRolePermissionService : IService<RolePermission>
    {
        //List<RolePermission> GetAllUserRolesbyApplicationUserID(Guid valapplicationUserID);
    }

    public class RolePermissionService : Service<RolePermission>, IRolePermissionService
    {
        private readonly IRepositoryAsync<RolePermission> _repository;

        public RolePermissionService(IRepositoryAsync<RolePermission> repository)
            : base(repository)
        {
            _repository = repository;
        }

        //public List<RolePermission> GetAllRolePermissionsbyRoleID(Guid roleID)
        //{
        //    List<RolePermission> varApplicationUserRoles = new List<RolePermission>();
        //    varApplicationUserRoles = _repository.Query(x => x.UserID == valapplicationUserID)
        //        .Include(inc => inc.Company)
        //        .Include(inc => inc.Company.Divisions)
        //        .Include(inc => inc.Company.Divisions.Select(d => d.Departments))
        //        .Include(inc => inc.Division)
        //        .Include(inc => inc.Division.Departments)
        //        .Include(inc => inc.Department)
        //        .Include(inc => inc.ApplicationRole)
        //        .Include(inc => inc.ApplicationRole.Permissions)
        //        //.Include(inc => inc.ApplicationUser.Permissions)
        //        .Select()
        //        .ToList();
        //    return varApplicationUserRoles;
        //}
    }
}
