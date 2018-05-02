#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class CurrencyController : ApiController
    {
        private readonly ICurrencyOrchestra _currencyOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CurrencyController(ICurrencyOrchestra currencyOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _currencyOrchestra = currencyOrchestra;
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _currencyOrchestra.GetCurrencyModelsForAC(search).Select(
                    currency => new Autocomplete
                    {
                        Id = currency.CurrencyID.ToString(),
                        label = currency.CurrencyCode + " - " + currency.CurrencyName,
                        value = currency.CurrencyID.ToString()
                    }
                    ).ToList();
        }

        //{
        //public List<Autocomplete> AutoCompletePC(string search)
        //[System.Web.Http.HttpGet]

        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //    List<Autocomplete> redoutput;
        //    //FOR DEBUG PURPOSES
        //    redoutput = string.IsNullOrEmpty(search) ? null : _departmentOrchestra.GetProduceForAcForProduceQuantityForTickets(search).Select(produce => new Autocomplete { Id = produce.ProduceCode.ToString() + "-" + produce.ConsignmentItemID, label = produce.ProduceName + "-(" + produce.ProduceCode + ")[" + produce.RemainingQuantity + "]" + " #" + produce.ConsignmentReference }).ToList();
        //    return redoutput;


        //    // return string.IsNullOrEmpty(search) ? null : _produceOrchestra.GetProduceForAcForProduceQuantityForTickets(search).Select(produce => new Autocomplete { Id = produce.ProduceCode.ToString(), label = produce.ProduceCode + " - " + produce.ProduceName + "-" + produce.RemainingQuantity }).ToList();
        //}
    }
}