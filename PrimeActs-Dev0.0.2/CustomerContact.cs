using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerContact : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerContact()
        {
        
            this.CustomerContactDepartments = new List<CustomerContactDepartment>();
            this.CustomerContactLocations = new List<CustomerContactLocation>();
        }

        public System.Guid CustomerContactID { get; set; }
        public System.Guid ContactID { get; set; }
        public Nullable<System.Guid> SortOrder { get; set; }
        public System.Guid CustomerID { get; set; }
        
        public virtual Contact Contact { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerContactDepartment> CustomerContactDepartments { get; set; }
        public virtual ICollection<CustomerContactLocation> CustomerContactLocations { get; set; }
    }
}
