using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class SupplierContactDepartment : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid SupplierContactDepartmentID { get; set; }
        public System.Guid SupplierContactID { get; set; }
        public System.Guid SupplierDepartmentID { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual SupplierContact SupplierContact { get; set; }
        public virtual SupplierDepartment SupplierDepartment { get; set; }
    
[System.ComponentModel.DataAnnotations.Schema.NotMapped]
public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
