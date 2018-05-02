using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class BankStatementItemMap : EntityTypeConfiguration<PrimeActs.Domain.BankStatementItem>
    {
        public BankStatementItemMap()
        {
            // Primary Key
            this.HasKey(t => t.BankStatementItemID);

            this.Property(t => t.BankStatementDate)
               .IsRequired();

            this.Property(t => t.TransactionAmount);

            this.Property(t => t.TransactionType);

            this.Property(t => t.IsReconciled);

            this.Property(t => t.Text1);

            this.Property(t => t.Text2);

            this.Property(t => t.Text3);

            this.Property(t => t.Text4);

            this.Property(t => t.UpdatedByUserID);

            this.Property(t => t.CreatedByUserID);

            this.Property(t => t.BankStatementID);

            // Table & Column Mappings
            this.ToTable("tblBankStatementItem");
            this.Property(t => t.BankStatementItemID).HasColumnName("BankStatementItemID");
            this.Property(t => t.BankStatementDate).HasColumnName("BankStatementDate");
            this.Property(t => t.TransactionAmount).HasColumnName("TransactionAmount");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.Text1).HasColumnName("Text1");
            this.Property(t => t.Text2).HasColumnName("Text2");
            this.Property(t => t.Text3).HasColumnName("Text3");
            this.Property(t => t.Text4).HasColumnName("Text4");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedByUserID).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.IsReconciled).HasColumnName("IsReconciled");
            this.Property(t => t.BankStatementID).HasColumnName("BankStatementID");
           
        }
    }
}
