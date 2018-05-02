using System;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Data.Service
{
    public interface ISupplierDepartmentService : IService<SupplierDepartment>
    {
        SupplierDepartment SupplierDepartmentById(Guid Id);
        SupplierDepartment SupplierDepartmentDetailsById(Guid Id);
        SupplierDepartment SupplierDepartmentBasicById(Guid Id);
    }
}
