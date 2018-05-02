using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class SupplierLocationService : Service<SupplierLocation>, ISupplierLocationService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<SupplierLocation> _repository;
        private readonly object lockboject = new object();

        public SupplierLocationService(IRepositoryAsync<SupplierLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public SupplierLocation GetSupplierLocationById(Guid id)
        {
            var item = _repository.Query(x => x.SupplierLocationID == id).Select().FirstOrDefault();
            return item;
        }

        public IEnumerable<SupplierLocation> GetSupplierLocations(IEnumerable<Guid> supplierLocationIds)
        {
            CheckCache();
            return (_cache.Get(typeof(SupplierLocation).FullName) as IEnumerable<SupplierLocation>).Where(t => supplierLocationIds.Contains(t.SupplierLocationID));
        }

        private void CheckCache()
        {
            var type = typeof(SupplierLocation).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var list = new List<SupplierLocation>();
                    foreach (
                        var entityType in
                            _repository.Query()
                                .Include(inc => inc.Address)
                                .Select())
                    {
                        list.Add(new SupplierLocation
                        {
                            SupplierLocationID = entityType.SupplierLocationID,
                            SupplierLocationName = entityType.SupplierLocationName,
                            Address = entityType.Address,
                            TelephoneNumber = entityType.TelephoneNumber,
                            FaxNumber = entityType.FaxNumber
                        });
                    }
                    _cache.Set(type, list);
                }
            }
        }
    }
}
