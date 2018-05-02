using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Data.Contexts;

namespace PrimeActs.Data.Service
{
    public interface ITempBankStatementItemNominalLedgerEntryService : IService<TempBankStatementItemNominalLedgerEntry>
    {
        List<TempBankStatementItemNominalLedgerEntry> GetByID(Guid id);
        List<TempBankStatementItemNominalLedgerEntry> GetByStatementID(Guid id);
        List<TempBankStatementItemNominalLedgerEntry> GetByNominalID(Guid id);
        List<TempBankStatementItemNominalLedgerEntry> GetAll();
        void DeleteAll();
    }

    public class TempBankStatementItemNominalLedgerEntryService : Service<TempBankStatementItemNominalLedgerEntry>, ITempBankStatementItemNominalLedgerEntryService
    {
        private readonly IRepositoryAsync<TempBankStatementItemNominalLedgerEntry> _repository;

        public TempBankStatementItemNominalLedgerEntryService(IRepositoryAsync<TempBankStatementItemNominalLedgerEntry> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public List<TempBankStatementItemNominalLedgerEntry> GetAll()
        {
            List<TempBankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().ToList();
            return ledgerEntries;
        }

        public void DeleteAll()
        {
            using (var context = new PAndIContext())
            {
                context.Database.ExecuteSqlCommand("Delete from tblTempBankStatementItemNominalLedgerEntry");
            }
        }

        public List<TempBankStatementItemNominalLedgerEntry> GetByID(Guid id)
        {
            List<TempBankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.BankStatementItemNominalLedgerEntryID == id).ToList();
            return ledgerEntries;
        }

        public List<TempBankStatementItemNominalLedgerEntry> GetByStatementID(Guid id)
        {
            List<TempBankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.BankStatementItemID == id).ToList();
            return ledgerEntries;
        }

        public List<TempBankStatementItemNominalLedgerEntry> GetByNominalID(Guid id)
        {
            List<TempBankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.NominalLedgerEntryID == id).ToList();
            return ledgerEntries;
        }
        
    }
}
