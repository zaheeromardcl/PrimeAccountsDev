using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<PrimeActs.Domain.Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);


            this.Property(t => t.CreatedBy);

            

            this.Property(t => t.CompanyNo)
                .HasMaxLength(20);

            this.Property(t => t.Telephone)
                .HasMaxLength(20);

            this.Property(t => t.FaxNo)
                .HasMaxLength(20);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(150);

            this.Property(t => t.Website)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblCompany");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ParentCompanyID).HasColumnName("ParentCompanyID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.RegisteredAddressID).HasColumnName("RegisteredAddressID");
            
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
          this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.FaxNo).HasColumnName("FaxNo");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.InvoiceInfo).HasColumnName("InvoiceInfo");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.Address)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.AddressID);
            this.HasOptional(t => t.RegisteredAddress)
                .WithMany(t => t.RegisteredAddressCompanies)
                .HasForeignKey(d => d.RegisteredAddressID);
            this.HasOptional(t => t.ParentCompany)
                .WithMany(t => t.ChildCompanies)
                .HasForeignKey(d => d.ParentCompanyID);

        }
    }
}
