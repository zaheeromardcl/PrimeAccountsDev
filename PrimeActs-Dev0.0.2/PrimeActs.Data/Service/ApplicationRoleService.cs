#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace PrimeActs.Data.Service
{
    public class ApplicationRoleService : Service<ApplicationRole>, IApplicationRoleService
    {
        private readonly IRepositoryAsync<ApplicationRole> _repository;

        public ApplicationRoleService(IRepositoryAsync<ApplicationRole> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ApplicationRole GetRoleByName(string RoleName)
        {
            return _repository.Query(fil => fil.Name == RoleName).Select().SingleOrDefault();
        }

        public ApplicationRole FindById(Guid id, bool withUsers, bool withPermissions)
        {
            var query = _repository.Query(x => x.Id == id);
            if (withUsers)
            {
                query = query
                    .Include(x => x.ApplicationUserRoles);
                //.Include(y => y.ApplicationUsers);
            }
            if (withPermissions)
            {
                query = query.Include(y => y.RolePermissions);
                query = query.Include(y => y.RolePermissions.Select(rp => rp.Permission));
            }
            
            var appRole = query.Select().FirstOrDefault();
            if (appRole != null)
            {
                appRole.RolePermissions = appRole.RolePermissions.OrderBy(p => p.Permission.PermissionController).ToList();
            }
            return appRole;
        }

        public ApplicationRole FindByName(string roleName)
        {
            return _repository.Query(x => x.Name == roleName).Select().FirstOrDefault();
        }

        public List<ApplicationRole> GetAll()
        {
            return _repository.Query().Select().ToList();
        }

        public List<ApplicationRole> GetRoles(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return
                _repository.Query(GetSearchCriteria(searchObject))
                //.Include(inc => inc.ProduceItems)
                //.Include(inc => inc.SupplierDepartment.Supplier)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }
        
        private Expression<Func<ApplicationRole, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<ApplicationRole, bool>> mainCriteria = x => true;
            if (!string.IsNullOrEmpty(searchObject.CommonSearchCriteria))
            {
                mainCriteria =
                    mainCriteria.And(c =>
                        c.Name.StartsWith(searchObject.CommonSearchCriteria) ||
                        c.Description.StartsWith(searchObject.CommonSearchCriteria)
                        );
            }
            //if (searchObject.FromDate.HasValue)
            //    mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            //if (searchObject.ToDate.HasValue)
            //    mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }

        private Func<IQueryable<ApplicationRole>, IOrderedQueryable<ApplicationRole>> GetOrder(string column, string sortOrder)
        {
            Func<IQueryable<ApplicationRole>, IOrderedQueryable<ApplicationRole>> orderBy = null;
            bool isAscending = sortOrder.ToUpper() == "ASC";
            switch (column)
            {
                case "Name":
                    return isAscending
                        ? orderBy = q => q.OrderBy(x => x.Name)
                        : orderBy = q => q.OrderByDescending(x => x.Name);
                case "Description":
                    return isAscending
                        ? orderBy = q => q.OrderBy(x => x.Description)
                        : orderBy = q => q.OrderByDescending(x => x.Description);
                case "ID":
                default:
                    return isAscending
                        ? orderBy = q => q.OrderBy(x => x.Id)
                        : orderBy = q => q.OrderByDescending(x => x.Id);
            }
        }
    }
}