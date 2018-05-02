#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.StockBoard;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain.ViewModels;




#endregion

namespace PrimeActs.Orchestras
{
    public interface IStockBoardOrchestra
    {

        
        List<ProduceQuantityForTicket> GetAllStocks();
        List<StockBoard> GetAllStockBoardsByUser(Guid userID);
        StockBoardPagingModel GetStockBoardPagingModel(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.StockBoard.SearchObject searchObject);
        List<StockBoardProduce> GetStockBoardProducesByStockBoardID(Guid stockBoardID);
          

    }

    //
      
    public class StockBoardOrchestra : IStockBoardOrchestra
    {
        private readonly IProduceForTicketService _produceForTicketService;
        private readonly IStockBoardService _stockBoardService;


        public StockBoardOrchestra(IProduceForTicketService produceForTicketService,IStockBoardService stockBoardService)
        {
            _produceForTicketService = produceForTicketService;
            _stockBoardService = stockBoardService;
        }

        public List<StockBoard> GetAllStockBoardsByUser(Guid userID) 
        {
              var stockBoards = _stockBoardService.GetStockBoardsByUserID(userID).ToList();
              return stockBoards;
        
        }

       

        public List<ProduceQuantityForTicket> GetAllStocks()
        {

            List<ProduceQuantityForTicket> varStocks = _produceForTicketService.PopulateStockLevels(Guid.Parse("76000200-0000-0070-9204-000068336078"), 2);
            return varStocks;
        }

        public StockBoardPagingModel GetStockBoardPagingModel(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.StockBoard.SearchObject searchObject)
        {
        //    var totalCount = 0;
        //    var stockBoardPagingModel = new StockBoardPagingModel();
        //    var stockBoards = _stockBoardService.GetAllStockBoards(queryOptions, searchObject, out totalCount);
        //    queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
        //    //This line gets rid of items!! Fix the error
        //    var result = new ResultList<StockBoardEditModel>(stockBoards.Select(BuildStockBoardEditModel).ToList(),
        //        queryOptions);
        //    stockBoardPagingModel.StockBoardEditModels = result;
        //    stockBoardPagingModel.SearchObject = new PrimeActs.Domain.ViewModels.StockBoard.StockBoardPagingModel.SearchObject
        //    {
        //        StockBoardName = searchObject.StockBoardName
        //    };
          //  return stockBoardPagingModel;
            return null;
        }

        public List<StockBoardProduce> GetStockBoardProducesByStockBoardID(Guid stockBoardID)
        {
            
            //var stockboard = _stockBoardService.GetStockBoardByID(stockBoardID);
            //int relatedConsTotal=0;
            //int relatedTicketItemTotal = 0;
            //List<ProduceGroup> producegroupList = stockboard.StockBoardProduceGroups();
            //List<Produce> produceList = new List<Produce>();
            //List<StockBoardProduce> varStockBoardProduces = new List<StockBoardProduce>();
            //List<ConsignmentItem> produceConsignmentItems = new List<ConsignmentItem>();
            //List<TicketItemService> produceTicketItems = new List<TicketItem>();
            //foreach (ProduceGroup x in producegroupList){
            //    varStockBoardProduces[x].ProduceGroupID = x.ProduceGroupID;
            //    foreach (Produce y in x.Produces) {
            //        varStockBoardProduces[y].ProduceID = y.ProduceID;
            //        foreach (ConsignmentItem z in produceConsignmentItems)
            //        {
            //            varStockBoardProduces[y].RelatedConsignments.Add(z.Consignment);
            //            foreach (ConsignmentItemArrival cia in z)
            //            {
            //                relatedConsTotal = relatedConsTotal + cia.QuantityReceived;
            //            }
            //        }
                    
                    
            //        foreach (TicketItem ti in produceTicketItems){
            //            varStockBoardProduces[y].RelatedTickets.Add(ti.Ticket);   
            //            relatedTicketItemTotal = relatedTicketItemTotal + ti.TicketItemQuantity;
            //        }
                    
            //            varStockBoardProduces[y].RemainignQty = relatedConsTotal-relatedTicketItemTotal;
                    
            //    }   
            //}
            //return varStockBoardProduces;
            return null;
            
           
            
        }

        private StockBoard BuildStockBoardModel(List<StockBoard> stockBoards)
        {
            //var stockBoards = new StockBoard();
            ////{
            ////    ProduceGroups = stockBoards.GroupBy(x => x.ProduceGroupName).Select(group => BuildProduceGroupModel(group)).ToList()
            ////};
            return null;
        }

        private StockBoardEditModel BuildStockBoardEditModel(List<StockBoardEditModel> produceGroups)
        {
            //var stockBoardModel = new StockBoardEditModel
            ////{
            ////    ProduceGroups = stockBoardModel.GroupBy(x => x.ProduceGroupName).Select(group => BuildProduceGroupModel(group)).ToList()
            ////};
            return null;        }

        
        private ProduceGroupModel BuildProduceGroupModel(IEnumerable<ProduceGroup> produceGroups)
        {
            //var produceGroups = new ProduceGroupModel
            //{
            //    ProduceGroupId = "id" + produceQuantitiesForGroup.First().ProduceGroupID.ToString("N"),
            //    ProduceGroupName = produceQuantitiesForGroup.First().ProduceGroupName,
            //    ProduceItems = produceQuantitiesForGroup.Select(x => BuildProduceModel(x)).ToList()
            //};
            return null;
        }

        private ProduceModel BuildProduceModel(ProduceQuantityForTicket produceQuantity)
        {
            var produceName = produceQuantity.ProduceName + " (" + produceQuantity.ProduceCode + ") " + produceQuantity.ItemBrand;
            if (!string.IsNullOrEmpty(produceQuantity.ItemPackType))
                produceName += " - " + produceQuantity.ItemPackType;
            if (!string.IsNullOrEmpty(produceQuantity.ItemPackSize))
                produceName += " - " + produceQuantity.ItemPackSize;

            var produceModel = new ProduceModel
            {
                ProduceName = produceName,
                //ExpectedQuantity = 12,
                QtyAvailable = (decimal)produceQuantity.TicketItemQuantity,
               // QtyStock= (decimal)produceQuantity.RemainingQuantity
            };
            return produceModel;
        }
    }
}