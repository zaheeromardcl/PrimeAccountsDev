using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class vwStockBoardMap : EntityTypeConfiguration<PrimeActs.Domain.vwStockBoard>
    {
        public vwStockBoardMap()
        {
            

           

            // Table & Column Mappings
            this.ToTable("vwStockBoard");


            // Primary Key
            this.HasKey(t => t.vwStockBoardID);


            this.Property(t => t.vwStockBoardID).HasColumnName("vwStockBoardID");
            this.Property(t => t.StockBoardID).HasColumnName("StockboardID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.ProduceGroupID).HasColumnName("ProduceGroupID");
            this.Property(t => t.ProduceGroupName).HasColumnName("ProduceGroupName");
            this.Property(t => t.ProduceID).HasColumnName("ProduceID");
            this.Property(t => t.ProduceName).HasColumnName("ProduceName");

            this.Property(t => t.QuantityAvailable).HasColumnName("QuantityAvailable");
            this.Property(t => t.QuantityExpected).HasColumnName("QuantityExpected");
            this.Property(t => t.QuantityReceived).HasColumnName("QuantityReceived");
            this.Property(t => t.QuantitySold).HasColumnName("QuantitySold");
            this.Property(t => t.QuantityStock).HasColumnName("QuantityStock");
            
            this.Property(t => t.ConsignmentItemID1).HasColumnName("ConsignmentItemID1");
            this.Property(t => t.ConsignmentReference1).HasColumnName("ConsignmentReference1");
            this.Property(t => t.QuantityExpected1).HasColumnName("QuantityExpected1");
            this.Property(t => t.ConsignmentItemID2).HasColumnName("ConsignmentItemID2");
            this.Property(t => t.ConsignmentReference2).HasColumnName("ConsignmentReference2");
            this.Property(t => t.QuantityExpected2).HasColumnName("QuantityExpected2");
            this.Property(t => t.ConsignmentItemID3).HasColumnName("ConsignmentItemID3");
            this.Property(t => t.ConsignmentReference3).HasColumnName("ConsignmentReference3");
            this.Property(t => t.QuantityExpected3).HasColumnName("QuantityExpected3");
            this.Property(t => t.TicketID1).HasColumnName("TicketID1");
            this.Property(t => t.TicketQuantity1).HasColumnName("TicketQuantity1");
            this.Property(t => t.TicketReference1).HasColumnName("TicketReference1");
            this.Property(t => t.TicketID2).HasColumnName("TicketID2");
            this.Property(t => t.TicketQuantity2).HasColumnName("TicketQuantity2");
            this.Property(t => t.TicketReference2).HasColumnName("TicketReference2");
            this.Property(t => t.TicketID3).HasColumnName("TicketID3");
            this.Property(t => t.TicketQuantity3).HasColumnName("TicketQuantity3");
            this.Property(t => t.TicketReference3).HasColumnName("TicketReference3");

        }
    }
}
