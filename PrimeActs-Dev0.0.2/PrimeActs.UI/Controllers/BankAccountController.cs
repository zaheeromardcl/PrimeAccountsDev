#region

using System.Collections.Generic;
using System.Web.Mvc;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class BankAccountController : PrimeActsController
    {
        private readonly IBankAccountOrchestra _bankAccountOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;

        public BankAccountController(IBankAccountOrchestra BankAccountOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _bankAccountOrchestra = BankAccountOrchestra;
            _unitofWork = unitofWork;
        }

        // GET: Consigment
        public ActionResult Index(int page = 1, int pageSize = 10, string searchString = "")
        {
            return
                View(_bankAccountOrchestra.GetBankAccountPagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.BankAccount.SearchObject
                    {
                        ToDate = null,
                        AccountName = "",
                        FromDate = null,
                        RecordsInDays = "LASTMONTH"
                    }));
        }

        public ActionResult IndexTab()
        {
            return
                PartialView("_BankAccount", _bankAccountOrchestra.GetBankAccountPagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.BankAccount.SearchObject
                    {
                        ToDate = null,
                        AccountName = "",
                        FromDate = null,
                        SupplierDepartmentId = "0",
                        RecordsInDays = "LASTMONTH"
                    }));
        }
    }
}