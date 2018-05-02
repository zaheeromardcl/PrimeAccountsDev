using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class SupplierService : Service<Supplier>, ISupplierService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Supplier> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;

        public SupplierService(IRepositoryAsync<Supplier> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof(Supplier).FullName;
        }

        public Supplier SupplierByName(string supplierCompanyName)
        {
            CheckCache();
            var data =
                (_cache.Get(type) as IEnumerable<Supplier>).Where(t => t.SupplierCompanyName == supplierCompanyName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Supplier SupplierById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(typeof(Supplier).FullName) as IEnumerable<Supplier>).Where(t => t.SupplierID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(Supplier supplier)
        {
            _repository.Insert(supplier);
            RefreshCache();
        }

        public List<Supplier> GetAllSuppliers()
        {
            CheckCache();
            return (_cache.Get(type) as List<Supplier>);
        }

        public void RefreshCache()
        {
            _cache.Remove(type);
        }

        public List<Supplier> GetAllSupplierDepts()
        {
            var suppliers = _repository.Query()
                    .Include(inc => inc.ParentSupplier)
                    .Include(inc => inc.SupplierDepartments)
                    .Select().ToList();
            return suppliers;
        }

        public Supplier GetSupplierByIdFromRepo(Guid Id)
        {
            var data = _repository.Query()
                .Include(inc => inc.Note)
                .Include(inc => inc.Company)
                .Include(inc => inc.ParentSupplier)
                .Include(inc => inc.SupplierLocations)
                .Include(inc => inc.SupplierDepartments)
                .Include(inc => inc.SupplierContacts)
                .Select().FirstOrDefault(x => x.SupplierID == Id);
            return data ?? null;
        }

        public List<Supplier> GetSuppliers(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            var suppliers = _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.Note)
                    .Include(inc => inc.Company)
                    .Include(inc => inc.ParentSupplier)
                    .Include(inc => inc.SupplierLocations)
                    .Include(inc => inc.SupplierDepartments)
                    .Include(inc => inc.SupplierContacts)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
            return suppliers;
        }

        private Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierID)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierID);
                case "SupplierCode":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierCode)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierCode);
                case "SupplierCompanyName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierCompanyName)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierCompanyName);
                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierID)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierID);
            }
        }

        private Expression<Func<Supplier, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Supplier, bool>> mainCriteria = null;
            if (searchObject.FromDate.HasValue)
                mainCriteria = c => c.CreatedDate.Value >= searchObject.FromDate.Value;
            else // this line should never be hit
                mainCriteria = c => c.CreatedDate.Value >= DateTime.Today.AddDays(-30);
            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);

            if (!string.IsNullOrEmpty(searchObject.SupplierCode))
                mainCriteria = mainCriteria.And(c =>
                    c.SupplierCode == searchObject.SupplierCode);
            else if (!string.IsNullOrEmpty(searchObject.SupplierCompanyName))
                mainCriteria = mainCriteria.And(c =>
                            c.SupplierCompanyName.StartsWith(searchObject.SupplierCompanyName) |
                            c.SupplierCompanyName.StartsWith(searchObject.SupplierCompanyName));
            return mainCriteria;
        }

        private void CheckCache()
        {
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var suppliers = new List<Supplier>();
                    foreach (var entityType in _repository.Query().Include(inc => inc.ParentSupplier).Select())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierID = entityType.SupplierID,
                            SupplierCode = entityType.SupplierCode,
                            SupplierCompanyName = entityType.SupplierCompanyName,
                            ParentSupplier = entityType.ParentSupplier,
                            IsActive = entityType.IsActive,
                            IsFactor = entityType.IsFactor
                        });
                    }
                    _cache.Set(type, suppliers);
                }
            }
        }

        public Supplier GetSupplierOnly(Guid id)
        {
            var item = _repository.Query(fil => fil.SupplierID == id)
                .Include(inc => inc.Note)
                .Include(inc => inc.Company)
                .Include(inc => inc.ParentSupplier)
                .Select().SingleOrDefault();
            return item;
        }
    }
}
