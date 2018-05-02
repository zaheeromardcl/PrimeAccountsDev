using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;
namespace PrimeActs.Domain
{
    public partial class TransactionTaxCode : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public TransactionTaxCode()
        {
            
            this.Produces = new List<Produce>();
            this.TicketItems = new List<TicketItem>();
            this.TransactionTaxRates = new List<TransactionTaxRate>();
        }

        public System.Guid TransactionTaxCodeID { get; set; }
        public string TransactionTaxCodeValue { get; set; }
        public string TransactionTaxCodeDescription { get; set; }
        public System.Guid CompanyID { get; set; }
        
        public virtual Company Company { get; set; }
        public virtual ICollection<Produce> Produces { get; set; }
        public virtual ICollection<TicketItem> TicketItems { get; set; }
        public virtual ICollection<TransactionTaxRate> TransactionTaxRates { get; set; }

       
    }
}
