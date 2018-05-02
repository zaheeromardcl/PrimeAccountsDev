CREATE PROCEDURE vwStock

AS

SELECT tblProduce.ProduceID, tblProduce.ProduceName, tblProduce.ProduceGroupID, tblProduceGroupDepartment.DepartmentID,
IsNull(alStockIn.QtyIn, 0) AS QtyRecieved, 
IsNull(alStockOut.QtyOut, 0) AS QtySold, IsNull(alStockExpected.QtyExpected, 0) AS QtyExpected,
alConsignment1.ConsignmentItemID AS ConsignmentItemID1, alConsignment1.ConsignmentReference AS ConsignmentReference1, alConsignment1.QuantityExpected AS QuantityExpected1,
alConsignment2.ConsignmentItemID AS ConsignmentItemID2, alConsignment2.ConsignmentReference AS ConsignmentReference2, alConsignment2.QuantityExpected AS QuantityExpected2, 
alConsignment3.ConsignmentItemID AS ConsignmentItemID3, alConsignment3.ConsignmentReference AS ConsignmentReference3, alConsignment2.QuantityExpected AS QuantityExpected3,
alTicketItem1.TicketItemID AS TicketItemID1, alTicketItem1.TicketItemQuantity AS TicketItemQuantity1, alTicketItem1.TicketReference AS TicketReference1,
alTicketItem2.TicketItemID AS TicketItemID2, alTicketItem2.TicketItemQuantity AS TicketItemQuantity2, alTicketItem2.TicketReference AS TicketReference2,
alTicketItem3.TicketItemID AS TicketItemID3, alTicketItem3.TicketItemQuantity AS TicketItemQuantity3, alTicket3.TicketReference AS TicketReference3
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
INNER JOIN tblStockBoardProduceGroup ON tblProduceGroupDepartment.ProduceGroupDepartmentID = tblStockBoardProduceGroup.ProduceGroupDepartmentID
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
LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alCI.ConsignmentItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alCI.ConsignmentItemID, alC.ConsignmentReference, alCI.QuantityExpected
	FROM tblConsignmentItem AS alCI
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alConsignment1 ON tblProduce.ProduceID = alConsignment1.ProduceID AND tblProduceGroupDepartment.DepartmentID = alConsignment1.DepartmentID AND alConsignment1.RowNo = 1
LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alCI.ConsignmentItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alCI.ConsignmentItemID, alC.ConsignmentReference, alCI.QuantityExpected
	FROM tblConsignmentItem AS alCI
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alConsignment2 ON tblProduce.ProduceID = alConsignment2.ProduceID AND tblProduceGroupDepartment.DepartmentID = alConsignment2.DepartmentID AND alConsignment2.RowNo = 2
LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alCI.ConsignmentItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alCI.ConsignmentItemID, alC.ConsignmentReference, alCI.QuantityExpected
	FROM tblConsignmentItem AS alCI
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alConsignment3 ON tblProduce.ProduceID = alConsignment3.ProduceID AND tblProduceGroupDepartment.DepartmentID = alConsignment3.DepartmentID AND alConsignment3.RowNo = 3

LEFT JOIN (
	SELECT tblTicketItem.TicketItemID, tblTicket.TicketReference, tblTicketItem.TicketItemQuantity, alFindTicket.ProduceID, alFindTicket.DepartmentID
	FROM (
		SELECT alCI.ProduceID, alCI.DepartmentID, Max(alTI.TicketItemID) AS TicketItemID
		FROM dbo.tblTicketItem AS alTI 
		INNER JOIN dbo.tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
		INNER JOIN dbo.tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
		WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
		GROUP BY alCI.ProduceID, alCI.DepartmentID
	) AS alFindTicket
	INNER JOIN dbo.tblTicketItem ON alFindTicket.TicketItemID = tblTicketItem.TicketItemID
	INNER JOIN dbo.tblTicket ON tblTicketItem.TicketID = tblTicket.TicketID	
) AS alTicketItem1 ON tblProduce.ProduceID = alTicketItem1.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem1.DepartmentID
LEFT JOIN (
	SELECT tblTicketItem.TicketItemID, tblTicket.TicketReference, tblTicketItem.TicketItemQuantity, alFindTicket.ProduceID, alFindTicket.DepartmentID
	FROM (
		SELECT alCI.ProduceID, alCI.DepartmentID, Max(alTI.TicketItemID) AS TicketItemID
		FROM dbo.tblTicketItem AS alTI 
		INNER JOIN dbo.tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
		INNER JOIN dbo.tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
		LEFT JOIN (
			SELECT alCI.ProduceID, alCI.DepartmentID, Max(alTI.TicketItemID) AS TicketItemID
			FROM dbo.tblTicketItem AS alTI 
			INNER JOIN dbo.tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
			INNER JOIN dbo.tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
			WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
			GROUP BY alCI.ProduceID, alCI.DepartmentID
		) AS al ON alTI.TicketItemID = al.TicketItemID
		WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0 AND al.TicketItemID Is Null
		GROUP BY alCI.ProduceID, alCI.DepartmentID
	) AS alFindTicket
	INNER JOIN dbo.tblTicketItem ON alFindTicket.TicketItemID = dbo.tblTicketItem.TicketItemID
	INNER JOIN dbo.tblTicket ON dbo.tblTicketItem.TicketID = dbo.tblTicket.TicketID	
) AS alTicketItem2 ON tblProduce.ProduceID = alTicketItem2.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem2.DepartmentID
LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM dbo.tblTicketItem AS alTI 
	INNER JOIN dbo.tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN dbo.tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem3 ON tblProduce.ProduceID = alTicketItem3.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem3.DepartmentID AND alTicketItem3.RowNo = 3
LEFT JOIN dbo.tblTicket AS alTicket3 ON alTicketItem3.TicketID = alTicket3.TicketID
ORDER BY tblProduce.ProduceName, tblProduceGroupDepartment.DepartmentID