using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public partial class Printer : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Printer()
        {
          //  this.Printers = new List<Printer>();
            this.DepartmentPrinters = new List<DepartmentPrinter>();
            
            //ak: /this.Departments = new List<Department>();
        }

        public System.Guid PrinterID { get; set; }
        public string PrinterName { get; set; }
        public string NetworkName { get; set; }
        public int DefaultOrder { get; set; }
        
        public bool? IsColour { get; set; }
        public bool? IsRaw { get; set; }
        public bool? HasTractor { get; set; }

        public ICollection<DepartmentPrinter> DepartmentPrinters { get; set; }
        //ak://public virtual ICollection<Department> Departments { get; set; }
      //  public virtual ICollection<Printer> Printers { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
