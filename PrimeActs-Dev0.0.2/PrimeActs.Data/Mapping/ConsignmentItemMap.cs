using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ConsignmentItemMap : EntityTypeConfiguration<PrimeActs.Domain.ConsignmentItem>
    {
        public ConsignmentItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ConsignmentItemID);

            // Properties
            this.Property(t => t.Brand)
                .HasMaxLength(50);

            this.Property(t => t.Rotation)
                .HasMaxLength(10);

            this.Property(t => t.PackType)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.PackSize)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.String1)
                .HasMaxLength(1000);

            this.Property(t => t.String2)
                .HasMaxLength(1000);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblConsignmentItem");
            this.Property(t => t.ConsignmentItemID).HasColumnName("ConsignmentItemID");
            this.Property(t => t.ConsignmentID).HasColumnName("ConsignmentID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.BestBeforeDate).HasColumnName("BestBeforeDate");
            this.Property(t => t.ProduceID).HasColumnName("ProduceID");
            this.Property(t => t.Brand).HasColumnName("Brand");
            this.Property(t => t.Rotation).HasColumnName("Rotation");
            this.Property(t => t.PackType).HasColumnName("PackType");
            this.Property(t => t.PackWtUnitID).HasColumnName("PackWtUnitID");
            this.Property(t => t.PackWeight).HasColumnName("PackWeight");
            this.Property(t => t.PackSize).HasColumnName("PackSize");
            this.Property(t => t.PackPall).HasColumnName("PackPall");
            this.Property(t => t.QuantityExpected).HasColumnName("QuantityExpected");
            
            this.Property(t => t.EstimatedProfit).HasColumnName("EstimatedProfit");
            this.Property(t => t.EstimatedChargeCost).HasColumnName("EstimatedChargeCost");
            this.Property(t => t.RetReduce).HasColumnName("RetReduce");
            this.Property(t => t.EstimatedPurchaseCost).HasColumnName("EstimatedPurchaseCost");
            this.Property(t => t.ItemStatus).HasColumnName("ItemStatus");
            this.Property(t => t.PorterageID).HasColumnName("PorterageID");
            
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.FK1).HasColumnName("FK1");
            this.Property(t => t.FK2).HasColumnName("FK2");
            this.Property(t => t.Bit1).HasColumnName("Bit1");
            this.Property(t => t.Bit2).HasColumnName("Bit2");
            this.Property(t => t.String1).HasColumnName("String1");
            this.Property(t => t.String2).HasColumnName("String2");
            this.Property(t => t.Numeric1).HasColumnName("Numeric1");
            this.Property(t => t.Numeric2).HasColumnName("Numeric2");
            this.Property(t => t.Int1).HasColumnName("Int1");
            this.Property(t => t.Int2).HasColumnName("Int2");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.OriginCountryID).HasColumnName("OriginCountryID");
            

            // Relationships
            this.HasOptional(t => t.Consignment)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.ConsignmentID);
            this.HasOptional(t => t.Country)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.OriginCountryID);
            this.HasOptional(t => t.Department)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.DepartmentID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.NoteID);
            this.HasOptional(t => t.PackWtUnit)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.PackWtUnitID);
            this.HasRequired(t => t.Porterage)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.PorterageID);
            this.HasRequired(t => t.Produce)
                .WithMany(t => t.ConsignmentItems)
                .HasForeignKey(d => d.ProduceID);

        }
    }
}
