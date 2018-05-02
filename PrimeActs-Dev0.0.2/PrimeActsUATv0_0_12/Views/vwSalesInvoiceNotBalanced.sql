CREATE VIEW vwSalesInvoiceNotBalanced

AS

SELECT tblSalesInvoice.SalesInvoiceID, tblSalesInvoice.CustomerDepartmentID, tblSalesInvoice.SalesInvoiceReference, tblSalesInvoice.SalesInvoiceDate, 
tblSalesInvoice.DivisionID, alBalance.InvoiceBalance
FROM tblSalesInvoice INNER JOIN (
	SELECT tblSalesLedgerInvoiceAllocation.SalesInvoiceID, SUM(tblSalesLedgerInvoiceAllocation.SaleAmount) AS InvoiceBalance
	FROM tblSalesLedgerInvoiceAllocation
	GROUP BY tblSalesLedgerInvoiceAllocation.SalesInvoiceID
	HAVING SUM(tblSalesLedgerInvoiceAllocation.SaleAmount) != 0
) AS alBalance ON tblSalesInvoice.SalesInvoiceID = alBalance.SalesInvoiceID
