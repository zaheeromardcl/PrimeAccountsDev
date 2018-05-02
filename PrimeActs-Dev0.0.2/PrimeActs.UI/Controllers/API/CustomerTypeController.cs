using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

namespace PrimeActs.UI.Controllers.API
{
    public class CustomerTypeController : ApiController
    {
        private readonly ICustomerTypeOrchestra _customerTypeOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerTypeController(ICustomerTypeOrchestra customerTypeOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _customerTypeOrchestra = customerTypeOrchestra;
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            var list = new List<Autocomplete>();
            if (!string.IsNullOrEmpty(search))
            {
                var acItems = _customerTypeOrchestra.GetCustomerTypeItemsForAC(search);
                list = acItems.Select(inc => new Autocomplete
                {
                    Id = inc.CustomerTypeID.ToString(),
                    label = inc.CustomerTypeDescription,
                    value = inc.CustomerTypeDescription
                })
                .ToList();
            }
            return list;
        }
    }
}
