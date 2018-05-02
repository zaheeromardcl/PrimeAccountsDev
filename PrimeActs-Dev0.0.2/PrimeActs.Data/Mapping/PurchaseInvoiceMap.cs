
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{

    public class PurchaseInvoiceMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseInvoice>
    {
        public PurchaseInvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseInvoiceID);

            // Properties
            this.Property(t => t.ServerCode)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblPurchaseInvoice");
            this.Property(t => t.PurchaseInvoiceID).HasColumnName("PurchaseInvoiceID");
            this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.PurchaseInvoiceReference).HasColumnName("PurchaseInvoiceReference");
            this.Property(t => t.ServerCode).HasColumnName("ServerCode");
            this.Property(t => t.PurchaseInvoiceDate).HasColumnName("PurchaseInvoiceDate");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Total).HasColumnName("IncTaxCheckTotal");
            
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Address)
                .WithMany(t => t.PurchaseInvoices)
                .HasForeignKey(d => d.AddressID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.PurchaseInvoices)
                .HasForeignKey(d => d.NoteID);
            this.HasRequired(t => t.SupplierDepartment)
                .WithMany(t => t.PurchaseInvoices)
                .HasForeignKey(d => d.SupplierDepartmentID);

        }
    }
}
