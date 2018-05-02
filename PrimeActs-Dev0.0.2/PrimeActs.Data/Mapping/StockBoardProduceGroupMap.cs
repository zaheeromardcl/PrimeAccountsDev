using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class StockBoardProduceGroupMap : EntityTypeConfiguration<PrimeActs.Domain.StockBoardProduceGroup>
    {
        public StockBoardProduceGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.StockBoardProduceGroupID);


            // Table & Column Mappings
            this.ToTable("tblStockboardProducegroup");
            this.Property(t => t.StockBoardProduceGroupID).HasColumnName("StockBoardProduceGroupID");
            this.Property(t => t.StockBoardID).HasColumnName("StockboardID");
            this.Property(t => t.ProduceGroupDepartmentID).HasColumnName("ProduceGroupDepartmentID");
          
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

             //relationships
            this.HasRequired(t=>t.StockBoard)
            .WithMany(p => p.StockBoardProduceGroups)
            .HasForeignKey(p => p.StockBoardID);
            
        }
    }
}
