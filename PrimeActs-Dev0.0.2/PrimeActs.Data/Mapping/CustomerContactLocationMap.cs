using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerContactLocationMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerContactLocation>
    {
        public CustomerContactLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerContactLocationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblCustomerContactLocation");
            this.Property(t => t.CustomerContactLocationID).HasColumnName("CustomerContactLocationID");
            this.Property(t => t.CustomerContactID).HasColumnName("CustomerContactID");
            this.Property(t => t.CustomerLocationID).HasColumnName("CustomerLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.CustomerContact)
                .WithMany(t => t.CustomerContactLocations)
                .HasForeignKey(d => d.CustomerContactID);
            this.HasRequired(t => t.CustomerLocation)
                .WithMany(t => t.CustomerContactLocations)
                .HasForeignKey(d => d.CustomerLocationID);

        }
    }
}
