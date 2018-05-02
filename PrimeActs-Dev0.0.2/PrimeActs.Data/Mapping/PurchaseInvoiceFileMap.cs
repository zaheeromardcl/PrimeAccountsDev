using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    public class PurchaseInvoiceFileMap : EntityTypeConfiguration<PrimeActs.Domain.PurchaseInvoiceFile>
    {
        public PurchaseInvoiceFileMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseInvoiceFileID);
            
            // Table & Column Mappings
            this.ToTable("tblPurchaseInvoiceFile");
            this.Property(t => t.PurchaseInvoiceFileID).HasColumnName("PurchaseInvoiceFileID");
            this.Property(t => t.PurchaseInvoiceID).HasColumnName("PurchaseInvoiceID");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            //this.HasOptional(t => t.PurchaseInvoice)
            //    .WithMany(t => t.PurchaseInvoiceFiles)
            //    .HasForeignKey(d => d.PurchaseInvoiceID);
            //this.HasOptional(t => t.File)
            //    .WithMany(t => t.PurchaseInvoiceFiles)
            //    .HasForeignKey(d => d.FileID);
        }
    }
}
