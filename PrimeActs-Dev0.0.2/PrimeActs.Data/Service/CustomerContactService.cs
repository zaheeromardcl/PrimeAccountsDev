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
    public class CustomerContactService : Service<CustomerContact>, ICustomerContactService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<CustomerContact> _repository;
        private readonly object lockboject = new object();

        public CustomerContactService(IRepositoryAsync<CustomerContact> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public IEnumerable<CustomerContact> GetCustomerContacts(List<Guid> customerContactIds)
        {
            CheckCache();
            return (_cache.Get(typeof(CustomerContact).FullName) as IEnumerable<CustomerContact>).Where(t => customerContactIds.Contains(t.CustomerContactID));
        }

        private void CheckCache()
        {
            var type = typeof(CustomerContact).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var list = new List<CustomerContact>();
                    foreach (
                        var entityType in
                            _repository.Query()
                                //.Include(inc => inc.Address)
                                .Select())
                    {
                        list.Add(new CustomerContact
                        {
                            CustomerContactID = entityType.CustomerContactID,
                            Contact = entityType.Contact,
                            Customer = entityType.Customer, ////////////
                            SortOrder = entityType.SortOrder ///////////
                        });
                    }
                    _cache.Set(type, list);
                }
            }
        }
    }
}
