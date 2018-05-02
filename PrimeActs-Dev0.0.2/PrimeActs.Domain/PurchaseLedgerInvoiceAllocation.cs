using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public partial class PurchaseLedgerInvoiceAllocation : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseLedgerInvoiceAllocation()
        {
           
            
            //this.PurchaseInvoices = new List<PurchaseInvoice>();
        }

        public System.Guid PurchaseLedgerInvoiceAllocationID { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public System.Guid SalesInvoiceID { get; set; }
        public SalesInvoice SalesInvoice { get; set; }

        //public System.Guid LedgerEntryTypeID { get; set; }
        //public string PurchaseLedgerEntryDescription { get; set; }
        //public decimal PurchaseAmount { get; set; }
        //public double FXPurchaseAmount { get; set; }
        //public Nullable<System.Guid> CurrencyID { get; set; }
        //public Nullable<decimal> ExchangeRate { get; set; }
        //public System.Guid PurchaseInvoiceID { get; set; }
        //public System.Guid BatchNumberLogID { get; set; }
        //public System.Guid SupplierDepartmentID { get; set; }
        //public Nullable<System.Guid> NoteID { get; set; }
        //                                public bool IsHistory { get; set; }
        //public int AccountingYear { get; set; }
        //public byte AccountingPeriod { get; set; }
        //public Nullable<bool> IsActive { get; set; }
        
        //public virtual BatchNumberLog BatchNumberLog { get; set; }
        //public virtual Currency Currency { get; set; }
        //public virtual LedgerEntryType LedgerEntryType { get; set; }
        //public virtual Note Note { get; set; }
        //public virtual ICollection<PurchaseAllocation> PurchaseAllocations { get; set; }
        //public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        //public virtual SupplierDepartment SupplierDepartment { get; set; }
    }
}
