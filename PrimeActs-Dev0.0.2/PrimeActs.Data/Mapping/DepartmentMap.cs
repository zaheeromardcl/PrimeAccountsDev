using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<PrimeActs.Domain.Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentID);

            // Properties
            this.Property(t => t.DepartmentName)
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblDepartment");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.DepartmentCode).HasColumnName("DepartmentCode");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.RebateNominalAccountID).HasColumnName("RebateNominalAccountID");

            // Relationships
            this.HasOptional(t => t.Address)
                .WithMany(t => t.Departments)
                .HasForeignKey(d => d.AddressID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.Departments)
                .HasForeignKey(d => d.DivisionID);

            //this.HasMany(t => t.Printers).WithMany(c => c.Departments)
            //    .Map(t => t.ToTable("tblDepartmentPrinters")
            //        .MapLeftKey("DepartmentID")
            //        .MapRightKey("PrinterID"));
        }
    }
}
