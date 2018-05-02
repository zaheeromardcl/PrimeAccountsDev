using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TempBankNominalLedgerEntryMap : EntityTypeConfiguration<PrimeActs.Domain.TempBankNominalLedgerEntry>
    {
        public TempBankNominalLedgerEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.TempBankNominalLedgerEntryID);
            // Properties
            this.Property(t => t.TransactionAmount).IsRequired();
            this.Property(t => t.NominalLedgerEntryID).IsRequired();
            this.Property(t => t.BankStatementID).IsRequired();
            this.Property(t => t.BankReconciliationDate);
            this.Property(t => t.TempDescriptionn);
            this.Property(t => t.TransactionType);
            this.Property(t => t.IsReconciled);
            this.Property(t => t.Text1);
            this.Property(t => t.Text2);
            this.Property(t => t.Text3);
            this.Property(t => t.Text4);

            // Table & Column Mappings
            this.ToTable("tblTempBankNominalLedgerEntry");
            this.Property(t => t.TempBankNominalLedgerEntryID).HasColumnName("TempBankNominalLedgerEntryID");
            this.Property(t => t.BankStatementID).HasColumnName("BankStatementID");
            this.Property(t => t.NominalLedgerEntryID).HasColumnName("NominalLedgerEntryID");
            this.Property(t => t.BankReconciliationDate).HasColumnName("BankReconciliationDate");
            this.Property(t => t.TempDescriptionn).HasColumnName("TempDescription");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.IsReconciled).HasColumnName("IsReconciled");
            this.Property(t => t.Text1).HasColumnName("Text1");
            this.Property(t => t.Text2).HasColumnName("Text2");
            this.Property(t => t.Text3).HasColumnName("Text3");
            this.Property(t => t.Text4).HasColumnName("Text4");
        }
    }
}
