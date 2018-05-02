using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerCurrencyMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerCurrency>
    {
        public CustomerCurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerCurrencyID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblCustomerCurrency");
            this.Property(t => t.CustomerCurrencyID).HasColumnName("CustomerCurrencyID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.Currency)
                .WithMany(t => t.CustomerCurrencies)
                .HasForeignKey(d => d.CurrencyID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerCurrencies)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
