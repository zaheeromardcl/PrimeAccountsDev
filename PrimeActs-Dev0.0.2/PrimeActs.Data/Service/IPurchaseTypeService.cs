#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IPurchaseTypeService : IService<PurchaseType>
    {
        PurchaseType PurchaseTypeByName(string PurchaseType);
        PurchaseType PurchaseTypeById(Guid Id);
        List<PurchaseType> GetAllPurchaseTypes();
        void RefreshCache();
    }
}