CREATE VIEW vwProduceQuantityByDepartment

AS

SELECT tblProduce.ProduceID, tblProduceGroupDepartment.DepartmentID, IsNull(alStockIn.QtyIn, 0) AS QtyRecieved, 
IsNull(alStockOut.QtyOut, 0) AS QtySold, IsNull(alStockExpected.QtyExpected, 0) AS QtyExpected
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
LEFT JOIN (
	SELECT tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, SUM(IsNull(tblConsignmentItemArrival.QuantityReceived, 0)) AS QtyIn
	FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
	INNER JOIN tblConsignmentItemArrival ON tblConsignmentItem.ConsignmentItemID = tblConsignmentItemArrival.ConsignmentItemID
	WHERE tblConsignment.IsHistory = 0
	GROUP BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID
) AS alStockIn ON tblProduce.ProduceID = alStockIn.ProduceID AND tblProduceGroupDepartment.DepartmentID = alStockIn.DepartmentID
LEFT JOIN (
	SELECT tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, SUM(IsNull(tblTicketItem.TicketItemQuantity, 0)) AS QtyOut
	FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
	INNER JOIN tblTicketItem ON tblConsignmentItem.ConsignmentItemID = tblTicketItem.ConsignmentItemID
	WHERE tblConsignment.IsHistory = 0
	GROUP BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID
) AS alStockout ON tblProduce.ProduceID = alStockOut.ProduceID AND tblProduceGroupDepartment.DepartmentID = alStockOut.DepartmentID
LEFT JOIN (
	SELECT tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, SUM(IsNull(tblConsignmentItem.QuantityExpected, 0)) AS QtyExpected
	FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
	WHERE tblConsignment.IsHistory = 0
	GROUP BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID
) AS alStockExpected ON tblProduce.ProduceID = alStockExpected.ProduceID AND tblProduceGroupDepartment.DepartmentID = alStockExpected.DepartmentID

