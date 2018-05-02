#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ICustomerBankAccountService : IService<CustomerBankAccount>
    {
        //CustomerDepartment DepartmentByName(string DepartmentName);
        CustomerBankAccount GetCustomerBankAccount(Guid CustomerBankAccountID);
        List<BankAccount> GetAllCustomerBankAccounts(Guid CustomerDepartmentID);
       
        void RefreshCache();
    }
}