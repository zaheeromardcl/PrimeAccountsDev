using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;


namespace PrimeActs.Orchestras
{
    public interface ISupplierContactOrchestra
    {
        SupplierContactEditModel CreateSupplierContact(SupplierContactEditModel model, List<Guid> locaGuidList, List<Guid> depaGuidList);
        List<SupplierContactEditModel> BuildSupplierContactModels(List<SupplierContact> supplierContacts);
        List<SupplierContactEditModel> GetSupplierContactModels(List<System.Guid> supplierContactIds);
        void Initialize(ApplicationUser principal);
    }
}
