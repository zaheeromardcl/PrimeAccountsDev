using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Users
{
    public class UserPagingModel
    {
        public ResultList<ApplicationUser> UserEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SearchObject
    {
        public string CommonSearchCriteria { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    
    public class AssignRoleModel
    {
        public Guid UserRoleID { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid DivisionId { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
    }
    public class PermissionDetailModel
    {
        public Guid PermissionDetailID { get; set; }
        public Guid UserID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid DivisionID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid PermissionID { get; set; }
    }
}
