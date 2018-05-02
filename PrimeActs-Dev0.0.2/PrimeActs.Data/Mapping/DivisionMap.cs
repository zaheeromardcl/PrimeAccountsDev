using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class DivisionMap : EntityTypeConfiguration<PrimeActs.Domain.Division>
    {
        public DivisionMap()
        {
            // Primary Key
            this.HasKey(t => t.DivisionID);

            // Properties
            this.Property(t => t.DivisionName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            

            // Table & Column Mappings
            this.ToTable("tblDivision");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DivisionName).HasColumnName("DivisionName");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t=>t.TransactionTaxLocationID).HasColumnName("TransactionTaxLocationID");
            this.Property(t => t.InvoiceNominalAccountID).HasColumnName("InvoiceNominalAccountID");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.Address)
                .WithMany(t => t.Divisions)
                .HasForeignKey(d => d.AddressID);
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Divisions)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
