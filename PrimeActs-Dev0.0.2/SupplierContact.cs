using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class SupplierContact : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SupplierContact()
        {
    
            this.SupplierContactDepartments = new List<SupplierContactDepartment>();
            this.SupplierContactLocations = new List<SupplierContactLocation>();
        }

        public System.Guid SupplierContactID { get; set; }
        public System.Guid ContactID { get; set; }
        public Nullable<System.Guid> SortOrder { get; set; }
        public System.Guid SupplierID { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplierContactDepartment> SupplierContactDepartments { get; set; }
        public virtual ICollection<SupplierContactLocation> SupplierContactLocations { get; set; }
    }
}
