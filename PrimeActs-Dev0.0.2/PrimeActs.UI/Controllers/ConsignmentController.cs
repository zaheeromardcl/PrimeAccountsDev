#region

using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;

using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;
using System.Text;
using Microsoft.AspNet.Identity;
using PrimeActs.Data.Service;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class ConsignmentController : PrimeActsAuthenticatedController
    {
        private readonly IConsignmentOrchestra _consignmentOrchestra;
        private IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly IPurchaseTypeOrchestra _purchaseTypeOrchestra;
        private const string VEHICLE_DEFAULT = "default";
        private const string ORDER_FILTER = "ORD";
        private readonly ISetupLocalService _setupService;
        private string _serverCode = "L";//Need to change with actual at runtime.


        public ConsignmentController(IConsignmentOrchestra consignmentOrchestra, IUnitOfWorkAsync unitofWork, IPurchaseTypeOrchestra purchasetypeOrchestra, IApplicationUserOrchestra applicationUserOrchestra, ISetupLocalService setupLocalService)
        {
            _consignmentOrchestra = consignmentOrchestra;
            _unitofWork = unitofWork;
            _purchaseTypeOrchestra = purchasetypeOrchestra;
            _applicationUserOrchestra = applicationUserOrchestra;
            _setupService = setupLocalService;
        }

        // GET: Consigment
        public ActionResult Index(int page = 1, int pageSize = 10, string searchString = "")
        {
            return
                View(_consignmentOrchestra.GetConsignmentPagingModel(new QueryOptions(),
                    new SearchObject
                    {
                        ToDate = null,
                        ConsignmentReference = "",
                        FromDate = null,
                        SupplierCode = "",
                        SupplierName = "",
                        RecordsInDays = "CURRENTMONTH"
                    }));
        }

        public ActionResult IndexTab(int forIndex = 0)
        {
            if (forIndex == 0)
            {
                return
                    PartialView("_Consignments", _consignmentOrchestra.GetConsignmentPagingModel(new QueryOptions(),
                        new SearchObject
                        {
                            ToDate = null,
                            ConsignmentReference = "",
                            FromDate = null,
                            SupplierDepartmentId = "0",
                            SupplierCode = "",
                            SupplierName = "",
                            RecordsInDays = "LASTMONTH"
                        }));
            }
            else
            {
                var dateTime = DateTime.Today.AddDays(1);
                return
                    PartialView("_Consignments", _consignmentOrchestra.GetConsignmentPagingModel(new QueryOptions() { SortOrder = "DESC", SortField = "CreatedDate" },
                        new SearchObject
                        {
                            ToDate = dateTime,
                            ConsignmentReference = "",
                            FromDate = DateTime.MinValue,
                            SupplierDepartmentId = "",
                            SupplierCode = "",
                            SupplierName = "",
                            RecordsInDays = "LASTMONTH"
                        }));
            }
        }

        public ActionResult CompletedConsignments()
        {
            var dateTime = DateTime.Today.AddDays(1);
            
            return
                PartialView("_CompletedConsignments", _consignmentOrchestra.GetConsignmentPagingModelSimplified(new QueryOptions() { SortOrder = "DESC", SortField = "CreatedDate" },
                    new SearchObject
                    {
                        ToDate = dateTime,
                        ConsignmentReference = "",
                        FromDate = DateTime.MinValue,
                        SupplierDepartmentId = "",
                        SupplierCode = "",
                        SupplierName = "",
                        RecordsInDays = "LASTWEEK",
                        CompletedConsignmentsOnly = true
                    }));
        }

        [HttpGet]
        public ActionResult DetailsTab(int tabId, Guid id)
        {
            var viewModel = _consignmentOrchestra.GetConsignmentDetailsViewModel(id);
            ViewBag.PanelName = viewModel.ConsignmentEditModel.ConsignmentReference;
            ViewBag.PanelId = string.Format("ConsignmentDetails{0}", tabId);
            return PartialView("_Details", viewModel);
        }

        private ActionResult EditTabSimple(int tabId)
        {
            //var viewModel = _consignmentOrchestra.GetConsignmentDetailsViewModel(id);

            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });

            var viewModel = new ConsignmentViewModel();

            var panelName = string.Format("ConsignmentEdit{0}", viewModel.ConsignmentEditModel.ConsignmentReference + tabId ?? "null");

            //ViewBag.PanelName = viewModel.ConsignmentEditModel.ConsignmentReference;
            ViewBag.PanelId = panelName;
            ViewBag.PanelIdentifier = string.Format("ConsignmentEdit{0}", tabId);
            //ViewBag.SupplierDepartmentID = viewModel.ConsignmentEditModel.SupplierDepartmentID;
            //ViewBag.SupplierDepartmentName = viewModel.ConsignmentEditModel.SupplierDepartmentName;

            //var allowedToModifyCost = user.Permissions.Any(p => ("Consignment-EditConsignmentItemCost").Equals(p.PermissionController + "-" + p.PermissionAction));

            //viewModel.ConsignmentEditModel.ConsignmentItemEditModels.ForEach(item => item.IsCostDisabled = item.IsCostDisabled && !allowedToModifyCost);

            viewModel.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                    "Consignment");

            return PartialView("_Edit", viewModel);
        }

        [HttpGet]
        public ActionResult EditTab(int tabId, Guid id)
        {
            if (id == Guid.Empty)
                return EditTabSimple(tabId);

            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });

            var viewModel = _consignmentOrchestra.GetConsignmentViewModel(id);

            var panelName = string.Format("ConsignmentEdit{0}", viewModel.ConsignmentEditModel.ConsignmentReference + tabId ?? "null");

            ViewBag.PanelName = viewModel.ConsignmentEditModel.ConsignmentReference;
            ViewBag.PanelId = panelName;
            ViewBag.PanelIdentifier = string.Format("ConsignmentEdit{0}", tabId);
            ViewBag.SupplierDepartmentID = viewModel.ConsignmentEditModel.SupplierDepartmentID;
            ViewBag.SupplierDepartmentName = viewModel.ConsignmentEditModel.SupplierDepartmentName;

            var allowedToModifyCost = user.Permissions.Any(p => ("Consignment-EditConsignmentItemCost").Equals(p.PermissionController + "-" + p.PermissionAction));

            viewModel.ConsignmentEditModel.ConsignmentItemEditModels.ForEach(item => item.IsCostDisabled = item.IsCostDisabled && !allowedToModifyCost);

            viewModel.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                    "Consignment");

            return PartialView("_Edit", viewModel);
        }

        public ActionResult _ConsTab()
        {
            return PartialView();
        }

        public ActionResult _SearchTab()
        {
            return PartialView();
        }


        [HttpGet]
        public ActionResult Details(Guid id)
        {
            return View(_consignmentOrchestra.GetConsignmentDetailsViewModel(id));
        }

        public ActionResult Results()
        {
            return View();
        }

        public ActionResult CreateConsignment()
        {

            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });

            return View(_consignmentOrchestra.GetConsignmentViewModel(Guid.Empty));
        }


        public ActionResult CreateConsignmentTabOLD(int? id)
        {
            StringBuilder panelName = new StringBuilder("Consignment");
            if (id.HasValue) panelName.Append(id);
            ViewBag.ConsignmentPanel = panelName.ToString(); // DC - we will track Panels open for User in database, viewbag will contain the next to use

            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });
            return PartialView(_consignmentOrchestra.GetConsignmentViewModel(Guid.Empty));
        }

        [PrimeActsAuthorize(OperationKey = "Consignment-ConsignmentIndexTab")]
        public ActionResult ConsignmentIndexTab(int? id)
        {
            //var user = User.Identity.GetApplicationUser();

            StringBuilder panelName = new StringBuilder("Consignments");
            if (id.HasValue) panelName.Append(id);
            ViewBag.ConsignmentsPanel = panelName.ToString();

            return PartialView("_ConsignmentIndexTab");
        }

        [PrimeActsAuthorize(OperationKey = "Consignment-ConsignmentIndexTab")]
        public ActionResult CompletedConsignmentsTab(int? id)
        {

            StringBuilder panelName = new StringBuilder("Consignments");
            if (id.HasValue) panelName.Append(id);
            ViewBag.ConsignmentsPanel = panelName.ToString();

            return PartialView("_CompletedConsignmentsTab");
        }

        [PrimeActsAuthorize(OperationKey = "Consignment-CreateConsignmentTab")]
        public ActionResult CreateConsignmentTab(int? id)
        {
            //return CreateConsignmentTabWithSupplier(id, null, string.Empty);
            //}

            //public ActionResult CreateConsignmentTabWithSupplier(int? id)
            //{
            var user = User.Identity.GetApplicationUser();

            var consignment = _consignmentOrchestra.GetLastConsignmentByUser(user.Id);

            return CreateConsignmentTabWithSupplier(id, consignment.SupplierDepartmentID, consignment.SupplierDepartmentName);
        }

        public ActionResult CreateConsignmentTabWithSupplier(int? id, Guid? supplierDeparmentID, string supplierDepartmentName)
        {
            StringBuilder panelName = new StringBuilder("Consignment");
            if (id.HasValue) panelName.Append(id);
            ViewBag.ConsignmentPanel = panelName.ToString(); // DC - we will track Panels open for User in database, viewbag will contain the next to use

            ViewBag.SupplierDepartmentID = supplierDeparmentID;
            ViewBag.SupplierDepartmentName = supplierDepartmentName;

            ViewBag.UploadFolder = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            var maxFileSize = _setupService.GetDisplayOption("MaxFileSizeToUpload").SetupValueInt;
            var maxNrOfFiles = _setupService.GetDisplayOption("MaxNrOfFilesToUpload").SetupValueInt;
            var mainFolder = _setupService.GetDisplayOption("UploadFolderPath").SetupValueNvarchar;
            ViewBag.MaxFileSize = maxFileSize;
            ViewBag.MaxNrOfFiles = maxNrOfFiles;
            ViewBag.MainFolder = mainFolder;
            var acceptedFileTypes = _setupService.GetDisplayOption("AllowedFileTypesToUpload").SetupValueNvarchar;
            ViewBag.AcceptedFileTypes = acceptedFileTypes;

            var user = User.Identity.GetApplicationUser();

            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });

            var viewModel = _consignmentOrchestra.GetConsignmentViewModel(Guid.Empty);

            viewModel.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                    "Consignment");

            return PartialView(viewModel);
        }

        [HttpGet]
        public JsonResult GetPackWtUnitAndPorterageList()
        {
            return Json(_consignmentOrchestra.GetPackWtUnitAndPorterageList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //public ConsignmentItemViewModel GetConsignmentItemViewModel(string id)
        public JsonResult GetConsignmentItemViewModel(string id)
        {
            var user = User.Identity.GetApplicationUser();
            Guid consignmentId = new Guid(id);
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });
            //return Json(_consignmentOrchestra.GetConsignmentItemViewModel(Guid.Empty));
            var item_defaults = _consignmentOrchestra.GetConsignmentItemViewModel(consignmentId);
            return Json(item_defaults, JsonRequestBehavior.AllowGet);
            //return _consignmentOrchestra.GetConsignmentItemViewModel(consignmentId);
        }


        [HttpPost]
        public JsonResult CreateConsignment(ConsignmentEditModel consignmentEditModel, HttpPostedFileBase file)
        {

            try
            {
                var user = User.Identity.GetApplicationUser();
                _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Nickname = user.Nickname,
                    CompanyId = user.CompanyId,
                    DepartmentId = user.DepartmentId,
                    DivisionId = user.DivisionId
                });


                //Default Values different than a Consignment
                //PE 28/4/17 Don't think this is neeed as never reached
                //NewOrderDefaults(ref consignmentEditModel);

                if (!_consignmentOrchestra.Validate(consignmentEditModel))
                    return Json(consignmentEditModel, JsonRequestBehavior.AllowGet);


                _consignmentOrchestra.CreateConsignment(consignmentEditModel);
                _unitofWork.SaveChanges();
                return Json(consignmentEditModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null; //View(_consignmentOrchestra.Rebuild(consignmentEditModel));
            }
        }

        private void NewOrderDefaults(ref ConsignmentEditModel consignmentEditModel)
        {
            if (consignmentEditModel.ConsignmentReference.StartsWith(ORDER_FILTER))
            {
                consignmentEditModel.ConsignmentReference = consignmentEditModel.ConsignmentReference.Remove(9);
                consignmentEditModel.ConsignmentDescription = consignmentEditModel.ConsignmentReference;
                consignmentEditModel.PurchaseTypeID = _purchaseTypeOrchestra.GetOutrightPurchaseID();
                consignmentEditModel.SelectPurchaseType = _purchaseTypeOrchestra.GetOutrightPurchaseID().ToString();
                consignmentEditModel.Vehicle = VEHICLE_DEFAULT;
                consignmentEditModel.VehicleDetail = VEHICLE_DEFAULT;
            }
        }

        [HttpPost]
        public JsonResult UpdateConsignmentHeader(ConsignmentEditModel consignmentEditModel)
        {
            try
            {
                if (!_consignmentOrchestra.Validate(consignmentEditModel))
                    return Json(consignmentEditModel, JsonRequestBehavior.AllowGet);

                _consignmentOrchestra.UpdateConsignmentHeader(consignmentEditModel);
                _unitofWork.SaveChanges();
                return Json(consignmentEditModel, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null; //View(_consignmentOrchestra.Rebuild(consignmentEditModel));
            }
        }
        
        [HttpPost]
        public JsonResult UpdateConsignment(ConsignmentEditModel consignmentEditModel)
        {
            try
            {
                if (!_consignmentOrchestra.Validate(consignmentEditModel))
                    return Json(consignmentEditModel, JsonRequestBehavior.AllowGet);

                _consignmentOrchestra.UpdateConsignment(consignmentEditModel);
                _unitofWork.SaveChanges();
                return Json(consignmentEditModel, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null; //View(_consignmentOrchestra.Rebuild(consignmentEditModel));
            }
        }

        [HttpPost]
        public void UpdateConsignmentDates(ConsignmentEditModel ConsignmentEditModel)
        {
            try
            {
                _consignmentOrchestra.UpdateConsignmentDates(ConsignmentEditModel);
                _unitofWork.SaveChanges();
            }
            catch
            {
            }
        }

        [HttpPost]
        public ActionResult RemoveConsignmentItem(ConsignmentItemEditModel model)
        {
            _consignmentOrchestra.UpdateConsignmentItem(model);
            _unitofWork.SaveChanges();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConsignmentItem(Guid id)
        {
            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });
            return View(_consignmentOrchestra.GetConsignmentItemViewModel(id));
        }

        [HttpPost]
        public JsonResult UpdateConsignmentItem(ConsignmentItemEditModel model)
        {
            _consignmentOrchestra.UpdateConsignmentItem(model);
            _unitofWork.SaveChanges();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult CreateConsignmentItem(ConsignmentItemEditModel model)
        //{
        //    _consignmentOrchestra.CreateConsignmentItem(model);
        //    _unitofWork.SaveChanges();
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult AutoComplete(string search)
        //{
        //    List<Autocomplete> autoCompleteList = new List<Autocomplete>();

        //    foreach (var consignment in _consignmentOrchestra.GetConsignmentUserNames(new QueryOptions()))
        //    {
        //        autoCompleteList.Add(new Autocomplete { Id = consignment.CreatedBy.ToString(), label = consignment.UserName+ "-" + consignment.UserName});
        //    }
        //    return Json(autoCompleteList, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult ProduceAutoComplete(string search)
        //{
        //    List<Autocomplete> autoCompleteList = new List<Autocomplete>();

        //    IProduceOrchestra _produceOrchestra;

        //    foreach (var produce in _produceOrchestra.GetAllModels(new QueryOptions()))
        //    {
        //        autoCompleteList.Add(new Autocomplete { Id = consignment.CreatedBy.ToString(), label = consignment.UserName + "-" + consignment.UserName });
        //    }
        //    return Json(autoCompleteList, JsonRequestBehavior.AllowGet);

        //}

        public override void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });
        }


        //Order Actions
        //GET: Orders
        //public ActionResult OrderIndex(int page = 1, int pageSize = 20, string searchString = "")
        //{
        //    return
        //            View(_consignmentOrchestra.GetOrderPagingModel(new QueryOptions(), new OrderSearchObject
        //                {
        //                    ToDate = null,
        //                    ConsignmentReference = "",
        //                    FromDate = null,
        //                    SupplierCode = "",
        //                    SupplierName = "",
        //                    RecordsInDays = "CURRENTMONTH"
        //                }));
        //}

        // Create Order
        public ActionResult Order()
        {

            var user = User.Identity.GetApplicationUser();
            _consignmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            });


            return View(_consignmentOrchestra.GetOrderViewModel(Guid.Empty));
        }

    }
}