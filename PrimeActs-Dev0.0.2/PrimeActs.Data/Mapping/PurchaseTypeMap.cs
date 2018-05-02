using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PurchaseTypeMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseType>
    {
        public PurchaseTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseTypeID);

            // Properties
            this.Property(t => t.PurchaseTypeName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PurchaseTypeCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UpdatedBy);

            

            // Table & Column Mappings
            this.ToTable("tlkpPurchaseType");
            this.Property(t => t.PurchaseTypeID).HasColumnName("PurchaseTypeID");
            this.Property(t => t.PurchaseTypeName).HasColumnName("PurchaseTypeName");
            this.Property(t => t.PurchaseTypeCode).HasColumnName("PurchaseTypeCode");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.PurchaseTypes)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
