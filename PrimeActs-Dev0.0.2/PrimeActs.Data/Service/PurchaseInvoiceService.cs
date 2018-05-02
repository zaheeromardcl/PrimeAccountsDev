#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class PurchaseInvoiceService : Service<PurchaseInvoice>, IPurchaseInvoiceService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<PurchaseInvoice> _repository;
        private readonly object lockboject = new object();

        public PurchaseInvoiceService(IRepositoryAsync<PurchaseInvoice> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }
        
        public PurchaseInvoice PurchaseInvoiceById(Guid Id)
        {
            
            PurchaseInvoice varPurchaseInvoiceByID = _repository.Query(p => p.PurchaseInvoiceID == Id)
                    .Include(inc => inc.PurchaseInvoiceItems)
                    .Include(inc => inc.PurchaseInvoiceItems.Select(item => item.ConsignmentItem))
                    .Include(inc => inc.SupplierDepartment)
                    .Include(inc => inc.Address)
                    .Include(inc => inc.Note)
                    .Select().FirstOrDefault();
            return varPurchaseInvoiceByID;
        }

        public List<PurchaseInvoice> GetPurchaseInvoicesByStatus(int status)
        {
            return
                 _repository.Query(p => p.Status == status)
                     .Include(inc => inc.PurchaseInvoiceItems)
                     .Include(inc => inc.PurchaseInvoiceItems.Select(item => item.ConsignmentItem))
                      .Include(inc => inc.SupplierDepartment)
                     .Include(inc => inc.Address)
                     .Select().ToList();
        }

        public List<PurchaseInvoice> GetAllPurchaseInvoices()
        {
            CheckCache();
            var returnValue = (_cache.Get(typeof (PurchaseInvoice).FullName) as List<PurchaseInvoice>);
            if (returnValue == null)
                return new List<PurchaseInvoice>();
            return returnValue;
        }

        public List<PurchaseInvoice> GetPurchaseInvoices(Domain.ViewModels.QueryOptions queryOptions, SearchObject searchObject,
            out int totalCount)
        {
            return _repository.Query(GetSearchCriteria(searchObject))
                    .Include(inc => inc.PurchaseInvoiceItems)
                    .Include(inc => inc.SupplierDepartment.Supplier)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList<PurchaseInvoice>();
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof (PurchaseInvoice).FullName);
        }

        private void CheckCache()
        {
            var type = typeof (PurchaseInvoice).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var PurchaseInvoice = new List<PurchaseInvoice>();
                    foreach (
                        var entityType in
                            _repository.Query()
                                .Select())
                    {
                        PurchaseInvoice.Add(new PurchaseInvoice
                        {
                            PurchaseInvoiceID = entityType.PurchaseInvoiceID,
                            PurchaseInvoiceReference = entityType.PurchaseInvoiceReference
                          
                        });
                    }
                    _cache.Set(type, PurchaseInvoice);
                }
            }
        }

        private Expression<Func<PurchaseInvoice, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            //AK: Amending maon
            Expression<Func<PurchaseInvoice, bool>> mainCriteria = c => c.PurchaseInvoiceID.ToString()  == c.PurchaseInvoiceID.ToString();
            if (!string.IsNullOrEmpty(searchObject.PurchaseInvoiceReference))
                mainCriteria =
                    mainCriteria.And(c => c.PurchaseInvoiceReference.ToString().StartsWith(searchObject.PurchaseInvoiceReference));
            if (!string.IsNullOrEmpty(searchObject.SupplierDepartmentId))
            {
                Guid supplierDepartmentId;
                if (!Guid.TryParse(searchObject.SupplierDepartmentId, out supplierDepartmentId))
                    supplierDepartmentId = Guid.Empty;
                mainCriteria =
                    mainCriteria.And(
                        c => c.SupplierDepartment.SupplierDepartmentID == supplierDepartmentId);
            }
            if (searchObject.FromDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            if (searchObject.ToDate.HasValue)
                mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }

        private Func<IQueryable<PurchaseInvoice>, IOrderedQueryable<PurchaseInvoice>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<PurchaseInvoice>, IOrderedQueryable<PurchaseInvoice>> orderBy = null;
            switch (column)
            {
                case "CreatedDate":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);
                case "SupplierCode":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.SupplierDepartment.Supplier.SupplierCode)
                        : orderBy = q => q.OrderByDescending(x => x.SupplierDepartment.Supplier.SupplierCode);
                case "Status":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.Status)
                        : orderBy = q => q.OrderByDescending(x => x.Status);
                case "Total":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.Total)
                        : orderBy = q => q.OrderByDescending(x => x.Total);
                case "PurchaseInvoiceReference":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.PurchaseInvoiceReference)
                        : orderBy = q => q.OrderByDescending(x => x.PurchaseInvoiceReference);
                default:
                    return orderBy = q => q.OrderByDescending(x => x.PurchaseInvoiceID);
            }
        }
    }
}