#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;
using System.Linq.Dynamic;

#endregion

namespace PrimeActs.Data.Service
{
    public class ConsignmentService : Service<Consignment>, IConsignmentService
    {
        private readonly IRepositoryAsync<Consignment> _repository;

        public ConsignmentService(IRepositoryAsync<Consignment> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Consignment ConsignmentByRef(string consignmentRef)
        {
            return
                _repository.Query()
                    .Include(inc => inc.Note)
                    .Include(inc => inc.PurchaseType)
                    .Include(inc => inc.DespatchLocation)
                    .Include(inc => inc.SupplierDepartment)
                    .Include(inc => inc.Port)
                    .Include(inc => inc.ConsignmentFiles)
                    .Include(inc => inc.ConsignmentItems)
                    .Select()
                    .FirstOrDefault(fil => fil.ConsignmentReference == consignmentRef);
        }

        public Consignment ConsignmentById(Guid Id)
        {
            Consignment varConsignment = _repository.Query(fil => fil.ConsignmentID == Id)
                    .Include(inc => inc.Note)
                    .Include(inc => inc.SupplierDepartment.Supplier)
                    .Include(inc => inc.PurchaseType)
                    .Include(inc => inc.DespatchLocation)
                    .Include(inc => inc.SupplierDepartment)
                    .Include(inc => inc.Port)
                    .Include(inc => inc.ConsignmentFiles)
                    .Include(inc => inc.ConsignmentItems.Select(ninc=>ninc.Department))
                    .Include(inc => inc.ConsignmentItems.Select(i => i.ConsignmentItemPriceReturns))
                    .Include(inc=>inc.ConsignmentItems.Select(jinc=>jinc.Produce))
                    .Include(inc => inc.ConsignmentItems.Select(cinc => cinc.Country))
                    .Select()
                    .FirstOrDefault();

            return varConsignment;
        }

        public Consignment ConsignmentAndSupplierDepartmentById(Guid Id)
        {
            
            Consignment consignment;
            try
            {
                consignment = _repository.Query(fil => fil.ConsignmentID == Id)
                    .Include(inc => inc.SupplierDepartment.Supplier)
                    .Include(inc => inc.SupplierDepartment)
                    .Select()
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                consignment = new Consignment();
            }
            return consignment;
        }

        public List<Consignment> GetAllConsignments()
        {
            return
                _repository.Query()
                    .Include(inc => inc.Note)
                    .Include(inc => inc.PurchaseType)
                    .Include(inc => inc.DespatchLocation)
                    .Include(inc => inc.SupplierDepartment)
                    .Include(inc => inc.Port)
                    .Include(inc => inc.ConsignmentFiles)
                    .Include(inc => inc.ConsignmentItems)
                    .Select()
                    .ToList();
        }

        public IEnumerable<Consignment> GetConsignmentsBySupplierDepartmentID(Guid Id, SupplierDepartmentSearch supplierDepartmentSearch)
        {
            var consignments = _repository.Query(x => x.SupplierDepartmentID == Id)
                .Include(inc => inc.ConsignmentItems)
                .Select()
                .Where(
                    c =>
                        c.DespatchDate >= supplierDepartmentSearch.From && c.DespatchDate <= supplierDepartmentSearch.To);
                
            return consignments;
        }
        
        public List<Consignment> GetConsignments(QueryOptions queryOptions, SearchObject searchObject,
            out int totalCount)
        {
            totalCount = 0;

            var result = new List<Consignment>();

            if (searchObject.CompletedConsignmentsOnly)
            {
                result = _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.ConsignmentItems)
                    .Include(inc => inc.ConsignmentItems.Select(ci => ci.Produce))
                    .Include(inc => inc.ConsignmentItems.Select(i => i.ConsignmentItemArrivals))
                    //.Include(inc => inc.ConsignmentItems.Select(i => i.PurchaseInvoiceItems))
                    .Include(inc => inc.ConsignmentItems.Select(ci => ci.TicketItems))
                    .Include(inc => inc.ConsignmentItems.Select(ci => ci.TicketItems.Select(ti => ti.Ticket)))
                    .Include(inc => inc.SupplierDepartment.Supplier)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();

                //result = result.AsQueryable().OrderBy(queryOptions.SortField + " " + queryOptions.SortOrder).ToList();
            }
            else
            {
                result = _repository.Query(GetSearchCriteria(searchObject))
                    //.Include(inc => inc.Note)
                    //.Include(inc => inc.SupplierDepartment)
                    //.Include(inc => inc.ConsignmentFiles)
                    .Include(inc => inc.ConsignmentItems)
                    .Include(inc => inc.ConsignmentItems.Select(ci => ci.Produce))
                    .Include(inc => inc.SupplierDepartment.Supplier)
                    //.Include(inc => inc.SupplierItems.Select(si => si.SupplierItemID)) /////////////
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList();
            }

            return result;
        }

