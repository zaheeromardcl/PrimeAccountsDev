using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class IntrastatItem : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public IntrastatItem()
        {
           
        }

        public System.Guid IntrastatItemID { get; set; }
        public string IntrastatCommodity { get; set; }
        public decimal IntrastatValue { get; set; }
        public string IntrastatTerms { get; set; }
        public int IntrastatNature { get; set; }
        public double IntrastatNetMassAmount { get; set; }
        public string IntrastatCountry { get; set; }
        public System.Guid IntrastatID { get; set; }
        public string InrastatConsignmentOriginCountry { get; set; }
      
        public virtual Intrastat Intrastat { get; set; }
    }
}
