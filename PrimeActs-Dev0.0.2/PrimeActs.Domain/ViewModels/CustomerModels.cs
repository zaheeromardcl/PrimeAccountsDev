using System;
using System.Collections.Generic;
using PrimeActs.Domain.ViewModels.Customer;

namespace PrimeActs.Domain.ViewModels
{
    public class CustomerEditModel
    {
        public CustomerEditModel()
        {
            CustomerItems = new List<CustomerItemEditModel>();
        }

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
        public bool? IsActive { get; set; }
        public Guid? NoteID { get; set; }
        public string Notes { get; set; }
        public string NoteDescription { get; set; }
        public Guid? ParentCustomerID { get; set; }
        public string ParentCustomerName { get; set; }
        public int? IsTransfer { get; set; }
        public bool? Statements { get; set; }
        public List<CustomerLocationModel> CustomerLocations { get; set; }
        public List<CustomerDepartmentEditModel> CustomerDepartments { get; set; }
        public List<CustomerContactModel> CustomerContacts { get; set; }
        public List<CustomerItemEditModel> CustomerItems { get; set; }
    }


    public class CustomerPagingModel
    {
        public ResultList<CustomerEditModel> CustomerEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class CustomerItemPagingModel
    {
        public ResultList<CustomerItemEditModel> CustomerItemEditModels { get; set; }
    }

    public class CustomerItemEditModel
    {
        public Guid CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ParentCustomerID { get; set; }
        public string IsTransfer { get; set; }
        public string Statements { get; set; }
        public string CompanyID { get; set; }
        public string NoteID { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDirty { get; set; }
    }

    // --- !!! --- the class below is very messy --- !!! ----
    // --- !!! --- I am not going to use it --- !!! ----
    // --- !!! --- and I am avoiding it --- !!! ----
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
        public List<CustomerLocation> CustomerLocations { get; set; }
        //calculate Balance from sales ledger entries.
        public decimal Balance { get; set; }

    }
}