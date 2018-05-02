using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerTypeMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerType>
    {
        public CustomerTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerTypeID);

            // Properties
            this.Property(t => t.CustomerTypeCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CustomerTypeDescription)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tlkpCustomerType");
            this.Property(t => t.CustomerTypeID).HasColumnName("CustomerTypeID");
            this.Property(t => t.CustomerTypeCode).HasColumnName("CustomerTypeCode");
            this.Property(t => t.CustomerTypeDescription).HasColumnName("CustomerTypeDescription");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CustomerTypes)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
