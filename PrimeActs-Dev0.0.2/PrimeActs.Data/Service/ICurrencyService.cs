#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICurrencyService : IService<Currency>
    {
        Currency CurrencyByName(string CurrencyName);
        Currency CurrencyById(Guid Id);
        Currency GetByCurrencyCode(string currencyCode);
        List<Currency> GetAllCurrencys();
        void RefreshCache();
      
    }
}