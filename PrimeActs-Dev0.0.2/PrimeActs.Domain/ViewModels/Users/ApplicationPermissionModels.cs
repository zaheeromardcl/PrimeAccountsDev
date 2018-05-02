using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Users
{
    public class PermissionEditModel
    {
        public string PermissionID { get; set; }
        public string PermissionController { get; set; }
        public string PermissionAction { get; set; }
        public string PermissionDescription { get; set; }
        public string PermissionName { get; set; }
    }

    public class PermissionPagingModel
    {
        public ResultList<PermissionEditModel> PermissionEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }
}
