using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierContactMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierContact>
    {
        public SupplierContactMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierContactID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSupplierContact");
            this.Property(t => t.SupplierContactID).HasColumnName("SupplierContactID");
            this.Property(t => t.ContactID).HasColumnName("ContactID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Contact)
                .WithMany(t => t.SupplierContacts)
                .HasForeignKey(d => d.ContactID);
            this.HasRequired(t => t.Supplier)
                .WithMany(t => t.SupplierContacts)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
