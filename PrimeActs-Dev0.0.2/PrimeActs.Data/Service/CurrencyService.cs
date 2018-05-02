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

    public class CurrencyService : Service<Currency>, ICurrencyService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Currency> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public CurrencyService(IRepositoryAsync<Currency> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (Currency).FullName;
        }

        public Currency CurrencyByName(string name)
        {
            var returnedCurrency = _repository.Query(t => t.CurrencyName==name).Select().FirstOrDefault();
            return returnedCurrency;
            //CheckCache();
            //var data = (_cache.Get(type) as IEnumerable<Currency>).Where(t => t.CurrencyName == name);
            //return data == null ? null : data.FirstOrDefault();
        }

        public Currency CurrencyById(Guid Id)
        {
            var returnedCurrency = _repository.Query().Select().Where(t => t.CurrencyID==Id).FirstOrDefault();
            return returnedCurrency;

            //CheckCache();
            //var data = (_cache.Get(type) as IEnumerable<Currency>).Where(t => t.CurrencyID == Id);
            //return data == null ? null : data.FirstOrDefault();
        }

        public Currency GetByCurrencyCode(string currencyCode)
        {
            var returnedCurrency = _repository.Query(t => t.CurrencyCode == currencyCode).Select().FirstOrDefault();
            return returnedCurrency;

            //CheckCache();
            //return (_cache.Get(type) as IEnumerable<Currency>).FirstOrDefault(x => x.CurrencyCode == currencyCode);
        }

        public List<Currency> GetAllCurrencys()
        {
            var returnedCurrency = _repository.Query().Select().ToList();
            return returnedCurrency;

           
            //CheckCache();
            //return (_cache.Get(type) as List<Currency>);
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
                    var currencies = new List<Currency>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        currencies.Add(new Currency
                        {
                            CurrencyID = entityType.CurrencyID,
                            CurrencyName = entityType.CurrencyName,
                            CurrencyCode = entityType.CurrencyCode,
                            DefaultExchangeRate = entityType.DefaultExchangeRate,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, currencies);
                }
            }
            return type;
        }
    }
}