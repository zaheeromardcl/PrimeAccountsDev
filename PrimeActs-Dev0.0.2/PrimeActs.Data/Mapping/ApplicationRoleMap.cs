using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ApplicationRoleMap : EntityTypeConfiguration<PrimeActs.Domain.ApplicationRole>
    {
        public ApplicationRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);
            
            // Table & Column Mappings
            this.ToTable("tblRole");
            this.Property(t => t.Id).HasColumnName("RoleID");
            this.Property(t => t.Name).HasColumnName("RoleName");
            this.Property(t => t.Description).HasColumnName("RoleDescription");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            
            ////// Relationships
            //this.HasMany(t => t.Permissions)
            //    .WithMany(t => t.ApplicationRoles)
            //    .Map(e => e.ToTable("tblRolePermission")
            //        .MapLeftKey("RoleID")
            //        .MapRightKey("PermissionID"));
        }
    }
}
