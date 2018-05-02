using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerDepartmentLocationMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerDepartmentLocation>
    {
        public CustomerDepartmentLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerDepartmentLocationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblCustomerDepartmentLocation");
            this.Property(t => t.CustomerDepartmentLocationID).HasColumnName("CustomerDepartmentLocationID");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.CustomerLocationID).HasColumnName("CustomerLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.CustomerDepartment)
                .WithMany(t => t.CustomerDepartmentLocations)
                .HasForeignKey(d => d.CustomerDepartmentID);
            this.HasOptional(t => t.CustomerLocation)
                .WithMany(t => t.CustomerDepartmentLocations)
                .HasForeignKey(d => d.CustomerLocationID);

        }
    }
}
