

CREATE VIEW [dbo].[vwConsignmentTicketsSingleByDate]

AS

SELECT tblTicketItem.ConsignmentItemID, tblTicket.TicketDate, IsNull(tblTicket.TicketPrefix, '') +  tblTicket.TicketReference AS ShowTicketReference, tblCustomer.CustomerCode, tblTicketItem.TicketItemQuantity, 
tblTicketItem.TicketItemTotalPrice, ROUND(tblTicketItem.TicketItemTotalPrice / tblTicketItem.TicketItemQuantity, 2) AS UnitPrice
FROM tblTicketItem
INNER JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID
INNER JOIN tblCustomerDepartment ON tblTicket.CustomerDepartmentID = tblCustomerDepartment.CustomerDepartmentID
INNER JOIN tblCustomer ON tblCustomerDepartment.CustomerID = tblCustomer.CustomerID
WHERE tblTicketItem.TicketItemQuantity != 0

