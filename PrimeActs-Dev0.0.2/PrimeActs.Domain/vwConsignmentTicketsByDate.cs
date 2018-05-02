using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public class vwConsignmentTicketsByDate : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public vwConsignmentTicketsByDate()
        {
            
        }

        public DateTime TicketDate { get; set; }
        public System.Guid ConsignmentItemID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQuantity { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
