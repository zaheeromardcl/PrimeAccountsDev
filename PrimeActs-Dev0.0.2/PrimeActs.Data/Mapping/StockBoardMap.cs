using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class StockBoardMap : EntityTypeConfiguration<PrimeActs.Domain.StockBoard>
    {
        public StockBoardMap()
        {
            // Primary Key
            this.HasKey(t => t.StockBoardID);


            // Table & Column Mappings
            this.ToTable("tblStockboard");
            this.Property(t => t.StockBoardID).HasColumnName("StockboardID");
            this.Property(t => t.StockBoardName).HasColumnName("StockboardName");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

   

        
        }

    }
}
