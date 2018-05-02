using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ICustomerLocationService : IService<CustomerLocation>
    {
        List<CustomerLocation> GetAllCustomerLocationsOrderedByCustomer();
        List<CustomerLocation> GetAllCustomerLocations();
        CustomerLocation CustomerLocationById(Guid id);
        List<CustomerLocation> GetAllCustomerLocationsByCustomerID(Guid id);
        List<CustomerLocation> GetAllCustomerLocationsByCusLocID(Guid id);
        void RefreshCache();
        //void CheckCache();
    }
}
//List<CustomerLocation> GetCustomerLocations(List<Guid> customerLocationIds);
