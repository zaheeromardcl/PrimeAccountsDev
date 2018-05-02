using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimeActs.Domain.ViewModels.StockBoard
{
    public class StockboardHubModel {
        public List<ProduceGroupModel> stocks { get; set; }
    }


    public class StockBoardViewModel {
        //maps to ProduceQuantityForTicket type
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
   
   
    public class StockBoardEditModel
    {
        public Guid StockBoardID { get; set; }
        [Required]
        public string StockBoardName { get; set; }
   
    }

    public class StockBoardPagingIndex{}

    public class StockBoardModel
    {
        public List<StockBoardProduce> Produces { get; set; }
    }

    public class SearchObject
    {
        public string StockBoardName { get; set; }
       
       
    }
    public class ProduceGroupModel
    {
        public string ProduceGroupId { get; set; }
        public string ProduceGroupName { get; set; }
        public List<ProduceModel> ProduceItems { get; set; }

        public ProduceGroupModel()
        {
            ProduceItems = new List<ProduceModel>();
        }
    }

    public class ProduceModel
    {
        public string ProduceName { get; set; }
        public decimal QtyAvailable { get; set; }
        public decimal QtyStock { get; set; }
        public List<Consignment.ConsignmentDetailsViewModel> RelatedOpenConsignments { get; set; }
        public List<Ticket> RelatedTickets { get; set; }

    }


    public class StockBoardProduce {

        
        public string  ProduceName { get; set; }
        public System.Guid ProduceGroupID { get; set; }
        public decimal QtyReceived { get; set; }
        public decimal QtySold { get; set; }
        public decimal QtyExpected { get; set; }
        public decimal QtyStock { get; set; }
        public decimal QtyAvailable { get; set; }
        public List<StockBoardRelatedConsignment> RelatedConsignments { get; set; }
        public List<StockBoardRelatedTicket> RelatedTickets { get; set; }
    
    }
    public class StockBoardRelatedConsignment {

        public Guid StockBoardID { get; set; }
        public Guid ProduceID { get; set; }
        public string  ConsignmentReference { get; set; }
        public System.Guid ConsignmentItemID { get; set; }
        public int QtyExpected { get; set; }
    
    }
    public class StockBoardRelatedTicket {

     public Guid StockBoardID { get; set; }
     public Guid ProduceID { get; set; }
        public string  TicketReference { get; set; }
        public System.Guid TicketID { get; set; }
        public int QtySold { get; set; }//sum of ticketitemqty for that ticket f
        
        
    }


   
}
