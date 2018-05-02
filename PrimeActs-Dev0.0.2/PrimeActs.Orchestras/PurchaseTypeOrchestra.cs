#region

using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IPurchaseTypeOrchestra
    {
        // ReSharper disable once InconsistentNaming
        List<PurchaseTypeEditModel> GetPurchaseTypeModelsForAC();

        System.Guid GetOutrightPurchaseID();
        
    }

    public class PurchaseTypeOrchestra : IPurchaseTypeOrchestra
    {
        private readonly IPurchaseTypeService _purchaseTypeService;
        private const string ORDER_OUTRIGHT_PURCHASE = "OP"; 

        public PurchaseTypeOrchestra(IPurchaseTypeService purchaseTypeService)
        {
            _purchaseTypeService = purchaseTypeService;
        }

        public List<PurchaseTypeEditModel> GetPurchaseTypeModelsForAC()
        {
            return _purchaseTypeService.GetAllPurchaseTypes().Select(BuildPurchaseTypeEditModelAC).ToList();
        }

        public System.Guid GetOutrightPurchaseID()
        {
            return _purchaseTypeService.PurchaseTypeByName(ORDER_OUTRIGHT_PURCHASE).PurchaseTypeID;
        }

        private PurchaseTypeEditModel BuildPurchaseTypeEditModelAC(PurchaseType entity)
        {
            return new PurchaseTypeEditModel
            {
                PurchaseTypeID = entity.PurchaseTypeID,
                PurchaseTypeCode = entity.PurchaseTypeCode,
                PurchaseTypeName = entity.PurchaseTypeName
            };
        }
    }
}