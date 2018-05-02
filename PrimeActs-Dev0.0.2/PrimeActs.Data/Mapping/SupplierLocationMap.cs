using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SupplierLocationMap : EntityTypeConfiguration<PrimeActs.Domain.SupplierLocation>
    {
        public SupplierLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierLocationID);

            // Properties
            this.Property(t => t.SupplierLocationName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TelephoneNumber)
                .HasMaxLength(30);

            this.Property(t => t.FaxNumber)
                .HasMaxLength(30);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSupplierLocation");
            this.Property(t => t.SupplierLocationID).HasColumnName("SupplierLocationID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.SupplierLocationName).HasColumnName("SupplierLocationName");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.TelephoneNumber).HasColumnName("TelephoneNumber");
            this.Property(t => t.FaxNumber).HasColumnName("FaxNumber");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Address)
                .WithMany(t => t.SupplierLocations)
                .HasForeignKey(d => d.AddressID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.SupplierLocations)
                .HasForeignKey(d => d.NoteID);
            this.HasRequired(t => t.Supplier)
                .WithMany(t => t.SupplierLocations)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
