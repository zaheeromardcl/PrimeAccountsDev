#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
//using AutoMapper.Internal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Domain.ViewModels.Division;
using SearchObject = PrimeActs.Domain.ViewModels.Users.SearchObject;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Extensions;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IApplicationUserOrchestra
    {
        ApplicationUser FindById(Guid id);
        ApplicationUser FindByName(string userName, bool withUserRoles);
        ApplicationUser FindByEmail(string email);  //Added by Paul Edwards 7/2/17

        AspNetUserViewModel GetApplicationUserViewModel();
        AspNetUserViewModel GetApplicationUserViewModels(int page, int pageSize, string searchString);
        List<AspNetUserEditModel> GetApplicationUserForAutoComplete(string search);
        List<AspNetUserEditModel> GetAllAppUsers();
        List<AspNetUserEditModel> GetApplicationUsers(string search);
        ApplicationUser GetApplicationUserByName(string search);
        List<AspNetUserEditModel> GetSalesUsersForAutoComplete(string search, Guid? departmentID, Guid roleID);
        UserPagingModel GetUserPagingModel(QueryOptions queryOptions, SearchObject searchObject);

        void Create(ApplicationUser applicationUser);
        void Update(ApplicationUser applicationUser);
        void Delete(ApplicationUser applicationUser);
        void DeleteById(Guid id);
        void AssignUserToRole(AssignRoleModel userViewModel);
        void RemoveRoleFromUser(AssignRoleModel userViewModel);
        UserContextModel GetUserContextByUserIDAndController(Guid userID, string controller);
        string GetUsernameById(Guid? id);
    }

    public class ApplicationUserOrchestra : IApplicationUserOrchestra
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IApplicationUserRoleService _applicationUserRoleService;
        private readonly ICompanyService _companyService;
        private readonly IDivisionService _divisonService;
        private readonly IDepartmentService _departmentService;
        private readonly IvwPermissionDetailService _vwPermissionDetailService;

        public ApplicationUserOrchestra(IApplicationUserService AspNetUserService, IApplicationUserRoleService applicationUserRoleService, ICompanyService companyService, 
            IDivisionService divisionService, IDepartmentService departmentService, IvwPermissionDetailService vwPermissionDetailService)
        {
            _applicationUserService = AspNetUserService;
            _applicationUserRoleService = applicationUserRoleService;
            _companyService = companyService;
            _departmentService = departmentService;
            _divisonService = divisionService;
            _vwPermissionDetailService = vwPermissionDetailService;
        }

        public ApplicationUser FindById(Guid id)
        {
            return _applicationUserService.FindById(id);
        }
        // Added by Paul 7/2/17
        public ApplicationUser FindByEmail(string email)
        {
            return _applicationUserService.FindByEmail(email);
        }
        //End of added by Paul 7/2/17

        public string GetUsernameById(Guid? id)
        {
            return id != null && id != Guid.Empty ? _applicationUserService.FindById((Guid) id).UserName : string.Empty;
        }

        public ApplicationUser FindByName(string userName, bool withUserRoles = false)
        {
            var applicationUser = _applicationUserService.FindByName(userName);
            if (applicationUser == null)
            {
                return null;
            }

            var applicationUserRoles = _applicationUserRoleService.GetAllUserRolesbyApplicationUserID(applicationUser.Id);
            
            // this builds the context for the user: which companies/divisions/departments he has access to
            //var contextOptions = GetUserContext(applicationUserRoles);

            //contextOptions.DefaultCompanyID = applicationUser.CompanyId.ToString();
            //contextOptions.DefaultDepartmentID = applicationUser.DepartmentId.ToString();
            //contextOptions.DefaultDivisionID = applicationUser.DivisionId.ToString();

            if (applicationUser.Company != null) applicationUser.Company.Logo = null;
            //applicationUser.ContextOptions = contextOptions;
            var permissions = new List<PermissionShort>();
            foreach (var applicationUserRole in applicationUserRoles)
            {
                foreach (var rolePermission in applicationUserRole.ApplicationRole.RolePermissions)
                {
                    if (permissions.All(p => !(p.PermissionController + " / " + p.PermissionAction).Equals(rolePermission.Permission.PermissionName)))
                    {
                        permissions.Add(new PermissionShort() { PermissionController = rolePermission.Permission.PermissionController, PermissionAction = rolePermission.Permission.PermissionAction });
                    }   
                }
            }
            
            applicationUser.Permissions = permissions;
            if (withUserRoles)
            {
                applicationUser.ApplicationUserRoleModels = applicationUser.ApplicationUserRoles.Select(aur => new ApplicationUserRoleModel()
                {
                    UserRoleID = aur.UserRoleID,
                    RoleID = aur.RoleID,
                    CompanyId = aur.CompanyId ?? Guid.Empty,
                    DivisionId = aur.DivisionId ?? Guid.Empty,
                    DepartmentId = aur.DepartmentId ?? Guid.Empty,
                    UserID = aur.UserID
                }).ToList();
            }

            return applicationUser;
        }

        public UserContextModel GetUserContextByUserIDAndController(Guid userID, string controller)
        {
            var applicationUserRoles = _applicationUserRoleService.GetAllUserRolesbyApplicationUserID(userID);

            var userRoles = applicationUserRoles.Where(aur => aur.ApplicationRole.RolePermissions.Any(rp=>rp.Permission.PermissionController.Equals(controller)));
            var userContextByUserIDAndController = GetUserContext(userRoles);

            var user = _applicationUserService.UserById(userID);
            userContextByUserIDAndController.DefaultCompanyID = user.CompanyId.ToString();
            userContextByUserIDAndController.DefaultDepartmentID = user.DepartmentId.ToString();
            userContextByUserIDAndController.DefaultDivisionID = user.DivisionId.ToString();

            return userContextByUserIDAndController;
        }

        public UserContextModel GetUserContext(IEnumerable<ApplicationUserRole> applicationUserRoles)
        {
            UserContextModel contextOptions = new UserContextModel();

            foreach (var applicationUserRole in applicationUserRoles)
            {
                CompanyModel company = null;
                DivisionModel division = null;
                DepartmentEditModel department = null;

                if (applicationUserRole.Company == null)
                {
                    // if company is null => access to all companies divisions and departments

                    foreach (var domainCompany in _companyService.GetAllCompanies())
                    {
                        company = new CompanyModel()
                        {
                            CompanyName = domainCompany.CompanyName,
                            CompanyId = domainCompany.CompanyID
                        };

                        if (contextOptions.Companies.All(c => c.CompanyId != company.CompanyId))
                        {
                            contextOptions.Companies.Add(company);
                        }
                        else
                        {
                            company = contextOptions.Companies.Find(c => c.CompanyId == company.CompanyId);
                        }

                        foreach (var domainDivision in _divisonService.GetDivisionsByCompanyID(domainCompany.CompanyID))
                        {
                            division = new DivisionModel()
                            {
                                DivisionName = domainDivision.DivisionName,
                                DivisionId = domainDivision.DivisionID
                            };

                            if (company.Divisions.All(d => d.DivisionId != division.DivisionId))
                            {
                                company.Divisions.Add(division);
                            }
                            else
                            {
                                division = company.Divisions.Find(d => d.DivisionId == division.DivisionId);
                            }

                            division.Departments.AddRange(
                                _departmentService.GetAllDeptByDivID(domainDivision.DivisionID)
                                    .Select(
                                        c =>
                                            new DepartmentEditModel()
                                            {
                                                DepartmentName = c.DepartmentName,
                                                DepartmentId = c.DepartmentID
                                            }));
                        }
                    }

                    continue;
                }

                company = new CompanyModel()
                {
                    CompanyName = applicationUserRole.Company.CompanyName,
                    CompanyId = applicationUserRole.CompanyId ?? Guid.Empty
                };

                if (contextOptions.Companies.All(c => c.CompanyId != company.CompanyId))
                {
                    contextOptions.Companies.Add(company);
                }
                else
                {
                    company = contextOptions.Companies.Find(c => c.CompanyId == company.CompanyId);
                }

                if (applicationUserRole.Division == null)
                {
                    // if division is null => access to all divisions and departments

                    foreach (
                        var domainDivision in _divisonService.GetDivisionsByCompanyID(applicationUserRole.Company.CompanyID))
                    {
                        division = new DivisionModel()
                        {
                            DivisionName = domainDivision.DivisionName,
                            DivisionId = domainDivision.DivisionID
                        };

                        if (company.Divisions.All(d => d.DivisionId != division.DivisionId))
                        {
                            company.Divisions.Add(division);
                        }
                        else
                        {
                            division = company.Divisions.Find(d => d.DivisionId == division.DivisionId);
                        }

                        division.Departments.AddRange(
                            _departmentService.GetAllDeptByDivID(domainDivision.DivisionID)
                                .Select(
                                    c =>
                                        new DepartmentEditModel()
                                        {
                                            DepartmentName = c.DepartmentName,
                                            DepartmentId = c.DepartmentID
                                        }));
                    }

                    continue;
                }

                division = new DivisionModel()
                {
                    DivisionName = applicationUserRole.Division.DivisionName,
                    DivisionId = applicationUserRole.DivisionId ?? Guid.Empty
                };

                if (company.Divisions.All(d => d.DivisionId != division.DivisionId))
                {
                    company.Divisions.Add(division);
                }
                else
                {
                    division = company.Divisions.Find(d => d.DivisionId == division.DivisionId);
                }

                if (applicationUserRole.Department == null)
                {
                    division.Departments.AddRange(
                        applicationUserRole.Division.Departments.Select(
                            c => new DepartmentEditModel() {DepartmentName = c.DepartmentName, DepartmentId = c.DepartmentID}));
                    continue;
                }

                department = new DepartmentEditModel()
                {
                    DepartmentName = applicationUserRole.Department.DepartmentName,
                    DepartmentId = applicationUserRole.DepartmentId ?? Guid.Empty
                };
                division.Departments.Add(department);
            }

            return contextOptions;
        }

        public AspNetUserViewModel GetApplicationUserViewModel()
        {
            throw new NotImplementedException();
        }

        public AspNetUserViewModel GetApplicationUserViewModels(int page, int pageSize, string searchString)
        {
            throw new NotImplementedException();
        }

        // Need another one in here that is all users that are sales role
        public List<AspNetUserEditModel> GetSalesUsersForAutoComplete(string search, Guid? departmentID, Guid roleID)
        {

            List<AspNetUserEditModel> varSalesUsersforAC = new List<AspNetUserEditModel>();

            varSalesUsersforAC= _applicationUserService.GetAllSalesUsers(search,departmentID,roleID).Select(inc => BuildAspNetUserEditModel(inc)).ToList();
            return varSalesUsersforAC;
        }

        public List<AspNetUserEditModel> GetApplicationUserForAutoComplete(string search)
        {
            return
                _applicationUserService.GetAllUsers()
                    .Where(inc => inc.Firstname.StartsWith(search,StringComparison.CurrentCultureIgnoreCase) || inc.UserName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    .Select(inc => BuildAspNetUserEditModel(inc))
                    .ToList();
        }

        public List<AspNetUserEditModel> GetAllAppUsers()
        {
            var list = _applicationUserService.GetAllUsers()
                .Select(inc => BuildAspNetUserEditModel(inc))
                .ToList();
            return list;
        }

        public List<AspNetUserEditModel> GetApplicationUsers(string search)
        {
            return _applicationUserService.GetAllUsers().Select(inc => BuildAspNetUserEditModel(inc)).ToList();
        }

        public ApplicationUser GetApplicationUserByName(string search)
        {
            return _applicationUserService.GetAllUsers().Where(x => x.UserName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public UserPagingModel GetUserPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var users = _applicationUserService.GetUsers(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);

            var resultList = new ResultList<ApplicationUser>(users, queryOptions);
            return new UserPagingModel
            {
                UserEditModels = resultList,
                SearchObject = searchObject
            };
        }

        public void Create(ApplicationUser applicationUser)
        {
            _applicationUserService.Insert(applicationUser);
        }

        public void Update(ApplicationUser applicationUser)
        {
            _applicationUserService.Update(applicationUser);
        }

        public void Delete(ApplicationUser applicationUser)
        {
            _applicationUserService.Delete(applicationUser);
        }

        public void DeleteById(Guid id)
        {
            var deleteUser = _applicationUserService.FindUserByIdWithAppUserRoles(id);
            var list = deleteUser.ApplicationUserRoles.Select(aur => aur.UserRoleID).ToArray();
            deleteUser = null;
            foreach (Guid guid in list)
            {
                RemoveRoleFromUser(new AssignRoleModel(){UserRoleID = guid});
            }
            
            _applicationUserService.Delete(id);
        }

        public void AssignUserToRole(AssignRoleModel userViewModel)
        {

            
            var departmentId = userViewModel.DepartmentId == Guid.Empty ? (Guid?) null : userViewModel.DepartmentId;
            var divisionId = userViewModel.DivisionId == Guid.Empty ? (Guid?)null : userViewModel.DivisionId;
            var companyId = userViewModel.CompanyId == Guid.Empty ? (Guid?)null : userViewModel.CompanyId;

            var aur = new ApplicationUserRole()
            {
                UserRoleID = userViewModel.UserRoleID,
                RoleID = userViewModel.RoleID,
                UserID = userViewModel.UserID,
                DepartmentId = departmentId,
                DivisionId = divisionId,
                CompanyId = companyId,
                CreatedBy = Guid.Parse("5C7B1948-FEC5-4573-AFA3-BD72FECC2997"),
                CreatedDate= DateTime.Now
            
            };
            _applicationUserRoleService.Insert(aur);
        }

        public void RemoveRoleFromUser(AssignRoleModel userViewModel)
        {
            _applicationUserRoleService.Delete(_applicationUserRoleService.Find(userViewModel.UserRoleID));
        }

        private ApplicationUser ApplyChanges(AspNetUserEditModel model)
        {
            return null;
        }

        private AspNetUserEditModel BuildAspNetUserEditModel(ApplicationUser entity)
        {
            var AspNetUserEditModel = new AspNetUserEditModel();
            AspNetUserEditModel.Id = entity.Id;
            AspNetUserEditModel.UserName = entity.UserName;
            AspNetUserEditModel.Firstname = entity.Firstname;
            AspNetUserEditModel.DepartmentId = entity.DepartmentId;
            AspNetUserEditModel.Lastname = entity.Lastname;

            return AspNetUserEditModel;
        }

        private AspNetUserViewModel BuildAspNetUserViewModel(ApplicationUser AspNetUser, List<ApplicationUser> AspNetUsers)
        {
            var AspNetUserViewModel = new AspNetUserViewModel();

            AspNetUserViewModel.AspNetUserEditModel = AspNetUser == null
                ? new AspNetUserEditModel()
                : BuildAspNetUserEditModel(AspNetUser);
            return AspNetUserViewModel;
        }
    }
}