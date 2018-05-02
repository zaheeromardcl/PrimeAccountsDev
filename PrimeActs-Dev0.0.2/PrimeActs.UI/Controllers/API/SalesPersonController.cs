using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.UI.Controllers.API
{
    public class SalesPersonController : ApiController
    {
        private IUnitOfWorkAsync _unitofWork;
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;


        private PrimeActsUserManager _userManager;
        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }


        private PrimeActsRoleManager _roleManager;
        public PrimeActsRoleManager RoleManager
        {
            get { return _roleManager ?? Request.GetOwinContext().Get<PrimeActsRoleManager>(); }
            private set { _roleManager = value; }
        }

        public SalesPersonController(IApplicationUserOrchestra applicationUserOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _applicationUserOrchestra = applicationUserOrchestra;
            _unitofWork = unitofWork;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            try
            {
                // Gets SalesPersonUserID for that department 
                var currentLoggedInUser = User.Identity.GetApplicationUser();
                //var currentLoggedInUser = UserManager.FindByName(User.Identity.Name);
                var salesRole = RoleManager.FindByName("Sales");

                //var users = _applicationUserOrchestra.GetSalesUsersForAutoComplete(search, '76000600-0000-0070-9204-000068336078', salesRole.Id);//testing only
                var users = _applicationUserOrchestra.GetSalesUsersForAutoComplete(search, currentLoggedInUser.SelectedDepartmentId, salesRole.Id);
                var SalesPerDisplay = users.Where(t => t.Firstname != null && (t.Firstname.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)))
                        .Select(
                           salesPersonUser =>
                                new Autocomplete
                                {
                                    Id = salesPersonUser.Id.ToString(),
                                    label = salesPersonUser.Firstname + " " + salesPersonUser.Lastname,
                                    value = salesPersonUser.Id.ToString()
                                })
                                .ToList();

                return string.IsNullOrEmpty(search)
                    ? null
                    : SalesPerDisplay;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<ItemViewModel> DropDown()
        {
            try
            {
                var list = _applicationUserOrchestra.GetAllAppUsers().Select(
                    sp => new ItemViewModel
                    {
                        Id = sp.Id.ToString(),
                        label = sp.Firstname + " " + sp.Lastname,
                        value = sp.Id.ToString()
                    }
                ).ToList();
            return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}