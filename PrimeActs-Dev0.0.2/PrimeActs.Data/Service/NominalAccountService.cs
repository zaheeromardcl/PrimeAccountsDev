#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class NominalAccountService : Service<NominalAccount>, INominalAccountService
    {
        private readonly ICache _cache;
        private readonly object _lockboject = new object();
        private readonly IRepositoryAsync<NominalAccount> _repository;
        private readonly string _type;


        public NominalAccountService(IRepositoryAsync<NominalAccount> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            _type = typeof (NominalAccount).FullName;
        }

        public NominalAccount NominalAccountByName(string code)
        {
            CheckCache();
            var data = (_cache.Get(_type) as IEnumerable<NominalAccount>).Where(t => t.NominalAccountName == code);
            return data.FirstOrDefault();
        }

        public NominalAccount NominalAccountById(Guid id)
        {
            CheckCache();
            var data = (_cache.Get(_type) as IEnumerable<NominalAccount>).Where(t => t.NominalAccountID == id);
            return data.FirstOrDefault();
        }


        public List<NominalAccount> GetAllNominalAccounts()
        {
            CheckCache();
            return (_cache.Get(_type) as List<NominalAccount>);
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
                    var NominalAccounts = _repository.Query().Select().Select(entityType => new NominalAccount
                    {
                        NominalAccountID = entityType.NominalAccountID,
                        NominalAccountName = entityType.NominalAccountName,
                        NominalCode = entityType.NominalCode
                    }).ToList();
                    _cache.Set(_type, NominalAccounts);
                }
            }
        }
    }
}