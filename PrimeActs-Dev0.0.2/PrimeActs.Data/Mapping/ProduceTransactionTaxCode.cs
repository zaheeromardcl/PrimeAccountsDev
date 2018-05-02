using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ProduceTransactionTaxCodeMap : EntityTypeConfiguration<PrimeActs.Domain.ProduceTransactionTaxCode>
    {
        public ProduceTransactionTaxCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProduceTransactionTaxCodeID);

            
            // Table & Column Mappings
            this.ToTable("tblProduceTransactionTaxCode");
            this.Property(t => t.ProduceTransactionTaxCodeID).HasColumnName("ProduceTransactionTaxCodeID");
            this.Property(t => t.ProduceID).HasColumnName("ProduceID");
            this.Property(t => t.TransactionTaxCodeID).HasColumnName("TransactionTaxCodeID");
            this.Property(t => t.TransactionTaxLocationID).HasColumnName("TransactionTaxLocationID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("CreatedDate");
            
        }
    }
}
