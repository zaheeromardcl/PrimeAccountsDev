#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ITransferTypeService : IService<TransferType>
    {
        TransferType GetByTransferTypeId(Guid id);
        TransferType GetByTransferTypeCode(string transferTypeCode);
        List<TransferType> GetAllTransferTypes();
        List<TransferType> GetAllActiveTransferTypes();
      
    }
}