using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TempBankStatementItemNominalLedgerEntryMap : EntityTypeConfiguration<PrimeActs.Domain.TempBankStatementItemNominalLedgerEntry>
    {
        public TempBankStatementItemNominalLedgerEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.BankStatementItemNominalLedgerEntryID);

            this.Property(t => t.BankStatementItemID)
                .IsRequired();
            this.Property(t => t.NominalLedgerEntryID)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("tblTempBankStatementItemNominalLedgerEntry");

            this.Property(t => t.BankStatementItemNominalLedgerEntryID).HasColumnName("BankStatementItemNominalLedgerEntryID");
            this.Property(t => t.BankStatementItemID).HasColumnName("BankStatementItemID");
            this.Property(t => t.NominalLedgerEntryID).HasColumnName("NominalLedgerEntryID");
        }
    }
}
