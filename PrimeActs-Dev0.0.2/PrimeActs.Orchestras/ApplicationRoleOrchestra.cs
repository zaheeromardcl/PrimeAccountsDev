using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AutoMapper.Internal;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;


namespace PrimeActs.Orchestras
{
    public interface IApplicationRoleOrchestra
    {
        ApplicationRole FindById(Guid id, bool withUsers = false, bool withPermissions = false);
        ApplicationRole FindByName(string roleName);
        List<ApplicationRole> GetAll();
        void Create(RoleEditModel roleToCreate);
        void Update(RoleEditModel roleToUpdate);

        RolePagingModel GetRolePagingModel(QueryOptions queryOptions, SearchObject searchObject);
        bool DeleteRole(Guid roleID);
        //IEnumerable<Guid> FindRolesByPermissionID(Guid id);
        void AddPermissionToRole(Guid roleID, Guid permissionID);
        void RemovePermissionFromRole(Guid roleID, Guid permissionID);
        void Initialize(ApplicationUser applicationUser);
    }

    public class ApplicationRoleOrchestra : IApplicationRoleOrchestra
    {
        private readonly IApplicationRoleService _applicationRoleService;
        
        private readonly IRolePermissionService _rolePermissionService;
        private ApplicationUser _principal;

        private readonly string _serverCode;

        public ApplicationRoleOrchestra(ISetupLocalService setupLocalService, IApplicationRoleService applicationRoleService, IRolePermissionService rolePermissionService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _applicationRoleService = applicationRoleService;
            
            _rolePermissionService = rolePermissionService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public ApplicationRole FindById(Guid id, bool withUsers = false, bool withPermissions = false)
        {
            return _applicationRoleService.FindById(id, withUsers, withPermissions);
        }

        public ApplicationRole FindByName(string roleName)
        {
            return _applicationRoleService.FindByName(roleName);
        }

        public List<ApplicationRole> GetAll()
        {
            return _applicationRoleService.GetAll();
        }

        public void Create(RoleEditModel roleToCreate)
        {
            var role = new ApplicationRole
            {
                Id = IDGenerator.NewGuid(_serverCode[0]),
                Name = roleToCreate.Name,
                Description = roleToCreate.Description,
                
                CreatedBy = _principal.Id,
                CreatedDate = DateTime.Now,
                ObjectState = ObjectState.Added
            };
            _applicationRoleService.Insert(role);
        }

        public void Update(RoleEditModel roleToUpdate)
        {
            var role = new ApplicationRole
            {
                Id = roleToUpdate.Id,
                Name = roleToUpdate.Name,
                Description = roleToUpdate.Description,
                
                UpdatedBy = _principal.Id,
                UpdatedDate = DateTime.Now,
                ObjectState = ObjectState.Modified
            };
            _applicationRoleService.Update(role);
        }

        public RolePagingModel GetRolePagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var roles = _applicationRoleService.GetRoles(queryOptions, searchObject, out totalCount);

            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var roleEditModels = roles.Select(role => new RoleEditModel
                {
                    Id = role.Id, Name = role.Name, Description = role.Description
                }).ToList();

            return new RolePagingModel
            {
                RoleEditModels = new ResultList<RoleEditModel>(roleEditModels, queryOptions),
                SearchObject = searchObject
            };
        }
        
        public bool DeleteRole(Guid roleID)
        {
            var role = FindById(roleID, false, true);
            role.RolePermissions.Clear();
            _applicationRoleService.Delete(role);
            return true;
        }

        public void AddPermissionToRole(Guid roleID, Guid permissionID)
        {
            var rolePermissionID = IDGenerator.NewGuid(_serverCode[0]);
            var createdBy = _principal.Id;

            _rolePermissionService.Insert(new RolePermission()
            {
                RolePermissionID = rolePermissionID,
                PermissionID = permissionID,
                RoleID = roleID,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy
            });
        }

        public void RemovePermissionFromRole(Guid roleID, Guid permissionID)
        {
            var role = FindById(roleID, false, true);
            var id = role.RolePermissions.FirstOrDefault(p => p.PermissionID == permissionID).RolePermissionID;
            _rolePermissionService.Delete(id);
        }
    }
}
