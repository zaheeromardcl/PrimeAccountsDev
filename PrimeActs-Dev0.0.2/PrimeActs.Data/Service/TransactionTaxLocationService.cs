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
    public interface ITransactionTaxLocationService : IService<TransactionTaxLocation>
    {
        TransactionTaxLocation TransactionTaxLocationByName(string TransactionTaxDisplayName);
        TransactionTaxLocation TransactionTaxLocationById(Guid Id);
        List<TransactionTaxLocation> GetAllTransactionLocations();
        TransactionTaxLocation TransactionTaxLocationByCompanyID(Guid CompanyID);

        void RefreshCache();
    }

    public class TransactionTaxLocationService : Service<TransactionTaxLocation>, ITransactionTaxLocationService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<TransactionTaxLocation> _repository;
        private readonly object lockboject = new object();

        public TransactionTaxLocationService(IRepositoryAsync<TransactionTaxLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof(TransactionTaxLocation).FullName);
        }

        public TransactionTaxLocation TransactionTaxLocationByName(string TransactionTaxDisplayName)
        {
            var type = typeof(TransactionTaxLocation).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var TransactionTaxLocations = new List<TransactionTaxLocation>();
                    foreach (var entityType in _repository.Query().Include(md => md.TransactionTaxLocationID).Select().ToList())
                    {
                        TransactionTaxLocations.Add(new TransactionTaxLocation
                        {
                            TransactionTaxLocationID = entityType.TransactionTaxLocationID,
                            TransactionTaxLocationName = entityType.TransactionTaxLocationName,
                            TransactionTaxLocationNominalAccountID = entityType.TransactionTaxLocationNominalAccountID,
                            TransactionTaxDisplayName = entityType.TransactionTaxDisplayName,
                            TransactionTaxReference = entityType.TransactionTaxReference,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, TransactionTaxLocations);
                }
            }
            var data = (_cache.Get(type) as IEnumerable<TransactionTaxLocation>).Where(t => t.TransactionTaxLocationName == TransactionTaxDisplayName);
            return data == null ? null : data.FirstOrDefault();
        }

        public TransactionTaxLocation TransactionTaxLocationByCompanyID(Guid CompanyID)
        {
            TransactionTaxLocation varTransactionTaxLocation = _repository.Query().Select().Where(t => t.CompanyID == CompanyID).FirstOrDefault();
            return varTransactionTaxLocation;
        }

        public TransactionTaxLocation TransactionTaxLocationById(Guid TransactionTaxLocationID)
        {
            TransactionTaxLocation varTransactionTaxLocation = _repository.Query().Select().Where(t => t.TransactionTaxLocationID == TransactionTaxLocationID).FirstOrDefault();
            return varTransactionTaxLocation;
        }

        public List<TransactionTaxLocation> GetAllTransactionLocations()
        {
            List<TransactionTaxLocation> varTransactionTaxLocation = _repository.Query().Select().ToList();
            return varTransactionTaxLocation;
        }
    }
}
