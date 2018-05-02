#region

using System;
using System.Data.Entity;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PrimeActs.UI.Models;
using PrimeActs.Domain;
using PrimeActs.Data.Service;
using System.Web.Mvc;
using PrimeActs.Orchestras;
using System.Security.Claims;
using PrimeActs.Infrastructure.EntityFramework;

#endregion
 
namespace PrimeActs.UI
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var setupLocalService = DependencyResolver.Current.GetService<ISetupLocalService>();
            var setupGlobalService = DependencyResolver.Current.GetService<ISetupGlobalService>();
            using (SmtpClient client = new SmtpClient())
            {
                string sentFrom;
                try
                {
                    sentFrom = setupGlobalService.GetAllSetupValuesBySetupName("IdentityEmailSender")[0].SetupValueNvarchar;
                    client.Host = setupLocalService.Find("SMTPServerURL").SetupValueNvarchar;
                    client.Port = (int)setupLocalService.Find("SMTPServerPort").SetupValueInt;
                }
                catch (Exception e)
                {
                    throw new System.Exception("There is a problem with the email setup. Please contact technical support.");
                }
                using (var mailMesssage = new MailMessage(sentFrom, message.Destination, message.Subject, message.Body))
                {
                    mailMesssage.IsBodyHtml = true;
                    await client.SendMailAsync(mailMesssage);
                }
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class PrimeActsUserManager : UserManager<ApplicationUser, Guid>
    {
        public PrimeActsUserManager(IUserStore<ApplicationUser, Guid> store)
            : base(store)
        { }

        public static PrimeActsUserManager Create(IdentityFactoryOptions<PrimeActsUserManager> options, IOwinContext context)
        {
            var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWorkAsync>();
            var applicationUserOrchestra = DependencyResolver.Current.GetService<IApplicationUserOrchestra>();
            var applicationRoleOrchestra = DependencyResolver.Current.GetService<IApplicationRoleOrchestra>();
            var manager = new PrimeActsUserManager(new PrimeActsUserStore(unitOfWork, applicationUserOrchestra, applicationRoleOrchestra));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(20);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, Guid>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, Guid>(
                        dataProtectionProvider.Create("ASP.NET Identity"))
                        {
                            TokenLifespan = TimeSpan.FromHours(3)
                        };
            }
            return manager;
        }
    }

    public class PrimeActsSignInManager : SignInManager<ApplicationUser, Guid>
    {
        public PrimeActsSignInManager(PrimeActsUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync(UserManager);
        }

        public static PrimeActsSignInManager Create(IdentityFactoryOptions<PrimeActsSignInManager> options, IOwinContext context)
        {
            return new PrimeActsSignInManager(context.GetUserManager<PrimeActsUserManager>(), context.Authentication);
        }
    }

    public class PrimeActsRoleManager : RoleManager<ApplicationRole, Guid>
    {
        public PrimeActsRoleManager(IRoleStore<ApplicationRole, Guid> roleStore)
            : base(roleStore)
        { }

        public static PrimeActsRoleManager Create(IdentityFactoryOptions<PrimeActsRoleManager> options, IOwinContext context)
        {
            var applicationRoleOrchestra = DependencyResolver.Current.GetService<IApplicationRoleOrchestra>();
            return new PrimeActsRoleManager(new PrimeActsRoleStore(applicationRoleOrchestra));
        }
    }
}