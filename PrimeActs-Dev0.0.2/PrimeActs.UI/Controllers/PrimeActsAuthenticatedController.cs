#region

using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.Identity.Owin;

#endregion

namespace PrimeActs.UI.Controllers
{
    public abstract class PrimeActsAuthenticatedController : PrimeActsController
    {
        private PrimeActsRoleManager _roleManager;

        private PrimeActsUserManager _userManager;

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        public PrimeActsRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<PrimeActsRoleManager>(); }
            private set { _roleManager = value; }
        }

        // GET: PrimeActs

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            PopulateUser();
        }

        public abstract void PopulateUser();
    }
}