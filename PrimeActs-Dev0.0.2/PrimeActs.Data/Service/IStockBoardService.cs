#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.StockBoard;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IStockBoardService : IService<StockBoard>
    {
        
        StockBoard StockBoardByName(string stockBoardName);
        
        List<StockBoard> GetStockBoardsByUserID(Guid userID);
        StockBoard GetStockBoardByID(Guid stockBoardID);
        List<StockBoard> GetStockBoardsByDepartment(Guid userID);
        
        
    }
}