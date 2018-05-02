using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public interface ISupplierOrchestra
    {
        SupplierEditModel GetSupplierModelBySupplierID(Guid supplierID);
        List<SupplierEditModel> GetSupplierModelsForAC(string search);
        List<SupplierDepartmentEditModel> GetSupplierDeptModelsForAC(string search);
        SupplierEditModel CreateSupplier(SupplierEditModel model);
        void Initialize(ApplicationUser applicationUser);
        SupplierPagingModel GetSupplierPagingModel(QueryOptions queryOptions, SearchObject searchObject);
        SupplierItemPagingModel GetSupplierItemPagingModel(Guid id, QueryOptions queryOptions);
        SupplierEditModel GetSupplierOnly(Guid id);
        List<SupplierItemEditModel> GetSupplierItemsOnly(Guid id);
        //List<PrimeActs.Domain.ViewModels.SupplierEditModel> GetSupplierModelsForAC(string search);
        //List<SupplierDeptViewModel> GetSupplierDeptModelsForAC(string search);
    }
}
