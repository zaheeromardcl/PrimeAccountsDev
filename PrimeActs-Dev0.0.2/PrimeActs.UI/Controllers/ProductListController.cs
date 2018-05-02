//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using PrimeActs.Helper;
//using PrimeActs.Helper.External;

//namespace PrimeActs.UI.Controllers
//{
//    public class ProductListController : Controller
//    {
        
//        // GET: ProductList
//        public ActionResult Index()
//        {
//            XMLReader readXML = new XMLReader();
//            var data = readXML.RetrunListOfProducts();
//            return View(data.ToList());
//        }
//    }
//}