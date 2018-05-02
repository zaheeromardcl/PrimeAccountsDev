using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Data.Contexts;

namespace PrimeActs.Data.Service
{
    public interface ITempBankNominalLedgerEntryService : IService<TempBankNominalLedgerEntry>
    {
        List<TempBankNominalLedgerEntry> GetTempBankNominalLenderEntriesByStatementID(Guid bankStatementID);
        TempBankNominalLedgerEntry GetTempBankNominalLenderEntriesByID(Guid id);
        Guid? GetCurrentStatementInReconciliation();
        decimal GetTotalIsReconciled(Guid bankStatementID);
        decimal GetStatementTotalToReconcile(Guid bankStatementID);
        void DeleteAll();
    }

    public class TempBankNominalLedgerEntryService : Service<TempBankNominalLedgerEntry>, ITempBankNominalLedgerEntryService
    {
        private readonly IRepositoryAsync<TempBankNominalLedgerEntry> _repository;

        public TempBankNominalLedgerEntryService(IRepositoryAsync<TempBankNominalLedgerEntry> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public void DeleteAll()
        {
            using (var context = new PAndIContext())
            {
                context.Database.ExecuteSqlCommand("Delete from tblTempBankNominalLedgerEntry");
            }
        }

        public List<TempBankNominalLedgerEntry> GetTempBankNominalLenderEntriesByStatementID(Guid bankStatementID)
        {
            List<TempBankNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.BankStatementID == bankStatementID).ToList();
            return ledgerEntries;
        }

       
        public TempBankNominalLedgerEntry GetTempBankNominalLenderEntriesByID(Guid id)
        {
            TempBankNominalLedgerEntry ledgerEntries =
                _repository.Query().Select().Where(t => t.TempBankNominalLedgerEntryID == id).FirstOrDefault();
            return ledgerEntries;
        }

        public Guid? GetCurrentStatementInReconciliation()
        {
            var lastEntry = _repository.Query().Select().OrderBy(a => a.BankReconciliationDate).LastOrDefault();
            return lastEntry == null ? (Guid?)null : lastEntry.BankStatementID;
        }

        public decimal GetStatementTotalToReconcile(Guid bankStatementID)
        {
            var entries = _repository.Query().Select().Where(t => t.BankStatementID == bankStatementID);
            var sumTransactionAmount = entries.Sum(a => a.TransactionAmount);
            return sumTransactionAmount;
        }

        public decimal GetTotalIsReconciled(Guid bankStatementID)
        {
            bool? testb = true;
            var entries = _repository.Query().Select().Where(t => t.BankStatementID == bankStatementID );
            var list1 = entries.ToList();
            var entries2 = _repository.Query().Select().Where(t => t.BankStatementID == bankStatementID && t.IsReconciled.GetValueOrDefault() == testb.GetValueOrDefault());
            var entries3 = _repository.Query(a => a.IsReconciled == true).Select().Where(t => t.BankStatementID == bankStatementID );
               // GetTempBankNominalLenderEntriesByStatementID(bankStatementID).Where(a => a.IsReconciled);
            var test = entries.Where(a => a.IsReconciled == true);
            var sumTransactionAmount = entries.Sum(a => a.TransactionAmount);
            decimal countn = 0;

            foreach (var x in list1)
            {
                
                
                if (x.TempDescriptionn.Contains("CHQ NO 50939 09/09/2016 10:20:24 JACANA PRODUCE LTD"))
                {
                    countn = countn + x.TransactionAmount;
                }
            }

            return sumTransactionAmount;
        }
    }
}
