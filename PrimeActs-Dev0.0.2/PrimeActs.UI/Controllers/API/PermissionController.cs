#region

using System;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.UI.Models;
using PrimeActs.Domain;
using PrimeActs.Data.Contexts;
using PrimeActs.Domain.ViewModels.Users;

using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    [PrimeActsAuthorize(Role = "Admin")]
    public class PermissionController : ApiController
    {
        private readonly PAndIContext db = new PAndIContext();
        private string _serverCode = "L";//Need to change with actual at runtime.
        private ApplicationUser _principal;

        [System.Web.Mvc.HttpPost]
        public JsonMessage Create(PermissionEditModel permission)
        {
            try
            {
                PopulateUser();
                if (db.Permissions.Any(x => x.PermissionController == permission.PermissionController && x.PermissionAction == permission.PermissionAction))
                    return new JsonMessage {StatusId = 0, Message = "Permission already exists"};
                db.Permissions.Add(new Permission
                {
                    PermissionID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    PermissionName = permission.PermissionName,
                    PermissionDescription = permission.PermissionDescription,
                    PermissionAction = permission.PermissionAction,
                    PermissionController = permission.PermissionController,
                    CreatedBy = _principal.Id,
                    CreatedDate = DateTime.Now
                });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage {StatusId = 0, Message = "Permission not created"};
            }


            return new JsonMessage {StatusId = 1, Message = "Permission created successfully"};
        }


        [System.Web.Mvc.HttpPost]
        public JsonMessage Edit(PermissionEditModel permission)
        {
            try
            {
                PopulateUser();
                var persmissionID = Guid.Parse(permission.PermissionID);
                if (db.Permissions.Any(x => x.PermissionController == permission.PermissionController && x.PermissionAction == permission.PermissionAction))
                    return new JsonMessage {StatusId = 0, Message = "Permission already exists"};

                var existingPermission = db.Permissions.Find(persmissionID);
                existingPermission.PermissionController = permission.PermissionController;
                existingPermission.PermissionAction = permission.PermissionAction;
                existingPermission.PermissionDescription = permission.PermissionDescription;
                existingPermission.UpdatedBy = _principal.Id;
                existingPermission.UpdatedDate = DateTime.Now;
                db.Entry(existingPermission).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage {StatusId = 0, Message = "Permission not updated"};
            }


            return new JsonMessage {StatusId = 1, Message = "Permission updated successfully"};
        }

        [HttpGet]
        public ResultList<Permission> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            queryOptions = queryOptions ?? new QueryOptions();
            searchObject = searchObject ?? new SearchObject();
            var commonSearchCriteria = searchObject.CommonSearchCriteria ?? "";
            var fromDate = searchObject.FromDate.HasValue
                ? DateTime.Parse(searchObject.FromDate.Value.ToString())
                : (DateTime?) null;
            var toDate = searchObject.ToDate.HasValue
                ? DateTime.Parse(searchObject.ToDate.Value.ToString())
                : (DateTime?) null;
            IQueryable<Permission> permissionsQuery = db.Permissions;
            if (!string.IsNullOrEmpty(commonSearchCriteria))
                permissionsQuery =
                    permissionsQuery.Where(
                        m => m.PermissionController.StartsWith(commonSearchCriteria) | m.PermissionAction.StartsWith(commonSearchCriteria));

            switch (queryOptions.SortField)
            {
                case "ID":
                    if (queryOptions.SortOrder == "ASC")
                        permissionsQuery = permissionsQuery.OrderBy(x => x.PermissionID);
                    else
                        permissionsQuery = permissionsQuery.OrderByDescending(x => x.PermissionID);
                    break;
                case "Controller":
                    if (queryOptions.SortOrder == "ASC")
                        permissionsQuery = permissionsQuery.OrderBy(x => x.PermissionController);
                    else
                        permissionsQuery = permissionsQuery.OrderByDescending(x => x.PermissionController);
                    break;
                case "Action":
                    if (queryOptions.SortOrder == "ASC")
                        permissionsQuery = permissionsQuery.OrderBy(x => x.PermissionAction);
                    else
                        permissionsQuery = permissionsQuery.OrderByDescending(x => x.PermissionAction);
                    break;
                default:
                    if (queryOptions.SortOrder == "ASC")
                        permissionsQuery = permissionsQuery.OrderBy(x => x.PermissionID);
                    else
                        permissionsQuery = permissionsQuery.OrderByDescending(x => x.PermissionID);
                    break;
            }

            var permissionsResults = permissionsQuery.ToList();
            var permissionsToReturn =
                permissionsQuery.Skip((queryOptions.CurrentPage - 1)*queryOptions.PageSize)
                    .Take(queryOptions.PageSize)
                    .ToList();
            queryOptions.TotalPages = (int) Math.Ceiling((double) permissionsResults.Count/queryOptions.PageSize);
            var permissions =
                permissionsToReturn.Select(x => new Permission
                {
                    PermissionID = x.PermissionID,
                    PermissionName = x.PermissionName,
                    PermissionAction = x.PermissionAction,
                    PermissionController = x.PermissionController,
                    PermissionDescription = x.PermissionDescription
                })
                .ToList();
            return new ResultList<Permission>(permissions, queryOptions);
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            Initialize(applicationUser);
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }
    }
}