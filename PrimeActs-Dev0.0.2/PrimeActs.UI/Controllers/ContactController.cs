#region

using System.Collections.Generic;
using System.Web.Mvc;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class ContactController : PrimeActsController
    {
        private readonly IContactOrchestra _contactOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public ContactController(IContactOrchestra contactOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _contactOrchestra = contactOrchestra;
            _unitofWork = unitofWork;
        }



    }
}