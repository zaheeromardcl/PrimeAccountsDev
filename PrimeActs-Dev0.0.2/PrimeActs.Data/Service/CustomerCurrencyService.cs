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
    public class CustomerCurrencyService : Service<CustomerCurrency>, ICustomerCurrencyService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<CustomerCurrency> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public CustomerCurrencyService(IRepositoryAsync<CustomerCurrency> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (CustomerCurrency).FullName;
        }

        public CustomerCurrency CustomerCurrencyById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<CustomerCurrency>).Where(t => t.CustomerCurrencyID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(CustomerCurrency CustomerCurrency)
        {
            _repository.Insert(CustomerCurrency);
            RefreshCache();
        }

        public List<CustomerCurrency> GetAllCustomerCurrencys()
        {
            CheckCache();
            return (_cache.Get(type) as List<CustomerCurrency>);
        }


        public void RefreshCache()
        {
            _cache.Remove(type);
        }

        //public CustomerCurrency CustomerCurrencyByName(string CustomerCurrencyName)
        //{
        //    CheckCache();
        //    var data = (_cache.Get(type) as IEnumerable<CustomerCurrency>).Where(t => t.Currency.CurrencyName == CustomerCurrencyName);
        //    return data == null ? null : data.FirstOrDefault();
        //}

        private string CheckCache()
        {
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var CustomerCurrencys = new List<CustomerCurrency>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        CustomerCurrencys.Add(new CustomerCurrency
                        {
                            CustomerCurrencyID = entityType.CustomerCurrencyID,
                            CustomerID = entityType.CustomerID,
                            CurrencyID = entityType.CurrencyID
                        });
                    }
                    _cache.Set(type, CustomerCurrencys);
                }
            }
            return type;
        }
    }
}