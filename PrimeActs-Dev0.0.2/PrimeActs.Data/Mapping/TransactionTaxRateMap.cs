using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TransactionTaxRateMap : EntityTypeConfiguration<PrimeActs.Domain.TransactionTaxRate>
    {
        public TransactionTaxRateMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionTaxRateID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpTransactionTaxRate");
            this.Property(t => t.TransactionTaxRateID).HasColumnName("TransactionTaxRateID");
            this.Property(t => t.TransactionTaxRatePercentage).HasColumnName("TransactionTaxRatePercentage");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.TransactionTaxCodeID).HasColumnName("TransactionTaxCodeID");
            this.Property(t => t.MinimumUnitCost).HasColumnName("MinimumUnitCost");

            // Relationships
           
        }
    }
}
