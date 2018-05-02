using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ErrorLog : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorCategory { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
