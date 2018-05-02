using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class WarehouseLocationMap : EntityTypeConfiguration<PrimeActs.Domain.WarehouseLocation>
    {
        public WarehouseLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.WarehouseLocationID);

            // Properties
            this.Property(t => t.WarehouseLocationName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpWarehouseLocation");
            this.Property(t => t.WarehouseLocationID).HasColumnName("WarehouseLocationID");
            this.Property(t => t.WarehouseLocationName).HasColumnName("WarehouseLocationName");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.WarehouseLocations)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
