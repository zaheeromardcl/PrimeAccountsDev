using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class IntrastatItemMap : EntityTypeConfiguration<PrimeActs.Domain.IntrastatItem>
    {
        public IntrastatItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IntrastatItemID);

            // Properties
            this.Property(t => t.IntrastatCommodity)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IntrastatTerms)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IntrastatCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InrastatConsignmentOriginCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblIntrastatItem");
            this.Property(t => t.IntrastatItemID).HasColumnName("IntrastatItemID");
            this.Property(t => t.IntrastatCommodity).HasColumnName("IntrastatCommodity");
            this.Property(t => t.IntrastatValue).HasColumnName("IntrastatValue");
            this.Property(t => t.IntrastatTerms).HasColumnName("IntrastatTerms");
            this.Property(t => t.IntrastatNature).HasColumnName("IntrastatNature");
            this.Property(t => t.IntrastatNetMassAmount).HasColumnName("IntrastatNetMassAmount");
            this.Property(t => t.IntrastatCountry).HasColumnName("IntrastatCountry");
            this.Property(t => t.IntrastatID).HasColumnName("IntrastatID");
            this.Property(t => t.InrastatConsignmentOriginCountry).HasColumnName("InrastatConsignmentOriginCountry");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Intrastat)
                .WithMany(t => t.IntrastatItems)
                .HasForeignKey(d => d.IntrastatID);

        }
    }
}
