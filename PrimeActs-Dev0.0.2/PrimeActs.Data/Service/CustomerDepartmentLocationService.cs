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
    public class CustomerDepartmentLocationService : Service<CustomerDepartmentLocation>, ICustomerDepartmentLocationService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<CustomerDepartmentLocation> _repository;
        private readonly object lockboject = new object();

        public CustomerDepartmentLocationService(IRepositoryAsync<CustomerDepartmentLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public List<CustomerDepartmentLocation> GetCustomerDepartmentLocationListByDepId(Guid id)
        {
            var item = _repository.Query(x => x.CustomerDepartmentID == id).Select().ToList();
            return item;
        }

        public CustomerDepartmentLocation GetCustomerDepartmentLocationByLocId(Guid id)
        {
            var item = _repository.Query(x => x.CustomerDepartmentLocationID == id).Select().FirstOrDefault();
            return item;
        }
    }
}
