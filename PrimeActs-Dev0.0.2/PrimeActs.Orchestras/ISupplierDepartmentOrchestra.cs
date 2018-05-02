using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public interface ISupplierDepartmentOrchestra
    {
        SupplierDepartmentViewModel GetSupplierDepartmentModel(System.Guid supplierDepartmentId);
        SupplierDepartmentEditModel CreateSupplierDepartment(SupplierDepartmentEditModel model, List<Guid> locaGuidList);
        SupplierDepartmentWithConsigmentViewModel GetSupplierDepartmentWithConsignmentsModel(Guid id, SupplierDepartmentSearch supplierDepartmentSearch);
        void Initialize(ApplicationUser applicationUser);
        List<SupplierDepartmentEditModel> BuildSupplierDepartmentModels(List<SupplierDepartment> supplierDepartments);
    }
}
