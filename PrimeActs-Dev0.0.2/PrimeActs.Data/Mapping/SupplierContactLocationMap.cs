using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierContactLocationMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierContactLocation>
    {
        public SupplierContactLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierContactLocationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblSupplierContactLocation");
            this.Property(t => t.SupplierContactLocationID).HasColumnName("SupplierContactLocationID");
            this.Property(t => t.SupplierContactID).HasColumnName("SupplierContactID");
            this.Property(t => t.SupplierLocationID).HasColumnName("SupplierLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.SupplierContact)
                .WithMany(t => t.SupplierContactLocations)
                .HasForeignKey(d => d.SupplierContactID);
            this.HasRequired(t => t.SupplierLocation)
                .WithMany(t => t.SupplierContactLocations)
                .HasForeignKey(d => d.SupplierLocationID);

        }
    }
}
