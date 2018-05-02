using Microsoft.AspNet.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.Abstract
{
    public abstract class Hub<T> : Hub where T : Hub
    {
        private static IHubContext hubContext;
        private Infrastructure.EntityFramework.IRepositoryAsync<StockBoard> repository;
        public Hub() { }
        //public Hub(Infrastructure.EntityFramework.IRepositoryAsync<StockBoard> repository)
        //{
        //    // TODO: Complete member initialization
        //    this.repository = repository;
        //}
        /// <summary>Gets the hub context.</summary>
         //<value>The hub context.</value>
        public static IHubContext HubContext
        {
            get
            {
                if (hubContext == null)
                    hubContext = GlobalHost.ConnectionManager.GetHubContext<T>();
                return hubContext;
            }
        }
    }
}