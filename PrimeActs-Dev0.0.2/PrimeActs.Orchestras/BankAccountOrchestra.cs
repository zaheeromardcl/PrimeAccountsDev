#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.BankAccount;
using SearchObject = PrimeActs.Domain.ViewModels.BankAccount.SearchObject;

#endregion

namespace PrimeActs.Orchestras
{
    public class BankAccountOrchestra : IBankAccountOrchestra
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ICustomerBankAccountService _customerBankAccountService;

        public BankAccountOrchestra(IBankAccountService bankAccountService, ICustomerBankAccountService customerBankAccountService)
        {
            _bankAccountService = bankAccountService;
            _customerBankAccountService = customerBankAccountService;
        }

        //To do: add Get GetGetAllCustomerDepartmentBankAccounts
        public List<BankAccount> GetAllCustomerDepartmentBankAccounts(Guid CustomerDepartmentID)
        {
            var BankAccounts = _bankAccountService.GetAllCustomerDepartmentBankAccounts(CustomerDepartmentID);
            return BankAccounts;
        }

        public ResultList<BankAccountModel> GetBankAccountModels(QueryOptions queryOptions, SearchObject
            searchObject)
        {
            var totalCount = 0;
            var list = _bankAccountService.GetBankAccounts(queryOptions, searchObject, out totalCount);

            return
                new ResultList<BankAccountModel>(
                    list != null ? list.Select(BuildBankAccountModel).ToList() : null, queryOptions);
        }

        public BankAccountPagingModel GetBankAccountPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var bankAccountPagingModel = new BankAccountPagingModel();
            var bankAccounts = _bankAccountService.GetBankAccounts(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            //This line gets rid of items!! Fix the error
            var result = new ResultList<BankAccountModel>(bankAccounts.Select(BuildBankAccountModel).ToList(),
                queryOptions);
            bankAccountPagingModel.BankAccountEditModels = result;
            bankAccountPagingModel.SearchObject = new PrimeActs.Domain.ViewModels.BankAccount.SearchObject
            {
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null
            };
            return bankAccountPagingModel;
        }

        private BankAccountModel BuildBankAccountModel(BankAccount entity)
        {
            return new BankAccountModel()
            {
                BankAccountID = entity.BankAccountID,
                AccountName = entity.AccountName,
                BankCode = entity.BankCode.ToString(),
                CountryID = entity.CountryID,
                AccountNumber = entity.AccountNumber.ToString()
            };
        }
    }
}
