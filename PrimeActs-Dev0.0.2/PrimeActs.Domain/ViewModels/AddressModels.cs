using System;

namespace PrimeActs.Domain.ViewModels
{
    public class AddressModels
    {
        public Guid AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Postcode { get; set; }
        public string PostalTown { get; set; }
        public string CountyCity { get; set; }
    }
}
