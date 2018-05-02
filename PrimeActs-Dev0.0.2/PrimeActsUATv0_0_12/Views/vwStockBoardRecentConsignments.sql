
CREATE VIEW [dbo].[vwStockBoardRecentConsignments]

AS

	SELECT *
	FROM (
		SELECT ROW_NUMBER() OVER (PARTITION BY tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID ORDER BY tblConsignmentItem.ConsignmentItemID DESC) AS RowNo, 
		tblConsignmentItem.ProduceID, tblConsignmentItem.DepartmentID, tblStockBoardProduceGroupDepartment.StockboardID,
		tblConsignmentItem.ConsignmentItemID, tblConsignment.ConsignmentReference, tblConsignmentItem.QuantityExpected
		FROM tblStockBoardProduceGroupDepartment
		INNER JOIN tblProduce ON tblStockBoardProduceGroupDepartment.ProduceGroupID = tblProduce.ProduceGroupID
		INNER JOIN tblConsignmentItem ON tblProduce.ProduceID = tblConsignmentItem.ProduceID AND tblStockBoardProduceGroupDepartment.DepartmentID = tblConsignmentItem.DepartmentID
		INNER JOIN tblConsignment ON tblConsignmentItem.ConsignmentID = tblConsignment.ConsignmentID
		WHERE tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0
	) AS al
	WHERE RowNo <= 3

