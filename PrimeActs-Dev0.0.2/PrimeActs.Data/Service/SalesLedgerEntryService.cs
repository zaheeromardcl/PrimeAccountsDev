#region

using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ISalesLedgerEntryService : IService<SalesLedgerEntry>
    {
        SalesLedgerEntry FindForTicket(Guid ticketId);
        SalesLedgerEntry FindById(Guid id);
        List<SalesLedgerEntry> GetDailySalesLedgerEntries(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
    }

    public class SalesLedgerEntryService : Service<SalesLedgerEntry>, ISalesLedgerEntryService
    {
        private readonly IRepositoryAsync<SalesLedgerEntry> _repository;
        private readonly string _type;

        public SalesLedgerEntryService(IRepositoryAsync<SalesLedgerEntry> repository)
            : base(repository)
        {
            _repository = repository;
            _type = typeof (SalesLedgerEntry).FullName;
        }

        public SalesLedgerEntry FindForTicket(Guid ticketId)
        {
            var rtnVal = _repository.Query()//sle => sle.SalesInvoices.Any(si => si.SalesInvoiceItems.Any(sli => sli.TicketItem.TicketID == ticketId)))
                    .Select()
                    .FirstOrDefault();
            return rtnVal;
        }

        public SalesLedgerEntry FindById(Guid id)
        {
            return _repository.Query(sle => sle.SalesLedgerEntryID == id)
                .Include(inc => inc.CustomerDepartment)
                .Include(inc => inc.CustomerDepartment.Customer)
                .Include(inc => inc.Currency)
                .Include(inc => inc.Note)
                .Select()
                .FirstOrDefault();
        }

        public List<SalesLedgerEntry> GetDailySalesLedgerEntries(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            var result = _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.CustomerDepartment)
                    .Include(inc => inc.CustomerDepartment.Customer)
                    .Include(inc => inc.SalesPerson)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();

           
            var result2 = new List<SalesLedgerEntry>();
            if (result.Count > 0)
            {

                for (int i = 0; i < result.Count; i++)
                {
                    if (i < (result.Count - 1))
                    {
                        if (result[i].SalesLedgerEntryDescription == result[i + 1].SalesLedgerEntryDescription)
                        {
                            if (result[i].SaleAmount > 0)
                            {
                                result2.Add(result[i]);
                            }

                            else if (result[i + 1].SaleAmount > 0) result2.Add(result[i + 1]);

                        }
                    }
                }
                // last record just in case
                if (result[result.Count - 1].SalesLedgerEntryDescription !=
                    result2[result2.Count - 1].SalesLedgerEntryDescription)
                {

                }
            }

            return result2;
        }

        private Func<IQueryable<SalesLedgerEntry>, IOrderedQueryable<SalesLedgerEntry>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<SalesLedgerEntry>, IOrderedQueryable<SalesLedgerEntry>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SalesLedgerEntryID)
                        : orderBy = q => q.OrderByDescending(x => x.SalesLedgerEntryID);
                case "CreatedDate":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);
                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SalesLedgerEntryID)
                        : orderBy = q => q.OrderByDescending(x => x.SalesLedgerEntryID);
            }
        }

        private Expression<Func<SalesLedgerEntry, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<SalesLedgerEntry, bool>> mainCriteria = null;
            
            if (searchObject.FromDate.HasValue)
            {
                if (mainCriteria == null)
                {
                    mainCriteria = c => c.CreatedDate.Value >= searchObject.FromDate.Value;
                }
                else
                {
                    mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
                }
            }

            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }
    }
}