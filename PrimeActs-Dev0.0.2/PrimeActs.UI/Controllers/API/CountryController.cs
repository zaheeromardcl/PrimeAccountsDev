#region
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class CountryController : ApiController
    {
        private readonly ICountryOrchestra _countryOrchestra;
        private IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;


        public CountryController(ICountryOrchestra countryOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _countryOrchestra = countryOrchestra;
            _unitofWork = unitofWork;
        }
        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            PopulateUser();
            var autoCompleteList = new List<Autocomplete>();

            foreach (var country in _countryOrchestra.GetCountryModelsForAC(search))
            {
                autoCompleteList.Add(new Autocomplete
                {
                    Id = country.CountryID.ToString(),
                    label =
                        country.CountryName ,
                    value = country.CountryName
                });
            }
            return autoCompleteList;
         
        }

        public void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            _countryOrchestra.Initialize1(new PrimeActs.Domain.ApplicationUser
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
        }

    }
}