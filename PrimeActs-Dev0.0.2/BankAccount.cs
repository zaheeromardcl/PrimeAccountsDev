using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class BankAccount : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public BankAccount()
        {
            
            this.CustomerBankAccounts = new List<CustomerBankAccount>();
            this.NominalAccounts = new List<NominalAccount>();
            this.SupplierBankAccounts = new List<SupplierBankAccount>();
        }

        public System.Guid BankAccountID { get; set; }
        public string AccountName { get; set; }
        public Nullable<byte> SortCode1 { get; set; }
        public Nullable<byte> SortCode2 { get; set; }
        public Nullable<byte> SortCode3 { get; set; }
        public Nullable<int> AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
        
        public virtual ICollection<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public virtual ICollection<NominalAccount> NominalAccounts { get; set; }
        public virtual ICollection<SupplierBankAccount> SupplierBankAccounts { get; set; }
    }
}
