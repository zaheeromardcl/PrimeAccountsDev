using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class MasterGroupMap : EntityTypeConfiguration<PrimeActs.Domain.MasterGroup>
    {
        public MasterGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.MasterGroupID);

            // Properties
            this.Property(t => t.MasterGroupCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.MasterGroupName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);
            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblMasterGroup");
            this.Property(t => t.MasterGroupID).HasColumnName("MasterGroupID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.MasterGroupCode).HasColumnName("MasterGroupCode");
            this.Property(t => t.MasterGroupName).HasColumnName("MasterGroupName");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            // Relationships

            // Relationships
            this.HasRequired(t => t.Division)
                .WithMany(t => t.MasterGroups)
                .HasForeignKey(d => d.DivisionID);

        }
    }
}
