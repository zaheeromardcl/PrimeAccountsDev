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
    public class DespatchService : Service<DespatchLocation>, IDespatchService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<DespatchLocation> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public DespatchService(IRepositoryAsync<DespatchLocation> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (DespatchLocation).FullName;
        }

        public DespatchLocation DespatchByName(string despatchName)
        {
            CheckCache();
            var data =
                (_cache.Get(type) as IEnumerable<DespatchLocation>).Where(t => t.DespatchLocationName == despatchName);
            return data == null ? null : data.FirstOrDefault();
        }

        public DespatchLocation DespatchById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<DespatchLocation>).Where(t => t.DespatchLocationID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(DespatchLocation despatch)
        {
            _repository.Insert(despatch);
            RefreshCache();
        }

        public List<DespatchLocation> GetAllDespatches()
        {
            CheckCache();
            return (_cache.Get(type) as List<DespatchLocation>);
        }


        public void RefreshCache()
        {
            _cache.Remove(type);
        }

        private string CheckCache()
        {
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var Despatchs = new List<DespatchLocation>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        Despatchs.Add(new DespatchLocation
                        {
                            DespatchLocationID = entityType.DespatchLocationID,
                            DespatchLocationName = entityType.DespatchLocationName,
                            DespatchLocationCode = entityType.DespatchLocationCode,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, Despatchs);
                }
            }
            return type;
        }
    }
}