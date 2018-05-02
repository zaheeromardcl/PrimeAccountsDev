using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface IBankStatementService : IService<BankStatement>
    {
        BankStatement GetBankStatementByName(string fileName);
        BankStatement GetBankStatementByID(Guid ID);
    }

    public class BankStatementService : Service<BankStatement>, IBankStatementService
    {
        private readonly IRepositoryAsync<BankStatement> _repository;
        
        public BankStatementService(IRepositoryAsync<BankStatement> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public BankStatement GetBankStatementByName(string fileName)
        {
            BankStatement bankStatement =
                _repository.Query().Select().Where(t => t.BankStatementFileName == fileName).FirstOrDefault();
            return bankStatement;
        }

        public BankStatement GetBankStatementByID(Guid ID)
        {
            BankStatement bankStatement =
                _repository.Query().Select().Where(t => t.BankStatementID == ID).FirstOrDefault();
            return bankStatement;
        }


    }
}
