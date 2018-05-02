#region

using PrimeActs.Data.Contexts;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    //[PrimeActsAuthorize(Role = "Admin")]
    public class RoleController : ApiController
    {
        private readonly IApplicationRoleOrchestra _applicationRoleOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly PAndIContext db = new PAndIContext();
        private string _serverCode = "L";//Need to change with actual at runtime.

        public RoleController(IApplicationRoleOrchestra applicationRoleOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _applicationRoleOrchestra = applicationRoleOrchestra;
            _unitofWork = unitofWork;
            PopulateUser();
        }

        [System.Web.Mvc.HttpPost]
        public JsonMessage Create(RoleEditModel role)
        {
            try
            {
                _applicationRoleOrchestra.Create(role);
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage {StatusId = 0, Message = "Role not created"};
            }

            return new JsonMessage {StatusId = 1, Message = "Role created successfully"};
        }

        [System.Web.Mvc.HttpPost]
        public JsonMessage Edit(RoleEditModel Role)
        {
            try
            {
                _applicationRoleOrchestra.Update(Role);
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage {StatusId = 0, Message = "Role not updated"};
            }

            return new JsonMessage {StatusId = 1, Message = "Role updated successfully"};
        }

        [System.Web.Mvc.HttpPost]
        public PermissionEditModel AddRole(Guid roleID, Guid permissionID)
        {
            try
            {
                _applicationRoleOrchestra.AddPermissionToRole(roleID, permissionID);

                //var role = db.ApplicationRoles.Find(roleID);
                var permission = db.Permissions.Find(permissionID);
                //role.Permissions.Add(permission);
                
                //db.ApplicationRoles.Attach(role);
                //db.SaveChanges();

                _unitofWork.SaveChanges();

                return new PermissionEditModel
                {
                    PermissionID = permission.PermissionID.ToString(),
                    PermissionAction = permission.PermissionAction,
                    PermissionController = permission.PermissionController,
                    PermissionDescription = permission.PermissionDescription
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [System.Web.Mvc.HttpPost]
        public PermissionEditModel RemovePermission(Guid roleID, Guid permissionID)
        {
            try
            {                
                _applicationRoleOrchestra.RemovePermissionFromRole(roleID, permissionID);

                _unitofWork.SaveChanges();
                return new PermissionEditModel
                {
                    PermissionID = permissionID.ToString()
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [System.Web.Mvc.HttpPost]
        public bool RemoveApplicationRole(Guid roleID)
        {
            try
            {
                db.ApplicationUserRoles.RemoveRange(db.ApplicationUserRoles.Where(x => x.RoleID == roleID));
                db.SaveChanges();
                _applicationRoleOrchestra.DeleteRole(roleID);
                _unitofWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public ResultList<RoleEditModel> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            queryOptions = queryOptions ?? new QueryOptions();
            searchObject = searchObject ?? new SearchObject();
            var model = _applicationRoleOrchestra.GetRolePagingModel(queryOptions, searchObject);
            return model.RoleEditModels;
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _applicationRoleOrchestra.Initialize(applicationUser);
        }
    }
}