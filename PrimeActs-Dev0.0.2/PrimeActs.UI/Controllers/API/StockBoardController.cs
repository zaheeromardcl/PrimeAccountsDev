using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using System;
using System.Web;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.StockBoard;
using System.Data.SqlClient;
using PrimeActs.Data.Contexts;


namespace PrimeActs.UI.Controllers.API
{
    public class StockBoardController: ApiController
    {
         private readonly IStockBoardOrchestra _stockBoardOrchestra;
        private IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;


        public StockBoardController(IStockBoardOrchestra stockBoardOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _stockBoardOrchestra = stockBoardOrchestra;
            _unitofWork = unitofWork;
        }


        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<vwStockBoard> LatestStocks()
        {
            Guid mystockboardID = Guid.Parse("D9BF0DFD-9679-4A51-B73C-4D85E793B39B");
           
            List<vwStockBoard> Results = new List<vwStockBoard>();
            var stockBoardIDparameter = new SqlParameter("@StockboardID", mystockboardID);
            using (var contextvwStockBoard = new PAndIContext())
            {

                
                if (mystockboardID != Guid.Empty)
                {
                    string sqlQueryString = "Select * from vwStockBoard where StockboardID=@StockboardID order by producename asc";
                    Results = contextvwStockBoard.Database.SqlQuery<vwStockBoard>(sqlQueryString, stockBoardIDparameter).ToList();

                }

            }

            return Results;
        }
        
        
        [HttpGet]
        public ResultList<StockBoardEditModel> Index([FromUri] QueryOptions queryOptions,
            [FromUri] PrimeActs.Domain.ViewModels.StockBoard.SearchObject searchObject)
        {
            return null;//s _stockBoardOrchestra.GetStockBoardModel(queryOptions, searchObject);
        }

        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }



    }
}