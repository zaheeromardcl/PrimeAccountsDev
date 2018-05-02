using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CompanyNominalAccountMap : EntityTypeConfiguration<PrimeActs.Domain.CompanyNominalAccount>
    {
        public CompanyNominalAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyNominalAccountID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblCompanyNominalAccount");
            this.Property(t => t.CompanyNominalAccountID).HasColumnName("CompanyNominalAccountID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.NominalAccountID).HasColumnName("NominalAccountID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CompanyNominalAccounts)
                .HasForeignKey(d => d.CompanyID);
            this.HasRequired(t => t.NominalAccount)
                .WithMany(t => t.CompanyNominalAccounts)
                .HasForeignKey(d => d.NominalAccountID);

        }
    }
}
