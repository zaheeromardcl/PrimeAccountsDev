using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class IntrastatMap : EntityTypeConfiguration<PrimeActs.Domain.Intrastat>
    {
        public IntrastatMap()
        {
            // Primary Key
            this.HasKey(t => t.IntrastatID);

            // Properties
            this.Property(t => t.IntrastatDescription)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IntrastatVATNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IntrastatBranchNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblIntrastat");
            this.Property(t => t.IntrastatID).HasColumnName("IntrastatID");
            this.Property(t => t.IntrastatDate).HasColumnName("IntrastatDate");
            this.Property(t => t.IntrastatDescription).HasColumnName("IntrastatDescription");
            this.Property(t => t.IntrastatValue).HasColumnName("IntrastatValue");
            this.Property(t => t.IntrastatCompanyID).HasColumnName("IntrastatCompanyID");
            this.Property(t => t.IntrastatVATNumber).HasColumnName("IntrastatVATNumber");
            this.Property(t => t.IntrastatBranchNumber).HasColumnName("IntrastatBranchNumber");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
          
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
