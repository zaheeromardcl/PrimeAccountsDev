#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICustomerService : IService<Customer>
    {
        Customer CustomerByName(string CustomerName);
        Customer CustomerById(Guid Id);
        Customer GetCustomerByCode(string customerCode);
        List<Customer> GetAllCustomers();
        List<Customer> GetAllCustomersByDivision(Guid id);
        Customer GetCustomerByIdFromRepo(Guid Id);
        List<Customer> GetCustomers(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        void RefreshCache();
    }
}