using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerContactMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerContact>
    {
        public CustomerContactMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerContactID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblCustomerContact");
            this.Property(t => t.CustomerContactID).HasColumnName("CustomerContactID");
            this.Property(t => t.ContactID).HasColumnName("ContactID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Contact)
                .WithMany(t => t.CustomerContacts)
                .HasForeignKey(d => d.ContactID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerContacts)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
