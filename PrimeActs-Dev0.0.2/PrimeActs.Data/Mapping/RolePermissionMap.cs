using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    public class RolePermissionMap : EntityTypeConfiguration<PrimeActs.Domain.RolePermission>
    {
        public RolePermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.RolePermissionID);

            // Table & Column Mappings
            this.ToTable("tblRolePermission");
            this.Property(t => t.RolePermissionID).HasColumnName("RolePermissionID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
