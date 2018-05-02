#region

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PrimeActs.UI.Models;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using Microsoft.AspNet.SignalR;
using Ninject;
using PrimeActs.Data.Service;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System.Collections.Generic;
using System.Web.Mvc;

#endregion

namespace PrimeActs.UI
{
    public partial class Startup
    {
        
   
        
        public void ConfigureAuth(IAppBuilder app)
        {            
            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<PrimeActsUserManager>(PrimeActsUserManager.Create);
            app.CreatePerOwinContext<PrimeActsRoleManager>(PrimeActsRoleManager.Create);
            app.CreatePerOwinContext<PrimeActsSignInManager>(PrimeActsSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<PrimeActsUserManager, ApplicationUser, Guid>(
                        TimeSpan.FromMinutes(480),
                        (manager, user) => user.GenerateUserIdentityAsync(manager),
                        id => Guid.Parse(id.GetUserId()))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            ///SignalR with dependency injection - do not remove.
            var ninjectHubActivator = new MvcHubActivator();
            GlobalHost.DependencyResolver.Register(
                typeof(IHubActivator),
                () => ninjectHubActivator);

            app.MapSignalR();

          
            
          
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }

    ///SignalR with dependency injection - do not remove.
    public class MvcHubActivator : IHubActivator
    {
        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)DependencyResolver.Current
                .GetService(descriptor.HubType);
        }
    }


}