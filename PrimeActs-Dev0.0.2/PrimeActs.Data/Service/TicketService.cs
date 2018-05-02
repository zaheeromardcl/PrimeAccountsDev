#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class TicketService : Service<Ticket>, ITicketService
    {
        private readonly IRepositoryAsync<Ticket> _repository;

        public TicketService(IRepositoryAsync<Ticket> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Ticket TicketByRef(string TicketRef)
        {
            return
                _repository.Query(fil => fil.TicketReference == TicketRef)
                    .Include(inc => inc.Currency)
                    .Include(inc => inc.CustomerDepartment)
                    .Include(inc => inc.TicketItems)
                    .Select()
                    .FirstOrDefault();
        }

        public Ticket TicketById(Guid Id)
        {

            var varticket= 
                _repository.Query(fil => fil.TicketID == Id)
                 .Include(inc => inc.Currency)
                  .Include(inc => inc.Note)
                  .Include(inc => inc.CustomerDepartment)
                   .Include(inc => inc.CustomerDepartment.Customer)
                   .Include(inc => inc.TicketItems)
                     .Include(inc => inc.TicketItems.Select(ti => ti.ConsignmentItem.Consignment))
                  .Include(inc => inc.TicketItems.Select(ti => ti.ConsignmentItem.Department))
                   .Include(inc => inc.TicketItems.Select(ti => ti.ConsignmentItem.Produce))
                   .Include(inc => inc.TicketItems.Select(ti => ti.ConsignmentItem.Porterage))
                  .Include(inc => inc.TicketItems.Select(ti => ti.TransferType))
                    .Select()
            .FirstOrDefault();

            return varticket;
            
        }

        public Ticket GetTicketOnly(Guid Id)
        {
            return
                _repository.Query(fil => fil.TicketID == Id)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.CustomerDepartment)
                    .Include(inc => inc.CustomerDepartment.Customer)
                    .Select().SingleOrDefault();
        }

        public Ticket GetTicket(Guid Id)
        {

            var tempTicket = _repository.Query(fil => fil.TicketID == Id).Include(inc => inc.CustomerDepartment)
               
                .Select().SingleOrDefault();
            return tempTicket;
            //return
            //    _repository.Query(fil => fil.TicketID == Id)// && fil.IsActive == true)
            //    .Include(inc => inc.CustomerDepartment)
            //    .Include(inc => inc.CustomerDepartment.Customer)
            //    .Include(inc => inc.TicketItems)
            //        .Select().SingleOrDefault();
        }
        
        public List<Ticket> GetAllTickets()
        {
            return
                _repository.Query()
                    .Include(inc => inc.Currency)
                    .Include(inc => inc.CustomerDepartment)
                    .Select()
                    .ToList();
        }

        public List<Ticket> GetAllTicketsByCustomerDepartment(Guid customerdepartmentID)
        {

            
               var customerdepartmenttickets = _repository.Query(fil => fil.CustomerDepartmentID==customerdepartmentID)
                    .Select().Where(c => c.TicketDate > DateTime.Now.AddDays(-3))
                    .ToList();
                
               return customerdepartmenttickets;
        }

        public List<Ticket> GetTickets(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            var tickets = _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.Note)
                    .Include(inc => inc.Currency)
                    .Include(inc => inc.CustomerDepartment)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
            return tickets;
        }

        public List<Ticket> GetDailyCashTickets(QueryOptions queryOptions, SearchObject searchObject, out int totalCount)
        {
            var result = _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.CustomerDepartment)
                    .Include(inc => inc.TicketItems)
                    .Include(inc => inc.TicketItems.Select(ti => ti.SalesInvoiceItems))
                    .Include(inc => inc.TicketItems.Select(ti => ti.SalesInvoiceItems.Select(si => si.SalesInvoice)))
                    .Include(inc => inc.TicketItems.Select(ti => ti.SalesInvoiceItems.Select(si => si.SalesInvoice.SalesLedgerInvoiceAllocations)))
                    .Include(inc => inc.TicketItems.Select(ti => ti.SalesInvoiceItems.Select(si => si.SalesInvoice.SalesLedgerInvoiceAllocations.Select(al => al.SalesLedgerEntry))))
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
            return result;

        }

        private Func<IQueryable<Ticket>, IOrderedQueryable<Ticket>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Ticket>, IOrderedQueryable<Ticket>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.TicketID)
                        : orderBy = q => q.OrderByDescending(x => x.TicketID);
                case "TicketReference":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.TicketReference)
                        : orderBy = q => q.OrderByDescending(x => x.TicketReference);
                case "Customer":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CustomerDepartment.CustomerDepartmentName)
                        : orderBy = q => q.OrderByDescending(x => x.CustomerDepartment.CustomerDepartmentName);

                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.TicketID)
                        : orderBy = q => q.OrderByDescending(x => x.TicketID);
            }
        }

        private Expression<Func<Ticket, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Ticket, bool>> mainCriteria=null;

            if (searchObject.FromDate.HasValue)
            {
                mainCriteria = c => c.CreatedDate.Value >= searchObject.FromDate.Value;
            }
            else
            {
                // this line should never be hit
                mainCriteria = c => c.CreatedDate.Value >= DateTime.Today.AddDays(-30);
            }

            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);

            if (searchObject.IsDailyCash)
            {
                mainCriteria = mainCriteria.And(c => c.IsCashSale.Value == true);
                //mainCriteria = mainCriteria.And(c => 0 == c.TicketItems.Sum(ti => ti.SalesInvoiceItems.Sum(ii => ii.SalesInvoice.SalesLedgerInvoiceAllocations.Sum(al => al.SaleAmount))));
                // later if you need to get the total price of the ticket: (c.TicketItems.Sum(x => x.TicketItemTotalPrice) + c.TicketItems.Sum(y => y.PorterageValue))
            }

            if (!string.IsNullOrEmpty(searchObject.TicketReference))
                mainCriteria = mainCriteria.And(c => c.TicketReference.StartsWith(searchObject.TicketReference));
            if (!string.IsNullOrEmpty(searchObject.CustomerCode))
            {
                var CustomerID = Guid.Parse(searchObject.CustomerCode);
                mainCriteria =
                    mainCriteria.And(
                        c => c.CustomerDepartment.CustomerDepartmentID == CustomerID);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchObject.CustomerCompanyName))
                    mainCriteria =
                        mainCriteria.And(
                            c =>
                                c.CustomerDepartment.CustomerDepartmentName.StartsWith(searchObject.CustomerCompanyName) |
                                c.CustomerDepartment.CustomerDepartmentName.StartsWith(searchObject.CustomerCompanyName));
            }
            
            return mainCriteria;
        }
    }
}