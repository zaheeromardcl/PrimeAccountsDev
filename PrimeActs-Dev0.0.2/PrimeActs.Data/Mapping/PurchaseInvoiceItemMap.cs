using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PurchaseInvoiceItemMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseInvoiceItem>
    {
        public PurchaseInvoiceItemMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseInvoiceItemID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblPurchaseInvoiceItem");
            this.Property(t => t.PurchaseInvoiceItemID).HasColumnName("PurchaseInvoiceItemID");
            this.Property(t => t.ConsignmentItemID).HasColumnName("ConsignmentItemID");
            this.Property(t => t.TotalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            
            this.Property(t => t.PurchaseInvoiceID).HasColumnName("PurchaseInvoiceID");
            this.Property(t => t.PurchaseDate).HasColumnName("PurchaseDate");
            this.Property(t => t.PurchaseInvoiceItemQuantity).HasColumnName("PurchaseInvoiceItemQuantity");
            this.Property(t => t.TransactionTaxRateID).HasColumnName("TransactionTaxRateID");
            //this.Property(t => t.BankAccountID).HasColumnName("BankAccountID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.PurchaseInvoiceItemChargeTypeID).HasColumnName("PurchaseChargeTypeID");
            this.Property(t => t.PurchaseInvoiceItemDescription).HasColumnName("PurchaseInvoiceItemDescription");

            // Relationships
            this.HasOptional(t => t.ConsignmentItem)
                .WithMany(t => t.PurchaseInvoiceItems)
                .HasForeignKey(d => d.ConsignmentItemID);
            
            this.HasRequired(t => t.PurchaseInvoice)
                .WithMany(t => t.PurchaseInvoiceItems)
                .HasForeignKey(d => d.PurchaseInvoiceID);

        }
    }
}
