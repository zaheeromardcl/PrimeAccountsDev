using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<PrimeActs.Domain.Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.AddressID);

            // Properties
            this.Property(t => t.AddressLine1)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(50);

            this.Property(t => t.AddressLine3)
                .HasMaxLength(50);

            this.Property(t => t.PostalTown)
                .HasMaxLength(50);

            this.Property(t => t.CountyCity)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Postcode)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("tblAddress");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.AddressLine3).HasColumnName("AddressLine3");
            this.Property(t => t.PostalTown).HasColumnName("PostalTown");
            this.Property(t => t.CountyCity).HasColumnName("CountyCity");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
        }
    }
}
