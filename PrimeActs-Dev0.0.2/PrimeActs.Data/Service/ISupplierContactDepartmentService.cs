using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ISupplierContactDepartmentService : IService<SupplierContactDepartment>
    {
        List<SupplierContactDepartment> GetSupplierContactDepartmentListByConId(Guid id);
    }
}
