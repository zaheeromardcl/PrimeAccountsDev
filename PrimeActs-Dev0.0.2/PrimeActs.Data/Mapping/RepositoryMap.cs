using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class RepositoryMap : EntityTypeConfiguration<PrimeActs.Domain.Repository>
    {
        public RepositoryMap()
        {
            // Primary Key
            this.HasKey(t => t.VersionID);

            // Properties
            this.Property(t => t.VersionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodeZipFile)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("tblRepository");
            this.Property(t => t.VersionID).HasColumnName("VersionID");
            this.Property(t => t.CodeZipFile).HasColumnName("CodeZipFile");
        }
    }
}
