using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PurchaseChargeTypeMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseChargeType>
    {
        public PurchaseChargeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseChargeTypeID);

            // Properties
            this.Property(t => t.PurchaseChargeTypeCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.PurchaseChargeTypeName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpPurchaseChargeType");
            this.Property(t => t.PurchaseChargeTypeID).HasColumnName("PurchaseChargeTypeID");
            this.Property(t => t.PurchaseChargeTypeCode).HasColumnName("PurchaseChargeTypeCode");
            this.Property(t => t.PurchaseChargeTypeName).HasColumnName("PurchaseChargeTypeName");
            this.Property(t => t.NominalAccountID).HasColumnName("NominalAccountID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.PurchaseChargeTypes)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
