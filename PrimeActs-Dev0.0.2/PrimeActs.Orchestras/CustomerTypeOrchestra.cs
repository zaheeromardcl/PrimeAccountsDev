using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Data.Service;

namespace PrimeActs.Orchestras
{
    public class CustomerTypeOrchestra : ICustomerTypeOrchestra
    {
        private readonly ICustomerTypeService _customerTypeService;

        public CustomerTypeOrchestra(ICustomerTypeService customerTypeService)
        {
            _customerTypeService = customerTypeService;
        }

        public List<CustomerTypeEditModel> GetCustomerTypeItemsForAC(string search)
        {
            var ctList = _customerTypeService.Query(x => 
                        //x.CustomerTypeCode.StartsWith(search) |
                        x.CustomerTypeDescription.StartsWith(search))
                    .Select().ToList();
            List<CustomerTypeEditModel> ctemList = new List<CustomerTypeEditModel>();
            foreach (var item in ctList)
                ctemList.Add(BuildCustomerTypeEditModelAC(item));
            return ctemList;
        }

        private CustomerTypeEditModel BuildCustomerTypeEditModelAC(CustomerType entity)
        {
            CustomerTypeEditModel ctem = new CustomerTypeEditModel();
            ctem.CustomerTypeDescription = entity.CustomerTypeDescription;
            ctem.CustomerTypeCode = entity.CustomerTypeCode;
            ctem.CustomerTypeID = entity.CustomerTypeID;
            return ctem;
        }
    }
}
