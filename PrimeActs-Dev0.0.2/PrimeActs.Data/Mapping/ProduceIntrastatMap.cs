using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ProduceIntrastatMap : EntityTypeConfiguration<PrimeActs.Domain.ProduceIntrastat>
    {
        public ProduceIntrastatMap()
        {
            // Primary Key
            this.HasKey(t => t.ProduceIntrastatID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblProduceIntrastat");
            this.Property(t => t.ProduceIntrastatID).HasColumnName("ProduceIntrastatID");
            this.Property(t => t.ProduceID).HasColumnName("ProduceID");
            this.Property(t => t.IntrastatCode).HasColumnName("IntrastatCode");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.Produce)
                .WithMany(t => t.ProduceIntrastats)
                .HasForeignKey(d => d.ProduceID);

        }
    }
}
