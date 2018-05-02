using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class vwPermissionDetail : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public vwPermissionDetail()
        {

        }

        public string PermissionDetailID { get; set; }
        public Guid UserID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid DivisionID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid PermissionID { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
