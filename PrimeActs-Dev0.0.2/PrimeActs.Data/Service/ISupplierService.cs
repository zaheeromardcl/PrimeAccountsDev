using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Data.Service
{
    public interface ISupplierService : IService<Supplier>
    {
        Supplier GetSupplierByIdFromRepo(Guid Id);
        Supplier SupplierByName(string SupplierName);
        Supplier SupplierById(Guid Id);
        List<Supplier> GetAllSuppliers();
        List<Supplier> GetAllSupplierDepts();
        List<Supplier> GetSuppliers(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        Supplier GetSupplierOnly(Guid id);
        void RefreshCache();
    }
}
