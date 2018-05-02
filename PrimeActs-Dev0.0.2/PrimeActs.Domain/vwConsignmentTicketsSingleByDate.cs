using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public class vwConsignmentTicketsSingleByDate : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public vwConsignmentTicketsSingleByDate()
        {
            
        }

        public System.Guid ConsignmentItemID { get; set; }
        public DateTime TicketDate { get; set; }
        public string CustomerCode { get; set; }
        public decimal TicketItemQuantity { get; set; }
        public decimal TicketItemTotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string ShowTicketReference { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}