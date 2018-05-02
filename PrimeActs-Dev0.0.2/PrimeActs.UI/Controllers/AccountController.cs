#region

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PrimeActs.UI.Models;
using PrimeActs.Domain;

#endregion

//this my changes77/7

namespace PrimeActs.UI.Controllers
{
    //TODO:
    public class Department
    {
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }

    [Authorize]
    public class AccountController : PrimeActsController
    {
        private PrimeActsRoleManager _roleManager;
        private PrimeActsSignInManager _signInManager;
        private PrimeActsUserManager _userManager;
        private string _serverCode = "L";//Need to change with actual at runtime.

        
        //commment

        public AccountController()
        {
        }

        public AccountController(PrimeActsUserManager userManager, PrimeActsSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public PrimeActsSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<PrimeActsSignInManager>(); }
            private set { _signInManager = value; }
        }

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

        //falsepositive
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if (User.Identity.IsAuthenticated == true)
            {
                return Redirect("/Home/Index/");
            }
            else { 
 
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel {URL = returnUrl});
            }
        }

        //[HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> DoesUserEmailExist(string Email)
        {
            var result = await UserManager.FindByNameAsync(Email) ??
                         await UserManager.FindByEmailAsync(Email);
            return Json(result == null, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result =
                await
                    SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe,
                        model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {Id = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]), UserName = model.Username, Email = model.Email};
                //TODO:
                //user.DepartmentId = Guid.Parse(model.SelectedDepartment.ToString());
                user.LastLoggedOn = DateTime.Today;

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userRoles = await UserManager.GetRolesAsync(user.Id);
                    var roleresult = await RoleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                    var selectedRole = RoleManager.Roles.ToList().First(x => x.Name == "Admin");

                    var resultRoles = await UserManager.AddToRolesAsync(user.Id, selectedRole.Name);


                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code},
                        Request.Url.Scheme);
                    await
                        UserManager.SendEmailAsync(user.Id, "Confirm your account",
                            "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return View("RegisterSummary", model);
                }
                AddErrors(result);
            }

            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (userId == null || code == null)
                {
                    return View("Error");
                }
                //var code = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(codeNotEncoded));
                code = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(code));
                var result = await UserManager.ConfirmEmailAsync(Guid.Parse(userId), code);
                return View(result.Succeeded ? "ConfirmEmail" : "Error");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword(string title)
        {
            var model = new ForgotPasswordViewModel();
            if (!string.IsNullOrEmpty(title) && title == "Forgot")
            {
                ViewBag.Title = "Forgot Password";
                model.Title = "Forgot";
            }

            else
            {
                ViewBag.Title = "Resend Email Varification";
            }
            return View(model);
        }


        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model, string btnNo)
        {
            if (btnNo.Equals("Resend Confirmation"))
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByNameAsync(model.Username);
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    code = HttpUtility.UrlEncode(code);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code},
                        Request.Url.Scheme);
                    await
                        UserManager.SendEmailAsync(user.Id, "Confirm your account",
                            "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return View("CheckYourMail");
                }
                return View();
            }
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Username);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                await
                    UserManager.SendEmailAsync(user.Id, "Reset Password",
                        "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            if (!string.IsNullOrEmpty(model.Title) && model.Title == "Forgot")
            {
                ViewBag.Title = "Forgot Password";
                model.Title = "Forgot";
            }

            else
            {
                ViewBag.Title = "Resend Email Varification";
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //public async Task<ActionResult> ResendPassword(ForgotPasswordViewModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Username);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //        await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }
        //    if (!string.IsNullOrEmpty(model.Title) && model.Title == "Forgot")
        //    {
        //        ViewBag.Title = "Forgot Password";
        //        model.Title = "Forgot";
        //    }

        //    else
        //    {
        //        ViewBag.Title = "Resend Email Varification";
        //    }
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET:
        [AllowAnonymous]
        public ActionResult CheckYourMail()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View(new ResetPasswordViewModel {Code = code});
        }


        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions =
                userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose}).ToList();
            return
                View(new SendCodeViewModel {Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode",
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, RememberMe = false});
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel {Email = loginInfo.Email});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //sdfdsf
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}