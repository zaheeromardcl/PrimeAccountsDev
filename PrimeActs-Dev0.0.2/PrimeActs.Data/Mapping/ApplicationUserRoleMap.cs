using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ApplicationUserRoleMap : EntityTypeConfiguration<PrimeActs.Domain.ApplicationUserRole>
    {
        public ApplicationUserRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.UserRoleID);
            
            // Table & Column Mappings
            this.ToTable("tblUserRole");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentID");
            this.Property(t => t.CompanyId).HasColumnName("CompanyID");
            this.Property(t => t.DivisionId).HasColumnName("DivisionID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            //this.HasOptional(t => t.Department)
            //    .WithMany(d => d.ApplicationUserRoles)
            //    .HasForeignKey(t => t.DepartmentId);

            ////this.HasOptional(t => t.Company)
            ////    .WithMany(c => c.ApplicationUserRoles)
            ////    .HasForeignKey(t => t.CompanyId);

            //this.HasOptional(t => t.Division)
            //    .WithMany(c => c.ApplicationUserRoles)
            //    .HasForeignKey(t => t.DivisionId);

            //this.HasOptional(t => t.ApplicationUser)
            //    .WithMany(c => c.ApplicationUserRoles)
            //    .HasForeignKey(t => t.ApplicationUserID);

            //this.HasOptional(t => t.ApplicationRole)
            //    .WithMany(c => c.ApplicationUserRoles)
            //    .HasForeignKey(t => t.ApplicationRoleID);

        }
    }
}
