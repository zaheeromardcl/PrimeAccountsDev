using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class RoleContextMap : EntityTypeConfiguration<PrimeActs.Domain.RoleContext>
    {
        public RoleContextMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleContextID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblRoleContext");
            this.Property(t => t.RoleContextID).HasColumnName("RoleContextID");
            this.Property(t => t.UserRoleID).HasColumnName("UserRoleID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");

            // Relationships
            this.HasOptional(t => t.Company)
                .WithMany(t => t.RoleContexts)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Department)
                .WithMany(t => t.RoleContexts)
                .HasForeignKey(d => d.DepartmentID);
            this.HasRequired(t => t.Division)
                .WithMany(t => t.RoleContexts)
                .HasForeignKey(d => d.DivisionID);

        }
    }
}
