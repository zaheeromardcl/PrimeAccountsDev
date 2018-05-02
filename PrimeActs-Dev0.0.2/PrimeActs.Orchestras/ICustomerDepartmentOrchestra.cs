using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public interface ICustomerDepartmentOrchestra
    {
        List<CustomerDepartment> GetCustomerDepartments(Guid customerDepartmetnID);
        CustomerDepartmentViewModel GetCustomerDepartment(Guid id);
        CustomerDepartmentViewModel UpdateRebate(CustomerDepartmentViewModel model);
        void Initialize(ApplicationUser applicationUser);
        CustomerDepartmentEditModel GetCustomerDepartmentEditModel(Guid id);
        List<CustomerDepartmentViewModel> GetAllCustomerDepartments();
        CustomerDepartmentEditModel CreateCustomerDepartment(CustomerDepartmentEditModel model, List<Guid> locaGuidList);
        List<CustomerDepartmentEditModel> BuildCustomerDepartmentModels(List<CustomerDepartment> customerDepartments);
    }
}