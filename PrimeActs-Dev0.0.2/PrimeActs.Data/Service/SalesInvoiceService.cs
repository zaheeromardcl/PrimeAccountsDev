#region

using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Invoice;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ISalesInvoiceService : IService<SalesInvoice>
    {
        SalesInvoice SalesInvoiceById(Guid Id);
        List<SalesInvoice> GetSalesInvoices(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Invoice.SearchObject searchObject, out int totalCount);
        List<SalesInvoice> GetSalesInvoicesByCustomerDepartment(Guid customerDepartmentID);
    }

    public class SalesInvoiceService : Service<SalesInvoice>, ISalesInvoiceService
    {

        private readonly IRepositoryAsync<SalesInvoice> _repository;
        private readonly string _type;

        public SalesInvoiceService(IRepositoryAsync<SalesInvoice> repository)
            : base(repository)
        {
            _repository = repository;
            _type = typeof(SalesInvoice).FullName;
        }

        public SalesInvoice SalesInvoiceById(Guid Id)
        {

            return
                _repository.Query(fil => fil.SalesInvoiceID == Id & fil.SalesInvoiceItems.Any(x=>x.TicketItem.Ticket.IsCashSale==false))
                    .Include(inc => inc.Currency)
                    .Include(inc => inc.CustomerDepartmentAddress)
                    .Include(inc=>inc.DivisionAddress)
                   .Include(inc => inc.CustomerDepartment)
                    .Include(inc => inc.CustomerDepartment.Customer)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.SalesInvoiceItems.Select(incp => incp.TicketItem.Ticket.Division.Company))                    
                    .Select().SingleOrDefault();

        }
        public List<SalesInvoice> GetSalesInvoices(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Invoice.SearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return
                _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.SalesInvoiceItems.Select(incp=>incp.TicketItem.Ticket.Division.Company))                    
                    .Include(inc => inc.CustomerDepartment.Customer)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
        }

        public List<SalesInvoice> GetSalesInvoicesByCustomerDepartment(Guid customerDepartmentID)
        {


            var customerDepartmentSalesInvoices =
                _repository.Query(inc => inc.CustomerDepartmentID == customerDepartmentID).Select()
                    .Where(c => c.SalesInvoiceDate > DateTime.Now.AddDays(-7)).ToList();

            return customerDepartmentSalesInvoices;
        }



        private Func<IQueryable<SalesInvoice>, IOrderedQueryable<SalesInvoice>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<SalesInvoice>, IOrderedQueryable<SalesInvoice>> orderBy = null;
            switch (column)
            {
                case "ID":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SalesInvoiceID)
                        : orderBy = q => q.OrderByDescending(x => x.SalesInvoiceID);
                case "SalesInvoiceReference":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SalesInvoiceReference)
                        : orderBy = q => q.OrderByDescending(x => x.SalesInvoiceReference);
                case "CustomerDepartmentName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CustomerDepartment.CustomerDepartmentName)
                        : orderBy = q => q.OrderByDescending(x => x.CustomerDepartment.CustomerDepartmentName);
                case "CreatedDate":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);
                default:
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SalesInvoiceID)
                        : orderBy = q => q.OrderByDescending(x => x.SalesInvoiceID);
            }
        }

        private Expression<Func<SalesInvoice, bool>> GetSearchCriteria(PrimeActs.Domain.ViewModels.Invoice.SearchObject searchObject)
        {
            Expression<Func<SalesInvoice, bool>> mainCriteria = c => c.SalesInvoiceItems.Any(x=>x.TicketItem.Ticket.IsCashSale==false);
            if (!string.IsNullOrEmpty(searchObject.SalesInvoiceReference))
                mainCriteria = mainCriteria.And(c => c.SalesInvoiceReference.StartsWith(searchObject.SalesInvoiceReference));
            if (!string.IsNullOrEmpty(searchObject.SalesInvoiceReference))
                mainCriteria = mainCriteria.And(c => c.CustomerDepartment.CustomerDepartmentName.StartsWith(searchObject.CustomerDepartmentName));
            if (!string.IsNullOrEmpty(searchObject.TicketReference))
                mainCriteria = mainCriteria.And(c => c.SalesInvoiceItems.Any(y=>y.TicketItem.Ticket.TicketReference.StartsWith(searchObject.TicketReference)));
            if (!string.IsNullOrEmpty(searchObject.ConsignmentReference))
                mainCriteria = mainCriteria.And(c => c.SalesInvoiceItems.Any(y=>y.TicketItem.ConsignmentItem.Consignment.ConsignmentReference.StartsWith(searchObject.ConsignmentReference)));

            if (!string.IsNullOrEmpty(searchObject.SalesInvoiceReference))
                mainCriteria = mainCriteria.And(c => c.CustomerDepartment.CustomerDepartmentName.StartsWith(searchObject.CustomerDepartmentName));

            if (searchObject.FromDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }
    }
}