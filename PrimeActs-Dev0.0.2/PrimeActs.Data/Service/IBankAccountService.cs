#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.BankAccount;

#endregion
//TODO: amend to build bank account service.
namespace PrimeActs.Data.Service
{
    public interface IBankAccountService : IService<BankAccount>
    {

        //List<BankAccount> GetAllBankAccounts();
        List<BankAccount> GetAllCustomerDepartmentBankAccounts(Guid CustomerDepartmentID);
       //void  RefreshCache();
       //void CheckCache();
        List<BankAccount> GetBankAccounts(Domain.ViewModels.QueryOptions queryOptions, SearchObject searchObject,
            out int totalCount);
        List<BankAccount> BankAccountBySupplierId(Guid Id);
    }
}