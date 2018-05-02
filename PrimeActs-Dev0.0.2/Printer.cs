using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public class Printer :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Printer()
        {
            this.Departments = new List<Department>();
        }

        public System.Guid PrinterID { get; set; }
        public string PrinterName { get; set; }
        public string NetworkName { get; set; }
        public int DefaultOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
