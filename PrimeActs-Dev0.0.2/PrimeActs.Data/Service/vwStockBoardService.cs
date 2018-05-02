#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain.ViewModels.StockBoard;

#endregion

namespace PrimeActs.Data.Service
{
    public class vwStockBoardService : Service<vwStockBoard>, IvwStockBoardService
    {
        private readonly IRepositoryAsync<vwStockBoard> _repository;


        public vwStockBoardService(IRepositoryAsync<vwStockBoard> repository)
            : base(repository)
        {
            _repository = repository;

        }
        public List<vwStockBoard> GetvwStockBoardByDepartmentID(Guid departmentID) 
        {
            List<vwStockBoard> vwStockBoardRecords = _repository.Query(t => t.DepartmentID == departmentID).Select().ToList();
            return vwStockBoardRecords;
        
        }
        public List<vwStockBoard> GetvwStockBoardByProduceGroupID(Guid produceGroupID)
        {
            List<vwStockBoard> vwStockBoardRecords = _repository.Query(t => t.ProduceGroupID == produceGroupID).Select().ToList();
            return vwStockBoardRecords;

        }

        public List<vwStockBoard> GetvwStockBoard(Guid stockBoardID)
        {
            List<vwStockBoard> vwStockBoardRecords = _repository.Query().Select().ToList();
            return vwStockBoardRecords;
        }



     

    }
}