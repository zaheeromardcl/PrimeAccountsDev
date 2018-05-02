using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class NoteMap : EntityTypeConfiguration<PrimeActs.Domain.Note>
    {
        public NoteMap()
        {
            // Primary Key
            this.HasKey(t => t.NoteID);

            // Properties
            this.Property(t => t.NoteDescription)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.NoteText)
                .IsRequired();

            
            // Table & Column Mappings
            this.ToTable("tblNote");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.NoteDescription).HasColumnName("NoteDescription");
            this.Property(t => t.NoteText).HasColumnName("NoteText");
            
        }
    }
}
