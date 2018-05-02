using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class LedgerEntryTypeMap : EntityTypeConfiguration<PrimeActs.Domain.LedgerEntryType>
    {
        public LedgerEntryTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.LedgerEntryTypeID);

            // Properties
            this.Property(t => t.LedgerEntryTypeDescription)
                .IsRequired()
                .HasMaxLength(50);

            

            // Table & Column Mappings
            this.ToTable("tlkpLedgerEntryType");
            this.Property(t => t.LedgerEntryTypeID).HasColumnName("LedgerEntryTypeID");
            this.Property(t => t.LedgerEntryTypeDescription).HasColumnName("LedgerEntryTypeDescription");
            this.Property(t => t.LedgerEntryTypeNumber).HasColumnName("LedgerEntryTypeNumber");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
