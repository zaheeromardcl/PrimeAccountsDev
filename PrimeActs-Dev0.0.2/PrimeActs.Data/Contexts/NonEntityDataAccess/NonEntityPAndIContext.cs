#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Contexts.NonEntityDataAccess
{
    public interface INonEntityDataService
    {
        List<TicketItem> GetTicketItemsForInvoicing(Guid divisionId);
    }

    public class NonEntityPAndIContext : DataContextBase, INonEntityDataService
    {
        static NonEntityPAndIContext()
        {
            Database.SetInitializer<PAndIContext>(null);
        }

        public NonEntityPAndIContext()
            : base("Name=PAndIContext")
        {
        }

        public List<TicketItem> GetTicketItemsForInvoicing(Guid customerDepartmentId)
        {
            var sqlQuery = "SELECT tblTicketItem.TicketItemID, tblTicketItem.TicketID, tblTicketItem.TicketItemDescription, tblTicketItem.TicketItemQuantity, tblTicketItem.TicketItemTotalPrice, tblTicketItem.ConsignmentItemID, tblTicketItem.HaulierSupplierID, tblTicketItem.TransactionTaxCodeID, tblTicketItem.TransactionTaxRatePercentage, tblTicketItem.CurrencyAmount, tblTicketItem.PorterageID, tblTicketItem.UpdatedBy, tblTicketItem.UpdatedDate, tblTicketItem.CreatedBy, tblTicketItem.CreatedDate, tblTicketItem.IsActive, tblTicketItem.DepartmentID, tblTicketItem.PorterageValue, tblTicketItem.OriginalTicketItemID FROM tblTicketItem INNER JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID LEFT OUTER JOIN tblSalesInvoiceItem ON tblTicketItem.TicketItemID = tblSalesInvoiceItem.TicketItemID WHERE (tblSalesInvoiceItem.TicketItemID IS NULL) AND tblTicket.CustomerDepartmentID = @customerDepartmentId";
            var customerDepartmentIdParameter = new SqlParameter("@customerDepartmentId", customerDepartmentId);
            return Database.SqlQuery<TicketItem>(sqlQuery, customerDepartmentIdParameter).ToList();
        }
    }
}