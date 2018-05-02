#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IProduceService : IService<Produce>
    {
        Produce ProduceByName(string ProduceName);
        Produce ProduceById(Guid Id);
        List<Produce> GetAllProduces();
        List<Produce> GetProducesByProduceGroupID(Guid produceGroupID);
        List<Produce> GetStockBoardProducesByProduceGroupID(Guid produceGroupID);
        List<Produce> GetAllProducesByConsignmentItemDepartmentID(Guid ciDepartmentID);
        List<Produce> GetProduces(PrimeActs.Domain.ViewModels.QueryOptions queryOptions, Domain.ViewModels.Produce.SearchObject searchObject, out int totalCount);
    }
}