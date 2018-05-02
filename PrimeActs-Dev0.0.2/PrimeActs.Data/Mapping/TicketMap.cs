using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TicketMap : EntityTypeConfiguration<PrimeActs.Domain.Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketID);

            // Properties
            this.Property(t => t.PONumber)
                .HasMaxLength(20);

            this.Property(t => t.ServerCode)
                .HasMaxLength(1);

            this.Property(t => t.TicketReference)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            this.Property(t => t.String1)
                .HasMaxLength(1000);

            this.Property(t => t.String2)
                .HasMaxLength(1000);

            this.Property(t => t.TicketPrefix)
                .HasMaxLength(1);

            this.Property(t => t.TransferReference)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblTicket");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.PONumber).HasColumnName("PONumber");
            this.Property(t => t.ServerCode).HasColumnName("ServerCode");
            this.Property(t => t.TicketReference).HasColumnName("TicketReference");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.TicketDate).HasColumnName("TicketDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.CurrencyRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsCashSale).HasColumnName("IsCashSale");
            this.Property(t => t.SalesPersonUserID).HasColumnName("SalesPersonUserID");
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
       
            this.Property(t => t.Commission).HasColumnName("Commission");
            this.Property(t => t.Handling).HasColumnName("Handling");
            this.Property(t => t.TicketPrefix).HasColumnName("TicketPrefix");
            
            this.Property(t => t.IsHistory).HasColumnName("IsHistory");
            this.Property(t => t.TransferReference).HasColumnName("TransferReference");

            // Relationships
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.CurrencyID);
            this.HasOptional(t => t.CustomerDepartment)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.CustomerDepartmentID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.DivisionID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.NoteID);

        }
    }
}
