using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface IBankStatementItemService : IService<BankStatementItem>
    {
        List<BankStatementItem> GetBankStatementsByID(Guid bankStatementID);
        decimal GetStatementTotalToReconcile(Guid bankStatementID);
    }

    public class BankStatementItemService : Service<BankStatementItem>, IBankStatementItemService
    {
        private readonly IRepositoryAsync<BankStatementItem> _repository;

        public BankStatementItemService(IRepositoryAsync<BankStatementItem> repository)
            : base(repository)
        {
            _repository = repository;
        }

        //public BankStatementHeader GetBankStatementByName(string fileName)
        //{
        //    BankStatementHeader bankStatement =
        //        _repository.Query().Select().Where(t => t.BankStatementFileName == fileName).FirstOrDefault();
        //    return bankStatement;
        //}

        public List<BankStatementItem> GetBankStatementsByID(Guid bankStatementID)
        {
            List<BankStatementItem> bankStatements =
                _repository.Query().Select().Where(t => t.BankStatementID == bankStatementID).ToList();
            return bankStatements;
        }

        public decimal GetStatementTotalToReconcile(Guid bankStatementID)
        {
            var entries = _repository.Query().Select().Where(t => t.BankStatementID == bankStatementID);
            var sumTransactionAmount = entries.Sum(a => a.TransactionAmount);
            return sumTransactionAmount;
        }
    }
}