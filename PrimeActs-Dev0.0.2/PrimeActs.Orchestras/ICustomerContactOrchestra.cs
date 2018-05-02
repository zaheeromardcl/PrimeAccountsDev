using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Customer;

namespace PrimeActs.Orchestras
{
    public interface ICustomerContactOrchestra
    {
        CustomerContactModel CreateCustomerContact(CustomerContactModel model, List<Guid> locaGuidList, List<Guid> depaGuidList);
        List<CustomerContactModel> BuildCustomerContactModels(List<CustomerContact> customerContacts);
        List<CustomerContactModel> GetCustomerContactModels(List<Guid> customerContactIds);
        void Initialize(ApplicationUser principal);
    }
}