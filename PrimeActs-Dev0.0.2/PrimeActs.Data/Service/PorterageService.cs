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
    public class PorterageService : Service<Porterage>, IPorterageService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Porterage> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public PorterageService(IRepositoryAsync<Porterage> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (Porterage).FullName;
        }

        public Porterage PorterageByCode(string portCode)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Porterage>).Where(t => t.PorterageCode == portCode);
            return data == null ? null : data.FirstOrDefault();
        }

        public Porterage PorterageById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Porterage>).Where(t => t.PorterageID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(Porterage port)
        {
            _repository.Insert(port);
            RefreshCache();
        }

        public List<Porterage> GetAllPorterages()
        {
            CheckCache();
            return (_cache.Get(type) as List<Porterage>);
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
                    var ports = new List<Porterage>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        ports.Add(new Porterage
                        {
                            PorterageID = entityType.PorterageID,
                            PorterageCode = entityType.PorterageCode,
                            DepartmentID = entityType.DepartmentID,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, ports);
                }
            }
            return type;
        }
    }
}