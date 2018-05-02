using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PackWtUnitMap : EntityTypeConfiguration<PrimeActs.Domain.PackWtUnit>
    {
        public PackWtUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.PackWtUnitID);

            // Properties
            this.Property(t => t.WtUnit)
                .HasMaxLength(10);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpPackWtUnit");
            this.Property(t => t.PackWtUnitID).HasColumnName("PackWtUnitID");
            this.Property(t => t.WtUnit).HasColumnName("WtUnit");
            this.Property(t => t.KgMultiple).HasColumnName("KgMultiple");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.PackWtUnits)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
