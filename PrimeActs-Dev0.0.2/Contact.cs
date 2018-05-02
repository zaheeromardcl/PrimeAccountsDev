using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Contact : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Contact()
        {
          
            this.CustomerContacts = new List<CustomerContact>();
            this.SupplierContacts = new List<SupplierContact>();
            this.CustomerDepartments = new List<CustomerDepartment>();
        }

        public System.Guid ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ContactType { get; set; }
        public string ContactReference { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public string EmailAddress { get; set; }
        public string DDITelephoneNo { get; set; }
        public string MobileNo { get; set; }
        
        public virtual Note Note { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }
        public virtual ICollection<CustomerDepartment> CustomerDepartments { get; set; }
    }
}
