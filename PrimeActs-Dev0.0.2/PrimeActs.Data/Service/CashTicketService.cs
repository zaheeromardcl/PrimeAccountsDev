using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class CashTicketService : Service<vwCashTicket>, ICashTicketService
    {
        private readonly IRepositoryAsync<vwCashTicket> _repository;
        
        public CashTicketService(IRepositoryAsync<vwCashTicket> repository) : base(repository)
        {
            _repository = repository;
        }

        // the method below is a solution for paging for daily cash, it works properly and fast
        // in order to use it rename it to "GetDailyCashTickets"
        public List<vwCashTicket> GetDailyCashTicketsAlternative(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            Func<vwCashTicket, bool> q = null;
            if (searchObject.Paid != null)
            {
                if ((bool) searchObject.Paid)
                    q = ticket => ticket.BalanceOwed == 0;
                else
                {
                    q = ticket => ticket.BalanceOwed != 0;
                }
            }

            if (q != null)
            {
                var all = _repository.Query(GetSearchCriteriaAlternativeForDailyCash(searchObject)).Select().Where(q);
                var page = all.Skip(queryOptions.PageSize * (queryOptions.CurrentPage - 1)).Take(queryOptions.PageSize);

                totalCount = all.Count();
                return page.ToList();
            }
            else
            {
                // paid boolean should be assigned with value
                totalCount = 0;
                return null;
            }
        }

        public List<vwCashTicket> GetDailyCashTicketsAll(QueryOptions queryOptions, SearchObject searchObject)
        {
            var result = _repository.Query(GetSearchCriteria(searchObject))
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .Select()
                    .ToList();
            return result;
        }

        public List<vwCashTicket> GetDailyCashTickets(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            var result = _repository.Query(GetSearchCriteria(searchObject))
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
            return result;
        }

        private Func<IQueryable<vwCashTicket>, IOrderedQueryable<vwCashTicket>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<vwCashTicket>, IOrderedQueryable<vwCashTicket>> orderBy = null;
            switch (column)
            {
                case "TicketReference":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.TicketReference)
                        : orderBy = q => q.OrderByDescending(x => x.TicketReference);

                case "CreatedDate":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);
                
                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);
            }
        }

        private Expression<Func<vwCashTicket, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<vwCashTicket, bool>> mainCriteria = null;

            if (searchObject.FromDate.HasValue)
            {
                mainCriteria = c => c.CreatedDate >= searchObject.FromDate.Value;
            }
            else
            {
                // this line should never be hit
                var temp = DateTime.Today.AddDays(-1).AddHours(22);
                mainCriteria = c => c.CreatedDate >= temp;
            }

            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate <= searchObject.ToDate.Value);

            // if paid == null then we want all paid and unpaid tickets
            if (searchObject.Paid != null)
            {
                mainCriteria = (bool) searchObject.Paid ? mainCriteria.And(c => c.BalanceOwed == 0) : mainCriteria.And(c => c.BalanceOwed != 0);
            }

            return mainCriteria;
        }

        private Expression<Func<vwCashTicket, bool>> GetSearchCriteriaAlternativeForDailyCash(SearchObject searchObject)
        {
            Expression<Func<vwCashTicket, bool>> mainCriteria = null;

            if (searchObject.FromDate.HasValue)
            {
                mainCriteria = c => c.CreatedDate >= searchObject.FromDate.Value;
            }
            else
            {
                // this line should never be hit
                var temp = DateTime.Today.AddDays(-1).AddHours(22);
                mainCriteria = c => c.CreatedDate >= temp;
            }

            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate <= searchObject.ToDate.Value);
            
            return mainCriteria;
        }
    }
}
