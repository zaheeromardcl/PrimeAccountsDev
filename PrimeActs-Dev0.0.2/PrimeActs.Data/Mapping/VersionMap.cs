using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class VersionMap : EntityTypeConfiguration<PrimeActs.Domain.Version>
    {
        public VersionMap()
        {
            // Primary Key
            this.HasKey(t => t.MasterVersion);

            // Properties
            this.Property(t => t.MasterVersion)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("tblVersion");
            this.Property(t => t.MasterVersion).HasColumnName("MasterVersion");
            this.Property(t => t.LocalVersion).HasColumnName("LocalVersion");
        }
    }
}
