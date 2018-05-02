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
    public class CustomerContactDepartmentService : Service<CustomerContactDepartment>, ICustomerContactDepartmentService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<CustomerContactDepartment> _repository;
        private readonly object lockboject = new object();

        public CustomerContactDepartmentService(IRepositoryAsync<CustomerContactDepartment> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public List<CustomerContactDepartment> GetCustomerContactDepartmentListByConId(Guid id)
        {
            var item = _repository.Query(x => x.CustomerContactID == id).Select().ToList();
            return item;
        }
    }
}
