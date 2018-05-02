using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public partial class ProduceQuantityForTicket 
    {

        //Also include a parameter-less construct like below
        public ProduceQuantityForTicket()
        {
        }

        
        public string ProduceCode { get; set; }
        public Guid ProduceGroupID { get; set; }
        public string SupplierCompanyName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDepartmentName { get; set; }
        public string ProduceGroupName { get; set; }
        public Guid UserDivisionID { get; set; }
        public string ProduceName { get; set; }
        public decimal ExpectedQuantity { get; set; }
        public Guid ConsignmentItemID { get; set; }
        public string ConsignmentReference { get; set; }
        public decimal TicketItemQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public Guid PorterageID { get; set; }
        public decimal PorterageUnitAmount { get; set; }
        public decimal PorterageMinimumAmount { get; set; }
        public Guid ConsignmentItemDivisionID { get; set; }
        public string ItemPackSize { get; set; }
        public string ItemBrand { get; set; }
        public decimal? ItemPackWeight { get; set; }
        public string ItemPackWeightUnit { get; set; }
        public Guid ItemDepartmentID { get; set; }
        public string ItemPackType { get; set; }
        public Nullable<decimal> AvgUnitPrice { get; set; }
        public int? itemReceived { get; set; }
        public string consDepartmentCode { get; set; }
        public Nullable<System.DateTime> Despatchdate { get; set; }
        public Nullable<bool> Invoiced { get; set; }


    }
}