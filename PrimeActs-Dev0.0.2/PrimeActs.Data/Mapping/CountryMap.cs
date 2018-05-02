using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CountryMap : EntityTypeConfiguration<PrimeActs.Domain.Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryID);

            // Properties
            this.Property(t => t.CountryName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CountryCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpCountry");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.BankCodeFormat).HasColumnName("BankCodeFormat");
            this.Property(t => t.BankAccountNumberFormat).HasColumnName("BankAccountNumberFormat");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Countries)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
