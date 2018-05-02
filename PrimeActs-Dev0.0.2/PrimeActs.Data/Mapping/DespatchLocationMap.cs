using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class DespatchLocationMap : EntityTypeConfiguration<PrimeActs.Domain.DespatchLocation>
    {
        public DespatchLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.DespatchLocationID);

            // Properties
            this.Property(t => t.DespatchLocationCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DespatchLocationName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            // Table & Column Mappings
            this.ToTable("tlkpDespatchLocation");
            this.Property(t => t.DespatchLocationID).HasColumnName("DespatchLocationID");
            this.Property(t => t.DespatchLocationCode).HasColumnName("DespatchLocationCode");
            this.Property(t => t.DespatchLocationName).HasColumnName("DespatchLocationName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
       
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.DespatchLocations)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
