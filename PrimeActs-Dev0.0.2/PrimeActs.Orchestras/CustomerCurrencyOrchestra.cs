#region

using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface ICustomerCurrencyOrchestra
    {
        //CustomerCurrencyViewModel GetCustomerCurrencyViewModel();
        //CustomerCurrencyViewModel GetCustomerCurrencyViewModels(int page, int pageSize, string searchString);
        List<CustomerCurrencyEditModel> GetCustomerCurrencyForAutoComplete(string search);
    }

    public class CustomerCurrencyOrchestra : ICustomerCurrencyOrchestra
    {
        private readonly ICustomerCurrencyService _currencyCustomerService;

        public CustomerCurrencyOrchestra(ICustomerCurrencyService currencyCustomerService)
        {
            _currencyCustomerService = currencyCustomerService;
        }

        //public CustomerCurrencyViewModel GetCustomerCurrencyViewModel()
        //{
        //    throw new NotImplementedException();
        //}

        //public CustomerCurrencyViewModel GetCustomerCurrencyViewModels(int page, int pageSize, string searchString)
        //{
        //    throw new NotImplementedException();
        //}
        public List<CustomerCurrencyEditModel> GetCustomerCurrencyForAutoComplete(string search)
        {
            return
                _currencyCustomerService.GetAllCustomerCurrencys()
                    .Select(inc => BuildCustomerCurrencyEditModel(inc))
                    .ToList();
        }

        //private CustomerCurrency ApplyChanges(CustomerCurrencyEditModel model) 
        //{
        //    return null;
        //}

        private CustomerCurrencyEditModel BuildCustomerCurrencyEditModel(CustomerCurrency entity)
        {
            var currencyCustomerEditModel = new CustomerCurrencyEditModel();
            currencyCustomerEditModel.CustomerCurrencyID = entity.CustomerCurrencyID;

            currencyCustomerEditModel.CurrencyID = entity.CurrencyID;
            currencyCustomerEditModel.CustomerID = entity.CustomerID;

            return currencyCustomerEditModel;
        }

        //        currencyCustomerViewModel.CustomerCurrencyEditModel = currencyCustomer == null ? new CustomerCurrencyEditModel() : BuildCustomerCurrencyEditModel(currencyCustomer);
        //    var currencyCustomerViewModel = new CustomerCurrencyViewModel();
        //{

        //private CustomerCurrencyViewModel BuildCustomerCurrencyViewModel(CustomerCurrency currencyCustomer, List<CustomerCurrency> currencyCustomers)
        //        currencyCustomerViewModel.CustomerCurrencyEditModels = currencyCustomers != null ? currencyCustomers.Select(x => BuildCustomerCurrencyEditModel(x)).ToList() : null;
        //    return currencyCustomerViewModel;
        //}
    }
}