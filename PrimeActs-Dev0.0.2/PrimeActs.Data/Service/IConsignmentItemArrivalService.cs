using System;
using System.Collections.Generic;
using PrimeActs.Domain;
namespace PrimeActs.Data.Service
{
    public interface IConsignmentItemArrivalService : IService<ConsignmentItemArrival>
    {
        List<ConsignmentItemArrival> ConsignmentItemArrivalsByConsignmentItemID(Guid ID);

        List<ConsignmentItemArrival> GetAllConsignmentItemArrivals();

        ConsignmentItemArrival ConsignmentItemArrivalByID(Guid ID);
        List<ConsignmentItemArrival> ConsignmentItemArrivalsOnlyByConsignmentItemID(Guid ID);
        ConsignmentItemArrival ConsignmentItemArrivalByIDSimple(Guid ID);
        List<ConsignmentItemArrival> ConsignmentItemArrivalsOnlyByConsignmentItemIDSQL(Guid ID);
    }
}
