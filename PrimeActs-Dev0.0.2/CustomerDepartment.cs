using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class CustomerDepartment : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public CustomerDepartment()
        {
          
            this.CustomerBankAccounts = new List<CustomerBankAccount>();
            this.CustomerDepartmentLocations = new List<CustomerDepartmentLocation>();
            this.SalesInvoices = new List<SalesInvoice>();
            this.SalesLedgerEntries = new List<SalesLedgerEntry>();
            this.Tickets = new List<Ticket>();
            this.Contacts = new List<Contact>();
        }

        public System.Guid CustomerDepartmentID { get; set; }
        public System.Guid CustomerID { get; set; }
        public string CustomerDepartmentName { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> CreditTerms { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public Nullable<decimal> Handling { get; set; }
        public string FactorRef { get; set; }
        public Nullable<byte> EDIType { get; set; }
        public string EDINumber { get; set; }
        public string EDIIdent { get; set; }
        public System.Guid InvoiceCustomerLocationID { get; set; }
        public byte InvoiceFrequency { get; set; }
        public string InvoiceEmailAddress { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public Nullable<System.Guid> SalesPersonUserID { get; set; }
        public Nullable<System.Guid> CustomerTypeID { get; set; }
        public byte RebateType { get; set; }
        public Guid? RebateCustomerDepartmentID { get; set; }
        public decimal? RebateRate { get; set; }

        //public virtual AspNetUser AspNetUser { get; set; }
      
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual Note Note { get; set; }
        public virtual CustomerDepartment RebateCustomerDepartment { get; set; }
        public virtual ICollection<CustomerDepartmentLocation> CustomerDepartmentLocations { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }

        public ICollection<CustomerContactDepartment> CustomerContactDepartments { get; set; }
    }
}
