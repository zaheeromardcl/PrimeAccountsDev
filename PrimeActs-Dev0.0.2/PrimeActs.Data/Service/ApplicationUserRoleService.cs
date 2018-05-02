using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Data.Mapping;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface IApplicationUserRoleService : IService<ApplicationUserRole>
    {
        List<ApplicationUserRole> GetAllUserRolesbyApplicationUserID(Guid valapplicationUserID);
        //List<ApplicationUserRole> GetAllUserRolesbyApplicationUserIDMinInfo(Guid valapplicationUserID);
    }

    public class ApplicationUserRoleService : Service<ApplicationUserRole>, IApplicationUserRoleService
    {

       
        private readonly IRepositoryAsync<ApplicationUserRole> _repository;

        public ApplicationUserRoleService(IRepositoryAsync<ApplicationUserRole> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public List<ApplicationUserRole> GetAllUserRolesbyApplicationUserID(Guid valapplicationUserID)
        {
            List<ApplicationUserRole> varApplicationUserRoles = new List<ApplicationUserRole>();
            varApplicationUserRoles = _repository.Query(x => x.UserID == valapplicationUserID)
                //.Include(inc => inc.Company)
                //.Include(inc => inc.Company.Divisions)
                //.Include(inc => inc.Company.Divisions.Select(d => d.Departments))
                //.Include(inc => inc.Division)
                //.Include(inc => inc.Division.Departments)
                //.Include(inc => inc.Department)
                .Include(inc => inc.ApplicationRole)
                .Include(inc=>inc.ApplicationRole.RolePermissions)
                .Include(inc => inc.ApplicationRole.RolePermissions.Select(rp => rp.Permission))
                .Select()
                .ToList();
            return varApplicationUserRoles;
        }
        /*
        public List<ApplicationUserRole> GetAllUserRolesbyApplicationUserIDMinInfo(Guid valapplicationUserID)
        {
            List<ApplicationUserRole> varApplicationUserRoles = new List<ApplicationUserRole>();
            varApplicationUserRoles = _repository.Query(x => x.UserID == valapplicationUserID)
                .Include(inc => inc.ApplicationRole)
                .Include(inc => inc.ApplicationRole.RolePermissions)
                .Include(inc => inc.ApplicationRole.RolePermissions.Select(rp => rp.Permission))
                .Select()
                .ToList();
            return varApplicationUserRoles;
        }
        */
    }
}
