#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class NominalAccountController : ApiController
    {
        private readonly INominalAccountOrchestra _nominalAccountOrchestra;
        private IUnitOfWorkAsync _unitofWork;

       
        public NominalAccountController(INominalAccountOrchestra nominalAccountOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _nominalAccountOrchestra = nominalAccountOrchestra;
            _unitofWork = unitofWork;
        }
        

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search, string companyID)
        {
            Guid guidCompanyID = new Guid(companyID);
            return string.IsNullOrEmpty(search)
                ? null
                : _nominalAccountOrchestra.GetNominalAccountModelsForAC(search, guidCompanyID)
                    .Select(
                        nominalAccount =>
                            new Autocomplete
                            {
                                Id = nominalAccount.NominalAccountID.ToString(),
                                label = nominalAccount.NominalCode + " - " + nominalAccount.NominalAccountName,
                                value = nominalAccount.NominalCode + " - " + nominalAccount.NominalAccountName
                            })
                    .ToList();
        }

    }
}
