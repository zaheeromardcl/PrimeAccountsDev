
﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Contexts;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ITicketItemService : IService<TicketItem>
    {
        List<TicketItem> TicketItemsByTicketID(Guid id);
        List<TicketItem> GetAllTicketItemsByConsignmentItemID(Guid id);
        List<TicketItem> GetAllTicketItems();
        TicketItem TicketItemByID(Guid id);
        List<TicketItem> GetTicketItemsOnly(Guid id);
        List<TicketItem> GetTicketItemsForNewInvoice(Guid divisionID, DateTime startDate, DateTime endDate, bool isCashSale);
        List<TicketItem> GetEditedTicketItemsForInvoice(Guid divisionID, DateTime startDate, DateTime endDate, bool isCashSale);
        List<TicketItem> GetTicketItemsForCreditNote(Guid divisionID, DateTime startDate, DateTime endDate, bool isCashSale);
        List<DailySalesReport> TicketItemsForDailySalesReports(DateTime idDateTime);
        List<vwConsignmentTicketsByDate> GetConsignmentsSoldToday(Guid id, DateTime ticketDate);
        List<vwConsignmentReturns> GetConsignmentReturns(Guid id);
        List<vwConsignmentTicketsByDate> GetConsignmentsSoldPriorToday(Guid id, DateTime ticketDate);
        List<vwConsignmentTicketsSingleByDate> GetConsignmentTicketsSingleByDate(Guid id, DateTime ticketDate);
        List<TicketItem> GetAllTicketItemsByConsignmentItemIDSQL(Guid id);
    }
    public class TicketItemService : Service<TicketItem>, ITicketItemService
    {
        private readonly IRepositoryAsync<TicketItem> _repository;
        private readonly IRepositoryAsync<SalesInvoiceItem> _repositorySalesInvoiceItem;

        public TicketItemService(IRepositoryAsync<TicketItem> repository, IRepositoryAsync<SalesInvoiceItem> repositorySalesInvoiceItem)
            : base(repository)
        {
            _repository = repository;
            _repositorySalesInvoiceItem = repositorySalesInvoiceItem;
        }

        public List<TicketItem> TicketItemsByTicketID(Guid id)
        {
            return
                _repository.Query(fil => fil.TicketID == id)
                    .Include(inc => inc.ConsignmentItem.Consignment)
                    .Include(inc => inc.Ticket)
                    .Select()
                    .ToList();
        }



        public List<DailySalesReport> TicketItemsForDailySalesReports(DateTime startDateTime)
        {
            var nextDay = startDateTime.AddDays(1);

            //return results;
            DateTime endDate = startDateTime.AddDays(1);
            string qry = "select * from tblTicketItem INNER JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID WHERE (tblTicket.TicketDate >= @p0 AND tblTicket.TicketDate < @p1)";
            string qry2 = "select * from tblTicketItem WHERE (tblTicketItem.CreatedDate >= '2016/06/14' and tblTicketItem.CreatedDate < '2016/06/15' AND IsCashSale = '1')";
            string qrytest =
                "select * from tblTicketItem WHERE (tblTicketItem.TicketItemID = '00760000-0000-0006-8307-508173720759')";
            var @p0 = String.Format("{0:yyyy/MM/dd}", startDateTime);
            var @p1 = String.Format("{0:yyyy/MM/dd}", endDate);
            // changed to use tblUser instead of ApplicationUser
            //            string dailySalesQuery = @"SELECT tblTicket.TicketReference, tblTicket.TicketDate, tblTicket.IsCashSale, tblTicketItem.TicketItemDescription, tblTicketItem.TicketItemQuantity, 
            //                         tblTicketItem.TicketItemTotalPrice, tblTicketItem.PorterageValue, tblConsignment.ConsignmentReference, tblConsignmentItem.PackSize, 
            //                         tblCustomerDepartment.CustomerDepartmentName, tblCustomer.CustomerCompanyName, tblConsignmentItem.Brand, tblConsignmentItem.PackType, 
            //                         tblTicket.Handling, tblApplicationUser.Firstname, tblApplicationUser.Lastname, tblApplicationUser.Nickname
            //FROM            tblTicket INNER JOIN
            //                         tblTicketItem ON tblTicket.TicketID = tblTicketItem.TicketID INNER JOIN
            //                         tblConsignmentItem ON tblTicketItem.ConsignmentItemID = tblConsignmentItem.ConsignmentItemID INNER JOIN
            //                         tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID INNER JOIN
            //                         tblCustomerDepartment ON tblTicket.CustomerDepartmentID = tblCustomerDepartment.CustomerDepartmentID INNER JOIN
            //                         tblCustomer ON tblCustomerDepartment.CustomerID = tblCustomer.CustomerID LEFT JOIN
            //                         tblApplicationUser ON tblTicket.SalesPersonUserID = tblApplicationUser.ApplicationUserId
            //WHERE        (tblTicket.TicketDate >= @p0) AND (tblTicket.TicketDate < @p1)
            //ORDER BY tblTicket.TicketReference, tblTicketItem.TicketItemID";

            string dailySalesQuery =
                @"SELECT tblTicket.TicketReference, tblTicket.TicketDate, tblTicket.IsCashSale, tblTicketItem.TicketItemDescription, tblTicketItem.TicketItemQuantity, 
                         tblTicketItem.TicketItemTotalPrice, tblTicketItem.PorterageValue, tblConsignment.ConsignmentReference, tblConsignmentItem.PackSize, 
                         tblCustomerDepartment.CustomerDepartmentName, tblCustomer.CustomerCompanyName, tblConsignmentItem.Brand, tblConsignmentItem.PackType, 
                         tblTicket.Handling, tblUser.Firstname, tblUser.Lastname, tblUser.Nickname, tblDepartment.DepartmentCode, tblProduce.ProduceName
FROM            tblTicket INNER JOIN
                         tblTicketItem ON tblTicket.TicketID = tblTicketItem.TicketID INNER JOIN
                         tblConsignmentItem ON tblTicketItem.ConsignmentItemID = tblConsignmentItem.ConsignmentItemID INNER JOIN
                         tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID INNER JOIN
                         tblCustomerDepartment ON tblTicket.CustomerDepartmentID = tblCustomerDepartment.CustomerDepartmentID INNER JOIN
                         tblCustomer ON tblCustomerDepartment.CustomerID = tblCustomer.CustomerID INNER JOIN
                         tblDepartment ON tblConsignmentItem.DepartmentID = tblDepartment.DepartmentID INNER JOIN
                         tblProduce ON tblConsignmentItem.ProduceID = tblProduce.ProduceID LEFT JOIN
                         tblUser ON tblTicket.SalesPersonUserID = tblUser.UserID
WHERE        (tblTicket.TicketDate >= @p0 ) AND (tblTicket.TicketDate < @p1)
ORDER BY tblTicket.TicketReference, tblTicketItem.TicketItemID";


            List<DailySalesReport> dailySales;
            using (var context = new PAndIContext())
            {
                dailySales = context.Database.SqlQuery<DailySalesReport>(dailySalesQuery, startDateTime, endDate).ToList();
            }

            return dailySales;
        }

        public List<vwConsignmentTicketsByDate> GetConsignmentsSoldToday(Guid id, DateTime ticketDate)
        {
            var ticketDateString = String.Format("{0:yyyy/MM/dd}", ticketDate);

            using (var context = new PrimeActs.Data.Contexts.PAndIContext())
            {
                var sqlqry = @"SELECT [TicketDate]
      ,[ConsignmentItemID]
      ,[UnitPrice]
      ,[TotalPrice]
      ,[TotalQuantity]
  FROM [vwConsignmentTicketsByDate] where ConsignmentItemID = @pID and TicketDate >= @p1";

                var @pID = new System.Data.SqlClient.SqlParameter("@pID", id);
                var @p1 = new System.Data.SqlClient.SqlParameter("@p1", ticketDateString);
                var recordsReturned = context.Database.SqlQuery<vwConsignmentTicketsByDate>(sqlqry, @pID, @p1).ToList();
                return recordsReturned;
            }
        }

        public List<vwConsignmentTicketsByDate> GetConsignmentsSoldPriorToday(Guid id, DateTime ticketDate)
        {
            var ticketDateString = String.Format("{0:yyyy/MM/dd}", ticketDate);

            using (var context = new PrimeActs.Data.Contexts.PAndIContext())
            {
                var sqlqry = @"SELECT [TicketDate]
      ,[ConsignmentItemID]
      ,[UnitPrice]
      ,[TotalPrice]
      ,[TotalQuantity]
  FROM [vwConsignmentTicketsByDate] where ConsignmentItemID = @pID and TicketDate < @p1";

                var @pID = new System.Data.SqlClient.SqlParameter("@pID", id);
                var @p1 = new System.Data.SqlClient.SqlParameter("@p1", ticketDateString);
                var recordsReturned = context.Database.SqlQuery<vwConsignmentTicketsByDate>(sqlqry, @pID, @p1).ToList();
                return recordsReturned;
            }
        }

        public List<vwConsignmentReturns> GetConsignmentReturns(Guid id)
        {
            using (var context = new PrimeActs.Data.Contexts.PAndIContext())
            {
                var sqlqry = "select ConsignmentItemID, ReturnUnitPrice, TotalQuantity from vwConsignmentReturns where ConsignmentItemID = @pID";
                //var sqlqry = @"ConsignmentItemID ,ReturnUnitPrice, TotalQuantity FROM vwConsignmentReturns where ConsignmentItemID = @pID";

                //var test = context.Database.SqlQuery<vwConsignmentReturns>(sqlqrytest).ToList();
                var @pID = new System.Data.SqlClient.SqlParameter("@pID", id);
                var recordsReturned = context.Database.SqlQuery<vwConsignmentReturns>(sqlqry, @pID).ToList();
                //var recordsReturned = context.Database.SqlQuery<vwConsignmentReturns>(sqlqry, @pID).ToList();

                return recordsReturned;
            }
        }

        public List<vwConsignmentTicketsSingleByDate> GetConsignmentTicketsSingleByDate(Guid id, DateTime ticketDate)
        {
            var ticketDateString = String.Format("{0:yyyy/MM/dd}", ticketDate);
            
            using (var context = new PrimeActs.Data.Contexts.PAndIContext())
            {
                var sqlqry = "select ConsignmentItemID, TicketDate, ShowTicketReference, CustomerCode, TicketItemQuantity, TicketItemTotalPrice, UnitPrice from vwConsignmentTicketsSingleByDate where ConsignmentItemID = @pID and TicketDate >= @p1";
                //var sqlqry = @"ConsignmentItemID ,ReturnUnitPrice, TotalQuantity FROM vwConsignmentReturns where ConsignmentItemID = @pID";

                //var test = context.Database.SqlQuery<vwConsignmentReturns>(sqlqrytest).ToList();
                var @pID = new System.Data.SqlClient.SqlParameter("@pID", id);
                var @p1 = new System.Data.SqlClient.SqlParameter("@p1", ticketDateString);
                var recordsReturned = context.Database.SqlQuery<vwConsignmentTicketsSingleByDate>(sqlqry, @pID, @p1).ToList();
                //var recordsReturned = context.Database.SqlQuery<vwConsignmentReturns>(sqlqry, @pID).ToList();

                return recordsReturned;
            }
        }

        public TicketItem TicketItemByID(Guid id)
        {
            return
                _repository.Query(fil => fil.TicketItemID == id)
                    .Include(inc => inc.Ticket)
                    .Select()
                    .FirstOrDefault();
        }

        public List<TicketItem> GetAllTicketItems()
        {
            return _repository.Query().Select().ToList();
        }

        public List<TicketItem> GetAllTicketItemsByConsignmentItemID(Guid id)
        {
            return _repository.Query(fil => fil.ConsignmentItemID == id).Select().ToList();
        }

        public List<TicketItem> GetAllTicketItemsByConsignmentItemIDSQL(Guid id)
        {
            var @p0 = id.ToString();
            string qry = "select * from tblTicketItem where ConsignmentItemID = @p0 ";
            var ticketItems =
                _repository.SelectQuery(qry, p0);
            return ticketItems.ToList();

            //return _repository.Query(fil => fil.ConsignmentItemID == id).Select().ToList();
        }

        public List<TicketItem> GetTicketItemsOnly(Guid id)
        {
            return
                _repository.Query(fil => fil.TicketID == id)
                    .Include(inc => inc.Department)
                    .Include(inc => inc.ConsignmentItem.Produce)
                    .Include(inc => inc.ConsignmentItem.Consignment)
                    .Include(inc => inc.Ticket)
                    .Select()
                    .OrderBy(inc => inc.CreatedDate)
                    .ToList();
        }

        public List<TicketItem> GetTicketItemsForNewInvoice(Guid divisionID, DateTime startDate, DateTime endDate, bool isCashSale)
        {
            return
                _repository.Query(x => (((x.Ticket.IsCashSale == true) | (x.Ticket.IsCashSale == false))) & (x.Ticket.IsHistory == false) & (x.CreatedDate >= startDate & x.CreatedDate <= endDate) & x.Ticket.DivisionID == divisionID & x.IsLatest.Value == true & x.TicketItemTotalPrice >= 0 & x.TicketItemID == x.OriginalTicketItemID.Value & !x.SalesInvoiceItems.Any())
                    .Include(x => x.Ticket)
                    .Include(x => x.Ticket.CustomerDepartment)
                    .Select()
                    .ToList();
        }

        public List<TicketItem> GetEditedTicketItemsForInvoice(Guid divisionID, DateTime startDate, DateTime endDate, bool isCashSale)
        {
            var ticketItems =
               _repository.Query(x => (((x.Ticket.IsCashSale == true) | (x.Ticket.IsCashSale == false))) & (x.Ticket.IsHistory == false) & (x.CreatedDate >= startDate & x.CreatedDate <= endDate) & x.Ticket.DivisionID == divisionID & x.IsLatest.Value == true & x.TicketItemTotalPrice >= 0 & x.TicketItemID != x.OriginalTicketItemID.Value & !x.SalesInvoiceItems.Any())
                   .Include(x => x.Ticket)
                   .Include(x => x.Ticket.CustomerDepartment)
                   .Select()
                   .GroupBy(x => x.OriginalTicketItemID)
                   .Select(x => x.OrderByDescending(y => y.CreatedDate))
                   .Select(y => y.First())
                   .ToList();
            var returnItems = new List<TicketItem>();

            foreach (var ticketItem in ticketItems)
            {


                if (ticketItem.TicketItemID != ticketItem.OriginalTicketItemID.Value)
                {
                    var existingInvoice = _repositorySalesInvoiceItem.Query(x => x.TicketItemID == ticketItem.OriginalTicketItemID.Value)
                        .Select();
                    if (existingInvoice != null)
                    {
                        var existingTicket = _repository.Query(x => x.TicketItemID == ticketItem.OriginalTicketItemID.Value).Select().SingleOrDefault();
                        if (existingTicket != null)
                        {
                            if (ticketItem.TicketItemTotalPrice >= existingTicket.TicketItemTotalPrice)
                                ticketItem.TicketItemTotalPrice = ticketItem.TicketItemTotalPrice - existingTicket.TicketItemTotalPrice;
                            else
                            {
                                ticketItem.TicketItemTotalPrice = existingTicket.TicketItemTotalPrice - ticketItem.TicketItemTotalPrice;

                            }
                        }
                    }
                }
                returnItems.Add(ticketItem);
            }
            return returnItems;
        }

        public List<TicketItem> GetTicketItemsForCreditNote(Guid divisionID, DateTime startDate, DateTime endDate, bool isCashSale)
        {
            return
               _repository.Query(x => (((x.Ticket.IsCashSale == true) | (x.Ticket.IsCashSale == false))) & (x.Ticket.IsHistory == false) & (x.CreatedDate >= startDate & x.CreatedDate <= endDate) & x.Ticket.DivisionID == divisionID & x.IsLatest.Value == true & x.TicketItemTotalPrice < 0 & !x.SalesInvoiceItems.Any())
                   .Include(x => x.Ticket)
                   .Include(x => x.Ticket.CustomerDepartment)
                   .Select()
                   .ToList();
        }
    }


}