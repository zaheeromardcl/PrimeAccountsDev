#region

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI
{
    public class PrimeActsAuthorizeAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        public string OperationKey { get; set; }
        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login" })
                    );
            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "NotAuthorized" })
                    );
            }
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool hasPermission = false;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                hasPermission = httpContext.User.Identity.GetApplicationUser()
                    .Permissions.Any(p => OperationKey.Equals(p.PermissionController + "-" + p.PermissionAction));
            }
            
            return hasPermission;
        }
    }

    public class PrimeActsAuthorizeAttributeAPI : System.Web.Http.AuthorizeAttribute
    {
        public string OperationKey { get; set; }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var currentPrincipal = HttpContext.Current.User.Identity.GetApplicationUser();
            if (currentPrincipal != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(OperationKey)) return true;
                var hasPermission = currentPrincipal.Permissions.Any(
                        p => OperationKey.Equals(p.PermissionController + "-" + p.PermissionAction));
                return hasPermission;
            }
            else
            {
                actionContext.Response =
                    new HttpResponseMessage(
                    System.Net.HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "User not authenticated."
                    };
                return false;
            }
        }
        
        protected string[] RolesSplit
        {
            get { return SplitStrings(OperationKey); }
        }

        protected static string[] SplitStrings(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return new string[0];
            var result = input.Split('-').Where(s => !String.IsNullOrWhiteSpace(s.Trim()));
            return result.Select(s => s.Trim()).ToArray();
        }
    }
}