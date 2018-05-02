#region

using System.Collections.Generic;
using System.Web.Http;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class UserController : ApiController
    {
        private IUnitOfWorkAsync _unitofWork;
        private IApplicationUserOrchestra _applicationUserOrchestra;

        public UserController(IApplicationUserOrchestra applicationUserOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _applicationUserOrchestra = applicationUserOrchestra;
            _unitofWork = unitofWork;
        }

        //public IEnumerable<PrimeActs.UI.Models.Autocomplete> GetAllUsers(string search)
        //{
        //    return _userOrchestra.GetUserForAutoComplete(search).Select(inc=>new PrimeActs.UI.Models.Autocomplete{Id=inc.Id.ToString(),value=inc.Id.ToString(),label=inc.UserName});
        //} 


        public IEnumerable<ApplicationUser> GetAllUsers(string Id)
        {
            return null;
            // _userOrchestra.GetUsers(Id).Select(inc => new { Id = inc.Id.ToString(), value = inc.Id.ToString(), label = inc.UserName });
        }
    }
}