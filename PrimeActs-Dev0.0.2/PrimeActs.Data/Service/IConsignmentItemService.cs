#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IConsignmentItemService : IService<ConsignmentItem>
    {
        List<ConsignmentItem> ConsignmentItemsByConsignmentID(Guid ID);

        List<ConsignmentItem> GetAllConsignmentItems();

        ConsignmentItem ConsignmentItemByID(Guid ID);

        //List<ConsignmentItem> ConsignmentItemsByProduceForDateRange(Guid ProduceID);
        List<ConsignmentItem> ConsignmentItemsByProduceForDateRange(Guid ProduceID, Guid optDepartment);
        ConsignmentItem ConsignmentItemByIDSimple(Guid ID);
        int ConsignmentItemsReceivedQuantity(Guid ConsignmentItemID);
    }
}