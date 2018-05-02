#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.BankAccount;

//using SearchObject = PrimeActs.Domain.ViewModels.BankAccount;

#endregion
//API Controller - to get list of customer bank accounts on click of show/hide panel.

namespace PrimeActs.UI.Controllers.API
{
    public class BankAccountController : ApiController
    {
        private readonly IBankAccountOrchestra _bankAccountOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;

        public BankAccountController(IBankAccountOrchestra bankAccountOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _bankAccountOrchestra = bankAccountOrchestra;
         
            _unitofWork = unitofWork;
        }


        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<BankAccount> GetAllCustomerDepartmentBankAccounts(Guid CustomerDepartmentID)
        {
            
            var bankAccounts = _bankAccountOrchestra.GetAllCustomerDepartmentBankAccounts(CustomerDepartmentID);
            return bankAccounts;
            
        }

        [HttpGet]
        public ResultList<BankAccountModel> Index([FromUri] QueryOptions queryOptions,
            [FromUri] PrimeActs.Domain.ViewModels.BankAccount.SearchObject searchObject)
        {
            if (string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "LASTMONTH";
            return _bankAccountOrchestra.GetBankAccountModels(queryOptions, searchObject);
        }
       

    }
}