#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

using System.Web;
//using System.Web.Mvc;
using System.Web.Http;
using System.Text;
using Microsoft.AspNet.Identity;
using PrimeActs.Data.Service;


#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class LookupTablesController : ApiController
    {
        private readonly ILookupOrchestra _lookupOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;


        public LookupTablesController(ILookupOrchestra lookupOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _lookupOrchestra = lookupOrchestra;
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public LookupLists GetLookupLists()
        {
            try
            {
                var applicationUser = User.Identity.GetApplicationUser();
                _lookupOrchestra.Initialize(applicationUser);
            return _lookupOrchestra.GetLookupLists();
        }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}