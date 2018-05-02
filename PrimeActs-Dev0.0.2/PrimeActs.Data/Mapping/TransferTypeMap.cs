using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TransferTypeMap : EntityTypeConfiguration<PrimeActs.Domain.TransferType>
    {
        public TransferTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TransferTypeID);

            // Properties
            this.Property(t => t.TransferTypeName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TransferTypeCode)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("tlkpTransferType");
            this.Property(t => t.TransferTypeID).HasColumnName("TransferTypeID");
            this.Property(t => t.TransferTypeName).HasColumnName("TransferTypeName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.TransferTypeCode).HasColumnName("TransferTypeCode");
        }
    }
}
