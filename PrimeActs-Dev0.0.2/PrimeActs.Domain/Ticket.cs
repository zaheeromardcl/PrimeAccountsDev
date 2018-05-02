using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeActs.Domain.Abstract;
namespace PrimeActs.Domain
{
    public partial class Ticket :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Ticket()
        {
            this.TicketItems = new List<TicketItem>();
        }

        public System.Guid TicketID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public string PONumber { get; set; }
        public string ServerCode { get; set; }
        public string TicketReference { get; set; }
        public Nullable<System.Guid> CustomerDepartmentID { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        
        public System.DateTime TicketDate { get; set; }
        public Nullable<System.Guid> CurrencyID { get; set; }
        public Nullable<decimal> CurrencyRate { get; set; }
        public Nullable<bool> IsCashSale { get; set; }
        public Nullable<System.Guid> SalesPersonUserID { get; set; }
        [NotMapped]
        public string SalesPersonName { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public Nullable<System.Guid> FK1 { get; set; }
        public Nullable<System.Guid> FK2 { get; set; }
        public Nullable<bool> Bit1 { get; set; }
        public Nullable<bool> Bit2 { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public Nullable<decimal> Numeric1 { get; set; }
        public Nullable<decimal> Numeric2 { get; set; }
        public Nullable<int> Int1 { get; set; }
        public Nullable<int> Int2 { get; set; }
        public decimal Commission { get; set; }
        public decimal Handling { get; set; }
        public string TicketPrefix { get; set; }
        
        public bool IsHistory { get; set; }
        public string TransferReference { get; set; }
        public virtual Currency Currency { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        public virtual CustomerDepartment CustomerDepartment { get; set; }
        public virtual Division Division { get; set; }
        public virtual Note Note { get; set; }
        public virtual ICollection<TicketItem> TicketItems { get; set; }
    }
}
