using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface IBankStatementItemNominalLedgerEntryService : IService<BankStatementItemNominalLedgerEntry>
    {
        List<BankStatementItemNominalLedgerEntry> GetByID(Guid id);
        List<BankStatementItemNominalLedgerEntry> GetByStatementID(Guid id);
        List<BankStatementItemNominalLedgerEntry> GetByNominalID(Guid id);
        List<BankStatementItemNominalLedgerEntry> GetAll();
    }

    public class BankStatementItemNominalLedgerEntryService : Service<BankStatementItemNominalLedgerEntry>, IBankStatementItemNominalLedgerEntryService
    {
        private readonly IRepositoryAsync<BankStatementItemNominalLedgerEntry> _repository;

        public BankStatementItemNominalLedgerEntryService(IRepositoryAsync<BankStatementItemNominalLedgerEntry> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public List<BankStatementItemNominalLedgerEntry> GetAll()
        {
            List<BankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().ToList();
            return ledgerEntries;
        }

        public List<BankStatementItemNominalLedgerEntry> GetByID(Guid id)
        {
            List<BankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.BankStatementItemNominalLedgerEntryID == id).ToList();
            return ledgerEntries;
        }

        public List<BankStatementItemNominalLedgerEntry> GetByStatementID(Guid id)
        {
            List<BankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.BankStatementItemID == id).ToList();
            return ledgerEntries;
        }

        public List<BankStatementItemNominalLedgerEntry> GetByNominalID(Guid id)
        {
            List<BankStatementItemNominalLedgerEntry> ledgerEntries =
                _repository.Query().Select().Where(t => t.NominalLedgerEntryID == id).ToList();
            return ledgerEntries;
        }
    }
}
