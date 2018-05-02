using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class DepartmentStockLocationMap : EntityTypeConfiguration<PrimeActs.Domain.DepartmentStockLocation>
    {
        public DepartmentStockLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentStockLocationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblDepartmentStockLocation");
            this.Property(t => t.DepartmentStockLocationID).HasColumnName("DepartmentStockLocationID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.StockLocationID).HasColumnName("StockLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.DepartmentStockLocations)
                .HasForeignKey(d => d.DepartmentID);
            this.HasRequired(t => t.StockLocation)
                .WithMany(t => t.DepartmentStockLocations)
                .HasForeignKey(d => d.StockLocationID);

        }
    }
}
