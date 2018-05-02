#region

using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PrimeActs.Domain;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;
using UserViewModel = PrimeActs.UI.Models.UserViewModel;

#endregion

namespace PrimeActs.UI.Controllers
{
    //[System.Web.Mvc.Authorize(Roles = "Admin")]
    public class UsersAdminController : PrimeActsController
    {
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly IApplicationRoleOrchestra _applicationRoleOrchestra;
        private readonly ICompanyOrchestra _companyOrchestra;
        private readonly IDepartmentOrchestra _departmentOrchestra;
        private readonly IDivisionOrchestra _divisionOrchestra;

        private PrimeActsRoleManager _roleManager;

        private PrimeActsUserManager _userManager;

        public UsersAdminController(IApplicationUserOrchestra applicationUserOrchestra,
            IApplicationRoleOrchestra applicationRoleOrchestra,
            IDepartmentOrchestra departmentOrchestra, ICompanyOrchestra companyOrchestra,
            IDivisionOrchestra divisionOrchestra)
        {
            _applicationUserOrchestra = applicationUserOrchestra;
            _applicationRoleOrchestra = applicationRoleOrchestra;
            _companyOrchestra = companyOrchestra;
            _departmentOrchestra = departmentOrchestra;
            _divisionOrchestra = divisionOrchestra;
        }

        public UsersAdminController(PrimeActsUserManager userManager, PrimeActsRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        public PrimeActsRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<PrimeActsRoleManager>(); }
            private set { _roleManager = value; }
        }

        [PrimeActsAuthorize(OperationKey = "UsersAdmin-Index")]
        public ActionResult Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            var model = _applicationUserOrchestra.GetUserPagingModel(queryOptions, searchObject);
            return View(model);
        }

        //[PrimeActsAuthorize(OperationKey = "UsersAdmin-ViewUserDetails")]
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = GetRegisterViewModel(user);
            return View(model);
        }


        //[PrimeActsAuthorize(OperationKey = "UsersAdmin-CreateUser")]
        public ActionResult Create()
        {
            var model = GetRegisterViewModel(null);
            return View(model);
        }

        //[PrimeActsAuthorize(OperationKey = "UsersAdmin-EditUser")]
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var model = GetRegisterViewModel(user);
            return View(model);
        }

        private RegisterViewModel GetRegisterViewModel(ApplicationUser user)
        {
            ApplicationUser fullUser = null;
            if (user != null)
                fullUser = _applicationUserOrchestra.FindByName(user.UserName, true);

            var roles = _applicationRoleOrchestra.GetAll().Select(r => new ApplicationRoleModel {Id = r.Id, Name = r.Name});

            var companies = _companyOrchestra.GetCompanieswithDivisionsAndDepartments();
            var companiesWithOptionAll = _companyOrchestra.GetCompanieswithDivisionsAndDepartmentsWithOptionAll();

            var model = new RegisterViewModel
            {
                //Companies = companies,
                CompaniesWithAll = new UserContextModel() {Companies = companiesWithOptionAll},
                CompaniesContext = new UserContextModel() {Companies = companies},
                Roles = roles,
                //ContextOptions = fullUser.ContextOptions,
                ApplicationUserRoleModels = fullUser != null ? fullUser.ApplicationUserRoleModels : new List<ApplicationUserRoleModel>(),
                Id = user != null ? user.Id : Guid.Empty,
                Email = user != null ? user.Email : string.Empty,
                Firstname = user != null ? user.Firstname : string.Empty,
                Lastname = user != null ? user.Lastname : string.Empty,
                Nickname = user != null ? user.Nickname : string.Empty, 
                IsActive = true,
                Username = user != null ? user.UserName : string.Empty,
                DepartmentID = (Guid) ((user != null && user.DepartmentId != null) ? user.DepartmentId : Guid.Empty),
                DivisionID = (Guid)((user != null && user.DivisionId != null) ? user.DivisionId : Guid.Empty),
                CompanyID = (Guid)((user != null && user.CompanyId != null) ? user.CompanyId : Guid.Empty)
            };
            return model;
        }

        //[PrimeActsAuthorize(OperationKey = "UsersAdmin-DeleteUser")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(Guid.Parse(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[PrimeActsAuthorize(OperationKey = "UsersAdmin-DeleteUser")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(Guid.Parse(id));
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult TestCollapseTemp()
        {
            return View();
        }
    }
}