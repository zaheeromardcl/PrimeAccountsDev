#region

using PrimeActs.Data.Contexts;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.UI.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers
{
    //[PrimeActsAuthorize(Role = "Admin")]
    public class PermissionsController : PrimeActsController
    {
        private readonly PAndIContext db = new PAndIContext();

        [PrimeActsAuthorize(OperationKey = "Permissions-Index")]
        public async Task<ActionResult> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            queryOptions = queryOptions ?? new QueryOptions();
            searchObject = searchObject ?? new SearchObject();
            var permissions =
                db.Permissions.OrderByDescending(x => x.PermissionID)
                    .Skip((queryOptions.CurrentPage - 1)*queryOptions.PageSize)
                    .Take(queryOptions.PageSize)
                    .ToList();
            var permissionEditModels =
                permissions.Select(
                    permission =>
                        new PermissionEditModel
                        {
                            PermissionID = permission.PermissionID.ToString(),
                            PermissionName = permission.PermissionName,
                            PermissionController = permission.PermissionController,
                            PermissionAction = permission.PermissionAction,
                            PermissionDescription = permission.PermissionDescription
                        }).ToList();
            queryOptions.TotalPages = (int)Math.Ceiling((double)db.Permissions.Count() / queryOptions.PageSize);
            return
                View(new PermissionPagingModel
                {
                    PermissionEditModels = new ResultList<PermissionEditModel>(permissionEditModels, queryOptions),
                    SearchObject = searchObject
                });
        }


        public ActionResult Details(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var permission = db.Permissions.Find(Id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return
                View(new PermissionEditModel
                {
                    PermissionID = permission.PermissionID.ToString(),
                    PermissionController = permission.PermissionController,
                    PermissionAction = permission.PermissionAction,
                    PermissionDescription = permission.PermissionDescription
                });
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var permission = db.Permissions.Find(Id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return
                View(new PermissionEditModel
                {
                    PermissionID = permission.PermissionID.ToString(),
                    PermissionAction = permission.PermissionAction,
                    PermissionDescription = permission.PermissionDescription,
                    PermissionController = permission.PermissionController
                });
        }

        [System.Web.Mvc.Authorize(Roles = "SuperAdmin, CanEditPermission")]
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonMessage Edit(PermissionEditModel permission)
        {
            try
            {
                db.Permissions.Add(new Permission
                {
                    PermissionID = Guid.Parse(permission.PermissionID),
                    PermissionAction = permission.PermissionAction,
                    PermissionDescription = permission.PermissionDescription,
                    PermissionController = permission.PermissionController
                });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage {StatusId = 0, Message = "Permission not updated"};
            }


            return new JsonMessage {StatusId = 1, Message = "Permission updated successfully"};
        }


        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            db.Permissions.Remove(permission);
            db.SaveChanges();
            return Redirect("/Permissions/Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}