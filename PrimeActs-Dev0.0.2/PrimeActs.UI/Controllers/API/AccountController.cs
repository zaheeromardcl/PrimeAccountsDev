using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.UI.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PrimeActs.Domain;
using PrimeActs.Orchestras;


//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain;
//using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
//using PrimeActs.Orchestras;
//using PrimeActs.UI.Models;
//using System;
//using System.Linq;
//using System.Net.Http;
using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;
using System.Web;
using PrimeActs.Domain.ViewModels.Users;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;
using PrimeActs.Infrastructure.Extensions;





namespace PrimeActs.UI.Controllers.API
{
    [Authorize]
    public class AccountController : ApiController
    {
        private PrimeActsSignInManager _signInManager;
        private IApplicationUserOrchestra _applicationUserOrchestra;
        private PrimeActsUserManager _userManager;

        public AccountController(IApplicationUserOrchestra applicationUserOrchestra)
        {
            _applicationUserOrchestra = applicationUserOrchestra;
        }

        public AccountController(PrimeActsSignInManager signInManager, IApplicationUserOrchestra applicationUserOrchestra, PrimeActsUserManager userManager)
        {
            _signInManager = signInManager;
            _applicationUserOrchestra = applicationUserOrchestra;
            _userManager = userManager;
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        public PrimeActsSignInManager SignInManager
        {
            get { return _signInManager ?? Request.GetOwinContext().Get<PrimeActsSignInManager>(); }
            private set { _signInManager = value; }
        }

        public IApplicationUserOrchestra ApplicationUserOrchestra
        {
            get { return _applicationUserOrchestra ?? Request.GetOwinContext().Get<IApplicationUserOrchestra>(); }
            private set { _applicationUserOrchestra = value; }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> LoginInvoiceAdmin(LoginViewModel model)
        {
            var appUser = ApplicationUserOrchestra.FindById(Guid.Parse(User.Identity.GetUserId()));
            var result = SignInManager.UserManager.PasswordHasher.VerifyHashedPassword(appUser.AdminPasswordHash, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                ChangeApplicationUserContext(true);
                return Request.CreateResponse(HttpStatusCode.OK, new {Url = "Index/17"});
            }
            else
            {
                ChangeApplicationUserContext(false);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid login attempt.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "User already logged in. User must logout first.");
            }
            var user = await UserManager.FindByNameAsync(model.Username);
            if (!user.EmailConfirmed) {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Email address has not been confirmed. Please check your email and click on link to confirm email address.");
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            try
            {
                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        return Request.CreateResponse(HttpStatusCode.OK, new { Url = "Home/Index" });
                    //return new JsonMessage
                    //{
                    //    StatusId = 1,
                    //    Message = "",
                    //    URL = model.URL ?? "Home/index"
                    //};
                    case SignInStatus.RequiresVerification:
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Requires verification.");
                    case SignInStatus.LockedOut:
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "User lockedout, contact administrator.");
                    case SignInStatus.Failure:
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid login attempt.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid login attempt.");
            }
        }

        public class SelectedUserContext
        {
            public Guid SelectedCompanyID { get; set; }
            public Guid SelectedDivisionID { get; set; }
            public Guid SelectedDepartmentID { get; set; }
        }

        [HttpPost]
        public void ChangeApplicationUserContext(SelectedUserContext selectedUserContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                var applicationUser = User.Identity.GetApplicationUser();
                applicationUser.SelectedCompanyId = selectedUserContext.SelectedCompanyID;
                applicationUser.SelectedDivisionId = selectedUserContext.SelectedDivisionID;
                applicationUser.SelectedDepartmentId = selectedUserContext.SelectedDepartmentID;
                
                var newClaim = applicationUser.GenerateClaim();
                
                User.Identity.ReplaceClaim(newClaim, SignInManager.AuthenticationManager);
            }
        }

        private void ChangeApplicationUserContext(bool isInvoiceAdminAuthenticated)
        {
            if (User.Identity.IsAuthenticated)
            {
                var applicationUser = User.Identity.GetApplicationUser();

                applicationUser.IsInvoiceAdminAuthenticated = isInvoiceAdminAuthenticated;
                
                var newClaim = applicationUser.GenerateClaim();

                User.Identity.ReplaceClaim(newClaim, SignInManager.AuthenticationManager);
            }
        }
    }
}
