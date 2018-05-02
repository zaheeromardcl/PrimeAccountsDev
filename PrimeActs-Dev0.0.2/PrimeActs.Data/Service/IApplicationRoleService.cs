#region

using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using System;
using System.Collections.Generic;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IApplicationRoleService : IService<ApplicationRole>
    {
        ApplicationRole GetRoleByName(string RoleName);
        ApplicationRole FindById(Guid id, bool withUsers, bool withPermissions);
        ApplicationRole FindByName(string roleName);
        List<ApplicationRole> GetAll();

        List<ApplicationRole> GetRoles(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        //List<ApplicationRole> FindByPermissionIDs(IEnumerable<Guid> permissionsIDs);
    }
}