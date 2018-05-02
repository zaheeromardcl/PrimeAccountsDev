#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;
using PrimeActs.Data.Contexts;

#endregion

namespace PrimeActs.Data.Service
{
    public class ApplicationUserService : Service<ApplicationUser>, IApplicationUserService
    {
        private readonly IRepositoryAsync<ApplicationUser> _repository;

        public ApplicationUserService(IRepositoryAsync<ApplicationUser> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override void Update(ApplicationUser entity)
        {
            var context = _repository.Context as PAndIContext;
            foreach (var role in entity.ApplicationRoles)
            {
                // Attach is required otherwise EF would try to create a new row in tblApplicationRole
                // with same Id and would fail.
                context.ApplicationRoles.Attach(role);
            }
            base.Update(entity);
        }

        public ApplicationUser UserByUserName(string UserName)
        {
            return null;
            // _repository.Query(fil => fil.UserReference == UserRef && fil.IsActive == true).Include(inc => inc.Currency).Include(inc => inc.Customer).Include(inc => inc.UserItems).Select().FirstOrDefault();
        }

        public ApplicationUser UserById(Guid Id)
        {
            return _repository.Query(fil => fil.Id == Id).Select().FirstOrDefault();
        }

        public ApplicationUser FindById(Guid id)
        {
            ApplicationUser varApplicationUser = _repository.Query(x => x.Id == id)
            //var varApplicationUser = _repository.Query(x => x.Id == id)
                //.Include(inc => inc.Department)
                .Include(inc => inc.ApplicationUserRoles)
                //.Include(inc => inc.ApplicationUserRoles.Select(a => a.ApplicationRole))
                //.Include(inc => inc.ApplicationUserRoles.Select(a => a.Company))
                .Include(inc => inc.ApplicationUserRoles.Select(a => a.Department))
                //.Include(inc => inc.ApplicationUserRoles.Select(a => a.Division))
                //.Include(inc => inc.ApplicationRoles)
                .Select().FirstOrDefault();

//            varApplicationUser.ApplicationUserRoles = null;
            if (varApplicationUser == null) return null;
            if (varApplicationUser.Company != null) varApplicationUser.Company.Logo = null;
            if (varApplicationUser != null && varApplicationUser.ApplicationUserRoles != null)
            {
                foreach (var a in varApplicationUser.ApplicationUserRoles)
                {
                    if (a.Company != null) a.Company.Logo = null;
                }
            }

            return varApplicationUser;

            //return _repository.Query(x => x.Id == id)
            //    .Include(inc => inc.Department)
            //    .Include(inc => inc.ApplicationUserRoles)
            //   // .Include(inc => inc.ApplicationRoles)
            //    .Select().FirstOrDefault();
        }

        //Added by Paul Edwards 7/2/17
        public ApplicationUser FindByEmail (string email)
        {
            ApplicationUser varApplicationUser = _repository.Query(x => x.Email == email)
                .Include(inc => inc.Department)
                .Include(inc => inc.ApplicationUserRoles)
               .Include(inc => inc.ApplicationUserRoles.Select(a => a.Department))
                .Select().FirstOrDefault();
            if (varApplicationUser.Company != null) varApplicationUser.Company.Logo = null;
            if (varApplicationUser != null && varApplicationUser.ApplicationUserRoles != null)
            {
                foreach (var a in varApplicationUser.ApplicationUserRoles)
                {
                    if (a.Company != null) a.Company.Logo = null;
                }
            }

            return varApplicationUser;
        }        
        public ApplicationUser FindUserByIdWithAppUserRoles(Guid id)
        {
            ApplicationUser user = _repository.Query(x => x.Id == id)
                 .Include(inc => inc.ApplicationUserRoles)
                 .Select().FirstOrDefault();
            if (user.Company != null) user.Company.Logo = null;
            if (user != null && user.ApplicationUserRoles != null)
            {
                foreach (var a in user.ApplicationUserRoles)
                {
                    if (a.Company != null) a.Company.Logo = null;
                }
            }

            return user;
        }

        public ApplicationUser FindByName(string userName)
        {
            return _repository.Query(x => x.UserName == userName).Select().FirstOrDefault();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return _repository.Query().Select().ToList();
        }

        public List<ApplicationUser> GetUsers(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            return
                _repository.Query(GetSearchCriteria(searchObject))
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }

        public List<ApplicationUser> GetAllSalesUsers(string search, Guid? departmentId, Guid roleId)
        {
            //search = search.ToUpperInvariant();
            // if department id == null then the user has access to all departments
            List<ApplicationUser> varSalesUsers = _repository.Query(
                x => x.Firstname.StartsWith(search)
                    & x.ApplicationUserRoles.Any(y => (departmentId == null || y.DepartmentId == departmentId) && y.RoleID == roleId))
                    .Select()
                    .ToList();

            return varSalesUsers;
                
                
        }

        private Expression<Func<ApplicationUser, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<ApplicationUser, bool>> mainCriteria = x => true;
            if (!string.IsNullOrEmpty(searchObject.CommonSearchCriteria))
            {
                mainCriteria =
                    mainCriteria.And(x =>
                        x.UserName.StartsWith(searchObject.CommonSearchCriteria) ||
                        x.Firstname.StartsWith(searchObject.CommonSearchCriteria) ||
                        x.Lastname.StartsWith(searchObject.CommonSearchCriteria)
                        );
            }
            if (searchObject.FromDate.HasValue)
                mainCriteria = mainCriteria.And(x => x.LastLoggedOn.Value >= searchObject.FromDate.Value);
            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(x => x.LastLoggedOn.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }

        private Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> GetOrder(string column, string sortOrder)
        {
            Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = null;
            bool isAscending = sortOrder.ToUpper() == "ASC";
            switch (column)
            {
                case "Username":
                    return isAscending
                        ? orderBy = q => q.OrderBy(x => x.UserName)
                        : orderBy = q => q.OrderByDescending(x => x.UserName);
                case "Email":
                    return isAscending
                        ? orderBy = q => q.OrderBy(x => x.Email)
                        : orderBy = q => q.OrderByDescending(x => x.Email);
                case "ID":
                default:
                    return isAscending
                        ? orderBy = q => q.OrderBy(x => x.Id)
                        : orderBy = q => q.OrderByDescending(x => x.Id);
            }
        }
    }
}