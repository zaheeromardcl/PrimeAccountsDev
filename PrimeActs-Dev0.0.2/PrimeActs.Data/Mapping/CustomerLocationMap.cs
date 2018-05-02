using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerLocationMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerLocation>
    {
        public CustomerLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerLocationID);

            // Properties
            this.Property(t => t.CustomerLocationName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TelephoneNumber)
                .HasMaxLength(30);

            this.Property(t => t.FaxNumber)
                .HasMaxLength(30);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblCustomerLocation");
            this.Property(t => t.CustomerLocationID).HasColumnName("CustomerLocationID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerLocationName).HasColumnName("CustomerLocationName");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.TelephoneNumber).HasColumnName("TelephoneNumber");
            this.Property(t => t.FaxNumber).HasColumnName("FaxNumber");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Address)
                .WithMany(t => t.CustomerLocations)
                .HasForeignKey(d => d.AddressID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerLocations)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.CustomerLocations)
                .HasForeignKey(d => d.NoteID);

        }
    }
}
