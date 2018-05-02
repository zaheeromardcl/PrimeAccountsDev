using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class vwCashTicket : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public vwCashTicket()
        {
            
        }

        
        public System.Guid TicketID { get; set; }
        public string TicketReference { get; set; }
        public decimal TicketTotal { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceOwed { get; set; }
        public DateTime CreatedDate { get; set; }
        public System.Guid CustomerDepartmentID { get; set; }
        public string SalesPersonName { get; set; }
        public System.Guid SalesInvoiceID { get; set; }
        public System.Guid DivisionID { get; set; }
        //public decimal MatchItemBalanceTotal { get; set; }
        //public decimal MatchItemAmountTotal { get; set; }
        
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
