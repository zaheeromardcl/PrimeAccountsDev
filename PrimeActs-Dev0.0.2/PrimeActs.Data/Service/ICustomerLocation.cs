#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICustomerLocationService : IService<CustomerLocation>
    {
        //CustomerLocation LocationByName(string LocationName);
        CustomerLocation CustomerLocationById(Guid Id);
        List<CustomerLocation> GetAllCustomerLocationsOrderedByCustomer();
        List<CustomerLocation> GetAllCustomerLocationsByCustomerID(Guid id);
        List<CustomerLocation> GetAllCustomerLocations();
        void RefreshCache();


     
    }
}