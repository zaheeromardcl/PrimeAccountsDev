using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    public class PrinterMap : EntityTypeConfiguration<PrimeActs.Domain.Printer>
    {
        public PrinterMap()
        {
            // Primary Key
            this.HasKey(t => t.PrinterID);

            // Properties
            this.Property(t => t.PrinterName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NetworkName)
                .IsRequired()
                .HasMaxLength(100);


            this.Property(t => t.DefaultOrder);
            this.Property(t => t.IsActive);
            this.Property(t => t.IsColour);
            this.Property(t => t.IsRaw);
            this.Property(t => t.HasTractor);



            // Table & Column Mappings
            this.ToTable("tblPrinter");
            this.Property(t => t.PrinterID).HasColumnName("PrinterID");
            this.Property(t => t.PrinterName).HasColumnName("PrinterName");
            this.Property(t => t.NetworkName).HasColumnName("NetworkName");

            this.Property(t => t.DefaultOrder).HasColumnName("DefaultOrder");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsColour).HasColumnName("IsColour");
            this.Property(t => t.IsRaw).HasColumnName("IsRaw");
            this.Property(t => t.HasTractor).HasColumnName("HasTractor");
        }
    }
}
