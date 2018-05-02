using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ApplicationPermissionMap : EntityTypeConfiguration<PrimeActs.Domain.Permission>
    {
        public ApplicationPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.PermissionID);

            // Properties
            //this.Property(t => t.PermissionName)
            //    .HasMaxLength(50);

            this.Property(t => t.PermissionController)
                .HasMaxLength(50);
            this.Property(t => t.PermissionAction)
                .HasMaxLength(50);
            

            this.Property(t => t.PermissionDescription)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblPermission");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
            //this.Property(t => t.PermissionGroupId).HasColumnName("PermissionGroupId");
            //this.Property(t => t.PermissionName).HasColumnName("PermissionName");
            this.Property(t => t.PermissionController).HasColumnName("PermissionController");
            this.Property(t => t.PermissionAction).HasColumnName("PermissionAction");
            
            this.Property(t => t.PermissionDescription).HasColumnName("PermissionDescription");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
