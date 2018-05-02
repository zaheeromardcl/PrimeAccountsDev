#region

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using PrimeActs.Domain.ViewModels.Users;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;
using PrimeActs.Infrastructure.Extensions;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class UsersAdminController : ApiController
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly IApplicationRoleOrchestra _applicationRoleOrchestra;
        private ICompanyOrchestra _companyOrchestra;
        private IDepartmentOrchestra _departmentOrchestra;
        private IDivisionOrchestra _divisionOrchestra;

        private PrimeActsRoleManager _roleManager;


        private PrimeActsSignInManager _signInManager;

        private PrimeActsUserManager _userManager;
        private string _serverCode = "L";//Need to change with actual at runtime.

        public UsersAdminController(IUnitOfWorkAsync unitOfWork,
            IApplicationUserOrchestra applicationUserOrchestra,
            IApplicationRoleOrchestra applicationRoleOrchestra,
            IDepartmentOrchestra departmentOrchestra, ICompanyOrchestra companyOrchestra,
            IDivisionOrchestra divisionOrchestra)
        {
            _unitOfWork = unitOfWork;
            _applicationUserOrchestra = applicationUserOrchestra;
            _applicationRoleOrchestra = applicationRoleOrchestra;
            _companyOrchestra = companyOrchestra;
            _departmentOrchestra = departmentOrchestra;
            _divisionOrchestra = divisionOrchestra;
        }

        public UsersAdminController(PrimeActsUserManager userManager, PrimeActsRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public PrimeActsSignInManager SignInManager
        {
            get { return _signInManager ?? Request.GetOwinContext().Get<PrimeActsSignInManager>(); }
            private set { _signInManager = value; }
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        public PrimeActsRoleManager RoleManager
        {
            get { return _roleManager ?? Request.GetOwinContext().Get<PrimeActsRoleManager>(); }
            private set { _roleManager = value; }
        }

        [HttpGet]
        //[PrimeActsAuthorize(OperationKey = "UsersAdmin-ViewAllUser")]
        public ResultList<ApplicationUser> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            queryOptions = queryOptions ?? new QueryOptions();
            searchObject = searchObject ?? new SearchObject();
            var model = _applicationUserOrchestra.GetUserPagingModel(queryOptions, searchObject);
            return model.UserEditModels;
        }

        [HttpPost]
        [PrimeActsAuthorizeAttributeAPI(OperationKey = "UsersAdmin-Create")]
        public async Task<JsonMessage> Create(RegisterViewModel userViewModel)
        {
            var user = new ApplicationUser
            {
                Id = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                UserName = userViewModel.Username,
                Email = userViewModel.Email,
                DepartmentId = userViewModel.DepartmentID,
                CompanyId = userViewModel.CompanyID,
                DivisionId = userViewModel.DivisionID,
                Firstname = userViewModel.Firstname,
                Lastname = userViewModel.Lastname,
                Nickname = userViewModel.Nickname,

                LastLoggedOn = DateTime.Now
            };

            try
            {
                var hashedPassword = UserManager.PasswordHasher.HashPassword(userViewModel.Password);
                var securityStamp = Guid.NewGuid().ToString("D");
                user.PasswordHash = hashedPassword;
                user.SecurityStamp = securityStamp;

                var adminresult = await UserManager.CreateAsync(user);//, userViewModel.Password);
                UserManager.EmailService = new EmailService();
                var codeNotEncoded = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var code = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(codeNotEncoded));
                var callbackUrl = Url.Link("Default",
                    new { controller = "Account", action = "ConfirmEmail", userId = user.Id, code });
                await
                    UserManager.SendEmailAsync(user.Id, "Confirm your account",
                        "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                // in case someone would like to use the UserManager to add roles:

                //Add User to the selected Roles 
                //if (adminresult.Succeeded)
                //{
                //    if (userViewModel.SelectedRoles != null && userViewModel.SelectedRoles.Any())
                //    {
                //        var result =
                //            await
                //                UserManager.AddToRolesAsync(user.Id,
                //                    userViewModel.SelectedRoles.Select(s => s.ToUpperInvariant()).ToArray());
                //        if (result.Succeeded)
                //        {
                //            return
                //                new JsonMessage
                //                {
                //                    StatusId = 1,
                //                    Message = string.Format("Successfully added user '{0}'.", userViewModel.Username)
                //                };
                //        }
                //        return new JsonMessage
                //        {
                //            StatusId = 0,
                //            Message =
                //                "User created sucessfully, but not roles associated. User need to confirm registration by email."
                //        };
                //    }
                //}
                //else
                //{
                //    return new JsonMessage
                //    {
                //        StatusId = 1,
                //        Message = string.Join(" ", adminresult.Errors)
                //    };
                //}

                //if (userViewModel.SelectedRoles != null && userViewModel.SelectedRoles.Any())
                //{
                //    foreach (var selectedRole in userViewModel.SelectedRoles)
                //    {
                //        var roleToAdd = _applicationRoleOrchestra.FindByName(selectedRole);
                //        user.ApplicationRoles.Add(roleToAdd);
                //    }

                //    _applicationUserOrchestra.Update(user);
                //    _unitOfWork.SaveChanges();
                //}

            }
            catch (Exception ex)
            {
                return
                    new JsonMessage
                    {
                        StatusId = 0,
                        Exception = ex.Message,
                        InnerException = ex.InnerException.ToString()
                    };
            }
            return
                new JsonMessage
                {
                    StatusId = 1,
                    Data = user.Id.ToString(),
                    Message =
                        string.Format("Successfully added user '{0}'. User needs to confirm registration by email.",
                            userViewModel.Username)
                };
        }

        [HttpPost]
        [PrimeActsAuthorizeAttributeAPI(OperationKey = "UsersAdmin-EditUserBasic")]
        public async Task<JsonMessage> EditUserBasic(RegisterViewModel userViewModel)
        {
            var user = _applicationUserOrchestra.FindById(userViewModel.Id);
            if (user == null)
            {
                return null;
            }

            user.UserName = userViewModel.Username;
            user.Email = userViewModel.Email;
            user.DepartmentId = userViewModel.DepartmentID;
            user.CompanyId = userViewModel.CompanyID;
            user.DivisionId = userViewModel.DivisionID;
            user.Firstname = userViewModel.Firstname;
            user.Lastname = userViewModel.Lastname;
            user.Nickname = userViewModel.Nickname;

            user.LastLoggedOn = DateTime.Now;

            try
            {
                _applicationUserOrchestra.Update(user);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return
                    new JsonMessage
                    {
                        StatusId = 0,
                        Exception = ex.Message,
                        InnerException = ex.InnerException.ToString()
                    };
            }
            return new JsonMessage
            {
                StatusId = 1,
                Message = string.Format("Successfully updated user '{0}'.", userViewModel.Username)
            };
        }

        [HttpPost]
        [PrimeActsAuthorizeAttributeAPI(OperationKey = "UsersAdmin-AssignRoleToUser")]
        public async Task<JsonMessage> AssignRoleToUser(AssignRoleModel userViewModel)
        {
            try
            {
                userViewModel.UserRoleID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);

                _applicationUserOrchestra.AssignUserToRole(userViewModel);

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return
                    new JsonMessage
                    {
                        StatusId = 0,
                        Exception = ex.Message,
                        InnerException = ex.InnerException.ToString()
                    };
            }
            return new JsonMessage
            {
                StatusId = 1,
                Data = userViewModel.UserRoleID.ToString(),
                Message = string.Format("Role assigned successfully. Changes for this user are effective after sign out. ")
            };
        }

        [HttpPost]
        [PrimeActsAuthorizeAttributeAPI(OperationKey = "UsersAdmin-RemoveRoleFromUser")]
        public async Task<JsonMessage> RemoveRoleFromUser(AssignRoleModel userViewModel)
        {
            try
            {
                _applicationUserOrchestra.RemoveRoleFromUser(userViewModel);

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return
                    new JsonMessage
                    {
                        StatusId = 0,
                        Exception = ex.Message,
                        InnerException = ex.InnerException.ToString()
                    };
            }
            return new JsonMessage
            {
                StatusId = 1,
                Message = string.Format("Role removed successfully. Changes for this user are effective after sign out. ")
            };
        }

        [HttpPost]
        [PrimeActsAuthorizeAttributeAPI(OperationKey = "UsersAdmin-DeleteUser")]
        public async Task<JsonMessage> DeleteUser(Guid id)
        {
            try
            {
                _applicationUserOrchestra.DeleteById(id);

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return
                    new JsonMessage
                    {
                        StatusId = 0,
                        Exception = ex.Message,
                        InnerException = ex.InnerException.ToString()
                    };
            }
            return new JsonMessage
            {
                StatusId = 1,
                Message = string.Format("User removed successfully.")
            };
        }

        public async Task<JsonMessage> ForgotPasswordConfirmation(ForgotPasswordViewModel model)
        {
            bool isFound = false;
            try
            {
                var user = await UserManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = "Unable to find your account. Please check you are using the correct details."
                    };
                }
                isFound = true;
                UserManager.EmailService = new EmailService();
                //Added new variable and changed calbackURL definition Paul Edwards 9/2/17
                var codeNotEncoded = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var code = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(codeNotEncoded));
                //End changes by Paul Edwards
                var callbackUrl = Url.Link("Default",
                    new { controller = "Account", action = "ConfirmEmail", userId = user.Id, code });
                await
                    UserManager.SendEmailAsync(user.Id, "Confirm your account",
                        "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return new JsonMessage
                {
                    StatusId = 0,
                    Message = "Please check your email for email to confirm your email address."
                };
            }
            catch (Exception e)
            {
                if (isFound)
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = e.Message
                    };
                }
                else
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = "Unable to find your account. Please check you are using the correct details."
                    };
                }
            }
        }

        public async Task<JsonMessage> ForgotPassword(ForgotPasswordViewModel model)
        {
            bool isFound = false;
            try
            {
                var user = await UserManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = "Unable to find your account. Please check you are using the correct details."
                    };
                }
                else if (!(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = "Please check your email for previously sent confirmation email. If unable to find click on Resend Confirmation button below."
                    };
                }
                isFound = true;

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                UserManager.EmailService = new EmailService();
                var codeNotEncoded = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var code = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(codeNotEncoded));
                var callbackUrl = Url.Link("Default",
                    new { controller = "Account", action = "ResetPassword", userId = user.Id, code });
                await
                    UserManager.SendEmailAsync(user.Id, "Reset Password",
                        "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return new JsonMessage
                {
                    StatusId = 0,
                    Message = "Please check your email to reset your password."
                };
            }
            catch (Exception e)
            {
                if (isFound)
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = e.Message
                    };
                }
                else
                {
                    return new JsonMessage
                    {
                        StatusId = 0,
                        Message = "Unable to find your account. Please check you are using the correct details."
                    };
                }
            }
        }

        public async Task<JsonMessage> ChangeResetPassword(ChangeResetPasswordViewModel model)
        {
            var result =
                await
                    UserManager.ChangePasswordAsync(Guid.Parse(User.Identity.GetUserId()), model.OldPassword,
                        model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }
                return new JsonMessage
                {
                    StatusId = 0,
                    Message = "Password changed successfully"
                };
            }
            return new JsonMessage
            {
                StatusId = 0,
                Message = result.Errors.Aggregate(string.Empty, (current, error) => current + " " + error)
            };
        }

        public async Task<JsonMessage> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Username);
            var code = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(model.Code));
            if (user == null)
            {
                return new JsonMessage
                {
                    StatusId = 0,
                    Message = "User name is not recognised"
                };
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {
                return new JsonMessage
                {
                    StatusId = 1,
                    Message = "Password changed successfully"
                };
            }

            return new JsonMessage
            {
                StatusId = 0,
                Message = result.Errors.Aggregate(string.Empty, (current, error) => current + " " + error)
            };
        }
    }
}