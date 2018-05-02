using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using SearchObject = PrimeActs.Domain.ViewModels.Company.SearchObject;

namespace PrimeActs.UI.Controllers.API
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyOrchestra _companyOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;


        public CompanyController(ICompanyOrchestra companyOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _companyOrchestra = companyOrchestra;
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public ResultList<CompanyEditModel> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            return _companyOrchestra.GetCompanies(queryOptions, searchObject);
        }

        [System.Web.Mvc.HttpPost]
        public JsonMessage Create(CompanyEditModel Company)
        {
            try
            {
                _companyOrchestra.CreateCompany(Company);
                _companyOrchestra.RefreshCache();
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage { StatusId = 0, Message = "Company not created" };
            }


            return new JsonMessage { StatusId = 1, Message = "Company created successfully" };
        }


        [System.Web.Mvc.HttpPost]
        public JsonMessage Edit(CompanyEditModel Company)
        {
            try
            {
                _companyOrchestra.UpdateCompany(Company);
                _companyOrchestra.RefreshCache();
                _unitofWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return new JsonMessage { StatusId = 0, Message = "Company not updated" };
            }


            return new JsonMessage { StatusId = 1, Message = "Company updated successfully" };
        }


        public List<Autocomplete> AutoComplete(QueryOptions queryOptions)
        {
            var autoCompleteList = new List<Autocomplete>();

            foreach (var company in _companyOrchestra.GetCompanysForAutoComplete(""))
            {
                autoCompleteList.Add(new Autocomplete
                {
                    Id = company.CompanyId.ToString(),
                    label = company.CompanyName + "-" + company.CompanyNo,
                    value = company.CompanyId.ToString()
                });
            }
            return autoCompleteList;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<ItemViewModel> DropDown()
        {
            return _companyOrchestra.GetCompanies().Select(
                     company => new ItemViewModel
                     {
                         Id = company.CompanyId.ToString(),
                         label = company.CompanyName,
                         value = company.CompanyId.ToString()
                     }
                     ).ToList();
        }
    }
}