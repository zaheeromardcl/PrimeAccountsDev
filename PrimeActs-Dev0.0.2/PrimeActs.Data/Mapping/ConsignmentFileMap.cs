using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ConsignmentFileMap : EntityTypeConfiguration<PrimeActs.Domain.ConsignmentFile>
    {
        public ConsignmentFileMap()
        {
            // Primary Key
            this.HasKey(t => t.ConsignmentFileID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblConsignmentFile");
            this.Property(t => t.ConsignmentFileID).HasColumnName("ConsignmentFileID");
            this.Property(t => t.ConsignmentID).HasColumnName("ConsignmentID");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasOptional(t => t.Consignment)
                .WithMany(t => t.ConsignmentFiles)
                .HasForeignKey(d => d.ConsignmentID);
           

        }
    }
}
