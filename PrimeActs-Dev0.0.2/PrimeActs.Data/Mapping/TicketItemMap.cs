using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TicketItemMap : EntityTypeConfiguration<PrimeActs.Domain.TicketItem>
    {
        public TicketItemMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketItemID);

            // Properties
            this.Property(t => t.TicketItemDescription)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblTicketItem");
            this.Property(t => t.TicketItemID).HasColumnName("TicketItemID");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.TicketItemDescription).HasColumnName("TicketItemDescription");
            this.Property(t => t.TicketItemQuantity).HasColumnName("TicketItemQuantity");
            this.Property(t => t.TicketItemTotalPrice).HasColumnName("TicketItemTotalPrice");
            this.Property(t => t.ConsignmentItemID).HasColumnName("ConsignmentItemID");
            this.Property(t => t.HaulierSupplierID).HasColumnName("HaulierSupplierID");
            this.Property(t => t.TransactionTaxRateID).HasColumnName("TransactionTaxRateID");
            
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            this.Property(t => t.PorterageID).HasColumnName("PorterageID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
           this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.PorterageValue).HasColumnName("PorterageValue");
            this.Property(t => t.OriginalTicketItemID).HasColumnName("OriginalTicketItemID");
            this.Property(t => t.IsLatest).HasColumnName("IsLatest");
            this.Property(t => t.TransferTypeID).HasColumnName("TransferTypeID");

            // Relationships
            this.HasOptional(t => t.ConsignmentItem)
                .WithMany(t => t.TicketItems)
                .HasForeignKey(d => d.ConsignmentItemID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.TicketItems)
                .HasForeignKey(d => d.HaulierSupplierID);
            this.HasRequired(t => t.Ticket)
                .WithMany(t => t.TicketItems)
                .HasForeignKey(d => d.TicketID);
            this.HasOptional(t => t.TransferType)
                .WithMany(t => t.TicketItems)
                .HasForeignKey(d => d.TransferTypeID);
 
            this.HasOptional(t => t.Department)
                .WithMany(t => t.TicketItems)
                .HasForeignKey(d => d.DepartmentID);
        }
    }
}
