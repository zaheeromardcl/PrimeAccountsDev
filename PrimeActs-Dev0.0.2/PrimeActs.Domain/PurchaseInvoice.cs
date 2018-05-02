using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class PurchaseInvoice : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public PurchaseInvoice()
        {


            this.PurchaseInvoiceItems = new List<PurchaseInvoiceItem>();
        }

        public System.Guid PurchaseInvoiceID { get; set; }
        public System.Guid SupplierDepartmentID { get; set; }
        public System.Guid AddressID { get; set; }
        public string PurchaseInvoiceReference { get; set; }
        public string ServerCode { get; set; }
        //public System.Guid PurchaseLedgerEntryID { get; set; }
        public System.DateTime PurchaseInvoiceDate { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActivated { get; set; }


        public int? Status { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
//        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Total { get; set; }
        
        
        public virtual Address Address { get; set; }

        public virtual Note Note { get; set; }

        //public virtual PurchaseLedgerEntry PurchaseLedgerEntry { get; set; }
        public virtual SupplierDepartment SupplierDepartment { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PurchaseInvoiceID")]
        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
 

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
