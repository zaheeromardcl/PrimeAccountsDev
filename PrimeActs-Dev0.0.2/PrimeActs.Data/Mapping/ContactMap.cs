using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ContactMap : EntityTypeConfiguration<PrimeActs.Domain.Contact>
    {
        public ContactMap()
        {
            // Primary Key
            this.HasKey(t => t.ContactID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.ContactType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactReference)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.DDITelephoneNo)
                .HasMaxLength(30);

            this.Property(t => t.MobileNo)
                .HasMaxLength(20);


            // Table & Column Mappings
            this.ToTable("tblContact");
            this.Property(t => t.ContactID).HasColumnName("ContactID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ContactType).HasColumnName("ContactType");
            this.Property(t => t.ContactReference).HasColumnName("ContactReference");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.DDITelephoneNo).HasColumnName("DDITelephoneNo");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");

            // Relationships
            this.HasOptional(t => t.Note)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.NoteID);

        }
    }
}
