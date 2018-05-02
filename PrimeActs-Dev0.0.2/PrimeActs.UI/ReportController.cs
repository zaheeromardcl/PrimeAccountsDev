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
    public class ReportController : Controller
    {
        
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        // GET: _DailyCash
        public ActionResult DailyCash()
        {
            return PartialView("_DailyCash");
        }

        
    }
}