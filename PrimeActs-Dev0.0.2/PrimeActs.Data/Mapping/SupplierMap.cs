using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierMap : EntityTypeConfiguration<PrimeActs.Domain.Supplier>
    {
        public SupplierMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierID);

            // Properties
            this.Property(t => t.SupplierCompanyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SupplierCode)
                .IsRequired()
                .HasMaxLength(10);
        
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSupplier");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.SupplierCompanyName).HasColumnName("SupplierCompanyName");
            this.Property(t => t.ParentSupplierID).HasColumnName("ParentSupplierID");
            this.Property(t => t.SupplierCode).HasColumnName("SupplierCode");
            this.Property(t => t.IsHaulier).HasColumnName("IsHaulier");
            this.Property(t => t.IsFactor).HasColumnName("IsFactor");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.Company)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.NoteID);
            this.HasOptional(t => t.ParentSupplier)
                .WithMany(t => t.ChildSuppliers)
                .HasForeignKey(d => d.ParentSupplierID);
        }
    }
}
