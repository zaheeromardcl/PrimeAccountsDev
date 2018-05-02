using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SetupGlobalMap : EntityTypeConfiguration<PrimeActs.Domain.SetupGlobal>
    {
        public SetupGlobalMap()
        {
            // Primary Key
           // this.HasKey(t => t.SetupName);
            this.HasKey(t => t.SetupID);

            // Properties
            this.Property(t => t.SetupName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SetupValueNvarchar)
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);
            //this.Property(t => t.UpdatedByUserID);

            this.Property(t => t.CreatedBy);
            //this.Property(t => t.CreatedByUserID);
            // Table & Column Mappings
            this.ToTable("tblSetupGlobal");
            this.Property(t => t.SetupName).HasColumnName("SetupName");
            this.Property(t => t.SetupValueType).HasColumnName("SetupValueType");
            this.Property(t => t.SetupValueInt).HasColumnName("SetupValueInt");
            this.Property(t => t.SetupValueNumeric).HasColumnName("SetupValueNumeric");
            this.Property(t => t.SetupValueBit).HasColumnName("SetupValueBit");
            this.Property(t => t.SetupValueNvarchar).HasColumnName("SetupValueNvarchar");
            this.Property(t => t.SetupValueUniqueIdentifier).HasColumnName("SetupValueUniqueIdentifier");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
           // this.Property(t => t.UpdatedByUserID).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            //this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //this.Property(t => t.SetupID).HasColumnName("SetupID");
            

            // Relationships
            //this.HasOptional(t => t.Department)
            //    .WithMany(t => t.SetupGlobals)
            //    .HasForeignKey(d => d.DepartmentID);
            //this.HasOptional(t => t.Division)
            //    .WithMany(t => t.SetupGlobals)
            //    .HasForeignKey(d => d.DivisionID);

        }
    }
}
