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
    public class PurchaseChargeTypeService : Service<PurchaseChargeType>, IPurchaseChargeTypeService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<PurchaseChargeType> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public PurchaseChargeTypeService(IRepositoryAsync<PurchaseChargeType> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof(PurchaseChargeType).FullName;
        }

        public PurchaseChargeType PurchaseChargeTypeByName(string PurchaseChargeType)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PurchaseChargeType>).Where(t => t.PurchaseChargeTypeName == PurchaseChargeType);
            return data == null ? null : data.FirstOrDefault();
        }

        public PurchaseChargeType PurchaseChargeTypeById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PurchaseChargeType>).Where(t => t.PurchaseChargeTypeID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(PurchaseChargeType PurchaseChargeType)
        {
            _repository.Insert(PurchaseChargeType);
            RefreshCache();
        }

        public List<PurchaseChargeType> GetAllPurchaseChargeTypes()
        {
            CheckCache();
            return (_cache.Get(type) as List<PurchaseChargeType>);
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
                    var purchaseChargeTypes = new List<PurchaseChargeType>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        purchaseChargeTypes.Add(new PurchaseChargeType
                        {
                            PurchaseChargeTypeID = entityType.PurchaseChargeTypeID,
                            PurchaseChargeTypeName = entityType.PurchaseChargeTypeName,
                            PurchaseChargeTypeCode = entityType.PurchaseChargeTypeCode,
                            IsActive = entityType.IsActive,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, purchaseChargeTypes);
                }
            }
            return type;
        }
    }
}