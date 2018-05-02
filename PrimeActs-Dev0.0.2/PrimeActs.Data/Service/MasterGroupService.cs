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
    public class MasterGroupService : Service<MasterGroup>, IMasterGroupService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<MasterGroup> _repository;
        private readonly object lockboject = new object();

        public MasterGroupService(IRepositoryAsync<MasterGroup> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public MasterGroup MasterGroupByName(string masterGroupName)
        {
            var type = typeof (MasterGroup).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var masterGroups = new List<MasterGroup>();
                    foreach (var entityType in _repository.Query().Select().ToList())
                    {
                        masterGroups.Add(new MasterGroup
                        {
                            MasterGroupID = entityType.MasterGroupID,
                            MasterGroupName = entityType.MasterGroupName
                        });
                    }
                    _cache.Set(type, masterGroups);
                }
            }
            var data = (_cache.Get(type) as IEnumerable<MasterGroup>).Where(t => t.MasterGroupName == masterGroupName);
            return data == null ? null : data.FirstOrDefault();
        }

        public MasterGroup MasterGroupById(Guid Id)
        {
            var type = typeof (MasterGroup).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var companies = new List<MasterGroup>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        companies.Add(new MasterGroup
                        {
                            MasterGroupID = entityType.MasterGroupID,
                            MasterGroupName = entityType.MasterGroupName
                        });
                    }
                    _cache.Set(type, companies);
                }
            }
            var data = (_cache.Get(type) as IEnumerable<MasterGroup>).Where(t => t.MasterGroupID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<MasterGroup> GetAllMasterGroups()
        {
            var type = typeof (MasterGroup).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    _cache.Set(type, _repository.Query().Select().ToList());
                }
            }
            return (_cache.Get(type) as List<MasterGroup>);
            ;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof (MasterGroup).FullName);
        }
    }
}