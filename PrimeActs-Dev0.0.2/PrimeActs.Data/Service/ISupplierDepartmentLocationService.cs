using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ISupplierDepartmentLocationService : IService<SupplierDepartmentLocation>
    {
        List<SupplierDepartmentLocation> GetSupplierDepartmentLocationListByDepId(Guid id);
    }
}
