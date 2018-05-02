using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<PrimeActs.Domain.Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerID);

            // Properties
            this.Property(t => t.CustomerCompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CustomerCode)
                .IsRequired()
                .HasMaxLength(10);
         
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblCustomer");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.ParentCustomerID).HasColumnName("ParentCustomerID");
            this.Property(t => t.CustomerCompanyName).HasColumnName("CustomerCompanyName");
            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.CreditLimitCash).HasColumnName("CreditLimitCash");
            this.Property(t => t.CreditLimitInvoice).HasColumnName("CreditLimitInvoice");
            this.Property(t => t.CreditRating).HasColumnName("CreditRatingID");       
            this.Property(t => t.Statements).HasColumnName("Statements");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsTransfer).HasColumnName("IsTransfer");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.Company)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.NoteID);
            this.HasOptional(t => t.ParentCustomer)
                .WithMany(t => t.ChildCustomers)
                .HasForeignKey(d => d.ParentCustomerID);
            //this.HasOptional(t => t.Customer2) //---!!!---???
            //    .WithMany(t => t.ChildCustomers)
            //    .HasForeignKey(d => d.ParentCustomerID);
        }
    }
}
