#region

using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class NavigationController : Controller
    {
        private PrimeActsRoleManager _roleManager;
        private PrimeActsUserManager _userManager;

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


        // GET: Navigation
        public ActionResult Menu()
        {
            var model = new MenuModel();
            var userRoles = UserManager.GetRolesAsync(Guid.Parse(HttpContext.User.Identity.GetUserId()));
            foreach (var rolename in userRoles.Result)
            {
                var role = RoleManager.FindByName(rolename);
                foreach (var rolePermission in role.RolePermissions)
                {
                    if (!model.Permissions.Contains(rolePermission.Permission.PermissionName))
                        model.Permissions.Add(rolePermission.Permission.PermissionName);
                }
            }

            return PartialView("_Menu", model);
        }
    }
}