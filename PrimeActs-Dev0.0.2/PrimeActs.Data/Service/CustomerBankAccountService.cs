#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{

    public class CustomerBankAccountService : Service<CustomerBankAccount>, ICustomerBankAccountService
    {
        private readonly ICache _cache;
        private readonly object _lockboject = new object();
        private readonly IRepositoryAsync<CustomerBankAccount> _repository;
        private readonly string _type;


        public CustomerBankAccountService(IRepositoryAsync<CustomerBankAccount> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            _type = typeof(CustomerBankAccount).FullName;
        }

   
        public CustomerBankAccount GetCustomerBankAccount(Guid CustomerBankAccountID)
        {
            CheckCache();
            var data = ((IEnumerable<CustomerBankAccount>)_cache.Get(_type)).Where(t => t.CustomerBankAccountID == CustomerBankAccountID);
            return data.FirstOrDefault();
        }

        public List<BankAccount> GetAllCustomerBankAccounts(Guid CustomerDepartmentID)
        {
            var ListBankAccounts = _repository.Query().Select(y => y.BankAccount).ToList();
            //var ListBankAccounts = _repository.Query().Select(y=>y.BankAccount).Select(entityType => new BankAccount
            //        {
            //            BankAccountID = entityType.BankAccountID,
            //            AccountNumber = entityType.AccountNumber,
            //            SortCode1 = entityType.SortCode1,
            //            SortCode2 = entityType.SortCode2,
            //            SortCode3 = entityType.SortCode3
            //        }).Select(z=>z.BankAccount).ToList();
            return ListBankAccounts;
        }

        public void RefreshCache()
        {
            _cache.Remove(_type);
        }

        private void CheckCache()
        {
            if (!_cache.Exists(_type))
            {
                lock (_lockboject)
                {
                    //var customerBankAccounts = _repository.Query().Include(inc => inc.Customer).Select().Select(entityType => new CustomerBankAccount
                    //{
                    //    CustomerDepartmentID = entityType.CustomerDepartmentID,
                    //    CustomerDepartmentName = entityType.CustomerDepartmentName,
                    //    CustomerID = entityType.CustomerID
                    //}).ToList();
                    //_cache.Set(_type, customerDepartments);
                }
            }
        }
    }
}