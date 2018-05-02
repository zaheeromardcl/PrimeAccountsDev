using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;



namespace PrimeActs.Data.Service
{
    public class LiveStock :  ILiveStock
    {

        //private readonly IRepositoryAsync<Produce> _repository;
        private readonly IProduceForTicketService _produceForTicketService;
        //// Singleton instance
        //private readonly static Lazy<LiveStock> _instance = new Lazy<LiveStock>(() => new LiveStock(GlobalHost.ConnectionManager.GetHubContext<LiveStockboardHub>().Clients));

        private readonly List<ProduceQuantityForTicket> _stocks = new List<ProduceQuantityForTicket>();

        private readonly object _updateStockPricesLock = new object();

        //stock can go up or down by a percentage of this factor on each change
        private readonly double _rangePercent = .002;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
        private readonly Random _updateOrNotRandom = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;
         public LiveStock(IHubConnectionContext<dynamic> clients) 
                {
            
            
            if (clients == null) {
                throw new ArgumentNullException("clients");            
            }
            Clients = clients;
            _stocks.Clear();
            List<ProduceQuantityForTicket> stocks = GetAllStocks();
           // List<ProduceQuantityForTicket> stocks = GetAllStocks();
             _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
          

        }


        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public List<ProduceQuantityForTicket> GetAllStocks()
        {
            List<ProduceQuantityForTicket> varStocks = _produceForTicketService.PopulateStockLevels(Guid.Parse("76000200-0000-0070-9204-000068336078"), 2);
            return varStocks;
        }
        public void UpdateStockPrices(object state)
        {

            //foreach (var stock in _stocks.Values)
            //{
            //    if (TryUpdateStockPrice(stock))
            //    {
            //        BroadcastStockPrice(stock);
            //    }
            //}

            //        _updatingStockPrices = false;
               
        }

        public bool TryUpdateStockPrice(ProduceQuantityForTicket stock)
        {
            
            return true;
        }

        public void BroadcastStockPrice(ProduceQuantityForTicket stock)
        {
            Clients.All.updateStockPrice(stock);
        }

    }
}
