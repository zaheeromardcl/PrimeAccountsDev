using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class BankStatementItem : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
         public BankStatementItem()
         {
             
         }

        public System.Guid BankStatementItemID { get; set; }
        public DateTime BankStatementDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public Nullable<System.Guid> UpdatedByUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsReconciled { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string TransactionType { get; set; }
        public System.Guid BankStatementID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
