using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Orchestras;

namespace PrimeActs.UI.Controllers
{
    public class InvoiceAdminController : Controller
    {
        private readonly IPurchaseInvoiceOrchestra _purchaseInvoiceOrchestra;

        public InvoiceAdminController(IPurchaseInvoiceOrchestra PurchaseInvoiceOrchestra)
        {
            _purchaseInvoiceOrchestra = PurchaseInvoiceOrchestra;
        }

        // GET: InvoiceAdmin
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: InvoiceAdmin
        [HttpGet]
        [PrimeActsAuthorize(OperationKey = "InvoiceAdmin-IndexTab")]
        public ActionResult IndexTab(int id)
        {
            ViewBag.PanelName = string.Format("InvoiceAdmin{0}", id.ToString());
            var result =
                _purchaseInvoiceOrchestra.GetPurchaseInvoicePagingModel(new QueryOptions()
                {
                    SortField = "CreatedDate",
                    SortOrder = Domain.ViewModels.SortOrder.DESC.ToString()
                },
                    new PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject
                    {
                        ToDate = null,
                        PurchaseInvoiceReference = "",
                        FromDate = null,
                        SupplierDepartmentId = "",
                        RecordsInDays = "LASTMONTH"
                    });
            return
                PartialView("_Report", result);
        }
        
        public ActionResult Login(int id)
        {
            ViewBag.PanelName = string.Format("InvoiceAdminLogin{0}", id.ToString());
            return
                PartialView("_Login");
        }

        // GET: InvoiceAdmin

        public ActionResult Report()
        {
            ViewBag.PanelName = "test";
            return
                View("Report", _purchaseInvoiceOrchestra.GetPurchaseInvoicePagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject
                    {
                        ToDate = null,
                        PurchaseInvoiceReference = "",
                        FromDate = null,
                        SupplierDepartmentId = "",
                        RecordsInDays = "LASTMONTH"
                    }));
        }

        // GET: InvoiceAdmin
        public ActionResult InvoiceDetail()
        {
            return View();
        }

        // GET: InvoiceAdmin
        public ActionResult ApproveConfirm()
        {
            return View();
        }

        // GET: InvoiceAdmin
        public ActionResult RejectConfirm()
        {
            return View();
        }
    }
}