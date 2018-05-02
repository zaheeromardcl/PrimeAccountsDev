using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class SalesInvoice : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public SalesInvoice()
        {
      
            this.SalesInvoiceItems = new List<SalesInvoiceItem>();
            this.SalesLedgerInvoiceAllocations = new List<SalesLedgerInvoiceAllocation>();
        }

        public System.Guid SalesInvoiceID { get; set; }
        public System.Guid CustomerDepartmentID { get; set; }
        public Nullable<System.Guid> CustomerDepartmentAddressID { get; set; }
        public string ServerCode { get; set; }
        public string SalesInvoiceReference { get; set; }
        public System.DateTime SalesInvoiceDate { get; set; }
        
        public Nullable<System.Guid> DivisionAddressID { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
       
        public virtual Address DivisionAddress { get; set; }
        public virtual Address CustomerDepartmentAddress { get; set; }
         public virtual Currency Currency { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual Division Division { get; set; }
        public virtual Note Note { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
       // public virtual SalesLedgerEntry SalesLedgerEntry { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public ICollection<SalesLedgerInvoiceAllocation> SalesLedgerInvoiceAllocations { get; set; }
    }
}
