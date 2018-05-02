
CREATE VIEW [dbo].[vwStockBoardRecentTickets]

AS

	SELECT *
	FROM (
		SELECT tblStockBoardProduceGroupDepartment.StockboardID, tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, tblTicket.TicketID, tblTicket.TicketReference, 
		SUM(CASE WHEN tblTicket.IsDeleted = 0 THEN tblTicketItem.TicketItemQuantity ELSE 0 END) AS TicketQuantity,
		ROW_NUMBER() OVER (PARTITION BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID ORDER BY tblTicket.TicketID DESC) AS RowNo
		FROM tblStockBoardProduceGroupDepartment
		INNER JOIN tblProduce ON tblStockBoardProduceGroupDepartment.ProduceGroupID = tblProduce.ProduceGroupID
		INNER JOIN tblConsignmentItem ON tblProduce.ProduceID = tblConsignmentItem.ProduceID AND tblStockBoardProduceGroupDepartment.DepartmentID = tblConsignmentItem.DepartmentID
		INNER JOIN tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID
		INNER JOIN tblTicketItem ON tblConsignmentItem.ConsignmentItemID = tblTicketItem.ConsignmentItemID
		INNER JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID
		WHERE tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0
		GROUP BY tblStockBoardProduceGroupDepartment.StockboardID, tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, tblTicket.TicketID, tblTicket.TicketReference
	) AS al
	WHERE RowNo <= 3

