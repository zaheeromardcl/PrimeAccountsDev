#region

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using PrimeActs.Data.Contexts;
using PrimeActs.Domain;
using System;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IProduceForTicketService
    {
        List<ProduceQuantityForTicket> GetProduceQuantityForTicket(string produceCode, string consignmentReference, Guid? userDivisionID, int numberOfDays = 7);
      //AK: commented out as not used at all : 11/01/2017:  List<ProduceQuantityForTicket> GetProduceQuantityForTicket(Guid userDivisionID, int numberOfDays = 7);
        List<ProduceQuantityForTicket> PopulateStockLevels(Guid departmentID, int numberOfDays = 2);
    }

    public class ProduceForTicketService : IProduceForTicketService
    {
        private readonly string sqlQuery = string.Empty;
        private readonly string sqlStockBoard = string.Empty;
        private readonly string sqlQueryPurchaseInvoice = string.Empty;
        private readonly string produceQuantityForDivisionQuery = string.Empty;

        public ProduceForTicketService()
        {
            sqlQuery = "SELECT tblSupplier.SupplierCode, tblsupplierdepartment.supplierdepartmentname, tblProduceGroup.ProduceGroupID, tblProduceGroup.ProduceGroupName, tblProduce.ProduceCode, tblProduce.ProduceName, tblConsignmentItem.ConsignmentItemID, tblConsignment.ConsignmentReference, ISNULL(B.TicketItemQuantity, 0) AS TicketItemQuantity, alConsignmentItemArrival.QuantityReceived -  ISNULL(B.TicketItemQuantity, 0) AS RemainingQuantity, tblConsignmentItem.PorterageID, tlkpPorterage.UnitPrice AS PorterageUnitAmount, tlkpPorterage.MinimumAmount AS PorterageMinimumAmount, tblConsignmentItem.Brand AS ItemBrand, tblconsignmentitem.PackType as ItemPackType, tblConsignmentItem.PackSize AS ItemPackSize, B.AvgUnitPrice, tblDepartment.DepartmentCode as consDepartmentCode, alConsignmentItemArrival.QuantityReceived as itemReceived, tblConsignment.DespatchDate as Despatchdate FROM tblConsignmentItem INNER JOIN tblProduce ON tblConsignmentItem.ProduceID = tblProduce.ProduceID INNER JOIN tblProduceGroup ON tblProduce.ProduceGroupID = tblProduceGroup.ProduceGroupID  INNER JOIN tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID  INNER JOIN (       SELECT ConsignmentItemID, MAX(ConsignmentItemArrivalDate) AS MaxConsignmentItemArrivalDate, Sum(QuantityReceived) AS QuantityReceived       FROM tblConsignmentItemArrival        GROUP BY ConsignmentItemID) AS alConsignmentItemArrival ON alConsignmentItemArrival.ConsignmentItemID = tblConsignmentItem.ConsignmentItemID inner join tblsupplierdepartment on tblsupplierdepartment.SupplierDepartmentID=tblConsignment.SupplierDepartmentID inner join tblSupplier on tblsupplier.supplierID = tblsupplierdepartment.supplierID inner JOIN tlkpPorterage ON tblConsignmentItem.PorterageID = tlkpPorterage.PorterageID INNER JOIN tlkpPackWtUnit ON tlkpPackWtUnit.PackWtUnitID = tblConsignmentItem.PackWtUnitID INNER JOIN tblDepartment ON tblConsignmentItem.DepartmentID= tblDepartment.DepartmentID LEFT OUTER JOIN (       SELECT ConsignmentItemID, SUM(tblTicketItem.TicketItemQuantity) AS TicketItemQuantity,        CASE WHEN SUM(tblTicketItem.TicketItemQuantity) = 0 THEN null ELSE Round(SUM(tblTicketItem.TicketItemTotalPrice)/ SUM(tblTicketItem.TicketItemQuantity), 2) END as AvgUnitPrice        FROM tblTicketItem        GROUP BY ConsignmentItemID) AS B ON tblConsignmentItem.ConsignmentItemID = B.ConsignmentItemID WHERE (tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0) AND ((@produceCode is not null and tblProduce.ProduceCode LIKE @produceCode) OR (@consignmentReference is not null and tblConsignment.ConsignmentReference like @consignmentReference))  ORDER BY alConsignmentItemArrival.MaxConsignmentItemArrivalDate ASC";
           
            
           
            //TODO: amend this query with new query so that number of days doesn't count.  Today : 11/01/2017 
            sqlQueryPurchaseInvoice = "SELECT tblSupplier.SupplierCode, tblsupplierdepartment.supplierdepartmentname, tblProduceGroup.ProduceGroupID, tblProduceGroup.ProduceGroupName, tblProduce.ProduceCode, tblProduce.ProduceName, tblConsignmentItem.ConsignmentItemID, tblConsignment.ConsignmentReference, ISNULL(B.TicketItemQuantity, 0) AS TicketItemQuantity, tblConsignmentItemArrival.QuantityReceived -  ISNULL(B.TicketItemQuantity, 0) AS RemainingQuantity, tblConsignmentItem.PorterageID, tlkpPorterage.UnitPrice AS PorterageUnitAmount, tlkpPorterage.MinimumAmount AS PorterageMinimumAmount, tblConsignmentItem.Brand AS ItemBrand, tblconsignmentitem.PackType as ItemPackType, tblConsignmentItem.PackSize AS ItemPackSize, B.AvgUnitPrice, tblDepartment.DepartmentCode as consDepartmentCode, tblConsignmentItemArrival.QuantityReceived as itemReceived, tblConsignment.DespatchDate as Despatchdate, CAST(CASE WHEN (tblPurchaseInvoiceItem.ConsignmentItemID is null) THEN 0 ELSE 1 END as BIT) Invoiced FROM tblConsignmentItem INNER JOIN tblProduce ON tblConsignmentItem.ProduceID = tblProduce.ProduceID INNER JOIN tblProduceGroup ON tblProduce.ProduceGroupID = tblProduceGroup.ProduceGroupID  INNER JOIN tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID  inner join tblConsignmentItemArrival on tblConsignmentItem.ConsignmentItemID=tblConsignmentItemArrival.ConsignmentItemID inner join tblsupplierdepartment on tblsupplierdepartment.SupplierDepartmentID=tblConsignment.SupplierDepartmentID inner join tblSupplier on tblsupplier.supplierID = tblsupplierdepartment.supplierID inner JOIN tlkpPorterage ON tblConsignmentItem.PorterageID = tlkpPorterage.PorterageID INNER JOIN tlkpPackWtUnit ON tlkpPackWtUnit.PackWtUnitID = tblConsignmentItem.PackWtUnitID INNER JOIN tblDepartment ON tblConsignmentItem.DepartmentID= tblDepartment.DepartmentID Left Join tblPurchaseInvoiceItem ON tblConsignmentItem.ConsignmentItemID = tblPurchaseInvoiceItem.ConsignmentItemID  LEFT OUTER JOIN (SELECT ConsignmentItemID, SUM(tblTicketItem.TicketItemQuantity) AS TicketItemQuantity, CASE WHEN SUM(tblTicketItem.TicketItemQuantity) = 0 THEN null ELSE Round(SUM(tblTicketItem.TicketItemTotalPrice)/ SUM(tblTicketItem.TicketItemQuantity), 2) END as AvgUnitPrice FROM tblTicketItem GROUP BY ConsignmentItemID) AS B ON tblConsignmentItem.ConsignmentItemID = B.ConsignmentItemID INNER JOIN tlkpPurchaseType ON tlkpPurchaseType.PurchaseTypeID = tblConsignment.PurchaseTypeID WHERE tlkpPurchaseType.PurchaseTypeCode in ('CP','OP') AND tblConsignment.IsHistory = 0 AND (tblConsignment.IsHistory = 0) AND ((@produceCode is not null and tblProduce.ProduceCode LIKE @produceCode) OR (@consignmentReference is not null and tblConsignment.ConsignmentReference like @consignmentReference)) AND (tblConsignmentItemArrival.ConsignmentItemArrivalDate>= GetDate()-@NumberOfDays) aND (tblConsignmentItemArrival.ConsignmentItemArrivalDate <= Getdate()) ORDER BY tblConsignmentItemArrival.ConsignmentItemArrivalDate ASC";
            

            
            //AK: commented out as not called from anywhere - leave in until confirmed 11/01/2017
            //produceQuantityForDivisionQuery = "SELECT tblProduceGroup.ProduceGroupID, tblProduceGroup.ProduceGroupName, tblProduce.ProduceCode, tblProduce.ProduceName,  tblConsignmentItem.ConsignmentItemID, tblConsignment.ConsignmentReference, ISNULL(B.TicketItemQuantity, 0) AS TicketItemQuantity, tblConsignmentItemArrival.Quantity - ISNULL(B.TicketItemQuantity, 0) AS RemainingQuantity, tblConsignmentItem.PorterageID, tlkpPorterage.UnitPrice AS PorterageUnitAmount, tlkpPorterage.MinimumAmount AS PorterageMinimumAmount, tblProduceGroup.DivisionID AS ConsignmentItemDivisionID, tblConsignmentItem.Brand AS ItemBrand, tblconsignmentitem.PackType as ItemPackType, tblConsignmentItem.PackSize AS ItemPackSize FROM tblConsignmentItem INNER JOIN tblProduce ON tblConsignmentItem.ProduceID = tblProduce.ProduceID INNER JOIN tblProduceGroup ON tblProduce.ProduceGroupID = tblProduceGroup.ProduceGroupID INNER JOIN tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID inner join tblConsignmentItemArrival on tblConsignmentItemArrival.ConsignmentItemID=tblConsignmentItem.ConsignmentItemID INNER JOIN tlkpPorterage ON tblConsignmentItem.PorterageID = tlkpPorterage.PorterageID INNER JOIN tlkpPackWtUnit ON tlkpPackWtUnit.PackWtUnitID = tblConsignmentItem.PackWtUnitID LEFT OUTER JOIN (SELECT     tblConsignmentItem_1.ConsignmentItemID, SUM(tblTicketItem.TicketItemQuantity) AS TicketItemQuantity FROM          tblConsignmentItem AS tblConsignmentItem_1 INNER JOIN tblConsignment AS tblConsignment_1 ON tblConsignmentItem_1.ConsignmentID = tblConsignment_1.ConsignmentID INNER JOIN tblTicketItem ON tblConsignmentItem_1.ConsignmentItemID = tblTicketItem.ConsignmentItemID WHERE      (tblConsignment_1.IsHistory = 0) GROUP BY tblConsignmentItem_1.ConsignmentItemID) AS B ON tblConsignmentItem.ConsignmentItemID = B.ConsignmentItemID WHERE tblProduceGroup.DivisionId=@DivisionID AND (tblConsignment.ReceivedDate >= GetDate()-@NumberOfDays) AND (tblConsignment.ReceivedDate <= Getdate()) ORDER BY tblConsignment.ReceivedDate ASC";


            sqlStockBoard = "SELECT tblSupplier.SupplierCode, tblsupplierdepartment.supplierdepartmentname, tblProduceGroup.ProduceGroupID, tblProduceGroup.ProduceGroupName, tblProduce.ProduceCode, tblProduce.ProduceName, tblConsignmentItem.ConsignmentItemID, tblConsignment.ConsignmentReference, ISNULL(B.TicketItemQuantity, 0) AS TicketItemQuantity, tblConsignmentItemArrival.QuantityReceived -  ISNULL(B.TicketItemQuantity, 0) AS RemainingQuantity, tblConsignmentItem.PorterageID, tlkpPorterage.UnitPrice AS PorterageUnitAmount, tlkpPorterage.MinimumAmount AS PorterageMinimumAmount, tblConsignmentItem.Brand AS ItemBrand, tblconsignmentitem.PackType as ItemPackType, tblConsignmentItem.PackSize AS ItemPackSize,  B.AvgUnitPrice,  tblDepartment.DepartmentCode as consDepartmentCode,  tblConsignmentItemArrival.QuantityReceived as itemReceived,  tblConsignment.DespatchDate as Despatchdate  FROM tblConsignmentItem  INNER JOIN tblProduce ON tblConsignmentItem.ProduceID = tblProduce.ProduceID  INNER JOIN tblProduceGroup ON tblProduce.ProduceGroupID = tblProduceGroup.ProduceGroupID   inner join tblProduceGroupDepartment on tblProduceGroupDepartment.ProduceGroupID = tblProduceGroup.ProduceGroupID inner join tblStockBoardProduceGroup on tblStockBoardProduceGroup.ProduceGroupDepartmentID = tblProduceGroupDepartment.ProduceGroupDepartmentID inner join tblStockBoard on tblStockBoardProduceGroup.StockboardID = tblStockBoard.StockboardID INNER JOIN tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID   inner join tblConsignmentItemArrival on tblConsignmentItem.ConsignmentItemID=tblConsignmentItemArrival.ConsignmentItemID  inner join tblsupplierdepartment on tblsupplierdepartment.SupplierDepartmentID=tblConsignment.SupplierDepartmentID  inner join tblSupplier on tblsupplier.supplierID = tblsupplierdepartment.supplierID  inner JOIN tlkpPorterage ON tblConsignmentItem.PorterageID = tlkpPorterage.PorterageID  INNER JOIN tlkpPackWtUnit ON tlkpPackWtUnit.PackWtUnitID = tblConsignmentItem.PackWtUnitID  INNER JOIN tblDepartment ON tblConsignmentItem.DepartmentID= tblDepartment.DepartmentID  LEFT OUTER JOIN (SELECT ConsignmentItemID,  SUM(tblTicketItem.TicketItemQuantity) AS TicketItemQuantity,  CASE WHEN SUM(tblTicketItem.TicketItemQuantity) = 0 THEN null ELSE Round(SUM(tblTicketItem.TicketItemTotalPrice)/ SUM(tblTicketItem.TicketItemQuantity), 2) END as AvgUnitPrice  FROM tblTicketItem GROUP BY ConsignmentItemID) AS B  ON tblConsignmentItem.ConsignmentItemID = B.ConsignmentItemID  WHERE (tblConsignment.IsHistory = 0)  and (tblStockBoard.StockboardID=@StockBoardID) AND (tblproducegroupdepartment.DepartmentID=@DepartmentID) AND (tblConsignmentItemArrival.ConsignmentItemArrivalDate>= GetDate()-@NumberOfDays) aND (tblConsignmentItemArrival.ConsignmentItemArrivalDate <= Getdate())  ORDER BY tblConsignmentItemArrival.ConsignmentItemArrivalDate ASC";

        }

        public List<ProduceQuantityForTicket> GetProduceQuantityForTicket(string produceCode, string consignmentReference, Guid? userDivisionID, int numberOfDays = 7)
        {
            using (var context = new PAndIContext())
            {
                var produceCodeParameter = new SqlParameter("@produceCode", string.Format("{0}{1}", produceCode, "%"));
                var consignmentReferenceParameter = new SqlParameter("@consignmentReference", string.Format("{0}{1}", consignmentReference, consignmentReference.EndsWith("%") ? "" : "%"));
                var userdivisionidParameter = new SqlParameter("@DivisionID", userDivisionID);
                var numerOfDaysParameter = new SqlParameter("@NumberOfDays", numberOfDays);
                var recordsReturned = context.Database.SqlQuery<ProduceQuantityForTicket>(sqlQuery, produceCodeParameter, consignmentReferenceParameter, userdivisionidParameter, numerOfDaysParameter).ToList();
                return recordsReturned;
            }
        }

        public List<ProduceQuantityForTicket> GetProduceQuantityForPurchaseInvoice(string produceCode, string consignmentReference, Guid? userDivisionID, int numberOfDays = 7)
        {
            using (var context = new PAndIContext())
            {
                var produceCodeParameter = new SqlParameter("@produceCode", string.Format("{0}{1}", produceCode, "%"));
                var consignmentReferenceParameter = new SqlParameter("@consignmentReference", string.Format("{0}{1}", consignmentReference, consignmentReference.EndsWith("%") ? "" : "%"));
                var userdivisionidParameter = new SqlParameter("@DivisionID", userDivisionID);
                var numerOfDaysParameter = new SqlParameter("@NumberOfDays", numberOfDays);
                var recordsReturned = context.Database.SqlQuery<ProduceQuantityForTicket>(sqlQueryPurchaseInvoice, produceCodeParameter, consignmentReferenceParameter, userdivisionidParameter, numerOfDaysParameter).ToList();
                return recordsReturned;
            }
        }

        //AK: commented out - as not used at all - 11/01/2017
        //public List<ProduceQuantityForTicket> GetProduceQuantityForTicket(Guid userDivisionID, int numberOfDays = 7)
        //{
        //    using (var context = new PAndIContext())
        //    {
        //        var userdivisionidParameter = new SqlParameter("@DivisionID", userDivisionID);
        //        var numerOfDaysParameter = new SqlParameter("@NumberOfDays", numberOfDays);

        //        return context.Database.SqlQuery<ProduceQuantityForTicket>(produceQuantityForDivisionQuery, userdivisionidParameter, numerOfDaysParameter).ToList();
        //    }
        //}

        public List<ProduceQuantityForTicket> PopulateStockLevels(Guid departmentID, int numberOfDays = 2)
        {
            using (var context = new PAndIContext())
            {

                var userdepartmentID = new SqlParameter("@DepartmentID", departmentID);
                var numerOfDaysParameter = new SqlParameter("@NumberOfDays", numberOfDays);
                var recordsReturned = context.Database.SqlQuery<ProduceQuantityForTicket>(sqlStockBoard, userdepartmentID, numerOfDaysParameter).ToList();
                return recordsReturned;
            }
        }
    }
}