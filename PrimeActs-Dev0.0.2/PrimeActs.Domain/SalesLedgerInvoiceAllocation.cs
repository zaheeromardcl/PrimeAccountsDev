using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class SalesLedgerInvoiceAllocation :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {

        public System.Guid SalesLedgerInvoiceAllocationID { get; set; }
        public System.Guid SalesLedgerEntryID { get; set; }
        public SalesLedgerEntry SalesLedgerEntry { get; set; }

        public System.Guid SalesInvoiceID { get; set; }
        public decimal SaleAmount { get; set; }

        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public SalesInvoice SalesInvoice { get; set; }
    }
}
