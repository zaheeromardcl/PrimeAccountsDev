using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class FileMap : EntityTypeConfiguration<PrimeActs.Domain.File>
    {
        public FileMap()
        {
            // Primary Key
            this.HasKey(t => t.FileID);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContentType)
                .HasMaxLength(50);

            this.Property(t => t.FileContent)
                .IsRequired();


            // Table & Column Mappings
            this.ToTable("tblFile");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.FileContent).HasColumnName("FileContent");
            
        }
    }
}
