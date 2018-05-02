#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IDepartmentPrinterService : IService<DepartmentPrinter>
    {
        DepartmentPrinter GetAllDepartmentPrintersByDepartmentId(Guid Id);
        List<DepartmentPrinter> GetAllDepartmentPrinters();
    }

    public class DepartmentPrinterService : Service<DepartmentPrinter>, IDepartmentPrinterService
    {
        private readonly IRepositoryAsync<DepartmentPrinter> _repository;

        public DepartmentPrinterService(IRepositoryAsync<DepartmentPrinter> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public DepartmentPrinter GetAllDepartmentPrintersByDepartmentId(Guid departmentID)
        {
            return _repository.Query().Select().Where(inc => inc.PrinterID == departmentID).FirstOrDefault();
        }



        public List<DepartmentPrinter> GetAllDepartmentPrinters()
        {
            return null;// _repository.Query().Include(x => x.Printer).Select().ToList();
        }
    }
}