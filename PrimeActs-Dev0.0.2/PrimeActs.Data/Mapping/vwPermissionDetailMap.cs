using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    public class vwPermissionDetailMap : EntityTypeConfiguration<PrimeActs.Domain.vwPermissionDetail>
    {
        public vwPermissionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.PermissionDetailID);

            // Table & Column Mappings
            this.ToTable("vwPermissionDetail");

            this.Property(t => t.PermissionDetailID).HasColumnName("PermissionDetailID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
        }
    }
}
