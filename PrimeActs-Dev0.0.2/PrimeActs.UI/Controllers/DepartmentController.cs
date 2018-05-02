#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using SearchObject = PrimeActs.Domain.ViewModels.Department.SearchObject;
using System.Text;
using PrimeActs.UI.Models;

//using SearchObject = PrimeActs.Domain.ViewModels.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers
{
    //[PrimeActsAuthorize(Role = "Admin")]
    public class DepartmentController : PrimeActsAuthenticatedController
    {
        private readonly IDepartmentOrchestra _departmentOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;

        public DepartmentController(IDepartmentOrchestra departmentOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _departmentOrchestra = departmentOrchestra;
            _unitofWork = unitofWork;
        }

        //public IEnumerable<Autocomplete> GetAllDepartmentsAutocompletes(string search)
        //{
        //    Guid divisionID = Guid.Parse("8510D575-22BB-4E12-B71C-1487B722FE35");
        //    return
        //        _departmentOrchestra.GetDepartmentsForAutoComplete(search,divisionID)
        //            .Select(
        //                inc =>
        //                    new Autocomplete
        //                    {
        //                        Id = inc.DepartmentId.ToString(),
        //                        value = inc.DepartmentId.ToString(),
        //                        label = inc.DepartmentName
        //                    });
        //}

        public async Task<ActionResult> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            var departments = _departmentOrchestra.GetDeparmentsWithPaging(queryOptions,searchObject);
            return View(departments);
        }

        public ActionResult GetDepartmentsPartial()
        {
            var departments = _departmentOrchestra.GetDeparments();
            return PartialView("Index", departments);
        }


        public ActionResult Details(Guid id)
        {
            var departmentEditModel = _departmentOrchestra.GetDepartment(id);
            return View(departmentEditModel);
        }

        
        public ActionResult Create()
        {
            return View(new DepartmentEditModel());
        }
        public ActionResult CreateTabbed(int? id)
        {
            StringBuilder panelName = new StringBuilder("Department");
            if (id.HasValue) panelName.Append(id);
            ViewBag.DepartmentPanel = panelName.ToString(); // DC - we will track Panels open for User in database, viewbag will contain the next to use
            
            return PartialView(new DepartmentEditModel());
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(DepartmentEditModel departmentEditModel)
        {
            try
            {
                departmentEditModel = _departmentOrchestra.CreateDepartment(departmentEditModel);
                _unitofWork.SaveChanges();
                return Redirect(Url.Action("Details", "Department") + "?id=" + departmentEditModel.DepartmentId);
            }
            catch
            {
                //return View(_departmentOrchestra.Rebuild(departmentEditModel));
            }
            return null;
        }

        // GET: Department/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_departmentOrchestra.GetDepartment(id));
        }

        // POST: Department/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(DepartmentEditModel departmentEditModel)
        {
            try
            {

                _departmentOrchestra.UpdateDepartment(departmentEditModel);
                _unitofWork.SaveChanges();
                return Redirect(Url.Action("Details", "Department") + "?id=" + departmentEditModel.DepartmentId);
            }
            catch
            {
                return View();
            }
        }
        
        public override void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            _departmentOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
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
    }
}