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
    public interface ITransactionTaxCodeService : IService<TransactionTaxCode>
    {
        TransactionTaxCode TransactionTaxCodeByName(string TransactionTaxCode);
        TransactionTaxCode TransactionTaxCodeById(Guid Id);
        List<TransactionTaxCode> GetAllTransactionTaxCodes();
        TransactionTaxCode TransactionTaxCodeByConsignmentItemID(Guid ConsignmentItemID);
       
        void RefreshCache();
    }

    public class TransactionTaxCodeService : Service<TransactionTaxCode>, ITransactionTaxCodeService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<TransactionTaxCode> _repository;
        private readonly object lockboject = new object();

        public TransactionTaxCodeService(IRepositoryAsync<TransactionTaxCode> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public TransactionTaxCode TransactionTaxCodeByName(string TransactionTaxCode)
        {
            var type = typeof (TransactionTaxCode).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var TransactionTaxCodes = new List<TransactionTaxCode>();
                    foreach (var entityType in _repository.Query().Include(md => md.TransactionTaxCodeID).Select().ToList())
                    {
                        TransactionTaxCodes.Add(new TransactionTaxCode
                        {
                            TransactionTaxCodeID = entityType.TransactionTaxCodeID,
                            TransactionTaxCodeValue = entityType.TransactionTaxCodeValue
                        });
                    }
                    _cache.Set(type, TransactionTaxCodes);
                }
            }
            var data = (_cache.Get(type) as IEnumerable<TransactionTaxCode>).Where(t => t.TransactionTaxCodeValue == TransactionTaxCode);
            return data == null ? null : data.FirstOrDefault();
        }

        public TransactionTaxCode TransactionTaxCodeByConsignmentItemID(Guid ConsignmentItemID)
        {
           TransactionTaxCode varTransactionTaxCode = new TransactionTaxCode();
            //TransactionTaxCode varTransactionTaxCode = _repository.Query().Include(x => x.Produces.Select(y => y.ConsignmentItems.Where(z => z.ConsignmentItemID == ConsignmentItemID))).Select().SingleOrDefault();
           return varTransactionTaxCode;
           
        }

       





        public TransactionTaxCode TransactionTaxRateByTaxCodeID(Guid TransactionTaxCodeID)
        {
            TransactionTaxCode varTransactionTaxCode_Rate = new TransactionTaxCode();

                //_repository
                //    .Query()
                //    .Include(x => x.TransactionTaxRates)
                //    .Select()
                //    .Where(y => y.TransactionTaxCodeID == TransactionTaxCodeID)
                //    .ToList()
                //    .SingleOrDefault();

            return varTransactionTaxCode_Rate;

           
        }






        public TransactionTaxCode TransactionTaxCodeById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(typeof (TransactionTaxCode).FullName) as IEnumerable<TransactionTaxCode>).Where(t => t.TransactionTaxCodeID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<TransactionTaxCode> GetAllTransactionTaxCodes()
        {
            CheckCache();
            var returnValue = (_cache.Get(typeof (TransactionTaxCode).FullName) as List<TransactionTaxCode>);
            if (returnValue == null)
                return new List<TransactionTaxCode>();
            return returnValue;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof (TransactionTaxCode).FullName);
        }

        private void CheckCache()
        {
            var type = typeof (TransactionTaxCode).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var TransactionTaxCode = new List<TransactionTaxCode>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        TransactionTaxCode.Add(new TransactionTaxCode
                        {
                            TransactionTaxCodeID = entityType.TransactionTaxCodeID,
                            TransactionTaxCodeValue = entityType.TransactionTaxCodeValue,
                            TransactionTaxCodeDescription = entityType.TransactionTaxCodeDescription,
                            IsActive = entityType.IsActive
                        });
                    }
                    _cache.Set(type, TransactionTaxCode);
                }
            }
        }
    }
}