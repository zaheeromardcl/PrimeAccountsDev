using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PortMap : EntityTypeConfiguration<PrimeActs.Domain.Port>
    {
        public PortMap()
        {
            // Primary Key
            this.HasKey(t => t.PortID);

            // Properties
            this.Property(t => t.PortName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            // Table & Column Mappings
            this.ToTable("tlkpPort");
            this.Property(t => t.PortID).HasColumnName("PortID");
            this.Property(t => t.PortName).HasColumnName("PortName");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Ports)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
