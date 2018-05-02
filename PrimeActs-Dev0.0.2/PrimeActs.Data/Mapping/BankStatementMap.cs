using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class BankStatementMap : EntityTypeConfiguration<PrimeActs.Domain.BankStatement>
    {
        public BankStatementMap()
        {
            // Primary Key
            this.HasKey(t => t.BankStatementID);

            // Properties
            this.Property(t => t.BankStatementFileName)
              .HasMaxLength(100);

            this.Property(t => t.BankStatementImportDate);
                

            this.Property(t => t.BankStatementReconciled)
                .IsRequired();
            //this.Property(t => t.BankStatementStartDate);
            //this.Property(t => t.BankStatementEndDate);

            this.Property(t => t.UpdatedByUserID);

            this.Property(t => t.CreatedByUserID);
            this.Property(t => t.BankAccountID);
            this.Property(t => t.BankStatementDescription);
            this.Property(t => t.OpeningBalance);
            this.Property(t => t.CurrentBalance);


            // Table & Column Mappings
            this.ToTable("tblBankStatement");
            this.Property(t => t.BankStatementID).HasColumnName("BankStatementID");
            this.Property(t => t.BankStatementFileName).HasColumnName("BankStatementFileName");
            this.Property(t => t.BankStatementImportDate).HasColumnName("BankStatementImportDate");
            //this.Property(t => t.BankStatementStartDate).HasColumnName("StartDate");
            // this.Property(t => t.BankStatementEndDate).HasColumnName("EndDate");
            this.Property(t => t.BankStatementReconciled).HasColumnName("BankStatementReconciled");
            this.Property(t => t.BankStatementDescription).HasColumnName("BankStatementDescription");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.UpdatedByUserID).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //DB changes: 10/11/2016 - columns added
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
        }
    }
}
