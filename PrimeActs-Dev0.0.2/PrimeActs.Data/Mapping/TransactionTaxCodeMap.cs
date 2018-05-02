using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TransactionTaxCodeMap : EntityTypeConfiguration<PrimeActs.Domain.TransactionTaxCode>
    {
        public TransactionTaxCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionTaxCodeID);

            // Properties
            this.Property(t => t.TransactionTaxCodeValue)
                .HasMaxLength(50);

            this.Property(t => t.TransactionTaxCodeDescription)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpTransactionTaxCode");
            this.Property(t => t.TransactionTaxCodeID).HasColumnName("TransactionTaxCodeID");
            this.Property(t => t.TransactionTaxCodeValue).HasColumnName("TransactionTaxCode");
            this.Property(t => t.TransactionTaxCodeDescription).HasColumnName("TransactionTaxCodeDescription");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.TransactionTaxLocationID).HasColumnName("TransactionTaxLocationID");
            this.Property(t => t.RateSetBySaleDate).HasColumnName("RateSetBySaleDate");

           

        }
    }
}
