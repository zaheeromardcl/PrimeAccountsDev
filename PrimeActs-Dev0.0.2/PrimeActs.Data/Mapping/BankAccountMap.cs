using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class BankAccountMap : EntityTypeConfiguration<PrimeActs.Domain.BankAccount>
    {
        public BankAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.BankAccountID);

            // Properties
            this.Property(t => t.AccountName)
                .HasMaxLength(50);

            this.Property(t => t.IBAN)
                .HasMaxLength(40);

            this.Property(t => t.SWIFT)
                .HasMaxLength(15);


            // Table & Column Mappings
            this.ToTable("tblBankAccount");
            this.Property(t => t.BankAccountID).HasColumnName("BankAccountID");
            this.Property(t => t.AccountName).HasColumnName("AccountName");
            this.Property(t => t.BankCode).HasColumnName("BankCode");

            this.Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            this.Property(t => t.IBAN).HasColumnName("IBAN");
            this.Property(t => t.SWIFT).HasColumnName("SWIFT");
            this.Property(t => t.CountryID).HasColumnName("CountryID");


        }
    }
}
