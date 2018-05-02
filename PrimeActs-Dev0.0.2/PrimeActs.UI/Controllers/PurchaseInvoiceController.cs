using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;

namespace PrimeActs.UI.Controllers
{
    public class PurchaseInvoiceController : PrimeActsController
    {
        private string _serverCode = "L";//Need to change with actual at runtime.
        private readonly IPurchaseInvoiceOrchestra _purchaseInvoiceOrchestra;
        private IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly ISetupLocalService _setupService;
        private readonly IPurchaseChargeTypeOrchestra _purchaseChargeTypeOrchestra;

        public PurchaseInvoiceController(IPurchaseInvoiceOrchestra purchaseInvoiceOrchestra, IUnitOfWorkAsync unitofWork, ISetupLocalService setupLocalService, IApplicationUserOrchestra applicationUserOrchestra, IPurchaseChargeTypeOrchestra purchaseChargetypeOrchestra)
        {
            _purchaseInvoiceOrchestra = purchaseInvoiceOrchestra;
            _applicationUserOrchestra = applicationUserOrchestra;
            _unitofWork = unitofWork;
            _setupService = setupLocalService;
            _purchaseChargeTypeOrchestra = purchaseChargetypeOrchestra;
        }

        // GET: Consigment
        public ActionResult Index(int page = 1, int pageSize = 10, string searchString = "")
        {
            return
                View(_purchaseInvoiceOrchestra.GetPurchaseInvoicePagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject
                    {
                        ToDate = null,
                        PurchaseInvoiceReference = "",
                        FromDate = null,
                        RecordsInDays = "LASTMONTH"
                    }));
        }

        public ActionResult IndexTab()
        {
            return
                PartialView("_PurchaseInvoice", _purchaseInvoiceOrchestra.GetPurchaseInvoicePagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject
                    {
                        ToDate = null,
                        PurchaseInvoiceReference = "",
                        FromDate = null,
                        SupplierDepartmentId = "0",
                        RecordsInDays = "LASTMONTH"
                    }));
        }

        public ActionResult PurchaseInvoiceItemConsignment()
        {
            return
                PartialView("_CreatePurchaseInvoiceItemConsignment");
        }

        public ActionResult PurchaseInvoiceItemSundry()
        {
            return
                PartialView("_CreatePurchaseInvoiceItemSundry");
        }

        [PrimeActsAuthorize(OperationKey = "PurchaseInvoice-CreateTab")]
        public ActionResult CreateTab(int id)
        {
            ViewBag.PanelName = string.Format("CreatePurchaseInvoice{0}", id.ToString());
            ViewBag.UploadFolder = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            var maxFileSize = _setupService.GetDisplayOption("MaxFileSizeToUpload").SetupValueInt;
            var maxNrOfFiles = _setupService.GetDisplayOption("MaxNrOfFilesToUpload").SetupValueInt;
            var mainFolder = _setupService.GetDisplayOption("UploadFolderPath").SetupValueNvarchar;
            ViewBag.MaxFileSize = maxFileSize;
            ViewBag.MaxNrOfFiles = maxNrOfFiles;
            ViewBag.MainFolder = mainFolder;
            var acceptedFileTypes = _setupService.GetDisplayOption("AllowedFileTypesToUpload").SetupValueNvarchar;
            ViewBag.AcceptedFileTypes = acceptedFileTypes;
            var purchaseChargeTypes = _purchaseChargeTypeOrchestra.GetPurchaseChargeTypeModelsForAC();

            PurchaseInvoiceViewModel model = new PurchaseInvoiceViewModel
            {
                PurchaseInvoiceDate = DateTime.Now.ToString(),
                UserContextModel =
                    _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                        "PurchaseInvoice"),
                PurchaseChargeTypes = purchaseChargeTypes
            };

            return PartialView("_CreatePurchaseInvoice", model);
        }

        [HttpGet]
        public ActionResult DetailsTab(int tabId, Guid id)
        {
            var panelName = string.Format("PurchaseInvoiceDetails{0}", tabId);
            ViewBag.ProducePanel = panelName;
            return PartialView("_Details", _purchaseInvoiceOrchestra.GetPurchaseInvoiceDetailsViewModel(id));
        }
    }
}