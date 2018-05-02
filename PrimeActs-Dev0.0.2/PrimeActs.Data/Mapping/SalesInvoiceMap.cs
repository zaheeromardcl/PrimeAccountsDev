using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class SalesInvoiceMap : EntityTypeConfiguration<PrimeActs.Domain.SalesInvoice>
    {
        public SalesInvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.SalesInvoiceID);

            // Properties
            this.Property(t => t.ServerCode)
                .IsRequired()
                .HasMaxLength(1);

      

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblSalesInvoice");
            this.Property(t => t.SalesInvoiceID).HasColumnName("SalesInvoiceID");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.CustomerDepartmentAddressID).HasColumnName("CustomerDepartmentAddressID");
            this.Property(t => t.ServerCode).HasColumnName("ServerCode");
            this.Property(t => t.SalesInvoiceReference).HasColumnName("SalesInvoiceReference");
            this.Property(t => t.SalesInvoiceDate).HasColumnName("SalesInvoiceDate");
            this.Property(t => t.DivisionAddressID).HasColumnName("DivisionAddressID");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            

            // Relationships
            this.HasOptional(t => t.CustomerDepartmentAddress)
                .WithMany(t => t.CustomerDepartmentAddressSalesInvoices)
                .HasForeignKey(d => d.CustomerDepartmentAddressID);
            this.HasOptional(t => t.DivisionAddress)
                .WithMany(t => t.DivisionAddressSalesInvoices)
                .HasForeignKey(d => d.DivisionAddressID);
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.SalesInvoices)
                .HasForeignKey(d => d.CurrencyID);
            this.HasRequired(t => t.CustomerDepartment)
                .WithMany(t => t.SalesInvoices)
                .HasForeignKey(d => d.CustomerDepartmentID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.SalesInvoices)
                .HasForeignKey(d => d.DivisionID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.SalesInvoices)
                .HasForeignKey(d => d.NoteID);

        }
    }
}
