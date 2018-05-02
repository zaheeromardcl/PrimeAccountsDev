using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class PurchaseLedgerEntryMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseLedgerEntry>
    {
        public PurchaseLedgerEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseLedgerEntryID);

            // Properties
            this.Property(t => t.PurchaseLedgerEntryDescription)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblPurchaseLedgerEntry");
            this.Property(t => t.PurchaseLedgerEntryID).HasColumnName("PurchaseLedgerEntryID");
            this.Property(t => t.LedgerEntryTypeID).HasColumnName("LedgerEntryTypeID");
            this.Property(t => t.PurchaseLedgerEntryDescription).HasColumnName("PurchaseLedgerEntryDescription");
            this.Property(t => t.PurchaseAmount).HasColumnName("PurchaseAmount");
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.TransactionTaxAmount).HasColumnName("TransactionTaxAmount");
            
            this.Property(t => t.BatchNumberLogID).HasColumnName("BatchNumberLogID");
            //PE removed from table, update to code 6/1/17
            //this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            
            //DB changes: 10/11/2016: column changed to IsAllocated
            this.Property(t => t.IsAllocated).HasColumnName("IsAllocated");

            this.Property(t => t.AccountingYear).HasColumnName("AccountingYear");
            //DB changes: 10/11/2016: columen deleted
            //this.Property(t => t.AccountingPeriod).HasColumnName("AccountingPeriod");
            

            // Relationships
            //this.HasRequired(t => t.BatchNumberLog)
            //    .WithMany(t => t.PurchaseLedgerEntries)
            //    .HasForeignKey(d => d.BatchNumberLogID);
            //this.HasOptional(t => t.Currency)
            //    .WithMany(t => t.PurchaseLedgerEntries)
            //    .HasForeignKey(d => d.CurrencyID);
            //this.HasRequired(t => t.LedgerEntryType)
            //    .WithMany(t => t.PurchaseLedgerEntries)
            //    .HasForeignKey(d => d.LedgerEntryTypeID);
            //this.HasOptional(t => t.Note)
            //    .WithMany(t => t.PurchaseLedgerEntries)
            //    .HasForeignKey(d => d.NoteID);
            //this.HasRequired(t => t.SupplierDepartment)
            //    .WithMany(t => t.PurchaseLedgerEntries)
            //    .HasForeignKey(d => d.SupplierDepartmentID);

        }
    }
}
