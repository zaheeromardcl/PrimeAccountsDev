#region

using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Invoice;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Data.Service;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain;
using System.Web.Script.Serialization;

#endregion

namespace PrimeActs.UI.Controllers
{
    public class InvoiceController : PrimeActsAuthenticatedController
    {
        private readonly ManageEventOrchestra _manageEventOrchestra;
        private readonly IDivisionOrchestra _divisionOrchestra;
        private readonly PrimeActs.Orchestra.IInvoiceOrchestra _invoiceOrchestra;
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly ICompanyOrchestra _compOrchestra;
        private IDivisionService _divService;
        private IUnitOfWorkAsync _unitofWork;
        
        public InvoiceController(IUnitOfWorkAsync unitofWork, IDivisionOrchestra divisionOrchestra, PrimeActs.Orchestra.IInvoiceOrchestra invoiceOrchestra, IApplicationUserOrchestra applicationUserOrchestra, ICompanyOrchestra compOrchestra, IDivisionService divService)
        {
            
            //_manageEventOrchestra = manageEventOrchestra;
            _divisionOrchestra = divisionOrchestra;
            _invoiceOrchestra = invoiceOrchestra;
            _applicationUserOrchestra = applicationUserOrchestra;
             _compOrchestra = compOrchestra;
             _divService = divService;
            _unitofWork = unitofWork;
        }

        //[PrimeActsAuthorize(OperationKey = "Invoice-Details")]
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var invoiceDetailModel = _invoiceOrchestra.GetInvoiceDetailViewModel(id);
            return View(invoiceDetailModel);
        }

        //[PrimeActsAuthorize(OperationKey = "Invoice-Details")]
        [HttpGet]
        public ActionResult DetailsByRef(string salesReferenceNumber)
        {
            var invoiceDetailModel = _invoiceOrchestra.GetInvoiceDetailViewModelByRef(salesReferenceNumber);
            return View("Details", invoiceDetailModel);
        }

        public ActionResult Index(int page = 1, int pageSize = 10, string searchString = "")
        {
            return
                View(_invoiceOrchestra.GetInvoicesPagingModel(new QueryOptions(),
                    new PrimeActs.Domain.ViewModels.Invoice.SearchObject
                    {
                        ToDate = null,
                        CustomerDepartmentName = "",
                        FromDate = null,                        
                        SalesInvoiceReference = "",
                        TicketReference="",
                        ConsignmentReference="",
                        RecordsInDays = "CURRENTMONTH"
                    }));
        }

        public ActionResult RetrieveImage(Guid id)
        {

            byte[] pic = _invoiceOrchestra.GetLogo(id);

            if (pic != null)
                return File(pic, "image/jpg");

            return null;
        }

        //[PrimeActsAuthorize(OperationKey = "Invoice-Execute")]
        public ActionResult RunInvoice()
        {
            var user = User.Identity.GetApplicationUser();

            var invoicModel = new InvoiceModel
            {
                InvoiceRunModel = new InvoiceRunModel { Username = user.UserName},
                ShowRunInvoice = true,
                DivisionList = _divService.GetAllDivisions().OrderBy(x => x.DivisionName).Select(x => new ItemViewModel
                {
                    Id = x.DivisionID.ToString(),
                    value = x.DivisionID.ToString(),
                    label = x.DivisionName,
                    Description = x.DivisionName,
                    Code = x.DivisionName,
                }).ToList()
            };

            return View(invoicModel);
        }

        //[PrimeActsAuthorize(OperationKey = "Invoice-Execute")]        
        [System.Web.Http.HttpPost]
        public ActionResult ExecuteRunInvoice(InvoiceModel invoiceModel)
        {
            var user = User.Identity.GetApplicationUser();
            _invoiceOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
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

            //var vm = _invoiceOrchestra.ExecuteInvoice(invoiceModel);
            InvoiceModel vm = null;

            try
            {
                _unitofWork.BeginTransaction();
                 vm = _invoiceOrchestra.ExecuteInvoice(invoiceModel);
                _unitofWork.SaveChanges();
                _unitofWork.Commit();
            }
            catch (Exception ex)
        {
            _unitofWork.Rollback();
                //throw;
            }
            return Json(vm);
        }
                
        [System.Web.Http.HttpPost]
        public ActionResult Test()
        {
            var user = User.Identity.GetApplicationUser();
            _invoiceOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
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

            //var vm =
            //_invoiceOrchestra.TestInvoiceTransactions();
            try
            {
                _unitofWork.BeginTransaction();
                //_invoiceOrchestra.TestInvoiceTransactions();
                _unitofWork.SaveChanges();
               // _invoiceOrchestra.TestInvoiceTransactions();
                _unitofWork.SaveChanges();
                dynamic n1 = "test";
                int n2 = n1; // runtime error test
                _unitofWork.Commit();
                
            }
            catch (Exception ex)
            {
                
                _unitofWork.Rollback();
                //throw;
            }
            return View();
        }

        public ActionResult CreateSundry()
        {
            return View();
        }

        public ActionResult CreateManual()
        {
            return View();
        }

        public override void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            _invoiceOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
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