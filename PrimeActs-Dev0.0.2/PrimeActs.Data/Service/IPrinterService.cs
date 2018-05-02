using PrimeActs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Service
{
    public interface IPrinterService : IService<Printer>
    {
        List<Printer> GetAllPrinters();
        List<Printer> GetAllPrintersByDepartmentId(Guid departmentID);
        List<Printer> DefaultPrinters();
    }
}
