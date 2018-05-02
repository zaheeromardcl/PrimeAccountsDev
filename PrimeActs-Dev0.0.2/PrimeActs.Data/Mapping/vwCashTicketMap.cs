using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    public class vwCashTicketMap : EntityTypeConfiguration<PrimeActs.Domain.vwCashTicket>
    {
        public vwCashTicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketID);
            
            // Table & Column Mappings
            this.ToTable("vwCashTicket");
         
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.TicketReference).HasColumnName("TicketReference");
            this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");
            this.Property(t => t.AmountPaid).HasColumnName("AmountPaid");
            this.Property(t => t.BalanceOwed).HasColumnName("BalanceOwed");
            this.Property(t => t.SalesPersonName).HasColumnName("SalesPersonName");
            this.Property(t => t.CreatedDate).HasColumnName("TicketDate");
            this.Property(t => t.TicketTotal).HasColumnName("TicketTotal");
            this.Property(t => t.SalesInvoiceID).HasColumnName("SalesInvoiceID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
        }
    }
}
