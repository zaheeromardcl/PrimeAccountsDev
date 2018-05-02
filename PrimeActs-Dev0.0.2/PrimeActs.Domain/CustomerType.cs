using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerType : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerType()
        {         
            this.CustomerDepartments = new List<CustomerDepartment>();
        }

        public System.Guid CustomerTypeID { get; set; }
        public string CustomerTypeCode { get; set; }
        public string CustomerTypeDescription { get; set; }
        public System.Guid CompanyID { get; set; }
      
        public virtual Company Company { get; set; }
        public virtual ICollection<CustomerDepartment> CustomerDepartments { get; set; }
    }
}
