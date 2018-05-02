using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Menu : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid MenuID { get; set; }
        public System.Guid ParentID { get; set; }
        public string MenuDescription { get; set; }
        public string MenuLinkTo { get; set; }
        public bool IsCurrent { get; set; }
       
        
                                            }
}
