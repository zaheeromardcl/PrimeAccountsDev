using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CompanyNominalAccount : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CompanyNominalAccount()
        {
          
        }

        public System.Guid CompanyNominalAccountID { get; set; }
        public System.Guid CompanyID { get; set; }
        public System.Guid NominalAccountID { get; set; }
       
        public virtual Company Company { get; set; }
        public virtual NominalAccount NominalAccount { get; set; }
    }
}
