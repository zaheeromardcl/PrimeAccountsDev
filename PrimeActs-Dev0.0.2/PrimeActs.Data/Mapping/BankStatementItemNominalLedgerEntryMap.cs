using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class BankStatementItemNominalLedgerEntryMap : EntityTypeConfiguration<PrimeActs.Domain.BankStatementItemNominalLedgerEntry>
    {
        public BankStatementItemNominalLedgerEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.BankStatementItemNominalLedgerEntryID);

            this.Property(t => t.BankStatementItemID)
                .IsRequired();
            this.Property(t => t.NominalLedgerEntryID)
                .IsRequired();
			this.Property(t => t.UpdatedByUserID);
            this.Property(t => t.CreatedByUserID);

            // Table & Column Mappings
            this.ToTable("tblBankStatementItemNominalLedgerEntry");

            this.Property(t => t.BankStatementItemNominalLedgerEntryID).HasColumnName("BankStatementItemNominalLedgerEntryID");
            this.Property(t => t.BankStatementItemID).HasColumnName("BankStatementItemID");
            this.Property(t => t.NominalLedgerEntryID).HasColumnName("NominalLedgerEntryID");
			this.Property(t => t.UpdatedByUserID).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
