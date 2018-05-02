#region

using Microsoft.AspNet.Identity;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace PrimeActs.UI.Models
{
    public class PrimeActsUserStore : 
        IUserStore<ApplicationUser, Guid>, 
        IUserLockoutStore<ApplicationUser, Guid>, 
        IUserPasswordStore<ApplicationUser, Guid>,
        IUserTwoFactorStore<ApplicationUser, Guid>,
        IUserPhoneNumberStore<ApplicationUser, Guid>,
        IUserLoginStore<ApplicationUser, Guid>,
        IUserRoleStore<ApplicationUser, Guid>,
        IUserEmailStore<ApplicationUser, Guid>    //Added by Paul 7/2/174
    {
        private readonly IUnitOfWork _unitOfWork;
        private IApplicationUserOrchestra _applicationUserOrchestra;
        private IApplicationRoleOrchestra _applicationRoleOrchestra;

        public PrimeActsUserStore(IUnitOfWork unitOfWork, IApplicationUserOrchestra applicationUserOrchestra, IApplicationRoleOrchestra applicationRoleOrchestra)
        {
            _unitOfWork = unitOfWork;
            _applicationUserOrchestra = applicationUserOrchestra;
            _applicationRoleOrchestra = applicationRoleOrchestra;
        }

        #region IUserStore

        public Task CreateAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            _applicationUserOrchestra.Create(user);
            _unitOfWork.SaveChanges();
            return Task.FromResult<int>(0);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            _applicationUserOrchestra.Delete(user);
            return Task.FromResult<int>(0);
        }

        public Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            return Task.FromResult<ApplicationUser>(_applicationUserOrchestra.FindById(userId));
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task.FromResult<ApplicationUser>(_applicationUserOrchestra.FindByName(userName, false));
        }
        public Task<ApplicationUser> FindByNameWithUserRolesAsync(string userName)
        {
            return Task.FromResult<ApplicationUser>(_applicationUserOrchestra.FindByName(userName, true));
        }
        //Start of added by Paul 7/2/7
        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return Task.FromResult<ApplicationUser>(_applicationUserOrchestra.FindByEmail(email));
        }
        //End of added by Paul 7/2/17
        public Task CreateAsync(ApplicationUser user)
        {
            _applicationUserOrchestra.Create(user);
            _unitOfWork.SaveChanges();
            return Task.FromResult<int>(0);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            _applicationUserOrchestra.Update(user);
            //Added Paul Edwards 13/2/17
            _unitOfWork.SaveChanges();
            //End added by Paul Edwards            
            return Task.FromResult<int>(0);
        }

        #endregion

        #region IUserLockoutStore

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<int>(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<DateTimeOffset>(user.LockoutEndDateUtc.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : default(DateTimeOffset));
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult<int>(0);
            //throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            _applicationUserOrchestra.Update(user);
            _unitOfWork.SaveChanges();
            return Task.FromResult<int>(0);
        }

        #endregion

        #region IUserTwoFactorStore

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.TwoFactorEnabled = enabled;

            return Task.FromResult<int>(0);
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumber = phoneNumber;

            return Task.FromResult<int>(0);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumberConfirmed = confirmed;

            return Task.FromResult<int>(0);
        }

        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            var logins = new List<UserLoginInfo> { };
            return Task.FromResult<IList<UserLoginInfo>>(logins);
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserRoleStore

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var roleToAdd = _applicationRoleOrchestra.FindByName(roleName);
            user.ApplicationRoles.Add(roleToAdd);
            return Task.FromResult<int>(0);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = user.ApplicationRoles.Select(x => x.Name).ToList();
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var isInRole = user.ApplicationRoles.Any(x => x.Name == roleName);
            return Task.FromResult(isInRole);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var roleToRemove = user.ApplicationRoles.FirstOrDefault(x => x.Name == roleName);
            if (roleToRemove != null)
            {
                user.ApplicationRoles.Remove(roleToRemove);
            }
            return Task.FromResult<int>(0);
        }

        #endregion
        //Added by Paul Edwards 7/2/17
        #region IUserEmailStore
        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Email = email;

            return Task.FromResult<int>(0);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;

            return Task.FromResult<int>(0);
        }

        #endregion
        //End Added by Paul Edwards 7/2/17
        public void Dispose()
        {
        }
    }

    public class PrimeActsRoleStore : IRoleStore<ApplicationRole, Guid>
    {
        private IApplicationRoleOrchestra _applicationRoleOrchestra;
        
        public PrimeActsRoleStore(IApplicationRoleOrchestra applicationRoleOrchestra)
        {
            _applicationRoleOrchestra = applicationRoleOrchestra;
        }

        public Task CreateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationRole> FindByIdAsync(Guid roleId)
        {
            return Task.FromResult<ApplicationRole>(_applicationRoleOrchestra.FindById(roleId));
        }

        public Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult<ApplicationRole>(_applicationRoleOrchestra.FindByName(roleName));
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}