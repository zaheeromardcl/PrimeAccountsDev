using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerLocation : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerLocation()
        {
          
            this.CustomerBankAccounts = new List<CustomerBankAccount>();
            this.CustomerContactLocations = new List<CustomerContactLocation>();
            this.CustomerDepartmentLocations = new List<CustomerDepartmentLocation>();
        }

        public System.Guid CustomerLocationID { get; set; }
        public System.Guid CustomerID { get; set; }
        public string CustomerLocationName { get; set; }
        public System.Guid AddressID { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public virtual Address Address { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public virtual ICollection<CustomerContactLocation> CustomerContactLocations { get; set; }
        public virtual ICollection<CustomerDepartmentLocation> CustomerDepartmentLocations { get; set; }
        public virtual Note Note { get; set; }
    }
}
