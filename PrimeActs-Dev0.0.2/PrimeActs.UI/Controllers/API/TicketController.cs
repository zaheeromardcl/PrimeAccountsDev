#region


using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using PrimeActs.Data.Service;

#endregion

namespace PrimeActs.UI.Controllers.API
{
    public class TicketController : ApiController
    {
        private readonly ITicketOrchestra _TicketOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;
        private readonly IPrintOrchestra _printOrchestra;
        private readonly IPrinterOrchestra _printerOrchestra;

        public TicketController(IUnitOfWorkAsync unitofWork,
            ITicketOrchestra TicketOrchestra,
            IPrintOrchestra printOrchestra,
            IPrinterOrchestra printerOrchestra)
        {
            _unitofWork = unitofWork;
            _TicketOrchestra = TicketOrchestra;
            _printOrchestra = printOrchestra;
            _printerOrchestra = printerOrchestra;
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        [HttpGet]
        public ResultList<TicketEditModel> Index([FromUri] QueryOptions queryOptions, [FromUri] PrimeActs.Domain.ViewModels.SearchObject searchObject)
        {
            if(string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "CURRENTMONTH";
            try
            {
                var tickets = _TicketOrchestra.GetTickets(queryOptions, searchObject);
                return tickets;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public ResultList<DailyCashTicketModel> DailyCashTicketPaidIndex([FromUri] QueryOptions queryOptions, [FromUri] PrimeActs.Domain.ViewModels.SearchObject searchObject)
        {
            if(string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "Yesterday10PM";
            try
            {
                searchObject.Paid = true;
                var tickets = _TicketOrchestra.GetDailyCashTickets(queryOptions, searchObject);
                return tickets;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public ResultList<DailyCashTicketModel> DailyCashTicketUnpaidIndex([FromUri] QueryOptions queryOptions, [FromUri] PrimeActs.Domain.ViewModels.SearchObject searchObject)
        {
            if (string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "Yesterday10PM";
            try
            {
                searchObject.Paid = false;
                var tickets = _TicketOrchestra.GetDailyCashTickets(queryOptions, searchObject);
                return tickets;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        [HttpPost]
        public TicketEditModel CreateReceiptTicket(TicketEditModel model)
        {
            PopulateUser();
            
            var salesLedgerEntry = _TicketOrchestra.CreateSalesLedgerEntry(model);
            model.TicketID = salesLedgerEntry.SalesLedgerEntryID;
            _unitofWork.SaveChanges();
            return model;
        }
         
        [HttpPost]
        public TicketEditModel CreateTicket(TicketEditModel model)
        {
            PopulateUser();
            if (model.TicketID == null || model.TicketID == Guid.Empty)
            {
                _TicketOrchestra.CreateTicket(model);
            }
            else
            {
                _TicketOrchestra.UpdateTicket(model);
            }

            if (model.TicketItems.Count != 0)
            {
                foreach (var ticketItem in model.TicketItems)
                {
                    ticketItem.TicketID = model.TicketID;
                    ticketItem.TicketItemDescription = ticketItem.TicketItemBrand + ticketItem.Produce;

                    _TicketOrchestra.CreateTicketItem(ticketItem);
                }
            }

            //Loop round the ticket items in ticket and save to the database.
            _unitofWork.SaveChanges();
            return _TicketOrchestra.GetTicketEditModel(model.TicketID);
        }

        [HttpPost]
        public TicketEditModel UpdateTicket(TicketEditModel model)
        {
            PopulateUser();
            try
            {
                _TicketOrchestra.UpdateTicket(model);
                _unitofWork.SaveChanges();
                return model;
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public TicketEditModel SaveTicket(TicketEditModel model)
        {
            PopulateUser();
            try
            { // ERROR when saving Cash Ticket
                model.UpdatedDate = DateTime.Now.ToString();
                model.TicketDate = model.TicketDate;
                model.CreatedDate = model.CreatedDate;
                

               _TicketOrchestra.SaveTicket(model);
                _unitofWork.SaveChanges();

                /*********************************************************/
                /*start*/
                /*********************************************************/
                //adding in code that updates SignalRHub for stockboard
                //LiveStockboardHub.HubContext.Clients.All.Refresh();
                LiveStockboardHub.Refresh();                               
                /*********************************************************/
                /*end*/
                /*********************************************************/
                return _TicketOrchestra.GetTicketEditModel(model.TicketID);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public HttpResponseMessage PrintTicket(Guid id)
        {
            PopulateUser();
            var viewModel = _TicketOrchestra.GetTicketPrintViewModel(id);
            var numberOfPrinters = _printerOrchestra.GetPrintersForUserSelectedDepartment().Count;
            var result = true;
            //var result = _printOrchestra.PrintReceipt(viewModel);

            if (numberOfPrinters > 0) // interim solution to prevent tickets going to default Printer
            {
                result = _printOrchestra.PrintReceipt(viewModel);
            }

            HttpResponseMessage response = Request.CreateResponse(
                result ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable,
                viewModel);
            return response;
        }

        //[HttpPost]
        //public HttpResponseMessage PrintCashTicket(Guid id)
        //{
        //    PopulateUser();
        //    var viewModel = _TicketOrchestra.GetTicketPrintViewModel(id);
        //    var result = _printOrchestra.PrintCashReceipt(viewModel);

        //    HttpResponseMessage response = Request.CreateResponse(
        //        result ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable,
        //        viewModel);
        //    return response;
        //}

        [HttpPost]
        public HttpResponseMessage PrintReceiptTicket(Guid id)
        {
            PopulateUser();
            var viewModel = _TicketOrchestra.GetReceiptTicketViewModel(id);
            var numberOfPrinters = _printerOrchestra.GetPrintersForUserSelectedDepartment().Count;
            var result = true;

            if (numberOfPrinters > 0) // interim solution to prevent tickets going to default Printer
            {
                 result = _printOrchestra.PrintReceiptTicket(viewModel);
            }

            HttpResponseMessage response = Request.CreateResponse(
                result ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable);
            return response;
        }

        [HttpPost]
        public TicketItemEditModel UpdateTicketItem(TicketItemEditModel model)
        {
            _TicketOrchestra.UpdateTicketItem(model);
            _unitofWork.SaveChanges();
            return model;
        }

        [HttpPost]
        public TicketItemEditModel CreateTicketItem(TicketItemEditModel model)
        {
            PopulateUser();
            _TicketOrchestra.CreateTicketItem(model);
            model.OriginalTicketItemID = model.TicketItemID;
            _unitofWork.SaveChanges();
            return model;
        }

        [HttpPost]
        public TicketItemEditModel SaveTicketItem(TicketItemEditModel model)
        {
            PopulateUser();
            if (model.TicketItemID == Guid.Empty)
            {
                _TicketOrchestra.CreateTicketItem(model);
            }
            else
            {
                _TicketOrchestra.UpdateTicketItem(model);
            }
            model.OriginalTicketItemID = model.TicketItemID;
            _unitofWork.SaveChanges();
            /*********************************************************/
            /*start*/
            /*********************************************************/
            //adding in code that updates SignalRHub for stockboard
        //    LiveStockboardHub.Refresh();                               
            /*********************************************************/
            /*end*/
            /*********************************************************/
            return model;
        }

        [HttpPost]
        public List<TicketItemEditModel> SaveTicketItems(List<TicketItemEditModel> models)
        {
            if (models == null && models.Count() <= 0)
            {
                return models;
            }

            for (var i = 0; i < models.Count; i++)
            {
                models[i].ConsignmentItemID = models[i].ConsignmentItemID;
                //models[i].SupplierID = models.;
           
                if (models[i].TicketItemID != null && models[i].TicketItemID != Guid.Empty)
                    _TicketOrchestra.UpdateTicketItem(models[i]);
                else
                    _TicketOrchestra.CreateTicketItem(models[i]);
            }
            _unitofWork.SaveChanges();




            return models;
        }
        
        
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Ticket> GetAllCustomerDepartmentTickets(Guid CustomerDepartmentID)
        {

            var customerDepartmentTickets = _TicketOrchestra.GetAllCustomerDepartmentTickets(CustomerDepartmentID);
            return customerDepartmentTickets;

        }


        [HttpPost]
        public HttpResponseMessage RemoveTicketItem(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                _TicketOrchestra.RemoveTicketItem(id.Value);
                _unitofWork.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public TicketEditModel CreateTransferTicket(TransferCreateViewModel model)
        {
            PopulateUser();
            var ticketId = _TicketOrchestra.CreateTransferTicket(model);
            _unitofWork.SaveChanges();
            return _TicketOrchestra.GetTicketEditModel(ticketId);
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _TicketOrchestra.Initialize(applicationUser);
            _printOrchestra.Initialize(applicationUser);
            _printerOrchestra.Initialize(applicationUser);
        }
    }
}