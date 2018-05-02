using System;
using System.Collections.Generic;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain;

namespace PrimeActs.Orchestras
{
    public interface ICustomerOrchestra
    {
        List<CustomerDepartmentEditModel> GetCustomerDepartmentModelsForAC(string search);
        CustomerEditModel CreateCustomer(CustomerEditModel model);
        void Initialize(ApplicationUser applicationUser);
        List<CustomerEditModel> GetCustomerModelsForAC(string search);
        CustomerEditModel GetCustomerModelByCustomerID(Guid customerID);
        CustomerPagingModel GetCustomerPagingModel(QueryOptions queryOptions, SearchObject searchObject);
    }
}
//CustomerViewModel GetCustomerViewModel();
//CustomerViewModel GetCustomerViewModels(int page, int pageSize, string searchString);
