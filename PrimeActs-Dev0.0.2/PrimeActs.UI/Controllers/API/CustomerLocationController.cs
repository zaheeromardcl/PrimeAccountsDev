using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Customer;

namespace PrimeActs.UI.Controllers.API
{
    public class CustomerLocationController : ApiController
    {
        private readonly ICustomerLocationOrchestra _customerLocationOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerLocationController(ICustomerLocationOrchestra customerLocationOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _customerLocationOrchestra = customerLocationOrchestra;
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<CustomerLocationModel> GetCustomerLocationModels(Guid CustomerDepartmentID)
        {
            var locationModels = _customerLocationOrchestra.GetCustomerLocationModels(CustomerDepartmentID);
            return locationModels;
        }

        [HttpGet]
        //public List<ItemViewModel> DDLInvoiceCustomerLocation(Guid? cdId)
        public List<ItemViewModel> DDLInvoiceCustomerLocation(Guid cdId)
        {
            //if (cdId.Value.GetType() == typeof(Guid?) && cdId.HasValue)
            if (cdId.GetType() == typeof(Guid) && cdId != null)
            {
                List<CustomerLocationModel> clmList = _customerLocationOrchestra
                    .GetCustomerLocationModelsForDDLinvc(cdId);
                    //.GetCustomerLocationModels(cdId.Value);
                var ivmList = new List<ItemViewModel>();
                foreach (var model in clmList)
                {
                    var item = new ItemViewModel
                    {
                        Id = model.CustomerLocationID.ToString(),
                        label = model.CustomerLocationName,
                        value = model.CustomerLocationID.ToString()
                    };
                    ivmList.Add(item);
                }
                return ivmList;
            }
            return null;
        }
    }
}
