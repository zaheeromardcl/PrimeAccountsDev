using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CreditRatingMap : EntityTypeConfiguration<PrimeActs.Domain.CreditRating>
    {
        public CreditRatingMap()
        {
            // Primary Key
            this.HasKey(t => t.CreditRatingID);

            // Properties
            this.Property(t => t.CreditRatingCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CreditRatingDescription)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpCreditRating");
            this.Property(t => t.CreditRatingID).HasColumnName("CreditRatingID");
            this.Property(t => t.CreditRatingCode).HasColumnName("CreditRatingCode");
            this.Property(t => t.CreditRatingDescription).HasColumnName("CreditRatingDescription");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CreditRatings)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
