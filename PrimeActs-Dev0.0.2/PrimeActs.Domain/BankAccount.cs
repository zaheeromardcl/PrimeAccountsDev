using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class BankAccount : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public BankAccount()
        {
            
            this.CustomerBankAccounts = new List<CustomerBankAccount>();
            this.NominalAccounts = new List<NominalAccount>();
            this.SupplierBankAccounts = new List<SupplierBankAccount>();
        }

        public System.Guid BankAccountID { get; set; }
        public string AccountName { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
        public System.Guid CountryID { get; set;  }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        
        public virtual ICollection<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public virtual ICollection<NominalAccount> NominalAccounts { get; set; }
        public virtual ICollection<SupplierBankAccount> SupplierBankAccounts { get; set; }


    }
}
