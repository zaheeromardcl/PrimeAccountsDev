#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using SearchObject = PrimeActs.Domain.ViewModels.Department.SearchObject;
using System;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentOrchestra _departmentOrchestra;
        private IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;
        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }


        public DepartmentController(IDepartmentOrchestra departmentOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _departmentOrchestra = departmentOrchestra;
            
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
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
            //return string.IsNullOrEmpty(search)
                var autoCompleteList  = string.IsNullOrEmpty(search)
                ? null
                : _departmentOrchestra.GetDepartmentsForAutoComplete(search, user.DivisionId.Value).Select(
                    department => new Autocomplete
                    {
                        Id = department.DepartmentId.ToString(),
                        label = department.DepartmentCode,
                        value = department.DepartmentId.ToString()
                    }
                    ).ToList();
            return autoCompleteList;
        }

        
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<ItemViewModel> DropDown()
        {
           return _departmentOrchestra.GetDeparments().Select(
                    department => new ItemViewModel
                    {
                        Id = department.DepartmentId.ToString(),
                        label = department.DepartmentName,
                        value = department.DepartmentId.ToString()
                    }
                    ).ToList();
        }






        [HttpGet]
        public ResultList<DepartmentEditModel> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            return _departmentOrchestra.GetDepartments(queryOptions, searchObject);
        }

        [System.Web.Mvc.HttpPost]
        public JsonMessage Create(DepartmentEditModel department)
        {
            try
            {
                _departmentOrchestra.CreateDepartment(department);
                _departmentOrchestra.RefreshCache();
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage { StatusId = 0, Message = "Department not created" };
            }


            return new JsonMessage { StatusId = 1, Message = "Department created successfully" };
        }


        [System.Web.Mvc.HttpPost]
        public JsonMessage Edit(DepartmentEditModel department)
        {
            try
            {
                _departmentOrchestra.UpdateDepartment(department);
                _departmentOrchestra.RefreshCache();
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage { StatusId = 0, Message = "Department not updated" };
            }


            return new JsonMessage { StatusId = 1, Message = "Department updated successfully" };
        }
    }
}