using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain.ViewModels;
using SearchObject = PrimeActs.Domain.ViewModels.SearchObject;

namespace PrimeActs.UI.Controllers
{
    public class SupplierController : PrimeActsController // -?!- PrimeActsAuthenticatedController
    {
        private IUnitOfWorkAsync _unitofWork;
        private readonly ISupplierOrchestra _supplierOrchestra;
        private IApplicationUserOrchestra _applicationUserOrchestra;

        public SupplierController(ISupplierOrchestra supplierOrchestra, IUnitOfWorkAsync unitofWork, IApplicationUserOrchestra applicationUserOrchestra)
        {
            _supplierOrchestra = supplierOrchestra;
            _unitofWork = unitofWork;
            _applicationUserOrchestra = applicationUserOrchestra;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public JsonResult AutoComplete(string search)
        {
            var autoCompleteList = new List<Autocomplete>();

            foreach (var supplier in _supplierOrchestra.GetSupplierDeptModelsForAC(search))
            {
                autoCompleteList.Add(new AutoCompleteSupplierDepartment
                {
                    Id = supplier.SupplierDepartmentID.ToString(),
                    label = supplier.SupplierDepartmentName,
                    value = supplier.SupplierDepartmentName,
                    CountryName = supplier.CountryName,
                    CountryID = supplier.CountryID,
                });
            }
            return Json(autoCompleteList, JsonRequestBehavior.AllowGet);
            //autoCompleteList.Add(new Autocomplete { Id = supplier.SupplierID.ToString(), label = supplier.SupplierCompanyName + "-" + supplier.SupplierDepartmentName });
        }

        public ActionResult SupplierHome()
        {
            return View();
        }

        public ActionResult IndexTab(int? id)
        {
            ViewBag.PanelName = string.Format("Supplier{0}", id ?? 0);
            var dateTime = DateTime.Today.AddDays(1);
            var supplierPagingModel = new SupplierPagingModel();
            if (id.HasValue && id.Value > 0)
                supplierPagingModel = _supplierOrchestra.GetSupplierPagingModel(new QueryOptions() { SortOrder = "DESC", SortField = "CreatedDate" },
                    new SearchObject
                    {
                        SupplierCode = "",
                        SupplierCompanyName = "",
                        ToDate = dateTime,
                        FromDate = DateTime.MinValue,
                        RecordsInDays = "CURRENTMONTH"
                    });
            return PartialView("_Suppliers", supplierPagingModel);
        }

        //[PrimeActsAuthorize(OperationKey = "Supplier-SupplierIndexTab")]
        public ActionResult SupplierIndexTab(int? id)
        {
            StringBuilder panelName = new StringBuilder("Supplier");
            if (id.HasValue) panelName.Append(id);
            ViewBag.SuppliersPanel = panelName.ToString();
            return PartialView("_SupplierIndexTab");
        }

        [HttpGet]
        public ActionResult CreateSupplier(int? tabId)
        {
            var panelName = string.Format("CreateSupplier{0}", tabId ?? 0);
            ViewBag.PanelName = panelName;
            return PartialView("_CreateSupplier");
        }

        [HttpGet]
        public ActionResult SupplierEdit(int tabId, Guid id)
        {
            var viewModel = _supplierOrchestra.GetSupplierModelBySupplierID(id);
            ViewBag.PanelName = "UpdateSupplier";
            ViewBag.SupplierPanel = string.Format("SupplierEdit{0}", tabId);
            return PartialView("_UpdateSupplier", viewModel);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult SupplierDetails(int tabId, Guid id) // SupplierDetails = DetailsTab
        {
            var viewModel = _supplierOrchestra.GetSupplierModelBySupplierID(id);
            ViewBag.PanelName = "SupplierDetails"; // -?!- viewModel.SupplierCode/SupplierCmpanyName
            ViewBag.SupplierPanel = string.Format("SupplierDetails{0}", tabId);
            return PartialView("_ShowSupplierDetails", viewModel); // -?!
        }

        public ActionResult Details(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult PurchaseInvoice()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KoTest(int? tabId)
        {
            var panelName = string.Format("KoTest{0}", tabId ?? 0);
            ViewBag.PanelName = panelName;
            return PartialView("_KoTest");
        }

        public ActionResult SupplierSummary(Guid id)
        {
            //_aspNetUserOrchestra.Initialize(ApplicationUser);
            return PartialView(_supplierOrchestra.GetSupplierOnly(id));
        }

        public ActionResult SupplierItemSummary(Guid id)
        {
            return PartialView(_supplierOrchestra.GetSupplierItemsOnly(id));
        }
    }
}
