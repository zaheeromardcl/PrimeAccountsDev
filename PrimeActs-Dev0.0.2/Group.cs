using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Group : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Group()
        {
        }

        public System.Guid GroupID { get; set; }
        public string GroupName { get; set; }
       
    }
}
