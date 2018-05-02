using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class BatchNumberLogMap : EntityTypeConfiguration<PrimeActs.Domain.BatchNumberLog>
    {
        public BatchNumberLogMap()
        {
            // Primary Key
            this.HasKey(t => t.BatchNumberLogID);

            // Properties
            this.Property(t => t.ServerPrefix)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.BatchNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblBatchNumberLog");
            this.Property(t => t.BatchNumberLogID).HasColumnName("BatchNumberLogID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.ServerPrefix).HasColumnName("ServerPrefix");
            this.Property(t => t.BatchNumber).HasColumnName("BatchNumber");
            this.Property(t => t.TransactionDateTime).HasColumnName("TransactionDateTime");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.BatchNumberLogs)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.BatchNumberLogs)
                .HasForeignKey(d => d.DivisionID);

        }
    }
}
