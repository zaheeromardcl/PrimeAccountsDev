using System;

namespace PrimeActs.UI.Models
{
    public class AutoCompleteSupplierDepartment : Autocomplete
    {
        public string CountryName { get; set; }
        public Guid CountryID { get; set; }
    }
}