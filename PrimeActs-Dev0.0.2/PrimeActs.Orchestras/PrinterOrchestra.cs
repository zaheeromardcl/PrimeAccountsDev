using PrimeActs.Data.Service;
using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Orchestras
{
    public interface IPrinterOrchestra
    {
        List<Printer> GetPrintersForDepartment(Guid departmentID);
        List<Printer> GetPrintersForDepartmentNoDefaults(Guid departmentID);
        List<Printer> GetPrintersForUserSelectedDepartment();
        void Initialize(ApplicationUser principal);
    }

    public class PrinterOrchestra : IPrinterOrchestra
    {
        private readonly IPrinterService _printerService;
        private readonly IDepartmentPrinterService _departmentPrinterService;
        private ApplicationUser _principal; // DC added princpal logic to cater allow Users Printers to be determined

        public PrinterOrchestra(IPrinterService printerService, IDepartmentPrinterService departmentPrinterService)
        {
            _printerService = printerService;
            _departmentPrinterService = departmentPrinterService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<Printer> GetPrintersForDepartment(Guid departmentID)
        {
            List<Printer> PrintersbyDepartmentbyId = new List<Printer>();
            PrintersbyDepartmentbyId = _printerService.GetAllPrintersByDepartmentId(departmentID);
            if (PrintersbyDepartmentbyId == null || PrintersbyDepartmentbyId.Count == 0)
            {
                // Get default printers
                var defaultPrinters = _printerService.DefaultPrinters();
                return defaultPrinters;

            }
            return PrintersbyDepartmentbyId;
        
        }

        // Sometimes we do not want defaults
        public List<Printer> GetPrintersForDepartmentNoDefaults(Guid departmentID)
        {
            List<Printer> PrintersbyDepartmentbyId = new List<Printer>();
            PrintersbyDepartmentbyId = _printerService.GetAllPrintersByDepartmentId(departmentID);
            return PrintersbyDepartmentbyId;
        }
        // Sometimes we do not want defaults
        public List<Printer> GetPrintersForUserSelectedDepartment()
        {
            List<Printer> PrintersbyDepartmentbyId = new List<Printer>();
            PrintersbyDepartmentbyId = _printerService.GetAllPrintersByDepartmentId(_principal.SelectedDepartmentId);
            return PrintersbyDepartmentbyId;
        }

           
        }
    }

