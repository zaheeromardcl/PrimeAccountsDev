CREATE VIEW vwSalesInvoiceTotal

AS

SELECT top 100 tblSalesInvoiceItem.SalesInvoiceID, SUM(Round((100 + tlkpTransactionTaxRate.TransactionTaxRatePercentage) * tblSalesInvoiceItem.SalesInvoiceItemLineTotal /100, 2)) AS InvoiceTotal
FROM tblSalesInvoiceItem
INNER JOIN tlkpTransactionTaxRate ON tblSalesInvoiceItem.TransactionTaxRateID = tlkpTransactionTaxRate.TransactionTaxRateID
GROUP BY tblSalesInvoiceItem.SalesInvoiceID
