using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TicketRangeMap : EntityTypeConfiguration<PrimeActs.Domain.TicketRange>
    {
        public TicketRangeMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketRangeID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            this.Property(t => t.TicketPrefix)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("tblTicketRange");
            this.Property(t => t.TicketRangeID).HasColumnName("TicketRangeID");
            this.Property(t => t.TicketRangeStart).HasColumnName("TicketRangeStart");
            this.Property(t => t.TicketRangeEnd).HasColumnName("TicketRangeEnd");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.TicketPrefix).HasColumnName("TicketPrefix");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.TicketRanges)
                .HasForeignKey(d => d.DepartmentID);

        }
    }
}
