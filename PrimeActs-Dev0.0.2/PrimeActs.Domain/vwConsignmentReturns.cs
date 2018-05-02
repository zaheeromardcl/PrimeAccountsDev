using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public class vwConsignmentReturns : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public vwConsignmentReturns()
        {
            
        }

        public System.Guid ConsignmentItemID { get; set; }
        public decimal ReturnUnitPrice { get; set; }
        public int TotalQuantity { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
