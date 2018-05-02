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
    public class CountryService : Service<Country>, ICountryService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Country> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public CountryService(IRepositoryAsync<Country> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof(Country).FullName;
        }

        public Country GetDefaultCountry()
        {
            //TODO ://to change in to setup local
            return _repository.Query(x => x.CountryCode == "UK")                                
                .Select()
                .FirstOrDefault();
        }

        public Country CountryByName(string countryName)
        {
            /*Country varCountryByName = _repository.Query().Select().Where(t => t.CountryName == countryName).FirstOrDefault();
            return varCountryByName;*/
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Country>).Where(t => t.CountryName == countryName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Country CountryByCode(string countryCode)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Country>).Where(t => t.CountryCode == countryCode);
            return data == null ? null : data.FirstOrDefault();
        }

        public Country CountryById(Guid Id)
        {
            /*Country varCountryById = _repository.Query().Select().Where(t => t.CountryID== Id).FirstOrDefault();
            return varCountryById;*/
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Country>).Where(t => t.CountryID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<Country> GetAllCountries()
        {
            /*List<Country> allcountries = new List<Country>();
            allcountries = _repository.Query().Select().ToList();
            return allcountries;*/
            CheckCache();
            return (_cache.Get(type) as List<Country>);
        }

        public override void Insert(Country Country)
        {
            _repository.Insert(Country);
            RefreshCache();
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
                    var countries = new List<Country>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        countries.Add(new Country
                        {
                            CountryID = entityType.CountryID,
                            CountryName = entityType.CountryName,
                            CountryCode = entityType.CountryCode,
                            CompanyID = entityType.CompanyID,
                            BankAccountNumberFormat = entityType.BankAccountNumberFormat,
                            BankCodeFormat = entityType.BankCodeFormat
                        });
                    }
                    _cache.Set(type, countries);
                }
            }
            return type;
        }
    }
}