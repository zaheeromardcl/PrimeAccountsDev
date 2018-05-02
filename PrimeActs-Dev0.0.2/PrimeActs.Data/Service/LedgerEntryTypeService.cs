#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ILedgerEntryTypeService : IService<LedgerEntryType>
    {
        LedgerEntryType LedgerEntryTypeByNumber(int ledgerEntryTypeByNumber);
        LedgerEntryType LedgerEntryTypeById(Guid id);
        List<LedgerEntryType> GetAllLedgerEntryTypes();
        
    }

    public class LedgerEntryTypeService : Service<LedgerEntryType>, ILedgerEntryTypeService
    {
       
        private readonly IRepositoryAsync<LedgerEntryType> _repository;
       

        public LedgerEntryTypeService(IRepositoryAsync<LedgerEntryType> repository)
            : base(repository)
        {
            _repository = repository;
           
        }

        public LedgerEntryType LedgerEntryTypeByNumber(int ledgerEntryTypeByNumber)
        {
            LedgerEntryType varLedgerEntryType = _repository.Query(t => t.LedgerEntryTypeNumber == ledgerEntryTypeByNumber).Select().FirstOrDefault();
            return varLedgerEntryType;
        }

        public LedgerEntryType LedgerEntryTypeById(Guid id)
        {
            LedgerEntryType varLedgerEntryType = _repository.Query(t => t.LedgerEntryTypeID == id).Select().FirstOrDefault();
            return varLedgerEntryType;
        }


        public List<LedgerEntryType> GetAllLedgerEntryTypes()
        {
            List<LedgerEntryType> varListLedgerEntryTypes = _repository.Query().Select().ToList();
            return varListLedgerEntryTypes;
        }


     
    }
}