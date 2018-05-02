#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class CurrencyEditModel
    {
        public Guid CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal DefaultExchangeRate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CurrencyViewModel : CurrencyEditModel
    {
        public CurrencyViewModel()
        {
            CurrencyEditModels = new List<CurrencyEditModel>();
        }

        public CurrencyEditModel CurrencyEditModel { get; set; }
        public List<CurrencyEditModel> CurrencyEditModels { get; set; }
    }
}