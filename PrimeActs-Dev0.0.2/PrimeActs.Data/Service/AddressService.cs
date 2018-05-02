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
    public interface IAddressService : IService<Address>
    {
        Address AddressByPostCode(string PostCode);
        Address AddressById(Guid Id);
        List<Address> GetAllAddresses();
        void RefreshCache();
    }
    public class AddressService : Service<Address>, IAddressService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Address> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;
        
        public AddressService(IRepositoryAsync<Address> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (Address).FullName;
        }

        public Address AddressByPostCode(string Postcode)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Address>).Where(t => t.Postcode == Postcode);
            return data == null ? null : data.FirstOrDefault();
        }

        public Address AddressById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Address>).Where(t => t.AddressID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<Address> GetAllAddresses()
        {
            CheckCache();
            return (_cache.Get(type) as List<Address>);
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
                    var currencies = new List<Address>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        currencies.Add(new Address
                        {
                            AddressID = entityType.AddressID,
                            Postcode = entityType.Postcode,
                            AddressLine1 = entityType.AddressLine1,
                            CountyCity = entityType.CountyCity
                        });
                    }
                    _cache.Set(type, currencies);
                }
            }
            return type;
        }
    }
}