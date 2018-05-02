using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public partial class vwStockBoard : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public vwStockBoard()
        {
            
        }
        public System.Guid vwStockBoardID { get; set; }
        public System.Guid StockBoardID { get; set; }
        public System.Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public Guid ProduceGroupID { get; set; }
        public string ProduceGroupName { get; set; }

        public System.Guid ProduceID { get; set; }
        public string ProduceName { get; set; }

        public decimal? QuantityAvailable { get; set; }
        public int QuantityExpected { get; set; }
        public int QuantityReceived { get; set; }
        public decimal QuantitySold { get; set; }
        public decimal? QuantityStock { get; set; }
        



        //Related Consignments
        public System.Guid? ConsignmentItemID1 { get; set; }
        public string ConsignmentReference1 { get; set; }
        public int? QuantityExpected1 { get; set; }


        public System.Guid? ConsignmentItemID2 { get; set; }
        public string ConsignmentReference2 { get; set; }
        public int? QuantityExpected2 { get; set; }


        public System.Guid? ConsignmentItemID3 { get; set; }
        public string ConsignmentReference3 { get; set; }
        public int? QuantityExpected3 { get; set; }


        //Related Tickets
        public System.Guid? TicketID1 { get; set; }
        public decimal? TicketQuantity1 { get; set; }
        public string TicketReference1 { get; set; }

        public System.Guid? TicketID2 { get; set; }
        public decimal? TicketQuantity2 { get; set; }
        public string TicketReference2 { get; set; }

        public System.Guid? TicketID3 { get; set; }
        public decimal? TicketQuantity3 { get; set; }
        public string TicketReference3 { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        
     
    }
}


   