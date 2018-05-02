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
    public class PurchaseTypeService : Service<PurchaseType>, IPurchaseTypeService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<PurchaseType> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public PurchaseTypeService(IRepositoryAsync<PurchaseType> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (PurchaseType).FullName;
        }

        public PurchaseType PurchaseTypeByName(string PurchaseType)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PurchaseType>).Where(t => t.PurchaseTypeName == PurchaseType);
            return data == null ? null : data.FirstOrDefault();
        }

        public PurchaseType PurchaseTypeById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PurchaseType>).Where(t => t.PurchaseTypeID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(PurchaseType PurchaseType)
        {
            _repository.Insert(PurchaseType);
            RefreshCache();
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            CheckCache();
            return (_cache.Get(type) as List<PurchaseType>);
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
                    var PurchaseTypes = new List<PurchaseType>();
                    foreach (var entityType in _repository.Query(x => x.IsActive == true).Select())
                    {
                        PurchaseTypes.Add(new PurchaseType
                        {
                            PurchaseTypeID = entityType.PurchaseTypeID,
                            PurchaseTypeName = entityType.PurchaseTypeName,
                            PurchaseTypeCode = entityType.PurchaseTypeCode,
                            IsActive = entityType.IsActive,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, PurchaseTypes);
                }
            }
            return type;
        }
    }
}