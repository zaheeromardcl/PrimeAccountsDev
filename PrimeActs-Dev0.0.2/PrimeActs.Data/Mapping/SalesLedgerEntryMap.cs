using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SalesLedgerEntryMap : EntityTypeConfiguration<PrimeActs.Domain.SalesLedgerEntry>
    {
        public SalesLedgerEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.SalesLedgerEntryID);

            // Properties
            this.Property(t => t.SalesLedgerEntryDescription)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSalesLedgerEntry");
            this.Property(t => t.SalesLedgerEntryID).HasColumnName("SalesLedgerEntryID");
            this.Property(t => t.LedgerEntryTypeID).HasColumnName("LedgerEntryTypeID");
            this.Property(t => t.SalesLedgerEntryDescription).HasColumnName("SalesLedgerEntryDescription");
            this.Property(t => t.SaleAmount).HasColumnName("SaleAmount");
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.BatchNumberLogID).HasColumnName("BatchNumberLogID");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //DB changes: 10/11/2016: columen deleted
            //this.Property(t => t.IsHistory).HasColumnName("IsHistory");
            this.Property(t => t.AccountingYear).HasColumnName("AccountingYear");
            //DB changes: 10/11/2016: columen deleted
            //this.Property(t => t.AccountingPeriod).HasColumnName("AccountingPeriod");
            
            this.Property(t => t.SalesPersonUserID).HasColumnName("SalesPersonUserID");
            

            // Relationships
            this.HasRequired(t => t.BatchNumberLog)
                .WithMany(t => t.SalesLedgerEntries)
                .HasForeignKey(d => d.BatchNumberLogID);
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.SalesLedgerEntries)
                .HasForeignKey(d => d.CurrencyID);
            this.HasRequired(t => t.CustomerDepartment)
                .WithMany(t => t.SalesLedgerEntries)
                .HasForeignKey(d => d.CustomerDepartmentID);
            this.HasRequired(t => t.LedgerEntryType)
                .WithMany(t => t.SalesLedgerEntries)
                .HasForeignKey(d => d.LedgerEntryTypeID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.SalesLedgerEntries)
                .HasForeignKey(d => d.NoteID);
            this.HasOptional(t => t.SalesPerson)
                .WithMany(t => t.SalesLedgerEntries)
                .HasForeignKey(d => d.SalesPersonUserID);
        }
    }
}
