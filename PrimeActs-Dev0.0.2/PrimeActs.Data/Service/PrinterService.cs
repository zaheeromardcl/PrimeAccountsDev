using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PrimeActs.Data.Service
{
    public class PrinterService : Service<Printer>, IPrinterService
    {
        private readonly IRepositoryAsync<Printer> _repository;
        
        public PrinterService(IRepositoryAsync<Printer> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public List<Printer> GetAllPrinters() 
        {
            var ListofPrinters =_repository
                .Query()
                .Select()
                .ToList();
            return ListofPrinters;
        }


        public List<Printer> GetAllPrintersByDepartmentId(Guid departmentID)
        {

            //var ListofPrintersByDepartmentID = _repository.Query()
            //    .Select()
            //    .Where((x => x.DepartmentPrinters.Any(z => z.DepartmentID == departmentID)))
            //    .ToList();
            //List<Printer> listOfPrintersByDepartmentID = _repository.Query().Include(t => t.DepartmentPrinters).Select().Where((x => x.DepartmentPrinters.Any(z => z.DepartmentID == departmentID))).ToList();
            List<Printer> listOfPrintersByDepartmentID = _repository.Query().Include(t => t.DepartmentPrinters).Select().Where((x => x.DepartmentPrinters.Any(z => z.DepartmentID == departmentID))).ToList();
            
            return listOfPrintersByDepartmentID;
        }

        public List<Printer> DefaultPrinters()
        {
            return
                _repository
                    .Query(p => p.IsActive == true)
                    .OrderBy(q => q.OrderBy(p => p.DefaultOrder))
                    .Select()
                    .Take(2)
                    .ToList();
        }
    }
}
