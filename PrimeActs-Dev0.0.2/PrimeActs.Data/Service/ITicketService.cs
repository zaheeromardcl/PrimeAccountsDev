#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ITicketService : IService<Ticket>
    {
        Ticket TicketByRef(string TicketRef);
        Ticket TicketById(Guid Id);
        Ticket GetTicketOnly(Guid Id);
        Ticket GetTicket(Guid Id);
        List<Ticket> GetAllTicketsByCustomerDepartment(Guid customerdepartmentID);
        List<Ticket> GetAllTickets();
        List<Ticket> GetTickets(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        List<Ticket> GetDailyCashTickets(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
    }
}