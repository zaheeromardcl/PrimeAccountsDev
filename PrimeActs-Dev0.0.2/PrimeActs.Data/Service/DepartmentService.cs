#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using SearchObject = PrimeActs.Domain.ViewModels.Department.SearchObject;

#endregion

namespace PrimeActs.Data.Service
{
    public class DepartmentService : Service<Department>, IDepartmentService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Department> _repository;
        private readonly object lockboject = new object();

        public DepartmentService(IRepositoryAsync<Department> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public Department DepartmentByName(string departmentName)
        {
            CheckCache();
            var data =
                (_cache.Get(typeof (Department).FullName) as IEnumerable<Department>).Where(
                    t => t.DepartmentName == departmentName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Department DepartmentById(Guid Id)
        {
            CheckCache();
            var data =
                (_cache.Get(typeof (Department).FullName) as IEnumerable<Department>).Where(t => t.DepartmentID == Id);
            return data == null ? null : data.FirstOrDefault();
        }
        public List<Department> GetAllDepartments()
        {
            CheckCache();
            var returnValue = (_cache.Get(typeof (Department).FullName) as List<Department>);
            if (returnValue == null)
                return new List<Department>();
            return returnValue;
        }

        public List<Department> GetDepartments(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            return
                _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.Division)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }
        public  List<Department> GetAllDeptByDivID(Guid DivisionId)
        {
            List<Department> returnValue = new List<Department>();
            returnValue = _repository.Query()
               .Include(inc => inc.Division)
               .Select()
               .Where(x => x.DivisionID == DivisionId)
               .ToList();

            return returnValue;
        }


        private Func<IQueryable<Department>, IOrderedQueryable<Department>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.DepartmentID)
                        : orderBy = q => q.OrderByDescending(x => x.DepartmentID);
                case "DepartmentName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.DepartmentName)
                        : orderBy = q => q.OrderByDescending(x => x.DepartmentName);
                //case "Customer":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.CustomerDepartment.CustomerDepartmentName)
                //        : orderBy = q => q.OrderByDescending(x => x.CustomerDepartment.CustomerDepartmentName);

                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.DepartmentID)
                        : orderBy = q => q.OrderByDescending(x => x.DepartmentID);
            }
        }

        private Expression<Func<Department, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Department, bool>> mainCriteria = c => c.IsActive == true;
            if (!string.IsNullOrEmpty(searchObject.DepartmentName))
                mainCriteria = mainCriteria.And(c => c.DepartmentName.StartsWith(searchObject.DepartmentName));
            //if (searchObject.FromDate.HasValue)
              //  mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            //if (searchObject.ToDate.HasValue)
              //  mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }
        
        public void RefreshCache()
        {
            _cache.Remove(typeof (Department).FullName);
        }

        private void CheckCache()
        {
            var type = typeof (Department).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var departments = new List<Department>();
                    foreach (var entityType in _repository.Query().Include(inc=>inc.Division).Select())
                    {
                        departments.Add(new Department
                        {
                            DepartmentID = entityType.DepartmentID,
                            DepartmentName = entityType.DepartmentName,
                            DepartmentCode = entityType.DepartmentCode,
                            Division = entityType.Division,
                            DivisionID = entityType.DivisionID,
                            CreatedBy = entityType.CreatedBy,
                            CreatedDate = entityType.CreatedDate.HasValue ? entityType.CreatedDate : (DateTime?)null,
                            UpdatedDate = entityType.UpdatedDate.HasValue ? entityType.UpdatedDate : (DateTime?)null,
                            UpdatedBy = entityType.UpdatedBy,
                            IsActive = entityType.IsActive,
                            RebateNominalAccountID = entityType.RebateNominalAccountID

                        });
                    }
                    _cache.Set(type, departments);
                }
            }
        }
    }
}