using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PrimeActs.Domain;
using PrimeActs.Data.Contexts;
using System;
using System.Collections.Concurrent;
using System.Timers;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PrimeActs.Domain.ViewModels.StockBoard;
using PrimeActs.Infrastructure.EntityFramework;


//***********************************************************//
//This class works together with the following: 
//changes made to Startup.Auth.config = to launch the SignalR and register the associated services(hubs)
//Uses base class PrimeActs.Domain.Abstract.Hub - which configures the hub using dependency injection. 
//Uses /Scripts/Typings/hubs.tt (template class which creates interfraces on the fly that work with the typescript classes.
//
//***********************************************************//

namespace PrimeActs.Data.Service
{
    [HubName("liveStockboardMini")]
    public class LiveStockboardHub : PrimeActs.Domain.Abstract.Hub<LiveStockboardHub>
    {

    
        


        //static properties so it can be called
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<LiveStockboardHub>();
        // ...
        public const string GroupLabelPrefix = "Feed_";

        //on update of stock (either consignment/ticket added/edited);
        public static void Refresh()
        {
            ////var groupName = GroupLabelPrefix + feedID;
            ////var group = context.Clients.Group(groupName);
            //////Testing groups
            ////hubContext.Clients.Group.refreshPage();
            //AK: Original code
            hubContext.Clients.All.refreshPage();
        }
        //Constructor

        public LiveStockboardHub() { }

        //[HubMethodName("getAllStocksfromDB")]
        //public List<vwStockBoard> GetAllStocksfromDB()
        //{

        //    List<vwStockBoard> varStocks = new List<vwStockBoard>();



        //    using (var context = new PAndIContext())
        //    {

        //        var sqlStockBoard = "Select * from vwStockBoard  where StockboardID=@StockBoardID and DepartmentID=@DepartmentID ORDER BY ProduceName ASC, DepartmentID ASC";
        //        var userdepartmentID = new SqlParameter("@DepartmentID", Guid.Parse("76000200-0000-0070-9204-000068336078"));
        //        //var numerOfDaysParameter = new SqlParameter("@NumberOfDays", 2);
        //        var stockBoardID = new SqlParameter("@StockBoardID", Guid.Parse("CE04DA11-817F-46E4-A769-0A3D2F8424F3"));



        //        varStocks = context.Database.SqlQuery<vwStockBoard>(sqlStockBoard, userdepartmentID, stockBoardID).ToList();
        //        //foreach(var x in varStocks) {
        //        //   eachProduce.Add(x.RemainingQuantity,x.ProduceName,)

        //        //}   


        //    }


        //    //   Send(varStocks);
        //    //o(eachProduce);
        //    return varStocks;
        //}
       

        public void Register(Guid feedID)
        {
            Groups.Add(Context.ConnectionId, GroupLabelPrefix + feedID);
        }

        public void Unregister(Guid feedID)
        {
            Groups.Remove(Context.ConnectionId, GroupLabelPrefix + feedID);
        }



        //on page load
        [HubMethodName("getProducesforStockBoard")]
        public List<vwStockBoard> GetStockBoardProducesfromDB()
        {

          
            Guid departmentID = Guid.Empty;
            Guid produceGroupID = Guid.Empty;
            Guid mystockboardID = Guid.Parse("D9BF0DFD-9679-4A51-B73C-4D85E793B39B");
            StockBoardModel mystockboard = new StockBoardModel();
            List<vwStockBoard> Results = new List<vwStockBoard>();
            
            /*******************************************************/
            //Starting the group feeds i.e. selection of multiple departments to determine results returned.
            List<vwStockBoard> DepartmentResultsFeed = new List<vwStockBoard>();
            List<Department> departmentFeeds = new List<Department>();
            /*******************************************************/
            /*******************************************************/
            //Starting the group feeds i.e. selection of multiple produce groups to determine results returned.
            List<vwStockBoard> produceGroupResultsFeed = new List<vwStockBoard>();
            List<ProduceGroup> produceGroupfeeds = new List<ProduceGroup>();
            /*******************************************************/
            var stockBoardIDparameter = new SqlParameter("@StockboardID", mystockboardID);
          
            using (var contextvwStockBoard = new PAndIContext()) {

                if (departmentID != Guid.Empty)
                {
                    Results = contextvwStockBoard.Database.SqlQuery<vwStockBoard>("Select * from vwStockBoard where DepartmentID = '76000300-0000-0070-9304-000068336078' order by producename asc").ToList();
                }
                if (produceGroupID != Guid.Empty)
                {
                    Results = contextvwStockBoard.Database.SqlQuery<vwStockBoard>("Select * from vwStockBoard where ProduceGroupID = '76002000-0000-0070-9304-000068336078' order by producename asc").ToList();
                }
                if (mystockboardID != Guid.Empty)
                {
                    string sqlQueryString = "Select * from vwStockBoard where StockboardID=@StockboardID order by producename asc";
                    Results = contextvwStockBoard.Database.SqlQuery<vwStockBoard>(sqlQueryString, stockBoardIDparameter).ToList();
                    
                }
                /************************/
                //Getting a list of departments - in this case one is hard coded but could be more; but needs to be passed from the page somehow
                departmentFeeds.Add(new Department { DepartmentName = "Covent Garden", DepartmentID = Guid.Parse("76000300-0000-0070-9304-000068336078"), DepartmentCode = "C" });
                /************************/

                /************************/
                //Getting a list of producegroups - in this case one is hard coded but could be more; but needs to be passed from the page somehow
                produceGroupfeeds.Add(new ProduceGroup { ProduceGroupName = "SPROUTS", ProduceGroupID = Guid.Parse("76002000-0000-0070-9304-000068336078"), ProduceGroupCode = "C" });
                /************************/

            }
                /*******************************************************/
                //AK: This is to get back stockboard results for any department(s) selected
                /*******************************************************/
                    foreach (Department d in departmentFeeds)
                    {
                        foreach (vwStockBoard sb in Results)
                        {
                            if (sb.DepartmentID == d.DepartmentID) {
                                DepartmentResultsFeed.Add(sb);
                            }
                        }

                    }
                    //DepartmentResultsFeed is not returned currently - need to implement on view.
                 /*******************************************************/

                /*******************************************************/
                //AK: This is to get back stockboard results for any producegroup(s) selected
                /*******************************************************/
                    foreach (ProduceGroup pg in produceGroupfeeds)
                    {
                        foreach (vwStockBoard sb in Results)
                        {
                            if (sb.ProduceGroupID== pg.ProduceGroupID)
                            {
                                produceGroupResultsFeed.Add(sb);
                            }
                        }

                }
                //DepartmentResultsFeed is not returned currently - need to implement on view.
                /*******************************************************/  
            
            //return results just for this stockboardID
            return Results;


        }
         
    
    }

    
}
