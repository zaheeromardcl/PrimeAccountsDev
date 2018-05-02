#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;

using System;
using System.Threading.Tasks;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.UI.Models;
using SearchObject = PrimeActs.Domain.ViewModels.Company.SearchObject;
using System.Text;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class CompanyController : PrimeActsController
    {
        private readonly ICompanyOrchestra _companyOrchestra;
        private IUnitOfWorkAsync _unitOfWork;

        public CompanyController(ICompanyOrchestra companyOrchestra, IUnitOfWorkAsync unitOfWork)
        {
            _companyOrchestra = companyOrchestra;
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Autocomplete> GetAllCompanies(string search)
        {
            return
                _companyOrchestra.GetCompanysForAutoComplete(search)
                    .Select(
                        inc =>
                            new Autocomplete
                            {
                                Id = inc.CompanyId.ToString(),
                                value = inc.CompanyId.ToString(),
                                label = inc.RelatedCompanyName
                            });
        }

        public async Task<ActionResult> Index([FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            var companies = _companyOrchestra.GetCompaniessWithPaging(queryOptions, searchObject);
            return View(companies);
        }


        public ActionResult Details(Guid id)
        {
            var companyEditModel = _companyOrchestra.GetCompany(id);
            return View(companyEditModel);
        }


        public ActionResult Create()
        {
            return View(new CompanyEditModel());
        }

        public ActionResult CreateTab(int? id)
        {
            StringBuilder panelName = new StringBuilder("Company");
            if (id.HasValue) panelName.Append(id);
            ViewBag.CompanyPanel = panelName.ToString(); // DC - we will track Panels open for User in database, viewbag will contain the next to use
            return PartialView(new CompanyEditModel());
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CompanyEditModel companyEditModel)
        {
            try
            {
                companyEditModel = _companyOrchestra.CreateCompany(companyEditModel);
                _unitOfWork.SaveChanges();
                return Redirect(Url.Action("Details", "Company") + "?id=" + companyEditModel.CompanyId);
            }
            catch
            {
                //return View(_companyOrchestra.Rebuild(companyEditModel));
            }
            return null;
        }

        // GET: Company/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_companyOrchestra.GetCompany(id));
        }

        // POST: Company/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CompanyEditModel companyEditModel)
        {
            try
            {

                _companyOrchestra.UpdateCompany(companyEditModel);
                _unitOfWork.SaveChanges();
                return Redirect(Url.Action("Details", "Company") + "?id=" + companyEditModel.CompanyId);
            }
            catch
            {
                return View();
            }
        }
    }
}