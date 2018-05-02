using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class StockLocationMap : EntityTypeConfiguration<PrimeActs.Domain.StockLocation>
    {
        public StockLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.StockLocationID);

            // Properties
            this.Property(t => t.StockLocationName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StockLocationCode)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("tlkpStockLocation");
            this.Property(t => t.StockLocationID).HasColumnName("StockLocationID");
            this.Property(t => t.StockLocationName).HasColumnName("StockLocationName");
            this.Property(t => t.StockLocationCode).HasColumnName("StockLocationCode");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.AddressID).HasColumnName("AddressID");

            // Relationships
            this.HasOptional(t => t.Address)
                .WithMany(t => t.StockLocations)
                .HasForeignKey(d => d.AddressID);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.StockLocations)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
