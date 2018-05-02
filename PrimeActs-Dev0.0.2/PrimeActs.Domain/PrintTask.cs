using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrimeActs.Domain
{
    public partial class PrintTask : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PrintTask()
        {
         //AK: uncommented out 
            List<DepartmentPrintTask> DepartmentPrintTasks = new List<DepartmentPrintTask>();
        }

        public System.Guid PrintTaskID { get; set; }
        public string PrintTaskName { get; set; }
        public bool? HasColour { get; set; }
        public bool? RequireColour { get; set; }
        public bool? HasRaw { get; set; }
        public bool? RequireRaw { get; set; }
        public bool? HasTractor { get; set; }
        public bool? RequireTractor { get; set; }

          [ForeignKey("PrintTaskID")]
                 public virtual ICollection<DepartmentPrintTask> DepartmentPrintTasks { get; set; }
        [ForeignKey("PrintTaskID")]
         public virtual DepartmentPrintTask DepartmentPrintTask { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}