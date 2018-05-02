using System;
using PrimeActs.Domain;

namespace PrimeActs.Data.Service
{
    public interface IConsignmentItemPriceReturnService : IService<ConsignmentItemPriceReturn>
    {
        ConsignmentItemPriceReturn ConsignmentItemPriceReturnByID(Guid ID);
    }
}
