using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PurchaseLedgerInvoiceAllocationMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseLedgerInvoiceAllocation>
    {
        public PurchaseLedgerInvoiceAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseLedgerInvoiceAllocationID);

            //// Properties
            //this.Property(t => t.PurchaseLedgerEntryID)
            //    .IsRequired();

            //this.Property(t => t.PurchaseInvoiceID)
            //    .IsRequired();

            //this.Property(t => t.PurchaseAmount)
            //    .IsRequired();

            //this.Property(t => t.UpdatedBy)
            //    .HasMaxLength(25);

            //this.Property(t => t.CreatedBy)
            //    .HasMaxLength(25);

            //// Table & Column Mappings
            this.ToTable("tblPurchaseLedgerInvoiceAllocation");
            this.Property(t => t.PurchaseLedgerInvoiceAllocationID).HasColumnName("PurchaseLedgerInvoiceAllocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //this.Property(t => t.SalesInvoiceID).HasColumnName("PurchaseInvoiceID");
            //this.Property(t => t.SaleAmount).HasColumnName("PurchaseAmount");
            //this.Property(t => t.FXSaleAmount).HasColumnName("FXPurchaseAmount");
            //this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            //this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            //this.Property(t => t.NoteID).HasColumnName("NoteID");
            //this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            //this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            //this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");


            //// Relationships
            //this.HasOptional(t => t.Currency)
            //    .WithMany(t => t.PurchaseLedgerInvoiceAllocations)
            //    .HasForeignKey(d => d.CurrencyID);
            //this.HasOptional(t => t.Note)
            //    .WithMany(t => t.PurchaseLedgerInvoiceAllocations)
            //    .HasForeignKey(d => d.NoteID);
            //this.HasRequired(t => t.PurchaseLedgerEntry)
            //    .WithMany(t => t.PurchaseLedgerInvoiceAllocations)
            //    .HasForeignKey(d => d.PurchaseLedgerEntryID);
            //this.HasRequired(t => t.PurchaseInvoice)
            //   .WithMany(t => t.PurchaseLedgerInvoiceAllocations)
            //   .HasForeignKey(d => d.PurchaseInvoiceID);

        
        }
    }
}
