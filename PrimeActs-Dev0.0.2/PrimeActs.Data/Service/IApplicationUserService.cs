#region

using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using System;
using System.Threading.Tasks;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IApplicationUserService : IService<ApplicationUser>
    {
        ApplicationUser UserByUserName(string UserName);
        ApplicationUser UserById(Guid Id);
        ApplicationUser FindById(Guid id);
        ApplicationUser FindByEmail(string email);
        ApplicationUser FindByName(string userName);
        List<ApplicationUser> GetAllUsers();
        List<ApplicationUser> GetUsers(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        List<ApplicationUser> GetAllSalesUsers(string search, Guid? departmentID, Guid roleID);
        ApplicationUser FindUserByIdWithAppUserRoles(Guid id);
    }
}