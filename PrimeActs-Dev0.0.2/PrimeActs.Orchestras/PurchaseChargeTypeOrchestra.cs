#region

using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IPurchaseChargeTypeOrchestra
    {
        // ReSharper disable once InconsistentNaming
        List<PurchaseChargeTypeEditModel> GetPurchaseChargeTypeModelsForAC();
    }

    public class PurchaseChargeTypeOrchestra : IPurchaseChargeTypeOrchestra
    {
        private readonly IPurchaseChargeTypeService _purchaseChargeTypeService;
        
        public PurchaseChargeTypeOrchestra(IPurchaseChargeTypeService purchaseChargeTypeService)
        {
            _purchaseChargeTypeService = purchaseChargeTypeService;
        }

        public List<PurchaseChargeTypeEditModel> GetPurchaseChargeTypeModelsForAC()
        {
            return _purchaseChargeTypeService.GetAllPurchaseChargeTypes().Select(BuildPurchaseChargeTypeEditModelAC).ToList();
        }
        
        private PurchaseChargeTypeEditModel BuildPurchaseChargeTypeEditModelAC(PurchaseChargeType entity)
        {
            return new PurchaseChargeTypeEditModel
            {
                PurchaseChargeTypeID = entity.PurchaseChargeTypeID,
                PurchaseChargeTypeCode = entity.PurchaseChargeTypeCode,
                PurchaseChargeTypeName = entity.PurchaseChargeTypeName
            };
        }
    }
}