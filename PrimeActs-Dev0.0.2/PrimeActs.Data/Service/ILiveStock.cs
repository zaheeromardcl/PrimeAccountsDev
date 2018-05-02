using System;

using PrimeActs.Domain;
using System.Collections.Generic;

namespace PrimeActs.Data.Service
{
    public interface ILiveStock
    {
        List<ProduceQuantityForTicket> GetAllStocks();
        void UpdateStockPrices(object state);
        bool TryUpdateStockPrice(ProduceQuantityForTicket stock);
        void BroadcastStockPrice(ProduceQuantityForTicket stock);
        
       
        
    }
}
