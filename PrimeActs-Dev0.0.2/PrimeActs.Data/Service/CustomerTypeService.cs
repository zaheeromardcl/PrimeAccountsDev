using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class CustomerTypeService : Service<CustomerType>, ICustomerTypeService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<CustomerType> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;

        public CustomerTypeService(IRepositoryAsync<CustomerType> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof(CustomerType).FullName;
        }

        public CustomerType CustomerTypeByDescription(string customerTypeDescription)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<CustomerType>).Where(t => t.CustomerTypeDescription == customerTypeDescription);
            return data == null ? null : data.FirstOrDefault();
        }

        public CustomerType CustomerTypeById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<CustomerType>).Where(t => t.CustomerTypeID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public CustomerType CustomerTypeByCode(string customerTypeCode)
        {
            return _repository.Query(x => x.CustomerTypeCode == customerTypeCode)
                .Include(inc => inc.CustomerDepartments)
                .Select()
                .FirstOrDefault();
        }

        public override void Insert(CustomerType CustomerType)
        {
            _repository.Insert(CustomerType);
            RefreshCache();
        }

        public List<CustomerType> ListOfCustomerTypes()
        {
            CheckCache();
            return (_cache.Get(type) as List<CustomerType>);
        }

        public void RefreshCache()
        {
            _cache.Remove(type);
        }

        private string CheckCache()
        {
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var customerTypes = new List<CustomerType>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        customerTypes.Add(new CustomerType
                        {
                            CustomerTypeID = entityType.CustomerTypeID,
                            CustomerTypeDescription = entityType.CustomerTypeDescription,
                            CustomerTypeCode = entityType.CustomerTypeCode,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, customerTypes);
                }
            }
            return type;
        }
    }
}
