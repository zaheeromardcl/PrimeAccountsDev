#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.BankAccount;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class BankAccountService : Service<BankAccount>, IBankAccountService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<BankAccount> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;

        public BankAccountService(IRepositoryAsync<BankAccount> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
          
        }

        public List<BankAccount> GetAllCustomerDepartmentBankAccounts(Guid CustomerDepartmentID)
        {
            ////CheckCache();
            var BankAccounts =
                _repository.Query(
                    fil =>
                        //fil.IsActive == true &&
                        fil.CustomerBankAccounts.Any(x => x.CustomerDepartmentID == CustomerDepartmentID))
                    .Select()
                    .ToList();
            return BankAccounts;
        }

        public BankAccount BankAccountById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(typeof(BankAccount).FullName) as IEnumerable<BankAccount>).Where(t => t.BankAccountID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        // DC Added for interim testing of Purchase Invoice CSV generation
        public List<BankAccount> BankAccountBySupplierId(Guid Id)
        {
            var BankAccounts =
                _repository.Query(
                    fil =>
                        //fil.IsActive == true &&
                        fil.SupplierBankAccounts.Any(x => x.SupplierID == Id))
                    .Select()
                    .ToList<BankAccount>();
            return BankAccounts;
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            CheckCache();
            var returnValue = (_cache.Get(typeof(BankAccount).FullName) as List<BankAccount>);
            if (returnValue == null)
                return new List<BankAccount>();
            return returnValue;
        }

        public List<BankAccount> GetBankAccounts(Domain.ViewModels.QueryOptions queryOptions, SearchObject searchObject,
            out int totalCount)
        {
            var result = new List<BankAccount>();
            Guid supplierDepartmentId;
            if (!Guid.TryParse(searchObject.SupplierDepartmentId, out supplierDepartmentId))
                result = _repository.Query(GetSearchCriteria(searchObject))
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList<BankAccount>();
            else
            {
                // search by SupplierDepartmentId
                result = _repository.Query(GetSearchCriteria(searchObject))
                    .Include(x => x.SupplierBankAccounts)
                    .OrderBy(GetOrder(queryOptions.SortField, queryOptions.SortOrder))
                    .SelectPage(queryOptions.CurrentPage, queryOptions.PageSize, out totalCount)
                    .ToList<BankAccount>();
            }

            return result;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof(BankAccount).FullName);
        }

        private void CheckCache()
        {
            var type = typeof(BankAccount).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var BankAccount = new List<BankAccount>();
                    foreach (
                        var entityType in
                            _repository.Query()
                                .Select())
                    {
                        BankAccount.Add(new BankAccount
                        {
                            BankAccountID = entityType.BankAccountID,
                            AccountName = entityType.AccountName,
                            AccountNumber = entityType.AccountNumber,
                            BankCode = entityType.BankCode
                        });
                    }
                    _cache.Set(type, BankAccount);
                }
            }
        }

        private Expression<Func<BankAccount, bool>> GetSearchCriteria(SearchObject searchObject)
        {
            Expression<Func<BankAccount, bool>> mainCriteria = x => true; // = c;//.IsActive == true;
            if (!string.IsNullOrEmpty(searchObject.AccountName))
                mainCriteria =
                    mainCriteria.And(c => c.AccountName.ToString().StartsWith(searchObject.AccountName));
            if (!string.IsNullOrEmpty(searchObject.SupplierDepartmentId))
            {
                Guid supplierDepartmentId;
                if (!Guid.TryParse(searchObject.SupplierDepartmentId, out supplierDepartmentId))
                    supplierDepartmentId = Guid.Empty;

                mainCriteria =
                    mainCriteria.And(
                        c => c.SupplierBankAccounts.Any(a => a.SupplierDepartmentID == supplierDepartmentId));
            }
            //if (searchObject.FromDate.HasValue)
            //    mainCriteria = mainCriteria.And(c => c.CreatedDate.Value >= searchObject.FromDate.Value);
            //if (searchObject.ToDate.HasValue)
            //    mainCriteria = mainCriteria.And(c => c.CreatedDate.Value <= searchObject.ToDate.Value);
            return mainCriteria;
        }

        private Func<IQueryable<BankAccount>, IOrderedQueryable<BankAccount>> GetOrder(string column, string ascDesc)
        {
            Func<IQueryable<BankAccount>, IOrderedQueryable<BankAccount>> orderBy = null;
            switch (column)
            {
                //case "CreatedDate":
                //    return ascDesc == "ASC"
                //        ? orderBy = q => q.OrderBy(x => x.CreatedDate)
                //        : orderBy = q => q.OrderByDescending(x => x.CreatedDate);
                case "AccountName":
                    return ascDesc == "ASC"
                        ? orderBy = q => q.OrderBy(x => x.AccountName)
                        : orderBy = q => q.OrderByDescending(x => x.AccountName);
                default:
                    return orderBy = q => q.OrderByDescending(x => x.BankAccountID);
            }
        }
    }
}