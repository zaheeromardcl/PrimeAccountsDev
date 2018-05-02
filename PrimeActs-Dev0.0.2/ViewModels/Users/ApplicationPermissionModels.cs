using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Users
{
    public class PermissionEditModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PermissionPagingModel
    {
        public ResultList<PermissionEditModel> PermissionEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }
}
