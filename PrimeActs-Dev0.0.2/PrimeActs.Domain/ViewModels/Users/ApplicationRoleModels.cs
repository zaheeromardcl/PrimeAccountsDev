using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Users
{
    public class RoleEditModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ApplicationUser> UserList { get; set; }
        public List<PermissionEditModel> SelectedPermissionList { get; set; }
        public List<PermissionEditModel> PermissionList { get; set; }
    }

    public class RolePagingModel
    {
        public ResultList<RoleEditModel> RoleEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }
}
