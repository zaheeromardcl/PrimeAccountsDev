#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain.ViewModels.Produce;

#endregion

namespace PrimeActs.Data.Service
{
    public class ProduceService : Service<Produce>, IProduceService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Produce> _repository;
        private readonly object lockboject = new object();

        public ProduceService(IRepositoryAsync<Produce> repository)
            : base(repository)
        {
            _repository = repository;
            
        }

        public Produce ProduceByName(string produceName)
        {

            
            Produce varProduce = _repository.Query().Select().Where(t => t.ProduceName == produceName).FirstOrDefault();
            return varProduce;

        }

        public Produce ProduceById(Guid Id)
        {
            Produce varProduce = new Produce();
            varProduce = _repository.Query().Select().Where(t => t.ProduceID == Id).SingleOrDefault();
            return varProduce;


          
        }

        public List<Produce> GetProducesByProduceGroupID(Guid produceGroupID)
        {
            List<Produce> varProduces = new List<Produce>();
            varProduces = _repository.Query(x => x.ProduceGroupID == produceGroupID).Select().ToList();
            return varProduces;
        }
        public List<Produce> GetStockBoardProducesByProduceGroupID(Guid produceGroupID)
        {


            List<Produce> varProduces = new List<Produce>();
            varProduces = _repository.Query(pr => pr.ProduceGroupID == produceGroupID && pr.IsActive == true)
                .Include(c => c.ConsignmentItems.Select(x => x.Consignment))
                .Include(ci => ci.ConsignmentItems.Select(ti => ti.TicketItems.Select(t => t.Ticket)))
                .Include(ci => ci.ConsignmentItems.Select(cia => cia.ConsignmentItemArrivals))
                .Select()
                .Where(c => c.ConsignmentItems.Any(t => t.Consignment.CreatedDate > DateTime.Now.AddDays(-7)))
                .Where(c => c.IsActive == true)
                .Where(c => c.ConsignmentItems.Any(cia => cia.Consignment.IsDeleted == false))
                .Where(y => y.ConsignmentItems.Any(x => x.Consignment.IsHistory == false))
                .ToList();
            return varProduces;
        }

        public List<Produce> GetAllProduces()
        {

            List<Produce> AllProduce = _repository.Query().Select().ToList();
            //List<Produce> AllProduce = _repository.Query().Select().ToList();
           
            return AllProduce;
        }


        public List<Produce> GetAllProducesByConsignmentItemDepartmentID(Guid ciDepartmentID)
        {

            List<Produce> AllProduce = _repository.Query()
                .Include(pg => pg.ProduceGroup.ProduceGroupDepartments.Select(z=>z.Department))
                .Select()
                .Where(pg => pg.ProduceGroup.ProduceGroupDepartments.Any(z=>z.DepartmentID == ciDepartmentID))
                .ToList();
            //List<Produce> AllProduce = _repository.Query().Select().ToList();

            return AllProduce;
        }


        public List<Produce> GetProduces(PrimeActs.Domain.ViewModels.QueryOptions queryOptions, SearchObject searchObject,
            out int totalCount)
        {
            totalCount = 0;
            return
                _repository.Query(GetSearchCriteria(searchObject))                
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }

        private Expression<Func<Produce, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Produce, bool>> mainCriteria = c => c.IsActive == true;
            if (!string.IsNullOrEmpty(searchObject.ProduceNameOrCode))
            {
                mainCriteria =
                    mainCriteria.And(
                        c =>
                            c.ProduceCode.StartsWith(searchObject.ProduceNameOrCode) || c.ProduceName.StartsWith(searchObject.ProduceNameOrCode)
                            || c.ProduceCode.EndsWith(searchObject.ProduceNameOrCode) || c.ProduceName.EndsWith(searchObject.ProduceNameOrCode));
            }
            else
            {
                if (!string.IsNullOrEmpty(searchObject.ProduceCode))
                    mainCriteria =
                        mainCriteria.And(
                            c =>
                                c.ProduceCode.StartsWith(searchObject.ProduceCode));
                else
                {
                    if (!string.IsNullOrEmpty(searchObject.ProduceName))
                        mainCriteria =
                            mainCriteria.And(
                                c =>
                                    c.ProduceName.StartsWith(searchObject.ProduceName));
                }
            }
            return mainCriteria;
        }

        private Func<IQueryable<Produce>, IOrderedQueryable<Produce>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Produce>, IOrderedQueryable<Produce>> orderBy = null;
            switch (column)
            {
                case "CreatedDate":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.ProduceID)
                        : orderBy = q => q.OrderByDescending(x => x.ProduceID);
                case "ProduceName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.ProduceName)
                        : orderBy = q => q.OrderByDescending(x => x.ProduceName);
                //case "Supplier":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.SupplierDepartment.Supplier.SupplierCode)
                //        : orderBy = q => q.OrderByDescending(x => x.SupplierDepartment.Supplier.SupplierCode);

                default:
                    return orderBy = q => q.OrderByDescending(x => x.ProduceID);
            }
        }
    }
}