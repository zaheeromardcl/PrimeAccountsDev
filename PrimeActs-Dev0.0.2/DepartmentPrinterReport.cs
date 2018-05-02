using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public class DepartmentPrinterReport : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public DepartmentPrinterReport()
        {

        }

        public System.Guid DepartmentPrinterReportID { get; set; }
        public Guid DepartmentPrinterID { get; set; }
        public Guid ReportPrintReferenceID { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
