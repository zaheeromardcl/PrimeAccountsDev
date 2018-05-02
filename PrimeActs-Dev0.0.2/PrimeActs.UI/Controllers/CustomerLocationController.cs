#region
using System.Collections.Generic;
using System.Web.Mvc;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
#endregion

namespace PrimeActs.UI.Controllers
{
    public class CustomerLocationController : PrimeActsController
    {
        private readonly ICustomerLocationOrchestra _customerLocationOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerLocationController(ICustomerLocationOrchestra customerLocationOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _customerLocationOrchestra = customerLocationOrchestra;
            _unitofWork = unitofWork;
        }
    }
}
