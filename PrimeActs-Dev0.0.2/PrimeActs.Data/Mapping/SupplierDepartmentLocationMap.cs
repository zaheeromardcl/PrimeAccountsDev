using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierDepartmentLocationMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierDepartmentLocation>
    {
        public SupplierDepartmentLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierDepartmentLocationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblSupplierDepartmentLocation");
            this.Property(t => t.SupplierDepartmentLocationID).HasColumnName("SupplierDepartmentLocationID");
            this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.SupplierLocationID).HasColumnName("SupplierLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.SupplierDepartment)
                .WithMany(t => t.SupplierDepartmentLocations)
                .HasForeignKey(d => d.SupplierDepartmentID);
            this.HasRequired(t => t.SupplierLocation)
                .WithMany(t => t.SupplierDepartmentLocations)
                .HasForeignKey(d => d.SupplierLocationID);

        }
    }
}
