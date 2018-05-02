using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class CustomerLocationService : Service<CustomerLocation>, ICustomerLocationService
    {
        private readonly ICache _cache;
        private readonly object _lockboject = new object();
        private readonly IRepositoryAsync<CustomerLocation> _repository;
        private readonly string _type;


        public CustomerLocationService(IRepositoryAsync<CustomerLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            _type = typeof(CustomerLocation).FullName;
        }

        //- used in method List<CustomerLocationModel> GetCustomerLocationModels(Guid cdId)
        public List<CustomerLocation> GetAllCustomerLocationsByCustomerID(Guid id)
        {
            var list = _repository.Query(x => x.Customer.CustomerLocations
                .Any(y => y.CustomerID == id)).Select().ToList();
            return list;
        }

        public List<CustomerLocation> GetAllCustomerLocationsByCusLocID(Guid id)
        {
            var list = _repository.Query(x => x.Customer.CustomerLocations
                .Any(y => y.CustomerLocationID == id)).Select().ToList();
            return list;
        }

        public List<CustomerLocation> GetAllCustomerLocationsOrderedByCustomer()
        {
            //return _repository.Query(x => x.CustomerLocationName.Select())                              
            //                    .Orderby(x => x.CustomerID)
            //                    .ToList();
            return null;
        }

        public List<CustomerLocation> GetAllCustomerLocations()
        {
            return _repository.Query().Include(inc => inc.Customer).Select().ToList();
        }

        public CustomerLocation CustomerLocationById(Guid id)
        {
            CheckCache();
            var data = ((IEnumerable<CustomerLocation>)_cache.Get(_type)).Where(t => t.CustomerLocationID == id);
            return data.FirstOrDefault();
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
                    var customerLocations = _repository.Query().Include(inc => inc.Customer).Select().Select(entityType => new CustomerLocation
                    {
                        CustomerLocationID = entityType.CustomerLocationID,
                        CustomerLocationName = entityType.CustomerLocationName,
                        CustomerID = entityType.CustomerID
                    }).ToList();
                    _cache.Set(_type, customerLocations);
                }
            }
        }
    }
}