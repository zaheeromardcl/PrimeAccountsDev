using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Users;

namespace PrimeActs.Domain.ViewModels
{
    public class DisectionReportViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ProduceName { get; set; }
        public string ProduceId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public string ProduceGroupStartId { get; set; }
        public string ProduceGroupStartName { get; set; }
        public string ProduceGroupEndId { get; set; }
        public string ProduceGroupEndName { get; set; }
        public string SelectedCompanyId { get; set; }
        public string SelectedDivisionId { get; set; }
        public string SelectedDepartmentId { get; set; }
        public UserContextModel UserContextModel { get; set; }
    }
}
