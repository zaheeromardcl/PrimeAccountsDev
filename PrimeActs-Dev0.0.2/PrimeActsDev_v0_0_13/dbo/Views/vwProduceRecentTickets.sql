<<<<<<< HEAD
﻿CREATE VIEW [dbo].[vwProduceRecentTickets]

AS

SELECT 
tblProduce.ProduceID, tblProduceGroupDepartment.DepartmentID, 
alTicketItem1.TicketItemID AS TicketItemID1, alTicketItem1.TicketItemQuantity AS TicketItemQuantity1, alTicket1.TicketReference AS TicketReference1,--alTicket1.TicketReference AS TicketReference1,
alTicketItem2.TicketItemID AS TicketItemID2, alTicketItem2.TicketItemQuantity AS TicketItemQuantity2, alTicket2.TicketReference AS TicketReference2,--alTicket2.TicketReference AS TicketReference2, 
alTicketItem3.TicketItemID AS TicketItemID3, alTicketItem3.TicketItemQuantity AS TicketItemQuantity3, alTicket3.TicketReference AS TicketReference3--alTicket3.TicketReference AS TicketReference3
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID

LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM tblTicketItem AS alTI 
	--INNER JOIN tblTicket AS alT ON alTI.TicketID = alT.TicketID
	INNER JOIN tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem1 ON tblProduce.ProduceID = alTicketItem1.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem1.DepartmentID AND alTicketItem1.RowNo = 1
LEFT JOIN tblTicket AS alTicket1 ON alTicketItem1.TicketID = alTicket1.TicketID
LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM tblTicketItem AS alTI 
	--INNER JOIN tblTicket AS alT ON alTI.TicketID = alT.TicketID
	INNER JOIN tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem2 ON tblProduce.ProduceID = alTicketItem2.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem2.DepartmentID AND alTicketItem2.RowNo = 2
LEFT JOIN tblTicket AS alTicket2 ON alTicketItem2.TicketID = alTicket2.TicketID

LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM tblTicketItem AS alTI 
	--INNER JOIN tblTicket AS alT ON alTI.TicketID = alT.TicketID
	INNER JOIN tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem3 ON tblProduce.ProduceID = alTicketItem3.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem3.DepartmentID AND alTicketItem3.RowNo = 3
LEFT JOIN tblTicket AS alTicket3 ON alTicketItem3.TicketID = alTicket3.TicketID




=======
﻿CREATE VIEW [dbo].[vwProduceRecentTickets]

AS

SELECT 
tblProduce.ProduceID, tblProduceGroupDepartment.DepartmentID, 
alTicketItem1.TicketItemID AS TicketItemID1, alTicketItem1.TicketItemQuantity AS TicketItemQuantity1, alTicket1.TicketReference AS TicketReference1,--alTicket1.TicketReference AS TicketReference1,
alTicketItem2.TicketItemID AS TicketItemID2, alTicketItem2.TicketItemQuantity AS TicketItemQuantity2, alTicket2.TicketReference AS TicketReference2,--alTicket2.TicketReference AS TicketReference2, 
alTicketItem3.TicketItemID AS TicketItemID3, alTicketItem3.TicketItemQuantity AS TicketItemQuantity3, alTicket3.TicketReference AS TicketReference3--alTicket3.TicketReference AS TicketReference3
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID

LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM tblTicketItem AS alTI 
	--INNER JOIN tblTicket AS alT ON alTI.TicketID = alT.TicketID
	INNER JOIN tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem1 ON tblProduce.ProduceID = alTicketItem1.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem1.DepartmentID AND alTicketItem1.RowNo = 1
LEFT JOIN tblTicket AS alTicket1 ON alTicketItem1.TicketID = alTicket1.TicketID
LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM tblTicketItem AS alTI 
	--INNER JOIN tblTicket AS alT ON alTI.TicketID = alT.TicketID
	INNER JOIN tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem2 ON tblProduce.ProduceID = alTicketItem2.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem2.DepartmentID AND alTicketItem2.RowNo = 2
LEFT JOIN tblTicket AS alTicket2 ON alTicketItem2.TicketID = alTicket2.TicketID

LEFT JOIN (
	SELECT ROW_NUMBER() OVER (PARTITION BY alCI.ProduceID, alCI.DepartmentID ORDER BY alTI.TicketItemID DESC) AS RowNo, 
	alCI.ProduceID, alCI.DepartmentID,
	alTI.TicketItemID, alTI.TicketItemQuantity, alTI.TicketID--, alT.TicketReference
	FROM tblTicketItem AS alTI 
	--INNER JOIN tblTicket AS alT ON alTI.TicketID = alT.TicketID
	INNER JOIN tblConsignmentItem AS alCI ON alTI.ConsignmentItemID = alCI.ConsignmentItemID
	INNER JOIN tblConsignment AS alC ON alCI.ConsignmentID = alC.ConsignmentID
	WHERE alC.IsHistory = 0 AND alC.IsDeleted = 0
) AS alTicketItem3 ON tblProduce.ProduceID = alTicketItem3.ProduceID AND tblProduceGroupDepartment.DepartmentID = alTicketItem3.DepartmentID AND alTicketItem3.RowNo = 3
LEFT JOIN tblTicket AS alTicket3 ON alTicketItem3.TicketID = alTicket3.TicketID




>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
