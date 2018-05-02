#region

using System.Collections.Generic;
using System.Web.Mvc;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class PortController : PrimeActsController
    {
        private readonly IPortOrchestra _portOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public PortController(IPortOrchestra portOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _portOrchestra = portOrchestra;
            _unitofWork = unitofWork;
        }

        public JsonResult AutoComplete(string search)
        {
            var autoCompleteList = new List<Autocomplete>();

            foreach (var port in _portOrchestra.GetPortModelsForAC())
            {
                autoCompleteList.Add(new Autocomplete
                {
                    Id = port.PortID.ToString(),
                    label = port.PortCode + "-" + port.PortName
                });
            }
            return Json(autoCompleteList, JsonRequestBehavior.AllowGet);
        }
    }
}