using Newtonsoft.Json;
using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Domain.ViewModels.Users;

namespace PrimeActs.UI
{
    public static class IdentityExtensions
    {
        public static ApplicationUser GetApplicationUser(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null || claimsIdentity.IsAuthenticated == false)
            {
                throw new AuthenticationException("User is not logged in.");
            }

            var applicationUserClaim = claimsIdentity.FindFirst("PrimeActs:ApplicationUser");
            if (applicationUserClaim == null)
            {
                throw new AuthenticationException("Claim does not exist.");
            }

            return JsonConvert.DeserializeObject<ApplicationUser>(applicationUserClaim.Value);
        }

        public static Claim ReplaceClaim(this IIdentity identity, Claim newClaim, IAuthenticationManager authenticationManager)
        {
            var claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null || claimsIdentity.IsAuthenticated == false)
            {
                throw new AuthenticationException("User is not logged in.");
            }

            var applicationUserClaim = claimsIdentity.FindFirst("PrimeActs:ApplicationUser");
            if (applicationUserClaim == null)
            {
                throw new AuthenticationException("Claim does not exist.");
            }

            claimsIdentity.RemoveClaim(applicationUserClaim);

            applicationUserClaim = claimsIdentity.FindFirst("PrimeActs:ApplicationUser");
            if (applicationUserClaim == null)
            {
                var w = 2;
            }

            claimsIdentity.AddClaim(newClaim);

            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = true });
            
            return applicationUserClaim;
        }
    }
}