using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public partial class SalesLedgerInvoiceAllocation : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {

        public System.Guid SalesLedgerInvoiceAllocationID { get; set; }
        
    }
}
