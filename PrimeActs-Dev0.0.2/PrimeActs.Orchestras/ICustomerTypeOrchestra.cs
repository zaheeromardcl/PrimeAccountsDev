using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public interface ICustomerTypeOrchestra
    {
        List<CustomerTypeEditModel> GetCustomerTypeItemsForAC(string search);
    }
}
