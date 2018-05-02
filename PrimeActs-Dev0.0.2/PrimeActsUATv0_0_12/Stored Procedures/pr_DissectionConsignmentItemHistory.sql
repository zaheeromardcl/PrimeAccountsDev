/*
DECLARE @ConsignmentItemID uniqueidentifier
DECLARE @ReportDate as datetime

SET @ConsignmentItemID = 'F4869D32-42A0-484A-809B-287D71234D85'
SET @ReportDate = '20170121'
*/
CREATE PROCEDURE pr_DissectionConsignmentItemHistory @ConsignmentItemID uniqueidentifier, @ReportDate as datetime

AS

DECLARE @tblPrice AS TABLE(UnitPrice numeric(18,4) PRIMARY KEY)

INSERT INTO @tblPrice
SELECT DISTINCT Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 0) 
FROM tblTicketItem INNER JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID
WHERE tblTicketItem.TicketItemQuantity != 0 AND tblTicketItem.ConsignmentItemID = @ConsignmentItemID AND CAST(CAST(tblTicket.TicketDate AS date) AS datetime) <= @ReportDate

INSERT INTO @tblPrice
SELECT tblConsignmentItemPriceReturn.ReturnUnitPrice
FROM tblConsignmentItemPriceReturn
LEFT JOIN @tblPrice AS alPrice ON tblConsignmentItemPriceReturn.ReturnUnitPrice = alPrice.UnitPrice
WHERE tblConsignmentItemPriceReturn.ConsignmentItemID = @ConsignmentItemID AND CAST(CAST(tblConsignmentItemPriceReturn.ReturnDate AS date) AS datetime) <= @ReportDate
AND alPrice.UnitPrice IS NULL

SELECT alPrice.UnitPrice, 
IsNull(alPriceReturn.ReturnQuantity, 0) AS ReturnQuantity, IsNull(Round(alPriceReturn.ReturnQuantity * alPriceReturn.ReturnUnitPrice, 2), 0) AS RetunrTotal,  
IsNull(alPreviousTicket.PreviousTicketQuantity, 0) AS PreviousTicketQuantity, IsNull(Round(alPreviousTicket.PreviousTicketQuantity * alPreviousTicket.PreviousTicketUnitPrice, 2), 0) AS PreviousTicketTotal,
IsNull(alTodayTicket.TodayTicketQuantity, 0) AS TodayTicketQuantity, IsNull(Round(alTodayTicket.TodayTicketQuantity * alTodayTicket.TodayTicketUnitPrice, 2), 0) AS TodayTicketTotal
FROM @tblPrice AS alPrice
LEFT JOIN (
	SELECT tblConsignmentItemPriceReturn.ReturnUnitPrice, SUM(tblConsignmentItemPriceReturn.ReturnQuantity) AS ReturnQuantity
	FROM tblConsignmentItemPriceReturn
	WHERE tblConsignmentItemPriceReturn.ConsignmentItemID = @ConsignmentItemID AND CAST(CAST(tblConsignmentItemPriceReturn.ReturnDate AS date) AS datetime) <= @ReportDate
	GROUP BY tblConsignmentItemPriceReturn.ReturnUnitPrice
) AS alPriceReturn ON alPrice.UnitPrice = alPriceReturn.ReturnUnitPrice
LEFT JOIN (
	SELECT Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 0) AS PreviousTicketUnitPrice, SUM(tblTicketItem.TicketItemQuantity) AS PreviousTicketQuantity
	FROM tblTicket INNER JOIN tblTicketItem ON tblTicket.TicketID = tblTicketItem.TicketID
	WHERE tblTicketItem.TicketItemQuantity != 0 AND tblTicketItem.ConsignmentItemID = @ConsignmentItemID AND CAST(CAST(tblTicket.TicketDate AS date) AS datetime) <= @ReportDate
	GROUP BY Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 0)
) AS alPreviousTicket ON alPrice.UnitPrice = alPreviousTicket.PreviousTicketUnitPrice
LEFT JOIN (
	SELECT Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 0) AS TodayTicketUnitPrice, SUM(tblTicketItem.TicketItemQuantity) AS TodayTicketQuantity
	FROM tblTicket INNER JOIN tblTicketItem ON tblTicket.TicketID = tblTicketItem.TicketID
	WHERE tblTicketItem.TicketItemQuantity != 0 AND tblTicketItem.ConsignmentItemID = @ConsignmentItemID AND CAST(CAST(tblTicket.TicketDate AS date) AS datetime) = @ReportDate
	GROUP BY Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 0)
) AS alTodayTicket ON alPrice.UnitPrice = alTodayTicket.TodayTicketUnitPrice
ORDER BY alPrice.UnitPrice DESC

