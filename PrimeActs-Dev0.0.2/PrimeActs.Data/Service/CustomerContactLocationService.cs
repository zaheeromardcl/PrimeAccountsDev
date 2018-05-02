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
    public class CustomerContactLocationService : Service<CustomerContactLocation>, ICustomerContactLocationService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<CustomerContactLocation> _repository;
        private readonly object lockboject = new object();

        public CustomerContactLocationService(IRepositoryAsync<CustomerContactLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public List<CustomerContactLocation> GetCustomerContactLocationListByConId(Guid id)
        {
            var item = _repository.Query(x => x.CustomerContactID == id).Select().ToList();
            return item;
        }
    }
}
