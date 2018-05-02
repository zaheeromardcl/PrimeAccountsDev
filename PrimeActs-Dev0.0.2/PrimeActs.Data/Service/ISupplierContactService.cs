using System;
using System.Collections.Generic;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ISupplierContactService : IService<SupplierContact>
    {
        IEnumerable<SupplierContact> GetSupplierContacts(List<Guid> supplierContactIds);
    }
}
