using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace PrimeActs.UI.Controllers
{
    public class JsonTabPanel
    {
        public JsonTabPanel() { }
        public string id { get; set; }
        public string name { get; set; }
        public string contentType { get; set; }
        public string holdingDiv { get; set; }
        public bool isSelected { get; set; }
        public string controllerState { get; set; }
        public string UriParam { get; set; }
    }
    public class TabPanelController : Controller
    {
        private enum ControllerActionStrings
        {
            Company = 1,
            Department = 2,
            Consignment = 3,
            Ticket = 10,
            Produce = 11,
            TransferTicket = 12,
            Supplier = 13,
            //CreateTicket = 14,
            CreateCashTicket = 18,
            CreateCreditTicket = 19,
            CreateReceipt = 20,
            Customer = 15,
            CreatePurchaseInvoice = 16,
            InvoiceAdmin = 17,
            StockBoard = 22,
            StockBoardAdmin = 23,
            AddSupplier = 24,
            ConsignmentIndexTab = 25,
            CompletedConsignmentsTab = 26,
            DailyCash = 27,
            TicketIndexTab = 28,
            AddCustomer = 29,
            ViewSupplier = 30,
            ViewCustomer = 31,
            KoTest = 32
        }

        private readonly ITabPanelOrchestra _tabPanelOrchestra;
        private PrimeActsUserManager _userManager;
        private IUnitOfWorkAsync _unitofWork;

        public TabPanelController(ITabPanelOrchestra tabPanelOrchestra, IUnitOfWorkAsync unitofWork)
        {
           
            _tabPanelOrchestra = tabPanelOrchestra;
           
            
            _unitofWork = unitofWork;
        }
        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        // GET: TabPanel
        // NOTE: After initial load, Menu actions notify the Knockout model to create Panels
        public ActionResult Index(int? Id)
        {
           // ViewBag.TabStateData = GetSimulatedStateData();
           
           ViewBag.TabPanelState = GetPanelDbPanelData();
         
            SetFirstPanelActionString(Id);
           
            return View();
        }
        
        private string GetPanelDbPanelData()
        {
            var user = User.Identity.GetApplicationUser();
            var userid = user.Id;
            _tabPanelOrchestra.Initialize(user);
            var tabPanels = _tabPanelOrchestra.GetAllTabPanels(userid).OrderBy(c => c.Name.Length).ThenBy(c => c.Name); // <= sort order very important, gaps not a problem, are expected, but must be in order
            List<JsonTabPanel> newtabs = new List<JsonTabPanel>();
          
            foreach (var tp in tabPanels)
            {          
                var viewTab = "viewTab" + tp.Name;
                var viewName = tp.HoldingDiv;
                newtabs.Add(new JsonTabPanel()
                {
                    name = viewName,
                    contentType = tp.ContentType,
                    holdingDiv = viewTab,
                    isSelected = tp.IsSelected,
                    controllerState = tp.ControllerState,
                    id = tp.Name,
                    UriParam = tp.UriParam
                });
            }
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            String serializedResult = serializer.Serialize(newtabs);
            return serializedResult;
        }
        
        private void SetFirstPanelActionString(int? Id)
        {
            if (Id.HasValue)
            {
                if (Enum.IsDefined(typeof(ControllerActionStrings), Id))
                {
                    ViewBag.CreateFromPost = Enum.GetName(typeof(ControllerActionStrings), Id);
                   
                    // set friendly name for javascript to get the panel on load
                }
            }            
        }
    }
}
