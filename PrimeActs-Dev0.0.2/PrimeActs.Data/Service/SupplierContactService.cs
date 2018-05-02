using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class SupplierContactService : Service<SupplierContact>, ISupplierContactService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<SupplierContact> _repository;
        private readonly object lockboject = new object();

        public SupplierContactService(IRepositoryAsync<SupplierContact> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public IEnumerable<SupplierContact> GetSupplierContacts(List<Guid> supplierContactIds)
        {
            CheckCache();
            return (_cache.Get(typeof(SupplierContact).FullName) as IEnumerable<SupplierContact>).Where(t => supplierContactIds.Contains(t.SupplierContactID));
        }

        private void CheckCache()
        {
            var type = typeof(SupplierContact).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var list = new List<SupplierContact>();
                    foreach (
                        var entityType in
                            _repository.Query()
                                //.Include(inc => inc.Address)
                                .Select())
                    {
                        list.Add(new SupplierContact
                        {
                            SupplierContactID = entityType.SupplierContactID,
                            Contact = entityType.Contact,
                            Supplier = entityType.Supplier, ////////////
                            SortOrder = entityType.SortOrder ///////////
                        });
                    }
                    _cache.Set(type, list);
                }
            }
        }
    }
}
