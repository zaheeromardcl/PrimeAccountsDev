
CREATE VIEW [dbo].[vwStockBoardMain]

AS
	SELECT tblStockBoard.StockBoardID,
	tblProduce.ProduceID, tblProduce.ProduceName, tblProduce.ProduceGroupID, tblStockBoardProduceGroupDepartment.DepartmentID,
	IsNull(alStockIn.QtyIn, 0) AS QuantityReceived, IsNull(alStockOut.QtyOut, 0) AS QuantitySold, IsNull(alStockExpected.QtyExpected, 0) AS QuantityExpected,
	IsNull(alStockIn.QtyIn, 0) - IsNull(alStockOut.QtyOut, 0) AS QuantityStock, IsNull(alStockExpected.QtyExpected, 0) - IsNull(alStockOut.QtyOut, 0) AS QuantityAvailable
	FROM tblStockBoard
	INNER JOIN tblStockBoardProduceGroupDepartment ON tblStockBoard.StockboardID = tblStockBoardProduceGroupDepartment.StockboardID
	INNER JOIN tblProduce ON tblStockBoardProduceGroupDepartment.ProduceGroupID = tblProduce.ProduceGroupID
	LEFT JOIN (
		SELECT tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, SUM(IsNull(tblConsignmentItemArrival.QuantityReceived, 0)) AS QtyIn
		FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
		INNER JOIN tblConsignmentItemArrival ON tblConsignmentItem.ConsignmentItemID = tblConsignmentItemArrival.ConsignmentItemID
		WHERE tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0
		GROUP BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID 
	) AS alStockIn ON tblProduce.ProduceID = alStockIn.ProduceID AND tblStockBoardProduceGroupDepartment.DepartmentID = alStockIn.DepartmentID
	INNER JOIN (
		SELECT tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, 
		SUM(IsNull(CASE WHEN tblTicket.IsDeleted = 0 THEN tblTicketItem.TicketItemQuantity ELSE 0 END, 0)) AS QtyOut
		FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
		LEFT JOIN tblTicketItem ON tblConsignmentItem.ConsignmentItemID = tblTicketItem.ConsignmentItemID
		LEFT JOIN tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID
		WHERE tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0
		GROUP BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID
	) AS alStockout ON tblProduce.ProduceID = alStockOut.ProduceID AND tblStockBoardProduceGroupDepartment.DepartmentID = alStockOut.DepartmentID
	INNER JOIN (
		SELECT tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, SUM(IsNull(tblConsignmentItem.QuantityExpected, 0)) AS QtyExpected
		FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
		WHERE tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0
		GROUP BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID
	) AS alStockExpected ON tblProduce.ProduceID = alStockExpected.ProduceID AND tblStockBoardProduceGroupDepartment.DepartmentID = alStockExpected.DepartmentID

