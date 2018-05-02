#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
//using System.Web.Mvc;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class DespatchController : ApiController
    {
        private readonly IDespatchOrchestra _despatchOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public DespatchController(IDespatchOrchestra despatchOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _despatchOrchestra = despatchOrchestra;
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _despatchOrchestra.GetDespatchModelsForAC(search).Select(
                    despatch => new Autocomplete
                    {
                        Id = despatch.DespatchID.ToString(),
                        label = despatch.DespatchCode + "-" + despatch.DespatchName,
                        value = despatch.DespatchID.ToString()
                    }
                    ).ToList();
        }
    }
}