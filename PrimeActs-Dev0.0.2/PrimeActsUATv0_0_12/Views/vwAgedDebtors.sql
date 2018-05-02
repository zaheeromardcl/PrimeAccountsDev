CREATE VIEW vwAgedDebtors

AS

SELECT tblCustomer.CustomerCompanyName, tblCustomerDepartment.CustomerDepartmentName, alHistory.*
FROM tblCustomer INNER JOIN tblCustomerDepartment ON tblCustomer.CustomerID = tblCustomerDepartment.CustomerID
INNER JOIN (
	SELECT tblSalesInvoice.CustomerDepartmentID, 
	SUM (
		CASE WHEN tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -1, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week0,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -2, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -1, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week1,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -3, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -2, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week2,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -4, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -3, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week3,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -5, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -4, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week4,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -6, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -5, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week5,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -7, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -6, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week6,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -8, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -7, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance 
		ELSE 
			0 
		END
	) AS Week7,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -9, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -8, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week8,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -10, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -9, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance 
		ELSE 
			0 
		END
	) AS Week9,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -11, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -10, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week10,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -12, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -11, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week11,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate >= DATEADD(wk, -13, CAST(CAST(getdate() AS int) AS datetime)) 
			AND
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -12, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Week12,
	SUM (
		CASE WHEN 
			tblSalesInvoice.SalesInvoiceDate < DATEADD(wk, -1, CAST(CAST(getdate() AS int) AS datetime)) 
		THEN 
			alInvoiceTotal.InvoiceBalance
		ELSE 
			0 
		END
	) AS Older
	FROM tblSalesInvoice 
	INNER JOIN (
		SELECT tblSalesLedgerInvoiceAllocation.SalesInvoiceID, SUM(tblSalesLedgerInvoiceAllocation.SaleAmount) AS InvoiceBalance
		FROM tblSalesLedgerInvoiceAllocation
		GROUP BY tblSalesLedgerInvoiceAllocation.SalesInvoiceID
	) AS alInvoiceTotal ON tblSalesInvoice.SalesInvoiceID = alInvoiceTotal.SalesInvoiceID
	GROUP BY tblSalesInvoice.CustomerDepartmentID
) AS alHistory ON tblCustomerDepartment.CustomerDepartmentID = alHistory.CustomerDepartmentID
WHERE alHistory.Older != 0 OR alHistory.Week0 != 0 OR alHistory.Week1 != 0 OR alHistory.Week2 != 0
AND alHistory.Week3 != 0 OR alHistory.Week4 != 0 OR alHistory.Week5 != 0 OR alHistory.Week6 != 0
AND alHistory.Week7 != 0 OR alHistory.Week8 != 0 OR alHistory.Week8 != 0 OR alHistory.Week9 != 0
AND alHistory.Week10 != 0 OR alHistory.Week11 != 0 OR alHistory.Week12 != 0


