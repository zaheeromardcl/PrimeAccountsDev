using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimeActs.Domain.ViewModels.StockBoard
{
    public class StockBoardPagingModel
    {
        public ResultList<StockBoardEditModel> StockBoardEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }
}
