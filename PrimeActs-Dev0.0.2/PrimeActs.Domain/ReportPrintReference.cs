using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public class ReportPrintReference : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ReportPrintReference()
        {
            
        }

        public System.Guid ReportPrintReferenceID { get; set; }
        public string ReportPrintReferenceName { get; set; }
        public bool RawPrintMode { get; set; }       

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}