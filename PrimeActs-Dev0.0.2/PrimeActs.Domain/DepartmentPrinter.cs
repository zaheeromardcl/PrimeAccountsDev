using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class DepartmentPrinter : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public DepartmentPrinter()
        {
            this.DepartmentPrintTasks = new List<DepartmentPrintTask>();
        }

        public System.Guid DepartmentPrinterID { get; set; }
        public System.Guid DepartmentID { get; set; }
        public System.Guid PrinterID { get; set; }
        public int Preference { get; set; }
       // public System.Guid? PrinterStationeryID { get; set; }
        public System.Guid? DefaultPrinterStationeryID { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

       
        public virtual Department Department { get; set; }
        public virtual Printer Printer { get; set; }

        public virtual ICollection<DepartmentPrintTask> DepartmentPrintTasks { get; set; }
     

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}