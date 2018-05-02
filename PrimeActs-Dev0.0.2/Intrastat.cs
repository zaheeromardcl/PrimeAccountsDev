using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Intrastat : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Intrastat()
        {
            
            this.IntrastatItems = new List<IntrastatItem>();
        }

        public System.Guid IntrastatID { get; set; }
        public System.DateTime IntrastatDate { get; set; }
        public string IntrastatDescription { get; set; }
        public decimal IntrastatValue { get; set; }
        public int IntrastatCompanyID { get; set; }
        public string IntrastatVATNumber { get; set; }
        public string IntrastatBranchNumber { get; set; }
      
        public virtual ICollection<IntrastatItem> IntrastatItems { get; set; }
    }
}
