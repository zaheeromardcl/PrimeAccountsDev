using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerDepartmentMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerDepartment>
    {
        public CustomerDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerDepartmentID);

            // Properties
            this.Property(t => t.CustomerDepartmentName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.FactorRef)
                .HasMaxLength(20);

            this.Property(t => t.EDINumber)
                .HasMaxLength(13);

            this.Property(t => t.EDIIdent)
                .HasMaxLength(20);

            this.Property(t => t.InvoiceEmailAddress)
                .HasMaxLength(250);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblCustomerDepartment");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerDepartmentName).HasColumnName("CustomerDepartmentName");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.CreditTerms).HasColumnName("CreditTerms");
            this.Property(t => t.CreditLimit).HasColumnName("CreditLimit");
            this.Property(t => t.Commission).HasColumnName("Commission");
            this.Property(t => t.Handling).HasColumnName("Handling");
            this.Property(t => t.FactorRef).HasColumnName("FactorRef");
            this.Property(t => t.EDIType).HasColumnName("EDIType");
            this.Property(t => t.EDINumber).HasColumnName("EDINumber");
            this.Property(t => t.EDIIdent).HasColumnName("EDIIdent");
            this.Property(t => t.InvoiceCustomerLocationID).HasColumnName("InvoiceCustomerLocationID");
            this.Property(t => t.InvoiceFrequency).HasColumnName("InvoiceFrequency");
            this.Property(t => t.InvoiceEmailAddress).HasColumnName("InvoiceEmailAddress");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.SalesPersonUserID).HasColumnName("SalesPersonUserID");
            this.Property(t => t.CustomerTypeID).HasColumnName("CustomerTypeID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.RebateType).HasColumnName("RebateType");
            this.Property(t => t.RebateCustomerDepartmentID).HasColumnName("RebateCustomerDepartmentID");
            this.Property(t => t.RebateRate).HasColumnName("RebateRate");
            this.Property(t => t.TransactionTaxReference).HasColumnName("TransactionTaxReference");
            
            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerDepartments)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.CustomerType)
                .WithMany(t => t.CustomerDepartments)
                .HasForeignKey(d => d.CustomerTypeID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.CustomerDepartments)
                .HasForeignKey(d => d.NoteID);

            this.HasOptional(t => t.RebateCustomerDepartment)
                .WithMany()
                .HasForeignKey(t => t.RebateCustomerDepartmentID);

            this.HasMany(t => t.Contacts).WithMany(c => c.CustomerDepartments)
                .Map(t => t.ToTable("tblCustomerContactDepartment")
                    .MapLeftKey("CustomerDepartmentID")
                    .MapRightKey("CustomerContactID"));
            //this.HasOptional(t => t.AspNetUser)
            //    .WithMany(t => t.CustomerDepartments)
            //    .HasForeignKey(d => d.SalesPersonUserID);
        }
    }
}
