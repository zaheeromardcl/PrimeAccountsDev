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
    public class TransferTypeService : Service<TransferType>, ITransferTypeService
    {
        
        private readonly IRepositoryAsync<TransferType> _repository;
        
        public TransferTypeService(IRepositoryAsync<TransferType> repository)
            : base(repository)
        {
            _repository = repository;

        }

        public TransferType GetByTransferTypeId(Guid transferTypeId)
        {
            TransferType varTransferType = _repository.Query(x => x.TransferTypeID == transferTypeId).Select().FirstOrDefault();
            return varTransferType;
        }

        public TransferType GetByTransferTypeCode(string transferTypeCode)
        {

            TransferType varTransferType = _repository.Query(x => x.TransferTypeCode == transferTypeCode).Select().FirstOrDefault();
            return varTransferType;
        }

        public List<TransferType> GetAllTransferTypes()
        {
            List<TransferType> varTransferTypes = _repository.Query().Select().ToList();
            return varTransferTypes;
        }

        public List<TransferType> GetAllActiveTransferTypes()
        {
           List<TransferType> varActiveTransferTypes = _repository.Query(x=>x.IsActive==true).Select().ToList();
            return varActiveTransferTypes;
        }

      
    }
    
}