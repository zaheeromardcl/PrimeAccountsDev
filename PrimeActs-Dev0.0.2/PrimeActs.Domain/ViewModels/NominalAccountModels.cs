using System;
using System.Collections.Generic;

namespace PrimeActs.Domain.ViewModels
{
    public class NominalAccountModel
    {
        public Guid NominalAccountID { get; set; }
        public string NominalAccountName { get; set; }
	    public string NominalCode { get; set; }
	    public bool IsPandL { get; set; }
	    public bool IsBroughtForward { get; set; }
	    public bool IsCurrent { get; set; }
	    public Guid BankAccountID { get; set; }
	    public bool IsPettyCashAccount { get; set; }
	    public bool IsSystem { get; set; }
	    public string UpdatedDate { get; set; }
	    public string CreatedDate { get; set; }
	    public Guid CompanyID { get; set; }
	    public bool IsActive { get; set; }
	    public Guid CreatedByUserID { get; set; }
        public Guid UpdatedByUserID { get; set; }
    }
}
