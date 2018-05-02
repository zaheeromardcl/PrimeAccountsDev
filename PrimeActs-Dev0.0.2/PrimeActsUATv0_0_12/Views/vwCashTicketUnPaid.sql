CREATE VIEW [dbo].[vwCashTicketUnPaid]

AS

SELECT tblTicket.TicketID, tblTicket.TicketReference, alTicket.TicketTotal, tblTicket.TicketDate, tblTicket.DivisionID, alTicket.SalesInvoiceID, 
alTicket.TicketTotal - IsNull(alBalanceowed.Balance, 0) AS AmountPaid,
IsNull(alBalanceOwed.Balance, 0) AS BalanceOwed, tblTicket.CustomerDepartmentID,
IsNull(tblUser.Firstname + ' ', '') + IsNull(tblUser.LastName, '') AS SalesPersonName
FROM (
	SELECT tblTicket.TicketID, Min(tblSalesInvoiceItem.SalesInvoiceID) AS SalesInvoiceID,
	SUM(tblTicketItem.TicketItemTotalPrice * ((tlkpTransactionTaxRate.TransactionTaxRatePercentage / 100) + 1) + tblTicketItem.PorterageValue) 
	+ IsNull(tblTicket.PorterageCorrector, 0) AS TicketTotal
	FROM tblTicket INNER JOIN tblTicketItem ON tblTicket.TicketID = tblTicketItem.TicketID
	INNER JOIN tlkpTransactionTaxRate ON tblTicketItem.TransactionTaxRateID = tlkpTransactionTaxRate.TransactionTaxRateID
	INNER JOIN tblSalesInvoiceItem ON tblTicketItem.TicketItemID = tblSalesInvoiceItem.TicketItemID
	WHERE tblTicket.IsCashSale = 1 --AND tblTicket.TicketDate >= @StartDate
	GROUP BY tblTicket.TicketID, tblTicket.PorterageCorrector
) AS alTicket
LEFT JOIN (
	SELECT SalesInvoiceID, Sum(SaleAmount) AS Balance
	FROM tblSalesLedgerInvoiceAllocation
	GROUP BY SalesInvoiceID
) AS alBalanceOwed ON alTicket.SalesInvoiceID = alBalanceOwed.SalesInvoiceID
INNER JOIN tblTicket ON alTicket.TicketID = tblTicket.TicketID
LEFT JOIN tblUser ON tblTicket.SalesPersonUserID = tblUser.UserID
WHERE IsNull(alBalanceowed.Balance, 0) != 0




--SELECT *
--FROM tblSalesLedgerInvoiceAllocation 
--WHERE SalesInvoiceID = '00760000-0000-0006-8453-291719434123' AND SaleAmount != 78.78


