#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using SearchObject = PrimeActs.Domain.ViewModels.Division.SearchObject;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IDivisionService : IService<Division>
    {
        Division DivisionByName(string divisionName);
        Division DivisionById(Guid Id);
        List<Division> GetAllDivisions();
        List<Division> GetDivisions(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Division.SearchObject searchObject, out int totalCount);
        void RefreshCache();
        List<Division> GetDivisionsByCompanyID(Guid companyID);
        Guid GetCompanyIDForDivisionID(Guid divisionID);
    }
    public class DivisionService : Service<Division>, IDivisionService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Division> _repository;
        private readonly object lockboject = new object();

        public DivisionService(IRepositoryAsync<Division> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public Division DivisionByName(string divisionName)
        {
            CheckCache();
            var data =
                (_cache.Get(typeof(Division).FullName) as IEnumerable<Division>).Where(
                    t => t.DivisionName == divisionName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Division DivisionById(Guid Id)
        {
            CheckCache();
            var data =
                (_cache.Get(typeof(Division).FullName) as IEnumerable<Division>).Where(t => t.DivisionID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<Division> GetAllDivisions()
        {
            CheckCache();
            var returnValue = (_cache.Get(typeof(Division).FullName) as List<Division>);
            if (returnValue == null)
                return new List<Division>();
            return returnValue;
        }

        public List<Division> GetDivisionsByCompanyID(Guid companyID)
        {
            return
                _repository.Query(x => x.CompanyID == companyID)
                    .Select().ToList();
        }

        public List<Division> GetDivisions(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            return
                _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.Company)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }
        public Guid GetCompanyIDForDivisionID(Guid divisionID)
        {
            return
                _repository.Query(x => x.DivisionID == divisionID).Select().First().CompanyID;
        }

        private Func<IQueryable<Division>, IOrderedQueryable<Division>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Division>, IOrderedQueryable<Division>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.DivisionID)
                        : orderBy = q => q.OrderByDescending(x => x.DivisionID);
                case "DivisionName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.DivisionName)
                        : orderBy = q => q.OrderByDescending(x => x.DivisionName);
                //case "Customer":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.CustomerDivision.CustomerDivisionName)
                //        : orderBy = q => q.OrderByDescending(x => x.CustomerDivision.CustomerDivisionName);

                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.DivisionID)
                        : orderBy = q => q.OrderByDescending(x => x.DivisionID);
            }
        }

        private Expression<Func<Division, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Division, bool>> mainCriteria = c => c.IsActive == true;
            if (!string.IsNullOrEmpty(searchObject.DivisionName))
                mainCriteria = mainCriteria.And(c => c.DivisionName.StartsWith(searchObject.DivisionName));
            //if (searchObject.FromDate.HasValue)
            //  mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            //if (searchObject.ToDate.HasValue)
            //  mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof(Division).FullName);
        }

        private void CheckCache()
        {
            var type = typeof(Division).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var divisions = new List<Division>();
                    foreach (var entityType in _repository.Query().Include(inc => inc.Company).Select())
                    {
                        divisions.Add(new Division
                        {
                            DivisionID = entityType.DivisionID,
                            DivisionName = entityType.DivisionName,
                            CompanyID = entityType.CompanyID,
                            CreatedBy = entityType.CreatedBy,
                            CreatedDate = entityType.CreatedDate.HasValue ? entityType.CreatedDate : (DateTime?)null,
                            UpdatedDate = entityType.UpdatedDate.HasValue ? entityType.UpdatedDate : (DateTime?)null,
                            UpdatedBy = entityType.UpdatedBy,
                            AddressID = entityType.AddressID,
                            IsActive = entityType.IsActive,
                            Company = entityType.Company
                        });
                    }
                    _cache.Set(type, divisions);
                }
            }
        }
    }
}