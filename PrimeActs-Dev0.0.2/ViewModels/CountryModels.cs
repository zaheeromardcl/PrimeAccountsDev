#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class CountryEditModel
    {
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class CountryViewModel : CountryEditModel
    {
    }
}