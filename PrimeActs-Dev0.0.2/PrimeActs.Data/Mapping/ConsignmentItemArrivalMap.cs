using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ConsignmentItemArrivalMap : EntityTypeConfiguration<PrimeActs.Domain.ConsignmentItemArrival>
    {
        public ConsignmentItemArrivalMap()
        {
            // Primary Key
            this.HasKey(t => t.ConsignmentItemArrivalID);

            // Properties
            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblConsignmentItemArrival");
            this.Property(t => t.ConsignmentItemArrivalID).HasColumnName("ConsignmentItemArrivalID");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.ConsignmentItemID).HasColumnName("ConsignmentItemID");
            //this.Property(t => t.ConsignmentArrivalDate).HasColumnName("ConsignmentItemArrivalDate");
            this.Property(t => t.ConsignmentItemArrivalDate).HasColumnName("ConsignmentItemArrivalDate");
            this.Property(t => t.QuantityReceived).HasColumnName("QuantityReceived");
            this.Property(t => t.StockLocationID).HasColumnName("StockLocationID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            

            // Relationships
            this.HasOptional(t => t.ConsignmentItem)
                .WithMany(t => t.ConsignmentItemArrivals)
                .HasForeignKey(d => d.ConsignmentItemID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.ConsignmentItemArrivals)
                .HasForeignKey(d => d.NoteID);

        }
    }
}
