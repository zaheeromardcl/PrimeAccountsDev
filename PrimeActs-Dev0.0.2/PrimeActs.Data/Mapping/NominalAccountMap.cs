using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class NominalAccountMap : EntityTypeConfiguration<PrimeActs.Domain.NominalAccount>
    {
        public NominalAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.NominalAccountID);

            // Properties
            this.Property(t => t.NominalAccountName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NominalCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblNominalAccount");
            this.Property(t => t.NominalAccountID).HasColumnName("NominalAccountID");
            this.Property(t => t.NominalAccountName).HasColumnName("NominalAccountName");
            this.Property(t => t.NominalCode).HasColumnName("NominalCode");
            this.Property(t => t.IsPandL).HasColumnName("IsPandL");
            this.Property(t => t.IsBroughtForward).HasColumnName("IsBroughtForward");
            this.Property(t => t.IsCurrent).HasColumnName("IsCurrent");
            this.Property(t => t.BankAccountID).HasColumnName("BankAccountID");
            this.Property(t => t.IsPettyCashAccount).HasColumnName("IsPettyCashAccount");
            this.Property(t => t.IsSystem).HasColumnName("IsSystem");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
   
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.BankAccount)
                .WithMany(t => t.NominalAccounts)
                .HasForeignKey(d => d.BankAccountID);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.NominalAccounts)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
