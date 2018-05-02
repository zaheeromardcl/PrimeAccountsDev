using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Customer : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Customer()
        {

            this.ChildCustomers = new List<Customer>();
            this.CustomerBankAccounts = new List<CustomerBankAccount>();
            this.CustomerContacts = new List<CustomerContact>();
            this.CustomerCurrencies = new List<CustomerCurrency>();
            this.CustomerDepartments = new List<CustomerDepartment>();
            this.CustomerLocations = new List<CustomerLocation>();
        }

        public System.Guid CustomerID { get; set; }
        public Nullable<System.Guid> ParentCustomerID { get; set; }
        public string CustomerCompanyName { get; set; }
        public string CustomerCode { get; set; }
        public Nullable<decimal> CreditLimitCash { get; set; }
        public Nullable<decimal> CreditLimitInvoice { get; set; }
        public Nullable<System.Guid> CreditRating { get; set; }
        
        public Nullable<bool> Statements { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        public int IsTransfer { get; set; }
        public Nullable<System.Guid> CompanyID { get; set; }

        public virtual Note Note { get; set; }
        public virtual Company Company { get; set; }
        public virtual Customer ParentCustomer { get; set; }
        //public virtual Customer Customer2 { get; set; }
        public virtual List<Customer> ChildCustomers { get; set; }
        public virtual List<CustomerDepartment> CustomerDepartments { get; set; }
        public virtual List<CustomerLocation> CustomerLocations { get; set; }
        public virtual List<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<CustomerCurrency> CustomerCurrencies { get; set; }
        public virtual ICollection<CustomerBankAccount> CustomerBankAccounts { get; set; }
    }
}
