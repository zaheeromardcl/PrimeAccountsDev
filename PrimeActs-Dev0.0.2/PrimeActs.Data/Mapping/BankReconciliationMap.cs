using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class BankReconciliationMap : EntityTypeConfiguration<PrimeActs.Domain.BankReconciliation>
    {
        public BankReconciliationMap()
        {
            // Primary Key
            this.HasKey(t => t.BankReconciliationID);

            // Properties
            this.Property(t => t.UpdatedBy);


            this.Property(t => t.CreatedBy);


            // Table & Column Mappings
            this.ToTable("tblBankReconciliation");
            this.Property(t => t.BankReconciliationID).HasColumnName("BankReconciliationID");
            this.Property(t => t.BankReconcilitationDate).HasColumnName("BankReconcilitationDate");
            this.Property(t => t.NominalLedgerEntryID).HasColumnName("NominalLedgerEntryID");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.TransactionAmount).HasColumnName("TransactionAmount");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            //this.HasRequired(t => t.AspNetUser)
            //    .WithMany(t => t.BankReconciliations)
            //    .HasForeignKey(d => d.UserID);
            //this.HasRequired(t => t.NominalLedgerEntry)
            //    .WithMany(t => t.BankReconciliations)
            //    .HasForeignKey(d => d.NominalLedgerEntryID);

        }
    }
}
