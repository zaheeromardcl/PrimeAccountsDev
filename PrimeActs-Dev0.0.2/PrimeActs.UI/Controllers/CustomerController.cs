using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

namespace PrimeActs.UI.Controllers
{
    public class CustomerController : PrimeActsController
    {
        private readonly ICustomerOrchestra _customerOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerController(ICustomerOrchestra customerOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _customerOrchestra = customerOrchestra;
            _unitofWork = unitofWork;
        }

        public IEnumerable<Autocomplete> GetAllCustomer(string search)
        {
            return
                _customerOrchestra.GetCustomerDepartmentModelsForAC(search)
                    .Select(
                        inc =>
                            new Autocomplete
                            {
                                Id = inc.CustomerDepartmentID.ToString(),
                                value = inc.CustomerDepartmentID.ToString(),
                                label = inc.CustomerDepartmentName,

                            });
        }

        public ActionResult CustomerHome()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IndexTab(int? id)
        {
            ViewBag.PanelName = string.Format("Customer{0}", id ?? 0);
            var dateTime = DateTime.Today.AddDays(1);
            var customerPagingModel = new CustomerPagingModel();
            if (id.HasValue && id.Value > 0)
                customerPagingModel = _customerOrchestra.GetCustomerPagingModel(new QueryOptions() { SortOrder = "DESC", SortField = "CreatedDate" },
                    new SearchObject
                    {
                        CustomerCode = "",
                        CustomerCompanyName = "",
                        ToDate = dateTime,
                        FromDate = DateTime.MinValue,
                        RecordsInDays = "CURRENTMONTH"
                    });
            return PartialView("_Customers", customerPagingModel);
        }

        //[PrimeActsAuthorize(OperationKey = "Customer-CustomerIndexTab")]
        public ActionResult CustomerIndexTab(int? id)
        {
            StringBuilder panelName = new StringBuilder("Customer");
            if (id.HasValue) panelName.Append(id);
            ViewBag.CustomersPanel = panelName.ToString();
            return PartialView("_CustomerIndexTab");
        }

        [HttpGet]
        public ActionResult CreateCustomer(int? tabId)
        {
            var panelName = string.Format("CreateCustomer{0}", tabId ?? 0);
            ViewBag.PanelName = panelName;
            return PartialView("_CreateCustomer");
        }

        [HttpGet]
        public ActionResult CustomerEdit(int tabId, Guid id)
        {
            var viewModel = _customerOrchestra.GetCustomerModelByCustomerID(id);
            ViewBag.PanelName = "UpdateSupplier";
            ViewBag.SupplierPanel = string.Format("SupplierEdit{0}", tabId);
            return PartialView("_UpdateCustomer", viewModel);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult CustomerDetails(int tabId, Guid id)
        {
            var viewModel = _customerOrchestra.GetCustomerModelByCustomerID(id);
            ViewBag.PanelName = "CustomerDetails";
            ViewBag.SupplierPanel = string.Format("CustomerDetails{0}", tabId);
            return PartialView("_ShowCustomerDetails", viewModel); // -?!
        }

        public ActionResult CustomerLocationDetails()
        {
            return View();
        }

        public ActionResult CustomerDepartmentDetails()
        {
            return View();
        }

        public ActionResult DetailsTab(int? tabId) //, string customerName
        {
            string path = "/Customer/DetailsTab/";
            path = tabId.HasValue ? path + tabId + "/" : path;
            var reqCurExecFilePath = Request.CurrentExecutionFilePath;
            var customerName = reqCurExecFilePath.Substring(path.Length);
            ViewBag.CustomerName = customerName;
            var panelName = string.Format("Customer{0}", tabId ?? 0);
            ViewBag.PanelName = panelName;
            return PartialView("_CustomerHome");
        }
    }
}