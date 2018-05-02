using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Data.Service;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;

namespace PrimeActs.Data.Service
{
    public class SupplierContactLocationService: Service<SupplierContactLocation>, ISupplierContactLocationService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<SupplierContactLocation> _repository;
        private readonly object lockboject = new object();

        public SupplierContactLocationService(IRepositoryAsync<SupplierContactLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public List<SupplierContactLocation> GetSupplierContactLocationListByConId(Guid id)
        {
            var item = _repository.Query(x => x.SupplierContactID == id).Select().ToList();
            return item;
        }
    }
}
