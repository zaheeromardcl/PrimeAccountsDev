#region

using System;
using System.Collections.Generic;
using System.Drawing.Printing;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class StockBoardModel
    {
        public List<ProduceGroupModel> ProduceGroups { get; set; }

        public StockBoardModel()
        {
            ProduceGroups = new List<ProduceGroupModel>();
        }
    }

    public class ProduceGroupModel
    {
        public string ProduceGroupId { get; set; }
        public string ProduceGroupName { get; set; }
        public List<ProduceModel> ProduceItems { get; set; }

        public ProduceGroupModel()
        {
            ProduceItems = new List<ProduceModel>();
        }
    }

    public class ProduceModel
    {
        public string ProduceName { get; set; }
        public decimal ExpectedQuantity { get; set; }
        public decimal TicketItemQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
    }
}