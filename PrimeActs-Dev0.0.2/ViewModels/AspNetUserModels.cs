#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class AspNetUserEditModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public bool Userstatus { get; set; }
        public DateTime? LastLoggedOn { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DivisionId { get; set; }
    }

    public class AspNetUserViewModel : AspNetUserEditModel
    {
        public AspNetUserViewModel()
        {
            BankReconciliations = new List<dropdownlistModel>();
            AspNetUserList = new List<dropdownlistModel>();
        }

        public AspNetUserEditModel AspNetUserEditModel { get; set; }
        public ResultList<AspNetUserEditModel> AspNetUserEditModels { get; set; }
        public ICollection<dropdownlistModel> AspNetUserList { get; set; }
        public ICollection<dropdownlistModel> BankReconciliations { get; set; }
    }
}