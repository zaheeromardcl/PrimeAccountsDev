using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SalesLedgerInvoiceAllocationMap : EntityTypeConfiguration<PrimeActs.Domain.SalesLedgerInvoiceAllocation>
    {
        public SalesLedgerInvoiceAllocationMap()
        {
            // Primary Key
          
            this.HasKey(t => t.SalesLedgerInvoiceAllocationID);

            // Properties
            this.Property(t => t.SalesLedgerEntryID)
                .IsRequired();

            this.Property(t => t.SalesInvoiceID)
                .IsRequired();

            this.Property(t => t.SaleAmount)
                .IsRequired();

            //this.Property(t => t.UpdatedBy)
            //    .HasMaxLength(25);

            //this.Property(t => t.CreatedBy)
            //    .HasMaxLength(25);

            //// Table & Column Mappings
            this.ToTable("tblSalesLedgerInvoiceAllocation");
            this.Property(t => t.SalesLedgerInvoiceAllocationID).HasColumnName("SalesLedgerInvoiceAllocationID");
            //this.Property(t => t.SalesInvoiceID).HasColumnName("SalesInvoiceID");
            this.Property(t => t.SaleAmount).HasColumnName("SaleAmount");
            //this.Property(t => t.FXSaleAmount).HasColumnName("FXSaleAmount");
            //this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            //this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            //this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            //// Relationships
            //this.HasOptional(t => t.Currency)
            //    .WithMany(t => t.SalesLedgerInvoiceAllocations)
            //    .HasForeignKey(d => d.CurrencyID);
            //this.HasOptional(t => t.Note)
            //    .WithMany(t => t.SalesLedgerInvoiceAllocations)
            //    .HasForeignKey(d => d.NoteID);
            this.HasRequired(t => t.SalesLedgerEntry)
                .WithMany(t => t.SalesLedgerInvoiceAllocations)
                .HasForeignKey(d => d.SalesLedgerEntryID);
            this.HasRequired(t => t.SalesInvoice)
               .WithMany(t => t.SalesLedgerInvoiceAllocations)
               .HasForeignKey(d => d.SalesInvoiceID);

        
        }
    }
}
