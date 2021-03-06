﻿using System;

namespace PrimeActs.Domain
{
    public partial class PurchaseInvoiceFile : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid PurchaseInvoiceFileID { get; set; }
        public Nullable<System.Guid> PurchaseInvoiceID { get; set; }
        public Nullable<System.Guid> FileID { get; set; }
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        public virtual File File { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
