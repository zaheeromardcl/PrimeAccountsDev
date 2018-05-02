using System;
using System.Collections.Generic;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface IPurchaseChargeTypeService : IService<PurchaseChargeType>
    {
        PurchaseChargeType PurchaseChargeTypeByName(string purchaseChargeType);
        PurchaseChargeType PurchaseChargeTypeById(Guid Id);
        List<PurchaseChargeType> GetAllPurchaseChargeTypes();
        void RefreshCache();
    }
}
