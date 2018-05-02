#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IDepartmentService : IService<Department>
    {
        Department DepartmentByName(string DepartmentName);
        Department DepartmentById(Guid Id);
        List<Department> GetAllDepartments();
        List<Department> GetDepartments(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Department.SearchObject searchObject, out int totalCount);
        List<Department> GetAllDeptByDivID(Guid DivisionId);
        void RefreshCache();
    }
}