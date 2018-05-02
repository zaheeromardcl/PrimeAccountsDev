using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class GroupMap : EntityTypeConfiguration<PrimeActs.Domain.Group>
    {
        public GroupMap()
        {
            // Primary Key
            this.HasKey(t => t.GroupID);

            // Properties
            this.Property(t => t.GroupName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblGroup");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
