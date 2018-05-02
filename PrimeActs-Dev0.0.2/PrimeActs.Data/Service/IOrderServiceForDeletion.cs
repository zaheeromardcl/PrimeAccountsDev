#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IOrderServiceForDeletion : IService<Consignment>
    {
        //    List<TicketItem> TicketItemsByTicketID(Guid id);
        //    List<TicketItem> GetAllTicketItems();
        Consignment OrderById(Guid Id);
        List<Consignment> GetOrders(QueryOptions queryOptions, OrderSearchObject searchObject, out int totalCount);
        //    TicketItem TicketItemByID(Guid id);
        //    List<TicketItem> GetTicketItemsOnly(Guid id);
        //    List<TicketItem> GetTicketItemsForNewInvoice(Guid divisionID, DateTime startDate, DateTime endDate);
        //    List<TicketItem> GetEditedTicketItemsForInvoice(Guid divisionID, DateTime startDate, DateTime endDate);
        //    List<TicketItem> GetTicketItemsForCreditNote(Guid divisionID, DateTime startDate, DateTime endDate);
    }
}