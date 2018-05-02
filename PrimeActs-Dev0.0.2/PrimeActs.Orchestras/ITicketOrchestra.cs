#region

using System;
using System.Collections.Generic;
using System.Security.Principal;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Orchestras
{
    public interface ITicketOrchestra
    {
        void Initialize(ApplicationUser principal);
        Guid TicketType(Guid id);
        bool Validate(TicketEditModel model);
        bool Validate(TicketItemEditModel model);
        CreateTicketViewModel GetCreateTicketViewModel(Guid id);
        TicketEditModel GetTicketEditModel(Guid id);
        TicketViewModel GetTicketViewModel(Guid id);
        TicketEditModel GetTicketOnly(Guid id);
        List<TicketItemEditModel> GetTicketItemsOnly(Guid id);
        TicketPagingModel GetTicketPagingModel(QueryOptions queryOptions, SearchObject searchObject);
        TicketItemPagingModel GetTicketItemPagingModel(Guid id, QueryOptions queryOptions);
        List<Ticket> GetAllCustomerDepartmentTickets(Guid customerDepartmentID);
        ResultList<TicketEditModel> GetTickets(QueryOptions queryOptions, SearchObject searchObject);
        TicketEditModel CreateTicket(TicketEditModel model);
        TicketEditModel UpdateTicket(TicketEditModel model);
        void RemoveTicketItem(Guid id);
        void SaveTicket(TicketEditModel model);
        TicketItemEditModel CreateTicketItem(TicketItemEditModel model);
        TicketItemEditModel UpdateTicketItem(TicketItemEditModel model);
        TicketPrintViewModel GetTicketPrintViewModel(Guid id);
        SalesLedgerEntry CreateSalesLedgerEntry(TicketEditModel model);
        ReceiptTicketViewModel GetReceiptTicketViewModel(Guid id);
        CreateTransferPageViewModel GetCreateTransferPageViewModel();
        Guid CreateTransferTicket(TransferCreateViewModel model);
        ResultList<DailyCashTicketModel> GetDailyCashTickets(QueryOptions queryOptions, SearchObject searchObject);
        ResultList<SalesLedgerEntryViewModel> GetDailySalesLedgerEntries(QueryOptions queryOptions, SearchObject searchObject);
        ResultList<TicketEditModel> GetDailyCashTicketsOld(QueryOptions queryOptions, SearchObject searchObject);
        ResultList<DailyCashTicketModel> GetDailyCashTicketsAll(QueryOptions queryOptions, SearchObject searchObject);
        ResultList<vwCashTicket> GetDailyCashTicketsAllViewModel(QueryOptions queryOptions, SearchObject searchObject);

        ResultList<vwCashTicketViewModel> GetDailyCashTicketsAllViewModelvw(QueryOptions queryOptions,
            SearchObject searchObject);
    }
}