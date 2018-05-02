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
using System.Web;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.UI.Controllers.API
{
    public class ProduceGroupController : ApiController
    {
        private readonly IProduceGroupOrchestra _produceGroupOrchestra;
        private IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;

        public ProduceGroupController(IProduceGroupOrchestra produceGroupOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _produceGroupOrchestra = produceGroupOrchestra;
            _unitofWork = unitofWork;
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            PopulateUser();
            return string.IsNullOrEmpty(search)
                ? null
                : _produceGroupOrchestra.GetProduceGroupModelsForAC(search)
                    .Select(
                        produceGroup =>
                            new Autocomplete
                            {
                                Id = produceGroup.ProduceGroupID.ToString(),
                                label = produceGroup.ProduceGroupName,
                                value = produceGroup.ProduceGroupCode
                            })
                    .ToList();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompleteSelect(string search)
        {
            PopulateUser();
            return string.IsNullOrEmpty(search)
                ? null
                : _produceGroupOrchestra.GetProduceGroupModelsForACSSelect(search)
                    .Select(
                        produceGroup =>
                            new Autocomplete
                            {
                                Id = produceGroup.ProduceGroupID.ToString(),
                                label = produceGroup.ProduceGroupName,
                                value = produceGroup.ProduceGroupCode
                            })
                    .ToList();
        }

        public void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            _produceGroupOrchestra.Initialize1(new PrimeActs.Domain.ApplicationUser
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

        private Guid? GetLoggedInUsersSelectedDivisionID()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.Identity.GetApplicationUser().SelectedDivisionId;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}