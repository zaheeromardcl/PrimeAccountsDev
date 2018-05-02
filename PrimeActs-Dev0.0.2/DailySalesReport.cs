using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public class DailySalesReport
    {
        public string TicketReference { get; set; }
        public DateTime TicketDate { get; set; }
        public bool? IsCashSale { get; set; }
        public string TicketItemDescription { get; set; }
        public double TicketItemQuantity { get; set; }
        public decimal TicketItemTotalPrice { get; set; }
        public decimal PorterageValue { get; set; }
       // public bool? IsActive { get; set; }
        public string ConsignmentReference { get; set; }
        public string PackSize { get; set; }
        public string CustomerDepartmentName { get; set; }
        public string CustomerCompanyName { get; set; }
        public string Brand { get; set; }
        public string PackType { get; set; }
        public decimal Handling { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
    }
}
