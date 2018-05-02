#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class CustomerEditModel
    {
        public Guid CustomerID { get; set; }
        public string CustomerCompanyName { get; set; }
        public string CustomerCode { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string CreditLimitCash { get; set; }
        public string CreditLimitInvoice { get; set; }
        public string CreditRating { get; set; }
        public bool DisplayStatements { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomerViewModel : CustomerEditModel
    {
        public CustomerEditModel CustomerEditModel { get; set; }
        public List<CustomerEditModel> CustomerEditModels { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<SalesInvoice> SalesInvoices { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Domain.BankAccount> BankAccounts { get; set; }
        public List<SalesLedgerEntry> Statement { get; set; }
        public List<CustomerDepartment> CustomerDepartments { get; set; }
        public List<CustomerLocation> CustomerLocations { get; set;  }
        //calculate Balance from sales ledger entries.
        public decimal Balance { get; set; }

    }
}