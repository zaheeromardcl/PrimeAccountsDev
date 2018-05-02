using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class SupplierContactLocation : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid SupplierContactLocationID { get; set; }
        public System.Guid SupplierContactID { get; set; }
        public System.Guid SupplierLocationID { get; set; }
        public virtual SupplierContact SupplierContact { get; set; }
        public virtual SupplierLocation SupplierLocation { get; set; }
    
[System.ComponentModel.DataAnnotations.Schema.NotMapped]
public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
