using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Data.Service
{
    public interface ICashTicketService : IService<vwCashTicket>
    {
        List<vwCashTicket> GetDailyCashTickets(QueryOptions queryOptions, SearchObject searchObject, out int totalCount);
        List<vwCashTicket> GetDailyCashTicketsAll(QueryOptions queryOptions, SearchObject searchObject);
    }
}
