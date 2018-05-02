#region

using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using PagedList;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using SearchObject = PrimeActs.Domain.ViewModels.Division.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class DivisionController : PrimeActsController
    {
        private readonly IDivisionOrchestra _divisionOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;

        public DivisionController(IDivisionOrchestra divisionOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _divisionOrchestra = divisionOrchestra;
            _unitofWork = unitofWork;
            //_divisionOrchestra.Initialize(new ModelStateValidation(ModelState));
        }




        public async Task<ActionResult> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            var divisions = _divisionOrchestra.GetDivisionsWithPaging(queryOptions, searchObject);
            return View(divisions);
        }


        public ActionResult Details(Guid id)
        {
            var divisionEditModel = _divisionOrchestra.GetDivision(id);
            return View(divisionEditModel);
        }


        public ActionResult Create()
        {
            return View(new DivisionEditModel());
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(DivisionEditModel divisionEditModel)
        {
            try
            {
                divisionEditModel = _divisionOrchestra.CreateDivision(divisionEditModel);
                _unitofWork.SaveChanges();
                return Redirect(Url.Action("Details", "Division") + "?id=" + divisionEditModel.DivisionId);
            }
            catch
            {
                //return View(_divisionOrchestra.Rebuild(divisionEditModel));
            }
            return null;
        }

        // GET: Division/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_divisionOrchestra.GetDivision(id));
        }

        // POST: Division/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(DivisionEditModel divisionEditModel)
        {
            try
            {

                _divisionOrchestra.UpdateDivision(divisionEditModel);
                _unitofWork.SaveChanges();
                return Redirect(Url.Action("Details", "Division") + "?id=" + divisionEditModel.DivisionId);
            }
            catch
            {
                return View();
            }
        }
    }
}