using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SetupLocalMap : EntityTypeConfiguration<PrimeActs.Domain.SetupLocal>
    {
        public SetupLocalMap()
        {
            // Primary Key
            this.HasKey(t => t.SetupName);

            // Properties
            this.Property(t => t.SetupName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SetupValueNvarchar)
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSetupLocal");
            this.Property(t => t.SetupName).HasColumnName("SetupName");
            this.Property(t => t.SetupValueType).HasColumnName("SetupValueType");
            this.Property(t => t.SetupValueInt).HasColumnName("SetupValueInt");
            this.Property(t => t.SetupValueNumeric).HasColumnName("SetupValueNumeric");
            this.Property(t => t.SetupValueBit).HasColumnName("SetupValueBit");
            this.Property(t => t.SetupValueNvarchar).HasColumnName("SetupValueNvarchar");
            this.Property(t => t.SetupValueUniqueIdentifier).HasColumnName("SetupValueUniqueIdentifier");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.SetupID).HasColumnName("SetupID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");

            
        }
    }
}
