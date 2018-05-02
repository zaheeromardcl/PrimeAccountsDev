using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Data.Service;
using PrimeActs.Domain;

namespace PrimeActs.Orchestras
{
    public interface IDepartmentPrintTaskOrchestra
    {
        DepartmentPrintTask GetDepartmentPrintTaskForID(Guid ID);
        List<DepartmentPrintTask> GetDepartmentPrintTaskForTaskID(Guid ID);
        void Initialize1(ApplicationUser principal);
    }
    public class DepartmentPrintTaskOrchestra : IDepartmentPrintTaskOrchestra
    {
        private ApplicationUser _principal;
        private readonly IDepartmentPrintTaskService _departmentPrintTaskService;

        public DepartmentPrintTaskOrchestra(IDepartmentPrintTaskService departmentPrintTaskService)
        {
            _departmentPrintTaskService = departmentPrintTaskService;
        }

        public void Initialize1(ApplicationUser principal)
        {
            _principal = principal;

        }

        public DepartmentPrintTask GetDepartmentPrintTaskForID(Guid ID)
        {
            DepartmentPrintTask departmentPrintTask = new DepartmentPrintTask();
            departmentPrintTask = _departmentPrintTaskService.DepartmentPrintTaskByID(ID);
            return departmentPrintTask;
        }

        public List<DepartmentPrintTask> GetDepartmentPrintTaskForTaskID(Guid ID)
        {
            List<DepartmentPrintTask> departmentPrintTask = new List<DepartmentPrintTask>();
            departmentPrintTask = _departmentPrintTaskService.DepartmentPrintTaskByTaskID(ID);
            return departmentPrintTask;
        }
    }
}
