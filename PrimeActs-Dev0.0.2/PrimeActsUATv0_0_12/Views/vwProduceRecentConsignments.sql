CREATE VIEW vwProduceRecentConsignments

AS

SELECT tblProduce.ProduceID, tblProduceGroupDepartment.DepartmentID, 
alConsignment1.ConsignmentItemID AS ConsignmentItemID1, alConsignment1.ConsignmentReference AS ConsignmentReference1, alConsignment1.QuantityExpected AS QuantityExpected1,
alConsignment2.ConsignmentItemID AS ConsignmentItemID2, alConsignment2.ConsignmentReference AS ConsignmentReference2, alConsignment2.QuantityExpected AS QuantityExpected2, 
alConsignment3.ConsignmentItemID AS ConsignmentItemID3, alConsignment3.ConsignmentReference AS ConsignmentReference3, alConsignment2.QuantityExpected AS QuantityExpected3
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
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

--WHERE tblProduceGroupDepartment.DepartmentID = '76000400-0000-0070-9204-000068336078' AND tblProduce.ProduceCode = 'APGS'
