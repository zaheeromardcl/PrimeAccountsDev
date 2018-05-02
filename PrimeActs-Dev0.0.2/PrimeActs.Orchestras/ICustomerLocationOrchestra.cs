using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Customer;

namespace PrimeActs.Orchestras
{
    public interface ICustomerLocationOrchestra
    {
        List<CustomerLocationModel> GetCustomerLocationModels(Guid cdId);
        CustomerLocationModel CreateCustomerLocation(CustomerLocationModel model);
        List<CustomerLocationModel> GetCustomerLocationModelsForDDLinvc(Guid cdId);
        List<CustomerLocationModel> BuildCustomerLocationModels(List<CustomerLocation> supplierLocations);
        void Initialize(ApplicationUser principal);
    }
}
