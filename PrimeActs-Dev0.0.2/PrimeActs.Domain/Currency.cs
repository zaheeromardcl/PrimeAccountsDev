using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Currency : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Currency()
        {
         
            this.CustomerCurrencies = new List<CustomerCurrency>();
  
           // this.PurchaseInvoiceItems = new List<PurchaseInvoiceItem>();
         //   this.PurchaseLedgerEntries = new List<PurchaseLedgerEntry>();
       
            this.SalesInvoices = new List<SalesInvoice>();
            this.SalesLedgerEntries = new List<SalesLedgerEntry>();
            this.Tickets = new List<Ticket>();
        }

        public System.Guid CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public Nullable<decimal> DefaultExchangeRate { get; set; }
        public System.Guid CompanyID { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<CustomerCurrency> CustomerCurrencies { get; set; }

        //public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
      //  public virtual ICollection<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
     
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
