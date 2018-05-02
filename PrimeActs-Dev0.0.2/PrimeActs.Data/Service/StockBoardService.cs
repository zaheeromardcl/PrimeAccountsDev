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
    public class StockBoardService : Service<StockBoard>, IStockBoardService
    {
        private readonly IRepositoryAsync<StockBoard> _repository;
        //private readonly IRepositoryAsync<StockBoardProduceGroup> _repositoryStockBoardProduceGroup;
        //private IRepositoryAsync<StockBoard> sbRepository;
     
        public StockBoardService(IRepositoryAsync<StockBoard> repository)
            : base(repository)
        {
            _repository = repository;
            
        }

        //public StockBoardService(IRepositoryAsync<StockBoard> sbRepository)
        //{
        //    // TODO: Complete member initialization
        //    this.sbRepository = sbRepository;
        //}
         
        public StockBoard StockBoardByName(string stockBoardName)
        {
           StockBoard varStockboard = _repository.Query().Select().Where(t => t.StockBoardName == stockBoardName).FirstOrDefault();
           return varStockboard;
        }


        //public List<StockBoard> GetAllStockBoards(PrimeActs.Domain.ViewModels.QueryOptions queryOptions, PrimeActs.Domain.ViewModels.StockBoard.SearchObject searchObject,
        //    out int totalCount)
        //{
        //    totalCount = 0;
        //    return
        //        _repository.Query()
        //            .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
        //            .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
        //            .ToList();
        //}


        public List<StockBoard>GetStockBoardsByUserID(Guid userID)
        {
            List<StockBoard> varStockBoards = new List<StockBoard>();
            
            //if (selectedDepartmentID = null) {
            //varStockBoards = _repository.Query()
            //    .Include(inc => inc.DepartmentID == defaultDeparmentID)
            //    .Select()
            //    .ToList();
            //}
            //else {
            //varStockBoards = _repository.Query()
            //    .Include(inc => inc.DepartmentID == selectedDepartmentID)
            //    .Select()
            //    .ToList();
            //}
            
            return varStockBoards;
        }
        public List<StockBoard> GetStockBoardsByDepartment(Guid departmentID)
        {
            List<StockBoard> varStockBoards = new List<StockBoard>();
            //varStockBoards = _repository.Query().Select(x => x.DepartmentID == departmentID).ToList();
            

            return varStockBoards;
        }

        public StockBoard GetStockBoardByID(Guid stockBoardID)
        {
            StockBoard varStockBoard = new StockBoard();
            varStockBoard = _repository.Query(x => x.StockBoardID == stockBoardID)

                //.Include(inc => inc.StockBoardProduceGroups)
                .Include(inc => inc.StockBoardProduceGroups.Select(inc2=> inc2.ProduceGroupDepartment.ProduceGroup.Produces))
               .Select()
                .SingleOrDefault();
                                              
            return varStockBoard;
        }

        //public List<StockBoard> GetStockBoardTest(Guid departmentID) --old code not needed.
        //{
        //    //List<StockBoard> varStockBoards = new List<StockBoard>();
        //    List<StockBoard> varStockBoards = _repository.Query().Select().Where(x => x.DepartmentID == departmentID).ToList();

        //    foreach (var q in varStockBoards)
        //    {
        //        List<StockBoardProduceGroup> sbpg = // might need to do new
        //            _repositoryStockBoardProduceGroup.Query().Select().Where(a => a.StockBoardID == q.StockBoardID).ToList();
        //        q.StockBoardProduceGroups = sbpg;
        //    } 

        //    return varStockBoards;
        //}

        //private Expression<Func<StockBoard, bool>> GetSearchCriteria(PrimeActs.Domain.ViewModels.StockBoard.SearchObject searchObject)
        //{
        //    //Expression<Func<StockBoard, bool>> mainCriteria = c;
        //    //if (!string.IsNullOrEmpty(searchObject.StockBoardName))
        //    //{
        //    //    mainCriteria =
        //    //        mainCriteria.And(
        //    //            c =>
        //    //                c.StockBoardName.StartsWith(searchObject.StockBoardName) || c.StockBoardName.StartsWith(searchObject.StockBoardName)
        //    //                || c.StockBoardName.EndsWith(searchObject.StockBoardName) || c.StockBoardName.EndsWith(searchObject.StockBoardName));
        //    //}
        //    //else
        //    //{
        //    //    if (!string.IsNullOrEmpty(searchObject.StockBoardName))
        //    //        mainCriteria =
        //    //            mainCriteria.And(
        //    //                c =>
        //    //                    c.StockBoardName.StartsWith(searchObject.StockBoardName));
        //    //    else
        //    //    {
        //    //        if (!string.IsNullOrEmpty(searchObject.StockBoardName))
        //    //            mainCriteria =
        //    //                mainCriteria.And(
        //    //                    c =>
        //    //                        c.StockBoardName.StartsWith(searchObject.StockBoardName));
        //    //    }
        //    //}
        //    //return mainCriteria;
        //}

        //--not needed as yet - paged query
        //private Func<IQueryable<StockBoard>, IOrderedQueryable<StockBoard>> GetOrder(string column, string ascDesc)
        //{
        //    Func<IQueryable<StockBoard>, IOrderedQueryable<StockBoard>> orderBy = null;
        //    switch (column)
        //    {
        //        case "CreatedDate":
        //            return ascDesc == "ASC"
        //                ? orderBy = q => q.OrderBy(x => x.StockBoardID)
        //                : orderBy = q => q.OrderByDescending(x => x.StockBoardID);
        //        case "StockBoardName":
        //            return ascDesc == "ASC"
        //                ? orderBy = q => q.OrderBy(x => x.StockBoardName)
        //                : orderBy = q => q.OrderByDescending(x => x.StockBoardName);
        //        //case "Supplier":
        //        //    return ascDesc == "ASC"
        //        //        ? orderBy = q => q.OrderBy(x => x.SupplierDepartment.Supplier.SupplierCode)
        //        //        : orderBy = q => q.OrderByDescending(x => x.SupplierDepartment.Supplier.SupplierCode);

        //        default:
        //            return orderBy = q => q.OrderByDescending(x => x.StockBoardID);
        //    }
        //}
    }
}