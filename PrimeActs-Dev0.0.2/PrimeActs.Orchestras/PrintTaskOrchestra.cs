using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Extensions;

namespace PrimeActs.Orchestras
{
    public interface IPrintTaskOrchestra
    {
        List<PrintTask> GetPrintTaskForName(string search);
        void Initialize1(ApplicationUser principal);
    }

    public class PrintTaskOrchestra : IPrintTaskOrchestra
    {
        private ApplicationUser _principal;
        private readonly IPrintTaskService _printTaskService;
        private readonly IDepartmentPrintTaskOrchestra _departmentPrintTaskOrchestra;

        public PrintTaskOrchestra(IPrintTaskService printTaskService, IDepartmentPrintTaskOrchestra printTaskOrchestra)
        {
            _printTaskService = printTaskService;
            _departmentPrintTaskOrchestra = printTaskOrchestra;
        }

        public void Initialize1(ApplicationUser principal)
        {
            _principal = principal;

        }

        public List<PrintTask> GetPrintTaskForName(string search)
        {
            List<PrintTask> printTaskList = new List<PrintTask>();
            printTaskList = _printTaskService.PrintTaskByName(search);

            foreach (var t in printTaskList) // done manually, Linq causing an error, had to move on with code, will typically only be one or 2 entries anyway.
            {
                // get associated DepartmentPrintTask
                var deptPrintTaskList = _departmentPrintTaskOrchestra.GetDepartmentPrintTaskForTaskID(t.PrintTaskID);
                t.DepartmentPrintTasks = new List<DepartmentPrintTask>();
                t.DepartmentPrintTasks.AddRange(deptPrintTaskList);
            }
            return printTaskList;
        }
    }
}
