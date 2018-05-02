using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class tgenPurchaseInvoiceNumber :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid PurchaseInvoiceID { get; set; }
        public int PurchaseInvoiceNumber { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    
}
}
