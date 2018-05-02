using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PriceMap : EntityTypeConfiguration<PrimeActs.Domain.Price>
    {
        public PriceMap()
        {
            // Primary Key
            this.HasKey(t => t.PriceID);

            // Properties
            this.Property(t => t.CurrentPrice)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            // Table & Column Mappings
            this.ToTable("tblPrice");
            this.Property(t => t.PriceID).HasColumnName("PriceID");
            this.Property(t => t.CurrentPrice).HasColumnName("CurrentPrice");
            this.Property(t => t.PriceDateTime).HasColumnName("PriceDateTime");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
