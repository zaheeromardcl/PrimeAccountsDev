using Microsoft.ServiceBus;
using PrimeActs.Data.Service;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Orchestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

namespace PrimeActs.UI.Controllers
{
 public class DemoController : Controller
    {
       private readonly IStockBoardOrchestra _stockBoardOrchestra;
        private readonly IPrintOrchestra _printOrchestra;

        public DemoController(IPrintOrchestra printOrchestra, IStockBoardOrchestra stockBoardOrchestra)
        {
            _stockBoardOrchestra = stockBoardOrchestra;
            _printOrchestra = printOrchestra;
        }

        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RightMenu()
        {
            return PartialView();
        }
        public ActionResult TopMenu()
        {
            return PartialView();
        }

        public ActionResult ConsignmentHub()
        {
            return View();
        }

        public ActionResult StockBoardStatic()
        {
            return View();
        }

        public ActionResult LiveStockBoard()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult StockBoard()
        {
        //    // Default to P & I, TODO: Change to current user's division id
            var userDivisionId = Guid.Parse("8510D575-22BB-4E12-B71C-1487B722FE35");
            //var model = _stockBoardOrchestra.GetStockBoardModel(userDivisionId, 365);
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Departures()
        {
            return View();
        }
        public ActionResult DeparturesTesting()
        {
            return View();
        }
        public ActionResult TabbedLayout()
        {
            return View();
        }

        public ActionResult Print()
        {
            return View();
        }

        public ActionResult RawPrint()
        {
            return View();
        }
        public ActionResult Consignment()
        {
            return View();
        }
        public ActionResult Ticket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PrintTestPage()
        {
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            _printOrchestra.PrintTest();
            return View();
        }
        [HttpPost]
        public ActionResult PrintRawTestPage()
        {
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            _printOrchestra.RawPrintTest();
            return View();
        }
        public ActionResult PrintTestDisection()
        {
            
           // _printOrchestra.Initialize(User.Identity.GetApplicationUser());
          //  _printOrchestra.DisectionPrintTest();
            return View();
        }

        [HttpPost]
        public ActionResult PrintDisection(DisectionReportViewModel disectionReportView)
        {
            var startdate = disectionReportView.StartDate;
            var enddate = disectionReportView.EndDate;
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            _printOrchestra.DisectionPrint(disectionReportView, true);
            //return View();
            return Json(true);
        }

        [HttpPost]
        public ActionResult PrintDisectionPost(DisectionReportViewModel disectionReportView)
        {
            var test = 1;
            return Json(true);
        }


        public FileResult RunDisplayPrintDisection(DisectionReportViewModel disectionReportView)
        {
            var startdate = disectionReportView.StartDate;
            var enddate = disectionReportView.EndDate;
            //disectionReportView.DepartmentId == null ? default(Guid) : Guid.Parse(disectionReportView.DepartmentId);
            
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            //var returnfile = @"C:\Users\Public\TestFolder\TestDisection.txt";// 
            var returnfile = _printOrchestra.DisectionPrint(disectionReportView, false);
            return new FilePathResult(returnfile,"text/plain");
        }
        public FileResult DisplayPrintDisection(DisectionReportViewModel disectionReportView)
        {
            var startdate = disectionReportView.StartDate;
            var enddate = disectionReportView.EndDate;
            if (startdate == enddate)
            {
                int n = 0;
            }

            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            var returnfile = @"C:\Users\Public\TestFolder\TestDisection.txt";// 
            //var returnfile = _printOrchestra.DisectionPrintTest(false);
            return new FilePathResult(returnfile, "text/plain");
        }

        public FileResult TestFileResult()
        {
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            var returnfile = @"C:\Users\Public\TestFolder\TestDisection.txt";// _printOrchestra.DisectionPrintTest();
            return new FilePathResult(returnfile, "text/plain");
        }

        public ActionResult PrintTestDailySales()
        {
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            //_printOrchestra.RawPrintCondensedTest();
           // DateTime runDate = DateTime.Parse("19/11/2016");
            DateTime runDate = DateTime.Parse("11/01/2017");
            _printOrchestra.PrintDailySalesReport(runDate);
            return View();
        }
     
        
        //[HttpPost]
        public ContentResult PostTestCSV()
        {
            _printOrchestra.Initialize(User.Identity.GetApplicationUser());
            string csvFile = _printOrchestra.CSVGenericTest();

            //UTF8Encoding encoding = new UTF8Encoding();
            //byte[] contentAsBytes = encoding.GetBytes("this is text content");

            //this.HttpContext.Response.ContentType = "text/plain";
            //this.HttpContext.Response.AddHeader("Content-Disposition", "filename=" + "text.txt");
            //this.HttpContext.Response.Buffer = true;
            //this.HttpContext.Response.Clear();
            //this.HttpContext.Response.OutputStream.Write(contentAsBytes, 0, contentAsBytes.Length);
            //this.HttpContext.Response.OutputStream.Flush();
            //this.HttpContext.Response.End();
            //return View("TestCSV");
            return Content(csvFile, "text/csv");
        }
    }
}