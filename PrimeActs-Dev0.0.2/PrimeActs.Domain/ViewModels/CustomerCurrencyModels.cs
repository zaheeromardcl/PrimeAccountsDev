#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class CustomerCurrencyEditModel
    {
        public Guid CustomerCurrencyID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid CurrencyID { get; set; }
        //public string CustomerCompanyName { get; set; }
        //public string CurrencyName { get; set; }
        public string SortOrder { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }


    public class CustomerCurrencyViewModel : CustomerCurrencyEditModel
    {
        public CustomerCurrencyViewModel()
        {
            CustomerCurrencyEditModels = new List<CustomerCurrencyEditModel>();
        }

        public CustomerCurrencyEditModel CustomerCurrencyEditModel { get; set; }
        public List<CustomerCurrencyEditModel> CustomerCurrencyEditModels { get; set; }
    }
}