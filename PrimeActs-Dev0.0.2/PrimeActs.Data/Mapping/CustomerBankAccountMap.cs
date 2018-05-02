using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerBankAccountMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerBankAccount>
    {
        public CustomerBankAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerBankAccountID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblCustomerBankAccount");
            this.Property(t => t.CustomerBankAccountID).HasColumnName("CustomerBankAccountID");
            this.Property(t => t.BankAccountID).HasColumnName("BankAccountID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.CustomerLocationID).HasColumnName("CustomerLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.BankAccount)
                .WithMany(t => t.CustomerBankAccounts)
                .HasForeignKey(d => d.BankAccountID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerBankAccounts)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.CustomerDepartment)
                .WithMany(t => t.CustomerBankAccounts)
                .HasForeignKey(d => d.CustomerDepartmentID);
            this.HasOptional(t => t.CustomerLocation)
                .WithMany(t => t.CustomerBankAccounts)
                .HasForeignKey(d => d.CustomerLocationID);

        }
    }
}
