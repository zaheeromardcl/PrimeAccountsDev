using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{

    public class CustomerDepartmentService : Service<CustomerDepartment>, ICustomerDepartmentService
    {
        private readonly ICache _cache;
        private readonly object _lockboject = new object();
        private readonly IRepositoryAsync<CustomerDepartment> _repository;
        private readonly string _type;


        public CustomerDepartmentService(IRepositoryAsync<CustomerDepartment> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            _type = typeof (CustomerDepartment).FullName;
        }

        public List<CustomerDepartment> GetAllCustomerDepartmentsByDivision(Guid id)
        {
            return _repository.Query(x => x.Customer.Company.Divisions.Any(y => y.DivisionID == id))
                .Select()
                .ToList();
        }

        public List<CustomerDepartment> CustomerDepartmentsByCustomerDepartmentID(Guid cdId) {

            var CustomerDepartments = _repository.Query(x => 
                x.Customer.CustomerDepartments.Any(y => 
                y.CustomerDepartmentID == cdId)).Select().ToList();
            return CustomerDepartments;
        }

        public List<CustomerDepartment> GetAllCustomerDepartments()
        {
            return _repository.Query().Include(inc => inc.Customer).Select().ToList();
        }

        public CustomerDepartment CustomerDepartmentById(Guid Id)
        {
            return
                _repository.Query(p => p.CustomerDepartmentID == Id)
                    .Include(inc => inc.Customer.CustomerDepartments)
                    .Select().FirstOrDefault();
            //CheckCache();
            //var data = ((IEnumerable<CustomerDepartment>) _cache.Get(_type)).Where(t => t.CustomerDepartmentID == id);
            //return data.FirstOrDefault();
        }

        //- used in methods GetCustomerLocationModels & GetCustomerLocationModelsForDDLinvc
        public CustomerDepartment CustomerDepartmentByCustomerDepartmentId(Guid cdId)
        {
            var result = _repository.Query(p => p.CustomerDepartmentID == cdId)
                .Include(inc => inc.Customer.CustomerDepartments)
                .Select().FirstOrDefault();
            return result;
        }

        public void RefreshCache()
        {
            _cache.Remove(_type);
        }

        private void CheckCache()
        {
            if (!_cache.Exists(_type))
            {
                lock (_lockboject)
                {
                    var customerDepartments = _repository.Query().Include(inc => inc.Customer).Select().Select(entityType => new CustomerDepartment
                    {
                        CustomerDepartmentID = entityType.CustomerDepartmentID,
                        CustomerDepartmentName = entityType.CustomerDepartmentName,
                        CustomerID = entityType.CustomerID
                    }).ToList();
                    _cache.Set(_type, customerDepartments);
                }
            }
        }
    }
}