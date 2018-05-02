using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    public class PaymentTypeMap : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentTypeID);

            // Properties
            this.Property(t => t.PaymentTypeName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PaymentTypeCode)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("tlkpPaymentType");
            this.Property(t => t.PaymentTypeID).HasColumnName("PaymentTypeID");
            this.Property(t => t.PaymentTypeName).HasColumnName("PaymentTypeName");
            this.Property(t => t.PaymentTypeCode).HasColumnName("PaymentTypeCode");
            this.Property(t => t.Order).HasColumnName("DisplayOrder");
            this.Property(t => t.Default).HasColumnName("IsDefault");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
