#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICustomerCurrencyService : IService<CustomerCurrency>
    {
        //CustomerCurrency CustomerCurrencyByName(string CustomerName);
        CustomerCurrency CustomerCurrencyById(Guid Id);
        List<CustomerCurrency> GetAllCustomerCurrencys();
        void RefreshCache();
    }
}