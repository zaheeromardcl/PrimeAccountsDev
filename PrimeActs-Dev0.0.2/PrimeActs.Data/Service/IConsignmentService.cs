#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IConsignmentService : IService<Consignment>
    {
        Consignment ConsignmentByRef(string consignmentRef);
        Consignment ConsignmentById(Guid Id);
        Consignment ConsignmentAndSupplierDepartmentById(Guid Id);
     
        List<Consignment> GetAllConsignments();
        List<Consignment> GetConsignments(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        Consignment LastConsigmentByUserId(Guid userID);
        Consignment GetConsignment(Guid consignmentID);
        IEnumerable<Consignment> GetConsignmentsBySupplierDepartmentID(Guid Id, SupplierDepartmentSearch supplierDepartmentSearch);
    }
}