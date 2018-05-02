using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public interface ISupplierLocationOrchestra
    {
        //IEnumerable<SupplierLocationModel> GetSupplierLocationModels(IEnumerable<System.Guid> supplierLocationIds);
        List<SupplierLocationModel> GetSupplierLocationModels(List<System.Guid> supplierLocationIds);
        SupplierLocationModel CreateSupplierLocation(SupplierLocationModel model);
        void Initialize(ApplicationUser principal);
        List<SupplierLocationModel> BuildSupplierLocationModels(List<SupplierLocation> supplierLocations);
    }
}
