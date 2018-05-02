#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using SearchObject = PrimeActs.Domain.ViewModels.Division.SearchObject;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class DivisionController : ApiController
    {
        private readonly IDivisionOrchestra _divisionOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;


        public DivisionController(IDivisionOrchestra divisionOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _divisionOrchestra = divisionOrchestra;
            _unitofWork = unitofWork;
        }


        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<ItemViewModel> DropDown()
        {
            return _divisionOrchestra.GetDivisions()
                    .Select(x => new ItemViewModel
                    {
                        Id = x.DivisionId.ToString(),
                        value = x.DivisionId.ToString(),
                        label = x.DivisionName,
                        Description = x.DivisionName,
                        Code = x.DivisionName
                    }).ToList();
        }
        
        [HttpGet]
        public ResultList<DivisionEditModel> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            return _divisionOrchestra.GetDivisions(queryOptions, searchObject);
        }

        [System.Web.Mvc.HttpPost]
        public JsonMessage Create(DivisionEditModel division)
        {
            try
            {
                _divisionOrchestra.CreateDivision(division);
                _divisionOrchestra.RefreshCache();
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage { StatusId = 0, Message = "Division not created" };
            }


            return new JsonMessage { StatusId = 1, Message = "Division created successfully" };
        }


        [System.Web.Mvc.HttpPost]
        public JsonMessage Edit(DivisionEditModel division)
        {
            try
            {
                _divisionOrchestra.UpdateDivision(division);
                _divisionOrchestra.RefreshCache();
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage { StatusId = 0, Message = "Division not updated" };
            }


            return new JsonMessage { StatusId = 1, Message = "Division updated successfully" };
        }
    }
}