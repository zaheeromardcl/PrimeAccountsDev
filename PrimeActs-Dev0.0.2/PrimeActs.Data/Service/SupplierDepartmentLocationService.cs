using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class SupplierDepartmentLocationService : Service<SupplierDepartmentLocation>, ISupplierDepartmentLocationService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<SupplierDepartmentLocation> _repository;
        private readonly object lockboject = new object();

        public SupplierDepartmentLocationService(IRepositoryAsync<SupplierDepartmentLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public List<SupplierDepartmentLocation> GetSupplierDepartmentLocationListByDepId(Guid id)
        {
            var item = _repository.Query(x => x.SupplierDepartmentID == id).Select().ToList();
            return item;
        }
    }
}
