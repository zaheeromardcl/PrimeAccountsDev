using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierBankAccountMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierBankAccount>
    {
        public SupplierBankAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierBankAccountID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblSupplierBankAccount");
            this.Property(t => t.SupplierBankAccountID).HasColumnName("SupplierBankAccountID");
            this.Property(t => t.BankAccountID).HasColumnName("BankAccountID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.SupplierLocationID).HasColumnName("SupplierLocationID");
            this.Property(t => t.UseToPayInvoice).HasColumnName("UseToPayInvoice");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            // Relationships
            this.HasRequired(t => t.BankAccount)
                .WithMany(t => t.SupplierBankAccounts)
                .HasForeignKey(d => d.BankAccountID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.SupplierBankAccounts)
                .HasForeignKey(d => d.SupplierID);
            this.HasOptional(t => t.SupplierDepartment)
                .WithMany(t => t.SupplierBankAccounts)
                .HasForeignKey(d => d.SupplierDepartmentID);
            this.HasOptional(t => t.SupplierLocation)
                .WithMany(t => t.SupplierBankAccounts)
                .HasForeignKey(d => d.SupplierLocationID);

        }
    }
}
