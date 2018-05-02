using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimeActs.Data.Service;
using PrimeActs.Orchestras;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using Microsoft.AspNet.SignalR;
using PrimeActs.Domain.ViewModels.StockBoard;

namespace PrimeActs.UI.Controllers
{


    public class StockboardController : PrimeActsController
    {

        private readonly IStockBoardOrchestra _stockBoardOrchestra;
        private readonly IPrintOrchestra _printOrchestra;

        public StockboardController(IPrintOrchestra printOrchestra, IStockBoardOrchestra stockBoardOrchestra)
        {
            _stockBoardOrchestra = stockBoardOrchestra;
            _printOrchestra = printOrchestra;
        }

        // GET: InvoiceAdmin
        [HttpGet]
        //public ActionResult Index(int id)
        //{
        //    ViewBag.PanelName = string.Format("StockBoard{0}", id.ToString());
        //    return
        //        PartialView("Index");
        //}

        public ActionResult DisplayStockboard()
        {
            //var hub = GlobalHost.ConnectionManager.GetHubContext<StockBoardHub>();
            //var helper = new PrimeActs.Domain.HubHelper(hub);
            //helper.DoStuff("controller stuff");
            return View();
        }

  
        public ActionResult LiveStockBoard()
        {
            return PartialView();
        }
        //     public ActionResult StockboardUpdating()
        //{


        //    /*********************************************************/
        //    /*start*/
        //    /*********************************************************/
        //    //adding in code that updates SignalRHub for stockboard
            
        //    //LiveStockboardHub.Send();
               
            
               

                 

            

        //    /*********************************************************/
        //    /*end*/
        //    /*********************************************************/
        //         return View();
        //}
        
    }
}