#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;

#endregion

namespace PrimeActs.Orchestras
{
    public class CurrencyOrchestra : ICurrencyOrchestra
    {
        private readonly ICurrencyService _currencyService;
 

        public CurrencyOrchestra(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }


        public List<CurrencyEditModel> GetCurrencyModelsForAC(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _currencyService.GetAllCurrencys()
                    .Where(x => x.CurrencyName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) || x.CurrencyCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(x => x.CurrencyName)
                    .Select(entity => BuildCurrencyEditModelAC(entity))
                    .ToList();
        }

        private CurrencyEditModel BuildCurrencyEditModelAC(Currency entity)
        {
            return new CurrencyEditModel
            {
                CurrencyID = entity.CurrencyID,
                CurrencyCode = entity.CurrencyCode,
               CurrencyName = entity.CurrencyName
            };
        }

      
    }
}