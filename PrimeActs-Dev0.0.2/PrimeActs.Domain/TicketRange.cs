using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class TicketRange : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TicketRange()
        {
            
        }

        public System.Guid TicketRangeID { get; set; }
        public int TicketRangeStart { get; set; }
        public int TicketRangeEnd { get; set; }
                                                public Nullable<System.Guid> DepartmentID { get; set; }
        public string TicketPrefix { get; set; }
        
        public virtual Department Department { get; set; }
    }
}
