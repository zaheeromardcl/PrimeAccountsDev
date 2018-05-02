using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ErrorLogMap : EntityTypeConfiguration<PrimeActs.Domain.ErrorLog>
    {
        public ErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrorID);

            // Properties
            this.Property(t => t.ErrorDescription)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ErrorCategory)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblErrorLog");
            this.Property(t => t.ErrorID).HasColumnName("ErrorID");
            this.Property(t => t.ErrorDescription).HasColumnName("ErrorDescription");
            this.Property(t => t.ErrorCategory).HasColumnName("ErrorCategory");
        }
    }
}
