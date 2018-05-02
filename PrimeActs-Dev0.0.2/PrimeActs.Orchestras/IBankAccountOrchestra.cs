#region
using PrimeActs.Domain;
using System.Collections.Generic;
using PrimeActs.Domain.ViewModels;
using System;
using PrimeActs.Domain.ViewModels.BankAccount;
using SearchObject = PrimeActs.Domain.ViewModels.BankAccount.SearchObject;

#endregion
//TODO: amend to make into bank account orchestra
namespace PrimeActs.Orchestras
{
    public interface IBankAccountOrchestra
    {
        List<BankAccount> GetAllCustomerDepartmentBankAccounts(Guid CustomerDepartmentID);

        ResultList<BankAccountModel> GetBankAccountModels(QueryOptions queryOptions, SearchObject
            searchObject);

        BankAccountPagingModel GetBankAccountPagingModel(QueryOptions queryOptions, SearchObject searchObject);
    }
}