using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Data.Service;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels.Users;
using SearchObject = PrimeActs.Domain.ViewModels.SearchObject;

namespace PrimeActs.UI.Controllers
{
    public class TicketController : PrimeActsAuthenticatedController
    {
        private IUnitOfWorkAsync _unitofWork;
        private readonly ITicketOrchestra _ticketOrchestra;
        //private readonly IApplicationRoleOrchestra _applicationRoleOrchestra;
        private IApplicationUserOrchestra _applicationUserOrchestra;

        public TicketController(IUnitOfWorkAsync unitofWork,
            ITicketOrchestra TicketOrchestra, IApplicationUserOrchestra applicationUserOrchestra)//IApplicationRoleOrchestra applicationRoleOrchestra)
        {
            _unitofWork = unitofWork;
            _ticketOrchestra = TicketOrchestra;
            //_applicationRoleOrchestra = applicationRoleOrchestra;
            _applicationUserOrchestra = applicationUserOrchestra;
        }

        public ActionResult Index(int? id, [FromUri] QueryOptions queryOptions, [FromUri] SearchObject searchObject)
        {
            ViewBag.PanelName = string.Format("Ticket{0}", id ?? 0);
            if (string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "CURRENTMONTH";
            var model = _ticketOrchestra.GetTicketPagingModel(queryOptions, searchObject);
            return PartialView(model);
        }

        public ActionResult TicketProduceListMockup() 
        {
            return View();
        }

        public ActionResult IndexTab(int? id)
        {
            ViewBag.PanelName = string.Format("Ticket{0}", id ?? 0);
            var dateTime = DateTime.Today.AddDays(1);
            var ticketPagingModel = new TicketPagingModel();
            if (id.HasValue && id.Value > 0)
                ticketPagingModel = _ticketOrchestra.GetTicketPagingModel(new QueryOptions() { SortOrder = "DESC", SortField = "CreatedDate" },
                        new SearchObject
                        {
                            TicketReference = "",
                            CustomerCode = "",
                            CustomerCompanyName = "",
                            IsDailyCash = false,
                            Paid = false,
                            ToDate = dateTime,
                            FromDate = DateTime.MinValue,
                            RecordsInDays = "CURRENTMONTH"
                        });
            return PartialView("_Tickets", ticketPagingModel);
        }

        public ActionResult Details(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        // this needs to take an array of data
        public ActionResult ReceiptTest(Guid Id)
        {
            //XMLReader readXML = new XMLReader();
            //var data = readXML.RetrunListOfProducts();
            //return View(data.ToList());
            var model = _ticketOrchestra.GetTicketPrintViewModel(Id);
            return View(model);
        }

        public ActionResult PaymentReceipt() {
            return View();
        }

        public ActionResult TicketSummary(Guid id)
        {
            //_aspNetUserOrchestra.Initialize(ApplicationUser);
            return PartialView(_ticketOrchestra.GetTicketOnly(id));
        }

        public ActionResult TicketItemSummary(Guid id)
        {
            return PartialView(_ticketOrchestra.GetTicketItemsOnly(id));
        }

        public ActionResult SearchUI()
        {
            return View();
        }

        //in case you want to switch back to the old way of creating tickets
        //public ActionResult Create(int? id)
        //{
        //    var panelName = string.Format("CreateTicket{0}", id ?? 0);
        //    ViewBag.TicketPanel = panelName;
        //    return PartialView("_Create", _ticketOrchestra.GetCreateTicketViewModel(Guid.Empty));
        //}

        [PrimeActsAuthorize(OperationKey = "Ticket-CreateCashTicket")]
        public ActionResult CreateCashTicket(int? id)
        {
            var panelName = string.Format("CreateCashTicket{0}", id ?? 0);
            ViewBag.TicketPanel = panelName;
            var createTicketViewModel = _ticketOrchestra.GetCreateTicketViewModel(Guid.Empty);
            createTicketViewModel.PaymentTypeList =
                createTicketViewModel.PaymentTypeList.FindAll(type => type.PaymentTypeCode == "C");        
            SetContext(createTicketViewModel);
            return PartialView("_Create", createTicketViewModel);
        }

        private void SetContext(CreateTicketViewModel createTicketViewModel)
        {
            createTicketViewModel.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()), "Ticket");
        }

        [PrimeActsAuthorize(OperationKey = "Ticket-CreateCreditTicket")]
        public ActionResult CreateCreditTicket(int? id)
        {
            var panelName = string.Format("CreateCreditTicket{0}", id ?? 0);
            ViewBag.TicketPanel = panelName;
            var createTicketViewModel = _ticketOrchestra.GetCreateTicketViewModel(Guid.Empty);
            createTicketViewModel.PaymentTypeList =
                createTicketViewModel.PaymentTypeList.FindAll(type => type.PaymentTypeCode == "CR");
            SetContext(createTicketViewModel);           
            return PartialView("_Create", createTicketViewModel);
        }

        [PrimeActsAuthorize(OperationKey = "Ticket-CreateReceipt")]
        public ActionResult CreateReceipt(int? id)
        {
            var panelName = string.Format("CreateReceipt{0}", id ?? 0);
            ViewBag.TicketPanel = panelName;
            var createTicketViewModel = _ticketOrchestra.GetCreateTicketViewModel(Guid.Empty);
            createTicketViewModel.PaymentTypeList =
                createTicketViewModel.PaymentTypeList.FindAll(type => type.PaymentTypeCode == "R");
            SetContext(createTicketViewModel);           
            return PartialView("_Create", createTicketViewModel);
        }

        public ActionResult CreateTransfer(int? id)
        {
            var model = _ticketOrchestra.GetCreateTransferPageViewModel();
            model.UserContextModel = _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()), "Ticket");
            ViewBag.PanelName = string.Format("TransferTicket{0}", id ?? 0);
            return PartialView("_CreateTransfer", model);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult DetailsTab(int tabId, Guid id)
        {
            var viewModel = _ticketOrchestra.GetTicketEditModel(id);
            ViewBag.PanelName = viewModel.TicketReference;
            ViewBag.TicketPanel = string.Format("TicketDetails{0}", tabId);
            return PartialView("_Details", viewModel);
        }

        //[PrimeActsAuthorize(OperationKey = "Customer-TicketIndexTab")]
        public ActionResult TicketIndexTab(int? id)
        {
            //var user = User.Identity.GetApplicationUser();
            StringBuilder panelName = new StringBuilder("Tickets");
            if (id.HasValue) panelName.Append(id);
            ViewBag.TicketsPanel = panelName.ToString();
            return PartialView("_TicketIndexTab");
        }

        public ActionResult ReceiptDetails(int tabId, Guid id)
        {
            ViewBag.ReceiptPanel = string.Format("ReceiptDetails{0}", tabId);
            return PartialView("_ReceiptDetails", _ticketOrchestra.GetReceiptTicketViewModel(id));
        }

        public ActionResult TransferDetails(int tabId, Guid id)
        {
            ViewBag.PanelName = string.Format("TransferDetails{0}", tabId);
            return PartialView("_TransferDetails", _ticketOrchestra.GetTicketEditModel(id));
        }

        public ActionResult UpdateTicket(Guid id)
        {
            return View(); //View(_ticketOrchestra.GetTicketItemViewModel(id));
        }

        public override void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _ticketOrchestra.Initialize(applicationUser);
        }
    }
}
