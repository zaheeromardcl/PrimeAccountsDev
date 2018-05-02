using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;



namespace PrimeActs.Domain
{
    [HubName("liveStockboardMini")]
    public class LiveStockboardHub : Hub
    {

        private readonly LiveStock _liveStock;

        public LiveStockboardHub() : this(LiveStock.Instance) { }

        public LiveStockboardHub(LiveStock liveStock)
        {
            _liveStock = liveStock;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _liveStock.GetAllStocks();
        }
    }
}
   