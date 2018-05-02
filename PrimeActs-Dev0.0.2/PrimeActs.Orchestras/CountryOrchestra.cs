#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public class CountryOrchestra : ICountryOrchestra
    {
        private readonly ICountryService _countryService;
        private ApplicationUser _principal;

        public CountryOrchestra(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public void Initialize1(ApplicationUser principal)
        {
            _principal = principal;
           
        }

        public List<CountryEditModel> GetCountryModelsForAC(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _countryService.GetAllCountries()
                    .Where(
                        x =>
                            x.CountryCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) |
                            x.CountryName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    .Select(BuildCountryEditModelAC)
                    .ToList();
        }
       

        private CountryEditModel BuildCountryEditModelAC(Country entity)
        {
            return new CountryEditModel
            {
                CountryID = entity.CountryID,
                CountryCode = entity.CountryCode,
                CountryName = entity.CountryName
            };
        }

         
    }
}
