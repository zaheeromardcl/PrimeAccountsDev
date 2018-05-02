#region

using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.UI.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Alias")]
        public string Alias { get; set; }


        [Display(Name = "User Status")]
        public bool UserStatus { get; set; }


        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public Guid CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Guid DivisionID { get; set; }
        public string DivisionName { get; set; }
        public List<RoleEditModel> Roles { get; set; }
    }
}