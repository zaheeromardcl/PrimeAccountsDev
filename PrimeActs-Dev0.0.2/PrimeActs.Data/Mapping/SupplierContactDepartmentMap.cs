using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierContactDepartmentMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierContactDepartment>
    {
        public SupplierContactDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierContactDepartmentID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblSupplierContactDepartment");
            this.Property(t => t.SupplierContactDepartmentID).HasColumnName("SupplierContactDepartmentID");
            this.Property(t => t.SupplierContactID).HasColumnName("SupplierContactID");
            this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.SupplierContact)
                .WithMany(t => t.SupplierContactDepartments)
                .HasForeignKey(d => d.SupplierContactID);
            this.HasRequired(t => t.SupplierDepartment)
                .WithMany(t => t.SupplierContactDepartments)
                .HasForeignKey(d => d.SupplierDepartmentID);

        }
    }
}
