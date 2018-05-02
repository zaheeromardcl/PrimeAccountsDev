using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class NominalLedgerEntryMap : EntityTypeConfiguration<PrimeActs.Domain.NominalLedgerEntry>
    {
        public NominalLedgerEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.NominalLedgerEntryID);

            // Properties
            this.Property(t => t.NominalLedgerEntryReference)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NominalLedgerEntryDescription)
                .HasMaxLength(400);
          //this.Property(t => t.AccountingPeriod).IsOptional();

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            // Table & Column Mappings
            this.ToTable("tblNominalLedgerEntry");
            this.Property(t => t.NominalLedgerEntryID).HasColumnName("NominalLedgerEntryID");
            this.Property(t => t.BatchNumberLogID).HasColumnName("BatchNumberLogID");
            this.Property(t => t.NominalAccountID).HasColumnName("NominalAccountID");
            this.Property(t => t.NominalLedgerEntryReference).HasColumnName("NominalLedgerEntryReference");
            this.Property(t => t.NominalLedgerEntryAmount).HasColumnName("NominalLedgerEntryAmount");
            this.Property(t => t.NominalLedgerEntryDate).HasColumnName("NominalLedgerEntryDate");
            this.Property(t => t.NominalLedgerEntryDescription).HasColumnName("NominalLedgerEntryDescription");
            this.Property(t => t.LedgerEntryTypeID).HasColumnName("LedgerEntryTypeID");
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //DB changes 10/11/2016:deleted column
            //IsHistory renamed to IsReconciled
            this.Property(t => t.IsReconciled).HasColumnName("IsReconciled");
            this.Property(t => t.AccountingYear).HasColumnName("AccountingYear");
            
            //DB changes 10/11/2016:deleted column
            //this.Property(t => t.AccountingPeriod).HasColumnName("AccountingPeriod");
            

            // Relationships
            this.HasRequired(t => t.BatchNumberLog)
                .WithMany(t => t.NominalLedgerEntries)
                .HasForeignKey(d => d.BatchNumberLogID);
            this.HasRequired(t => t.NominalAccount)
                .WithMany(t => t.NominalLedgerEntries)
                .HasForeignKey(d => d.NominalAccountID);

        }
    }
}
