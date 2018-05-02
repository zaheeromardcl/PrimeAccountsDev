#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IvwStockBoardService : IService<vwStockBoard>
    {

        List<vwStockBoard> GetvwStockBoard(Guid stockBoardID);
        List<vwStockBoard> GetvwStockBoardByProduceGroupID(Guid produceGroupID);
        List<vwStockBoard> GetvwStockBoardByDepartmentID(Guid departmentID);
    }
}