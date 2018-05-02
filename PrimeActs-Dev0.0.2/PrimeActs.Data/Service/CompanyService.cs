#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using SearchObject = PrimeActs.Domain.ViewModels.Company.SearchObject;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICompanyService : IService<Company>
    {
        Company CompanyByName(string companyName);
        Company CompanyById(Guid Id);
        List<Company> GetAllCompanies();
        List<Company> GetCompanies(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Company.SearchObject searchObject, out int totalCount);
        void RefreshCache();
        Company CompanyWithAddress(Guid Id);
        List<Company> GetCompanieswithDivisionsAndDepartments();
    }

    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Company> _repository;
        private readonly object lockboject = new object();

        public CompanyService(IRepositoryAsync<Company> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public Company CompanyByName(string companyName)
        {
            CheckCache();
            var data =
                (_cache.Get(typeof(Company).FullName) as IEnumerable<Company>).Where(
                    t => t.CompanyName == companyName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Company CompanyById(Guid Id)
        {
            CheckCache();
            var data =
                (_cache.Get(typeof(Company).FullName) as IEnumerable<Company>).Where(t => t.CompanyID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<Company> GetAllCompanies()
        {
            CheckCache();
            var returnValue = (_cache.Get(typeof(Company).FullName) as List<Company>);
            if (returnValue == null)
                return new List<Company>();
            return returnValue;
        }

        public List<Company> GetCompanies(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            return
                _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.ParentCompany)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }

        public List<Company> GetCompanieswithDivisionsAndDepartments()
        {
            return
                _repository.Query()
                    .Include(inc => inc.Divisions)
                    .Include(inc => inc.Divisions.Select(d => d.Departments))
                    .Select().ToList();
        }

        private Func<IQueryable<Company>, IOrderedQueryable<Company>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CompanyID)
                        : orderBy = q => q.OrderByDescending(x => x.CompanyID);
                case "CompanyName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CompanyName)
                        : orderBy = q => q.OrderByDescending(x => x.CompanyName);
                //case "Customer":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.CustomerCompany.CustomerCompanyName)
                //        : orderBy = q => q.OrderByDescending(x => x.CustomerCompany.CustomerCompanyName);

                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CompanyID)
                        : orderBy = q => q.OrderByDescending(x => x.CompanyID);
            }
        }

        private Expression<Func<Company, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Company, bool>> mainCriteria = c => c.IsActive == true;
            if (!string.IsNullOrEmpty(searchObject.CompanyName))
                mainCriteria = mainCriteria.And(c => c.CompanyName.StartsWith(searchObject.CompanyName));
            //if (searchObject.FromDate.HasValue)
            //  mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            //if (searchObject.ToDate.HasValue)
            //  mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof(Company).FullName);
        }

        private void CheckCache()
        {
            var type = typeof(Company).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var companys = new List<Company>();
                    foreach (var entityType in _repository.Query().Include(inc => inc.ParentCompany).Select())
                    {
                        companys.Add(new Company
                        {
                            CompanyID = entityType.CompanyID,
                            CompanyName = entityType.CompanyName,
                            EmailAddress = entityType.EmailAddress,
                            AddressID = entityType.AddressID,
                            FaxNo = entityType.FaxNo,
                            InvoiceInfo = entityType.InvoiceInfo,
                            Website = entityType.Website,
                            Telephone = entityType.Telephone,
                            CompanyNo = entityType.CompanyNo,
                            CreatedBy = entityType.CreatedBy,
                            CreatedDate = entityType.CreatedDate.HasValue?entityType.CreatedDate:(DateTime?)null,
                            UpdatedDate = entityType.UpdatedDate.HasValue ? entityType.UpdatedDate : (DateTime?)null,
                            UpdatedBy = entityType.UpdatedBy,
                            ParentCompany = entityType.ParentCompany,
                            IsActive = entityType.IsActive
                        });
                    }
                    _cache.Set(type, companys);
                }
            }
        }

        public Company CompanyWithAddress(Guid Id)
        {
            return _repository.Query(x => x.CompanyID == Id)
                .Include(x => x.Address)
                .Select()
                .FirstOrDefault();
        }
    }
}