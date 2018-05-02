using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class tgenSalesInvoiceNumber : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid SalesInvoiceID { get; set; }
        public string SalesInvoiceNumber { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public System.Guid DivisionID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    
}
}
