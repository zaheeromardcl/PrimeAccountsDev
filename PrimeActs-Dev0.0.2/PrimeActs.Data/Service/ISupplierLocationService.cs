using System;
using System.Collections.Generic;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface ISupplierLocationService : IService<SupplierLocation>
    {
        IEnumerable<SupplierLocation> GetSupplierLocations(IEnumerable<Guid> supplierLocationIds);
        SupplierLocation GetSupplierLocationById(Guid id);
    }
}
