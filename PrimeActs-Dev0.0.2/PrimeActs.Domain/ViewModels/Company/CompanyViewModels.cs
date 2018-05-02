using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public class CompanyViewModel
    {
        public Guid CompanyID { get; set; }
        public string CompanyName { get; set; }
       
        public string CompanyNo { get; set; }
        public byte[] Logo { get; set; }

        public string LogoAsString
        {
            get
            {
                if (Logo != null)
                {
                    return Convert.ToBase64String(Logo);
                }
                return null;
            }
        }

        public AddressViewModel Address { get; set; }
    }

    public class AddressViewModel
    {
        public Guid AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Postcode { get; set; }
    }
}