        public Consignment LastConsigmentByUserId(Guid userID)
        {
            return
                _repository.Query(fil => fil.CreatedBy == userID)
                    .Include(inc => inc.SupplierDepartment)
                    .Include(inc => inc.SupplierDepartment.Supplier)
                    //.Include(inc => inc.SupplierItems) /////////////
                    //.Include(inc => inc.SupplierItems.Select(si => si.Supplier)) /////////////
                    .Select()
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();
        }

        public Consignment GetConsignment(Guid consignmentID)
        {
            Consignment varConsignment = _repository.Query(fil => fil.ConsignmentID == consignmentID)
                    .Select()
                    .FirstOrDefault();

            return varConsignment;
        }
        
        private Func<IQueryable<Consignment>, IOrderedQueryable<Consignment>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<Consignment>, IOrderedQueryable<Consignment>> orderBy = null;

            switch (column)
            {
                //case "CreatedDate":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                //        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);//    
                //case "ConsignmentReference":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.ConsignmentReference)
                //        : orderBy = q => q.OrderByDescending(x => x.ConsignmentReference);
                case "SupplierDepartmentName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierDepartment.SupplierDepartmentName)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierDepartment.SupplierDepartmentName);
                case "SupplierCompanyName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierDepartment.Supplier.SupplierCompanyName)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierDepartment.Supplier.SupplierCompanyName);
                case "CreatedByName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CreatedByUser.UserName)
                        : orderBy = q => q.OrderByDescending(x => x.CreatedByUser.UserName);
                case "Supplier":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierDepartment.Supplier.SupplierCode)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierDepartment.Supplier.SupplierCode);

                default:
                    orderBy = q => (IOrderedQueryable<Consignment>) q.OrderBy(column + " " + ascDesc);
                    return orderBy;
            }
        }

        private Expression<Func<Consignment, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<Consignment, bool>> mainCriteria = c => c.IsHistory == false;
            mainCriteria = mainCriteria.And(c => c.IsDeleted == false);

            if (searchObject.CompletedConsignmentsOnly)
            {
                //mainCriteria = mainCriteria.And(c => c.ConsignmentItems.Count > 0 &&  c.ConsignmentItems.All(ci => ci.ConsignmentItemArrivals.Sum(a => a.QuantityReceived) == 0));
                
                mainCriteria = mainCriteria.And(c => c.ConsignmentItems.Count > 0 && c.ConsignmentItems.All(ci => ci.PurchaseInvoiceItems.Count > 0));
            }

            if (!string.IsNullOrEmpty(searchObject.SupplierDepartmentNameOrConsignmentReference))
            {
                mainCriteria =
                    mainCriteria.And(
                        c =>
                            c.ConsignmentReference.StartsWith(searchObject.SupplierDepartmentNameOrConsignmentReference) ||
                            c.SupplierDepartment.SupplierDepartmentName.StartsWith(
                                searchObject.SupplierDepartmentNameOrConsignmentReference));
            }
            else
            {
                if (!string.IsNullOrEmpty(searchObject.ConsignmentReference))
                    mainCriteria =
                        mainCriteria.And(c => c.ConsignmentReference.StartsWith(searchObject.ConsignmentReference));

                if (!string.IsNullOrEmpty(searchObject.SupplierDepartmentId))
                {
                    Guid supplierDepartmentId;
                    if (!Guid.TryParse(searchObject.SupplierDepartmentId, out supplierDepartmentId))
                        supplierDepartmentId = Guid.Empty;
                    mainCriteria =
                        mainCriteria.And(
                            c => c.SupplierDepartment.SupplierDepartmentID == supplierDepartmentId);
                }
                if (!string.IsNullOrEmpty(searchObject.SupplierCode))
                {
                    var SupplierID = Guid.Parse(searchObject.SupplierCode);
                    mainCriteria =
                        mainCriteria.And(
                            c => c.SupplierDepartment.Supplier.SupplierID == SupplierID);
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchObject.SupplierName))
                        mainCriteria =
                            mainCriteria.And(
                                c =>
                                    c.SupplierDepartment.Supplier.SupplierCode.StartsWith(searchObject.SupplierName) |
                                    c.SupplierDepartment.Supplier.SupplierCompanyName.StartsWith(
                                        searchObject.SupplierName));
                }
            }

            if (searchObject.FromDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
            Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public static class Utility
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }
}