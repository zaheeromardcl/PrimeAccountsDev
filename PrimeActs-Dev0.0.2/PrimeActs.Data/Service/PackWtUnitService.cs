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
    public class PackWtUnitService : Service<PackWtUnit>, IPackWtUnitService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<PackWtUnit> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public PackWtUnitService(IRepositoryAsync<PackWtUnit> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (PackWtUnit).FullName;
        }

        public PackWtUnit PackWtUnitByWtUnit(string wUnit)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PackWtUnit>).Where(t => t.WtUnit == wUnit);
            return data == null ? null : data.FirstOrDefault();
        }

        public PackWtUnit PackWtUnitById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PackWtUnit>).Where(t => t.PackWtUnitID == Id);
            return data == null ? null : data.FirstOrDefault();
        }


        public List<PackWtUnit> GetAllPackWtUnits()
        {
            CheckCache();
            return (_cache.Get(type) as List<PackWtUnit>);
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
                    var packWtUnits = new List<PackWtUnit>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        packWtUnits.Add(new PackWtUnit
                        {
                            PackWtUnitID = entityType.PackWtUnitID,
                            WtUnit = entityType.WtUnit,
                            KgMultiple = entityType.KgMultiple,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, packWtUnits);
                }
            }
            return type;
        }
    }
}