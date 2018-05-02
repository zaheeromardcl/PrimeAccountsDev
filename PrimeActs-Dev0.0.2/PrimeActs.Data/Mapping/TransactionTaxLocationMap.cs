using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TransactionTaxLocationMap : EntityTypeConfiguration<PrimeActs.Domain.TransactionTaxLocation>
    {
        public TransactionTaxLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionTaxLocationID);

            // Properties
            this.Property(t => t.TransactionTaxLocationName)
                .HasMaxLength(50);

            this.Property(t => t.TransactionTaxLocationNominalAccountID)
               .IsRequired();

            this.Property(t => t.TransactionTaxDisplayName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TransactionTaxReference)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyID);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpTransactionTaxLocation");
            this.Property(t => t.TransactionTaxLocationID).HasColumnName("TransactionTaxLocationID");
            this.Property(t => t.TransactionTaxLocationName).HasColumnName("TransactionTaxLocationName");
            this.Property(t => t.TransactionTaxLocationNominalAccountID).HasColumnName("TransactionTaxLocationNominalAccountID");
            this.Property(t => t.TransactionTaxDisplayName).HasColumnName("TransactionTaxDisplayName");
            this.Property(t => t.TransactionTaxReference).HasColumnName("TransactionTaxReference");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
