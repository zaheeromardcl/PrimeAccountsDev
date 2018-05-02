using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class BankStatement : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public BankStatement()
        {

        }

        public System.Guid BankStatementID { get; set; }
        public DateTime? BankStatementImportDate { get; set; }
        //public DateTime? BankStatementStartDate { get; set; }
        //public DateTime? BankStatementEndDate { get; set; }
        public string BankStatementFileName { get; set; }
        public bool BankStatementReconciled { get; set; }
        public Nullable<System.Guid> UpdatedByUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid BankAccountID { get; set; }
        //DB Changes 10/11/2016 --columns added 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string BankStatementDescription { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? CurrentBalance { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}

