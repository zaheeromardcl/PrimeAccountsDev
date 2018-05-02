#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class CustomerCurrencyController : ApiController
    {
        private readonly ICustomerCurrencyOrchestra _currencyCustomerOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerCurrencyController(ICustomerCurrencyOrchestra customerCurrencyOrchestra,
            IUnitOfWorkAsync unitofWork)
        {
            _currencyCustomerOrchestra = customerCurrencyOrchestra;
            _unitofWork = unitofWork;
        }

        public IEnumerable<Autocomplete> GetAllCustomerCurrencys(string search)
        {
            return
                _currencyCustomerOrchestra.GetCustomerCurrencyForAutoComplete(search)
                    .Select(
                        inc =>
                            new Autocomplete
                            {
                                Id = inc.CustomerCurrencyID.ToString(),
                                value = inc.CustomerCurrencyID.ToString()
                            });
        }
    }
}