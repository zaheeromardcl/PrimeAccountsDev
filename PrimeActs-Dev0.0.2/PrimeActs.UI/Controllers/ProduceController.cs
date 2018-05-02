#region

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain.ViewModels.Produce;
using ProduceViewModel = PrimeActs.Domain.ViewModels.Produce.ProduceViewModel;
using ProduceEditModel = PrimeActs.Domain.ViewModels.Produce.ProduceEditModel;

#endregion

namespace PrimeActs.UI.Controllers
{
    //[PrimeActsAuthorize(Role = "Admin")]
    public class ProduceController : PrimeActsController
    {
        private readonly IProduceOrchestra _produceOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;
        private IApplicationUserOrchestra _applicationUserOrchestra;

        public ProduceController(IProduceOrchestra produceOrchestra, IUnitOfWorkAsync unitofWork, IApplicationUserOrchestra applicationUserOrchestra)
        {
            _produceOrchestra = produceOrchestra;
            //_produceOrchestra.Initialize(new ModelStateValidation(ModelState));
            _unitofWork = unitofWork;
            _applicationUserOrchestra = applicationUserOrchestra;
        }

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);
        //}

        public ActionResult Index()
        {
            //IPagedList<ProduceViewModel> produceToReturn = _produceOrchestra.GetAllModels(page, pageSize, searchString).ToPagedList(page, pageSize);
            //ViewBag.SearchString = searchString;
//            return View();
            return
                View(_produceOrchestra.GetProducePagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.Produce.SearchObject
                    {
                        ProduceCode = "",
                        ProduceName = ""
                    }));
        }

        public ActionResult SearchResult(string IsActive, int page = 1, int pageSize = 15, string searchString = "",
            string sortColumn = "", string sortOrder = "")
        {
            ViewBag.activeToggle = IsActive;
            var produceToReturn =
                _produceOrchestra.GetAllModels(IsActive, page, pageSize, searchString, sortColumn, sortOrder)
                    .ToPagedList(page, pageSize);
            ViewBag.SearchString = searchString;
            return PartialView("SearchProduce", produceToReturn);
        }

        // GET: Produce/Details/5
        public ActionResult Details(Guid id)
        {
            var produceEditModel = _produceOrchestra.GetModel(id);
            return View(produceEditModel);
        }

        // GET: Produce/Create
        public ActionResult Create()
        {
            return View(_produceOrchestra.Rebuild(new ProduceViewModel()));
        }

        // POST: Produce/Create
        [HttpPost]
        public ActionResult Create(ProduceEditModel produceEditModel)
        {
            try
            {
                if (!_produceOrchestra.Validate(produceEditModel))
                    return View(_produceOrchestra.Rebuild(produceEditModel));

                produceEditModel = _produceOrchestra.Insert(produceEditModel);
                _unitofWork.SaveChanges();
                //_produceOrchestra.RefreshCache();

                return Redirect(Url.Action("Details", "Produce") + "?id=" + produceEditModel.ProduceID);
            }
            catch
            {
                return View(_produceOrchestra.Rebuild(produceEditModel));
            }
        }

        // GET: Produce/Edit/5
        public ActionResult Edit(Guid id)
        {
            var produceEditModel = _produceOrchestra.GetModel(id);
            return View(_produceOrchestra.Rebuild(produceEditModel));
        }

        // POST: Produce/Edit/5
        [HttpPost]
        public ActionResult Edit(ProduceEditModel produceEditModel)
        {
            try
            {
                if (!_produceOrchestra.Validate(produceEditModel))
                    return View(_produceOrchestra.Rebuild(produceEditModel));

                _produceOrchestra.Update(produceEditModel);
                _unitofWork.SaveChanges();
                //_produceOrchestra.RefreshCache();

                return Redirect(Url.Action("Details", "Produce") + "?id=" + produceEditModel.ProduceID);
            }
            catch
            {
                return View(_produceOrchestra.Rebuild(produceEditModel));
            }
        }

        // GET: Produce/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Produce/Delete/5
        [HttpPost]
        public ActionResult Delete(ProduceEditModel produceViewModel)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult AutoComplete(string search)
        {
            var autoCompleteList = new List<Autocomplete>();

            foreach (var produce in _produceOrchestra.GetAllModels("1", 1, 25, search, string.Empty, string.Empty))
            {
                autoCompleteList.Add(new Autocomplete
                {
                    Id = produce.ProduceID.ToString(),
                    label = produce.ProduceCode + "-" + produce.ProduceName
                });
            }
            return Json(autoCompleteList, JsonRequestBehavior.AllowGet);
        }

        [PrimeActsAuthorize(OperationKey = "Produce-Index")]
        public ActionResult IndexTab(int? id)
        {
            var panelName = string.Format("Produce{0}", id ?? 0);
            ViewBag.ProducePanel = panelName;

            var model = new ProducePagingViewModel();
            model.ProducePagingModel = _produceOrchestra.GetProducePagingModel(new QueryOptions(),
                new PrimeActs.Domain.ViewModels.Produce.SearchObject
                {
                    ProduceCode = "",
                    ProduceName = ""
                });

            model.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                    "Produce");

            return
                PartialView("_Produce", model);
        }

        [PrimeActsAuthorize(OperationKey = "Produce-Index")]
        public ActionResult DetailsTab(int tabId, Guid id)
        {
            var produceEditModel = _produceOrchestra.GetModel(id);

            ViewBag.PanelName = produceEditModel.ProduceName;
            ViewBag.ProducePanelId = "ProduceDetails" + tabId;

            produceEditModel.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                    "Produce");

            return PartialView("_ProduceDetails", produceEditModel);
        }
    }
}