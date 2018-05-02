using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain.ViewModels.Company;

namespace PrimeActs.Domain.ViewModels.Users
{
    public class UserContextModel
    {
        public List<CompanyModel> Companies { get; set; }
        public string DefaultCompanyID { get; set; }
        public string DefaultDivisionID { get; set; }
        public string DefaultDepartmentID { get; set; }

        public UserContextModel()
        {
            Companies = new List<CompanyModel>();
        }
    }
}
