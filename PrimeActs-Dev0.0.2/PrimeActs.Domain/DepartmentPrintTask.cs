using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class DepartmentPrintTask : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public DepartmentPrintTask()
        {
           this.PrintTasks = new List<PrintTask>();
        }

        public Guid DepartmentPrintTaskID { get; set; }
        public Guid DepartmentPrinterID { get; set; }
        public Guid PrintTaskID { get; set; }
        public int Preference { get; set; }
        public Guid? PrinterStationeryID { get; set; }

        public virtual DepartmentPrinter DepartmentPrinter { get; set; }
        [ForeignKey("PrintTaskID")]
        public virtual ICollection<PrintTask> PrintTasks { get; set; }
   
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
