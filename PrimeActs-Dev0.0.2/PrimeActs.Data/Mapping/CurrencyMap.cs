using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CurrencyMap : EntityTypeConfiguration<PrimeActs.Domain.Currency>
    {
        public CurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyID);

            // Properties
            this.Property(t => t.CurrencyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpCurrency");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.CurrencyName).HasColumnName("CurrencyName");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.DefaultExchangeRate).HasColumnName("DefaultExchangeRate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Currencies)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
