using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PorterageMap : EntityTypeConfiguration<PrimeActs.Domain.Porterage>
    {
        public PorterageMap()
        {
            // Primary Key
            this.HasKey(t => t.PorterageID);

            // Properties
            this.Property(t => t.PorterageCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy); 

            // Table & Column Mappings
            this.ToTable("tlkpPorterage");
            this.Property(t => t.PorterageID).HasColumnName("PorterageID");
            this.Property(t => t.PorterageCode).HasColumnName("PorterageCode");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.MinimumAmount).HasColumnName("MinimumAmount");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Porterages)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
