using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Users;
using PrimeActs.Orchestras;
using SearchObject = PrimeActs.Domain.ViewModels.SearchObject;

namespace PrimeActs.UI.Controllers
{
    public class ReportController : PrimeActsController
    {
        private readonly ITicketOrchestra _ticketOrchestra;
        private IApplicationUserOrchestra _applicationUserOrchestra;

        public ReportController(ITicketOrchestra TicketOrchestra, IApplicationUserOrchestra applicationUserOrchestra)
        {
            _ticketOrchestra = TicketOrchestra;
            _applicationUserOrchestra = applicationUserOrchestra;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        // GET: _DailyCash
        [PrimeActsAuthorize(OperationKey = "Report-DailyCash")]
        public ActionResult DailyCash(int? id)
        {
            var panelName = string.Format("DailyCash{0}", id ?? 0);
            ViewBag.PanelName = panelName;

            var model = new DailyCashTicketPagingModel();
            model.PaidTickets = new DailyCashPagingModel() { TicketEditModels = _ticketOrchestra.GetDailyCashTickets(new QueryOptions() { }, new SearchObject() { Paid = true, IsDailyCash = true, FromDate = DateTime.Today.AddDays(-1).AddHours(22), ToDate = DateTime.Now }) };
            model.UnpaidTickets = new DailyCashPagingModel() { TicketEditModels = _ticketOrchestra.GetDailyCashTickets(new QueryOptions() { }, new SearchObject() { Paid = false, IsDailyCash = true, FromDate = DateTime.Today.AddDays(-1).AddHours(22), ToDate = DateTime.Now }) };

            return PartialView("_DailyCash", model);
        }

        [PrimeActsAuthorize(OperationKey = "Report-DailyCashAllocations")]
        public ActionResult DailyCashAllocations()
        {
            var model = new DailyCashTicketAllocationsModel();
            model.TicketEditModels = _ticketOrchestra.GetDailyCashTicketsAll(new QueryOptions() { }, new SearchObject() { Paid = null, IsDailyCash = true, FromDate = DateTime.Today.AddDays(-1).AddHours(22), ToDate = DateTime.Now });
            var customerDepartmentIDs = model.TicketEditModels.Results.Select(t => t.CustomerDepartmentID).Distinct();
            model.SalesLedgerEntries = _ticketOrchestra.GetDailySalesLedgerEntries(new QueryOptions() { }, new SearchObject() { IsDailyCash = true, FromDate = DateTime.Today.AddDays(-1).AddHours(22), ToDate = DateTime.Now });
            var testticketentries = _ticketOrchestra.GetDailyCashTicketsAllViewModel(new QueryOptions() { }, new SearchObject() { Paid = null, IsDailyCash = true, FromDate = DateTime.Today.AddDays(-1).AddHours(22), ToDate = DateTime.Now });
            var ticketViewModel = _ticketOrchestra.GetDailyCashTicketsAllViewModelvw(new QueryOptions() { }, new SearchObject() { Paid = null, IsDailyCash = true, FromDate = DateTime.Today.AddDays(-1).AddHours(22), ToDate = DateTime.Now });
            model.TicketViewModelsVw = ticketViewModel;
            model.TicketEditModelsVw = testticketentries;
            return View("DailyCashAllocation", model);
        }

        public ActionResult DisectionReport()
        {
            DateTime today = DateTime.Today;
            DateTime startDate = DateTime.Today.AddMonths(-1);
            var model = new DisectionReportViewModel();
            model.EndDate = today.ToShortDateString();
            model.StartDate = startDate.ToShortDateString();
            model.UserContextModel = new UserContextModel();
            var user = User.Identity.GetApplicationUser();
            
            model.UserContextModel =
                _applicationUserOrchestra.GetUserContextByUserIDAndController(new Guid(User.Identity.GetUserId()),
                    "Consignment");
            return View("DisectionReport", model);
        }

        public ActionResult DailyBankingUpdate()
        {
            return View("DailyBankingUpdate");
        }

        public ActionResult ConfirmBankedAmount()
        {
            return View("ConfirmBankedAmount");
        }
        
    }
}