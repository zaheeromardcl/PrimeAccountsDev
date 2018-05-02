using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ICustomerTypeService : IService<CustomerType>
    {
        CustomerType CustomerTypeByDescription(string customerTypeDescription);
        CustomerType CustomerTypeById(Guid Id);
        CustomerType CustomerTypeByCode(string customerTypeCode);
        List<CustomerType> ListOfCustomerTypes();
        void RefreshCache();
    }
}
