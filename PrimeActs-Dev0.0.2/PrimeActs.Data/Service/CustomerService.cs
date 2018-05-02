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
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Customer> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;

        public CustomerService(IRepositoryAsync<Customer> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (Customer).FullName;
        }

        public Customer CustomerByName(string CustomerName)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Customer>).Where(t => t.CustomerCompanyName == CustomerName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Customer CustomerById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Customer>).Where(t => t.CustomerID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            return _repository.Query(x => x.CustomerCode == customerCode)
                .Include(inc => inc.CustomerDepartments)
                .Select()
                .FirstOrDefault();
        }

        public Customer GetCustomerByIdFromRepo(Guid Id)
        {
            var data = _repository.Query()
                .Include(inc => inc.Note)
                .Include(inc => inc.Company)
                .Include(inc => inc.ParentCustomer)
                .Include(inc => inc.CustomerLocations)
                .Include(inc => inc.CustomerDepartments)
                .Include(inc => inc.CustomerContacts)
                .Select().FirstOrDefault(x => x.CustomerID == Id);
            return data ?? null;
        }

        public List<Customer> GetCustomers(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            var searchCriteria = GetSearchCriteria(searchObject);
            var customers = _repository.Query(searchCriteria)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.Company)
                    .Include(inc => inc.ParentCustomer)
                    .Include(inc => inc.CustomerLocations)
                    .Include(inc => inc.CustomerDepartments)
                    .Include(inc => inc.CustomerContacts)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
            return customers;
        }

        private Expression<Func<Customer, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Customer, bool>> mainCriteria = null;
            if (searchObject.FromDate.HasValue)
                mainCriteria = c => c.CreatedDate.Value >= searchObject.FromDate.Value;
            else // this line should never be hit
                mainCriteria = c => c.CreatedDate.Value >= DateTime.Today.AddDays(-30);
            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);

            if (!string.IsNullOrEmpty(searchObject.CustomerCode))
                mainCriteria = mainCriteria.And(c =>
                    c.CustomerCode == searchObject.CustomerCode);
            else if (!string.IsNullOrEmpty(searchObject.CustomerCompanyName))
                mainCriteria = mainCriteria.And(c =>
                            c.CustomerCompanyName.StartsWith(searchObject.CustomerCompanyName) |
                            c.CustomerCompanyName.StartsWith(searchObject.CustomerCompanyName));
            mainCriteria = mainCriteria.And(c => c.IsTransfer == 0);
            return mainCriteria;
        }

        private Func<IQueryable<Customer>, IOrderedQueryable<Customer>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CustomerID)
                        : orderBy = q => q.OrderByDescending(x => x.CustomerID);
                case "SupplierCode":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CustomerCode)
                        : orderBy = q => q.OrderByDescending(x => x.CustomerCode);
                case "SupplierCompanyName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CustomerCompanyName)
                        : orderBy = q => q.OrderByDescending(x => x.CustomerCompanyName);
                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CustomerID)
                        : orderBy = q => q.OrderByDescending(x => x.CustomerID);
            }
        }

        public override void Insert(Customer Customer)
        {
            _repository.Insert(Customer);
            RefreshCache();
        }

        public List<Customer> GetAllCustomers()
        {
            CheckCache();
            return (_cache.Get(type) as List<Customer>);
        }


        public void RefreshCache()
        {
            _cache.Remove(type);
        }


        public List<Customer> GetAllCustomersByDivision(Guid id)
        {
            return
                _repository.Query(x => x.Company.Divisions.Any(anyx => anyx.DivisionID == id))
                    .Include(inc => inc.CustomerDepartments)
                    .Include(y => y.Company.Divisions)
                    .Select()
                    .ToList();
        }

        private string CheckCache()
        {
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var Customers = new List<Customer>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        Customers.Add(new Customer
                        {
                            CustomerID = entityType.CustomerID,
                            CustomerCompanyName = entityType.CustomerCompanyName,
                            CustomerCode = entityType.CustomerCode
                        });
                    }
                    _cache.Set(type, Customers);
                }
            }
            return type;
        }
    }
}
