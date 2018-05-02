using System;
using System.Collections.Generic;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ICustomerDepartmentService : IService<CustomerDepartment>
    {
        //CustomerDepartment DepartmentByName(string DepartmentName);
        CustomerDepartment CustomerDepartmentById(Guid Id);
        List<CustomerDepartment> GetAllCustomerDepartments();
        List<CustomerDepartment> GetAllCustomerDepartmentsByDivision(Guid id);
        CustomerDepartment CustomerDepartmentByCustomerDepartmentId(Guid cdId);
        List<CustomerDepartment> CustomerDepartmentsByCustomerDepartmentID(Guid cdId);
        void RefreshCache();
    }
}