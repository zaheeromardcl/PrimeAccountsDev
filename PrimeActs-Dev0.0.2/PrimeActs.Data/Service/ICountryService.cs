#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICountryService : IService<Country>
    {
        Country CountryByName(string CountryName);
        Country CountryById(Guid Id);
        List<Country> GetAllCountries();
     
        Country GetDefaultCountry();
    }
}