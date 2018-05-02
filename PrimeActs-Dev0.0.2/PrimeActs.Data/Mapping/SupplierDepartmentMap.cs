using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierDepartmentMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierDepartment>
    {
        public SupplierDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierDepartmentID);

            // Properties
            this.Property(t => t.SupplierDepartmentName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.EDINumber)
                .HasMaxLength(13);

            this.Property(t => t.EDIIdent)
                .HasMaxLength(20);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSupplierDepartment");
            this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.SupplierDepartmentName).HasColumnName("SupplierDepartmentName");
            this.Property(t => t.CreditTerm).HasColumnName("CreditTerm");
            this.Property(t => t.CreditLimit).HasColumnName("CreditLimit");
            this.Property(t => t.Commission).HasColumnName("Commission");
            this.Property(t => t.Handling).HasColumnName("Handling");
            this.Property(t => t.FactorSupplierDepartmentID).HasColumnName("FactorSupplierDepartmentID");
            this.Property(t => t.Rebate).HasColumnName("RebateAmount");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.EDIType).HasColumnName("EDIType");
            this.Property(t => t.EDINumber).HasColumnName("EDINumber");
            this.Property(t => t.EDIIdent).HasColumnName("EDIIdent");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsTransactionTaxExempt).HasColumnName("IsTransactionTaxExempt");
            this.Property(t => t.TransactionTaxReference).HasColumnName("TransactionTaxReference");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.GivesRebate).HasColumnName("GivesRebate");
            this.Property(t => t.NoteID).HasColumnName("NoteID");

            // Relationships
            this.HasOptional(t => t.Note)
                .WithMany(t => t.SupplierDepartments)
                .HasForeignKey(d => d.NoteID);
            this.HasRequired(t => t.Supplier)
                .WithMany(t => t.SupplierDepartments)
                .HasForeignKey(d => d.SupplierID);
            this.HasOptional(t => t.SupplierDepartment2)
                .WithMany(t => t.SupplierDepartment1)
                .HasForeignKey(d => d.FactorSupplierDepartmentID);

            //this.HasMany(t => t.SupplierLocations).WithMany(sl => sl.SupplierDepartments)
            //    .Map(t => t.ToTable("tblSupplierDepartmentLocation")
            //        .MapLeftKey("SupplierDepartmentID")
            //        .MapRightKey("SupplierLocationID"));
        }
    }
}
