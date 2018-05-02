CREATE VIEW vwConsignmentTicketsAllDays

AS

SELECT Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 2) AS UnitPrice,
SUM(tblTicketItem.TicketItemTotalPrice) AS TotalPrice, SUM(tblTicketItem.TicketItemQuantity) AS TotalQuantity
FROM tblTicketItem
INNER JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID
WHERE tblTicketItem.TicketItemQuantity != 0
GROUP BY tblTicketItem.ConsignmentItemID, Round(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 2)