#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Users;

#endregion

namespace PrimeActs.UI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string Email { get; set; }

        public string URL { get; set; }
    }

    public class JsonMessage
    {
        public int StatusId { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Result { get; set; }
        public string Data { get; set; }
        public List<string> Collection { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string URL { get; set; }
    }
    
    public class RegisterViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(15, ErrorMessage = "Username length Should be less than 50")]
        [RegularExpression(@"([a-zA-Z\d]+[\w\d]*|)[a-zA-Z]+[\w\d.]*", ErrorMessage = "Invalid username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        [StringLength(15, ErrorMessage = "First Name length Should be less than 50")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        [StringLength(50, ErrorMessage = "Last Name length Should be less than 50")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Last Name")]
        public string Lastname { get; set; }

        [Display(Name = "Nickname")]
        [StringLength(35, ErrorMessage = "Nickname Length Should be less than 35")]
        public string Nickname { get; set; }

        public UserContextModel ContextOptions { get; set; }
        public UserContextModel CompaniesContext { get; set; }

        public Nullable<System.Guid> DefaultDepartmentId { get; set; }
        public Nullable<System.Guid> DefaultCompanyId { get; set; }
        public Nullable<System.Guid> DefaultDivisionId { get; set; }

        public Guid DepartmentID { get; set; }
        public IEnumerable<ItemViewModel> DepartmentList { get; set; }
        public Guid CompanyID { get; set; }
        public IEnumerable<ItemViewModel> CompanyList { get; set; }
        public Guid DivisionID { get; set; }
        public IEnumerable<ItemViewModel> DivisionList { get; set; }
        public string[] SelectedRoles { get; set; }
        public IEnumerable<CheckBoxListItem> RolesList { get; set; }

        public ICollection<ApplicationUserRoleModel> ApplicationUserRoleModels { get; set; }

        //public ICollection<ApplicationRoleModel> Companies { get; set; }
        public IEnumerable<ApplicationRoleModel> Roles { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public UserContextModel CompaniesWithAll { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }
    }
}