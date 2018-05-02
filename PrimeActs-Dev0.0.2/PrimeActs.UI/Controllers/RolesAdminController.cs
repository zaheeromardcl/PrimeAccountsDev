#region

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Data.Contexts;
using PrimeActs.Domain;
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
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers
{
    //[PrimeActsAuthorize(Role = "Admin")]
    public class RolesAdminController : PrimeActsController
    {
        private readonly PAndIContext db = new PAndIContext();
        private PrimeActsUserManager _userManager;
        private PrimeActsRoleManager _roleManager;
        private readonly IApplicationRoleOrchestra _applicationRoleOrchestra;

        public RolesAdminController(IApplicationRoleOrchestra applicationRoleOrchestra)
        {
            _applicationRoleOrchestra = applicationRoleOrchestra;
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            set { _userManager = value; }
        }

        public PrimeActsRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<PrimeActsRoleManager>(); }
            private set { _roleManager = value; }
        }

        [PrimeActsAuthorize(OperationKey = "RolesAdmin-Index")]
        public ActionResult Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            queryOptions = queryOptions ?? new QueryOptions();
            searchObject = searchObject ?? new SearchObject();
            var model = _applicationRoleOrchestra.GetRolePagingModel(queryOptions, searchObject);
            return View(model);
        }

        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-ViewRole")]
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var applicationRole = _applicationRoleOrchestra.FindById(id, false, true);
            var roleViewModel = new RoleEditModel
            {
                Id = applicationRole.Id,
                Name = applicationRole.Name,
                Description = applicationRole.Description,
                UserList = applicationRole.ApplicationUsers.Select(x => new ApplicationUser
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname
                }).ToList(),
                PermissionList = applicationRole.RolePermissions.OrderBy(rp => rp.Permission.PermissionController).Select(x => new PermissionEditModel
                {
                    PermissionID = x.Permission.PermissionID.ToString(),
                    PermissionAction = x.Permission.PermissionAction,
                    PermissionController = x.Permission.PermissionController,
                    PermissionDescription = x.Permission.PermissionDescription
                }).ToList()
            };
            return View(roleViewModel);
        }

        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-CreateRole")]
        public ActionResult Create()
        {
            var SelectedRoles = new string[0];
            var roleModel = new RoleEditModel();

            // Update the new Description property for the ViewModel:
            return View(roleModel);
        }


        [System.Web.Mvc.HttpPost]
        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-CreateRole")]
        public async Task<ActionResult> Create(RoleEditModel roleViewModel, params string[] SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole { Name = roleViewModel.Name, Description = roleViewModel.Description };
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                }
                if (SelectedRoles != null && SelectedRoles.Count() > 0)
                {
                    role = _applicationRoleOrchestra.FindById(role.Id);
                    foreach (var selectedRole in SelectedRoles)
                    {
                        //role.Roles.Add(db.Roles.Find(Guid.Parse(role)));
                    }
                    //db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            SelectedRoles = SelectedRoles ?? new string[] {};
            /*roleViewModel.RoleList = roles.Select(p => new CheckBoxListItem()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Description = p.Description,
                IsChecked = SelectedRoles.Contains(p.Id.ToString())

            }).ToList();*/
            return View(roleViewModel);
        }


        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-EditRole")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var role = await RoleManager.FindByIdAsync(Guid.Parse(id));
            var role = _applicationRoleOrchestra.FindById(Guid.Parse(id), false, true);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleModel = new RoleEditModel
            {
                Id = role.Id,
                Name = role.Name,
                SelectedPermissionList = role.RolePermissions.Select(p => new PermissionEditModel
                {
                    PermissionID = p.PermissionID.ToString(),
                    PermissionAction = p.Permission.PermissionAction,
                    PermissionController = p.Permission.PermissionController,
                    PermissionDescription = p.Permission.PermissionDescription
                }).ToList(),
                Description = role.Description,
                PermissionList = db.Permissions.OrderBy(p => p.PermissionController).Select(p => new PermissionEditModel
                {
                    PermissionID = p.PermissionID.ToString(),
                    PermissionAction = p.PermissionAction,
                    PermissionController = p.PermissionController,
                    PermissionDescription = p.PermissionDescription
                }).ToList()
            };

            // Update the new Description property for the ViewModel:
            return View(roleModel);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-EditRole")]
        public async Task<ActionResult> Edit(RoleEditModel roleModel, params string[] SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;

                // Update the new Description property:
                role.Description = roleModel.Description;
                //role.Roles.Clear();
                await RoleManager.UpdateAsync(role);
                if (SelectedRoles != null && SelectedRoles.Count() > 0)
                {
                    role = _applicationRoleOrchestra.FindById(role.Id);
                    foreach (var selectedRole in SelectedRoles)
                    {
                        //role.Roles.Add(db.Roles.Find(Guid.Parse(role)));
                    }
                    //db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-DeleteRole")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(Guid.Parse(id));
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }


        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[PrimeActsAuthorize(OperationKey = "RolesAdmin-DeleteRole")]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(Guid.Parse(id));
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    //This row is the issue - try to figure it out
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}