using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SalesInvoiceItemMap : EntityTypeConfiguration<PrimeActs.Domain.SalesInvoiceItem>
    {
        public SalesInvoiceItemMap()
        {
            // Primary Key
            this.HasKey(t => t.SalesInvoiceItemID);

            // Properties
            this.Property(t => t.SalesInvoiceItemDescription)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSalesInvoiceItem");
            this.Property(t => t.SalesInvoiceItemID).HasColumnName("SalesInvoiceItemID");
            this.Property(t => t.SalesInvoiceID).HasColumnName("SalesInvoiceID");
            this.Property(t => t.SalesInvoiceItemDescription).HasColumnName("SalesInvoiceItemDescription");
            this.Property(t => t.SalesInvoiceItemLineTotal).HasColumnName("SalesInvoiceItemLineTotal");
            this.Property(t => t.TicketItemID).HasColumnName("TicketItemID");
            this.Property(t => t.TransactionTaxRateID).HasColumnName("TransactionTaxRateID");
       
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            

            // Relationships
            this.HasRequired(t => t.SalesInvoice)
                .WithMany(t => t.SalesInvoiceItems)
                .HasForeignKey(d => d.SalesInvoiceID);
            this.HasRequired(t => t.TicketItem)
                .WithMany(t => t.SalesInvoiceItems)
                .HasForeignKey(d => d.TicketItemID);

        }
    }
}
