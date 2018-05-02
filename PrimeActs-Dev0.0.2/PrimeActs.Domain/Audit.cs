using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Audit : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Audit()
        {

        }

        public System.Guid AuditID { get; set; }        
        public string JsonDataBefore { get; set; }
        public string JsonDataAfter { get; set; }
        public string ContentType { get; set; }        
        public System.Guid UserID { get; set; }
        public System.Guid CompanyID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public DateTime EditDate { get; set; }
        public Nullable<System.Guid> ReferenceID { get; set; }
        public string Reference { get; set; }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}

