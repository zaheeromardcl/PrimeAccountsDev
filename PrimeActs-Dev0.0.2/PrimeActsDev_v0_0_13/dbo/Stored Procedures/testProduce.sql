<<<<<<< HEAD
﻿CREATE PROCEDURE testProduce

AS

CREATE TABLE #Produce (ProduceID uniqueidentifier PRIMARY KEY, DepartmentID uniqueidentifier)

CREATE TABLE #ProduceQuantityByDepartment (ProduceID uniqueidentifier, DepartmentID uniqueidentifier, QtyExpected int, QtyRecieved int, QtySold int,
PRIMARY KEY (ProduceID, DepartmentID))

CREATE TABLE #vwProduceRecentTickets (ProduceID uniqueidentifier, DepartmentID uniqueidentifier, TicketReference1 nvarchar(50), TicketItemQuantity1 numeric(12,4), 
TicketReference2 nvarchar(50), TicketItemQuantity2 numeric(12,4), TicketReference3 nvarchar(50), TicketItemQuantity3 numeric(12,4), PRIMARY KEY (ProduceID, DepartmentID)) 

CREATE TABLE #vwProduceRecentConsignments (ProduceID uniqueidentifier, DepartmentID uniqueidentifier, ConsignmentReference1 nvarchar(50), ConsignmentItemQuantityExpected1 int, 
ConsignmentReference2 nvarchar(50), ConsignmentItemQuantityExpected2 int, ConsignmentReference3 nvarchar(50), ConsignmentItemQuantityExpected3 int,
PRIMARY KEY (ProduceID, DepartmentID)) 

select getdate()
--Get just the correct produce for department AND ProduceCode
INSERT INTO #Produce
SELECT tblProduce.ProduceID, tblProduceGroupDepartment.DepartmentID
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
WHERE tblProduceGroupDepartment.DepartmentID = '76000400-0000-0070-9204-000068336078' --AND tblProduce.ProduceCode = 'APGS'
select getdate()

INSERT INTO #ProduceQuantityByDepartment
SELECT #Produce.ProduceID, #Produce.DepartmentID,
vwProduceQuantityByDepartment.QtyExpected,
vwProduceQuantityByDepartment.QtyRecieved, vwProduceQuantityByDepartment.QtySold
FROM #Produce 
INNER JOIN vwProduceQuantityByDepartment ON #Produce.ProduceID = vwProduceQuantityByDepartment.ProduceID AND #Produce.DepartmentID = vwProduceQuantityByDepartment.DepartmentID
select getdate()

INSERT INTO #vwProduceRecentTickets
SELECT #Produce.ProduceID, #Produce.DepartmentID, alTicket1.TicketReference, alTicketItem1.TicketItemQuantity, alTicket2.TicketReference, alTicketItem2.TicketItemQuantity, 
alTicket3.TicketReference, alTicketItem3.TicketItemQuantity
FROM #Produce
INNER JOIN vwProduceRecentTickets ON #Produce.ProduceID = vwProduceRecentTickets.ProduceID AND #Produce.DepartmentID = vwProduceRecentTickets.DepartmentID
LEFT JOIN tblTicketItem AS alTicketItem1 ON vwProduceRecentTickets.TicketItemID1 = alTicketItem1.TicketItemID
LEFT JOIN tblTicket AS alTicket1 ON alTicketItem1.TicketID = alTicket1.TicketID
LEFT JOIN tblTicketItem AS alTicketItem2 ON vwProduceRecentTickets.TicketItemID2 = alTicketItem2.TicketItemID
LEFT JOIN tblTicket AS alTicket2 ON alTicketItem2.TicketID = alTicket2.TicketID
LEFT JOIN tblTicketItem AS alTicketItem3 ON vwProduceRecentTickets.TicketItemID3 = alTicketItem3.TicketItemID
LEFT JOIN tblTicket AS alTicket3 ON alTicketItem3.TicketID = alTicket3.TicketID
select getdate()

INSERT INTO #vwProduceRecentConsignments
SELECT #Produce.ProduceID, #Produce.DepartmentID,
alCon1.ConsignmentReference, alConItem1.QuantityExpected, alCon2.ConsignmentReference, alConItem2.QuantityExpected, alCon3.ConsignmentReference, alConItem3.QuantityExpected
FROM #Produce INNER JOIN vwProduceRecentConsignments ON #Produce.ProduceID = vwProduceRecentConsignments.ProduceID AND #Produce.DepartmentID = vwProduceRecentConsignments.DepartmentID
LEFT JOIN tblConsignmentItem AS alConItem1 ON vwProduceRecentConsignments.ConsignmentItemID1 = alConItem1.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon1 ON alConItem1.ConsignmentID = alCon1.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem2 ON vwProduceRecentConsignments.ConsignmentItemID2 = alConItem2.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon2 ON alConItem2.ConsignmentID = alCon2.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem3 ON vwProduceRecentConsignments.ConsignmentItemID3 = alConItem3.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon3 ON alConItem3.ConsignmentID = alCon3.ConsignmentID
select getdate()

SELECT tblProduce.ProduceID, tblProduce.ProduceName, 
#ProduceQuantityByDepartment.DepartmentID, #ProduceQuantityByDepartment.QtyExpected,
#ProduceQuantityByDepartment.QtyRecieved, #ProduceQuantityByDepartment.QtySold,
#vwProduceRecentTickets.TicketReference1, #vwProduceRecentTickets.TicketItemQuantity1, #vwProduceRecentTickets.TicketReference2, #vwProduceRecentTickets.TicketItemQuantity2,
#vwProduceRecentTickets.TicketReference3, #vwProduceRecentTickets.TicketItemQuantity3,
#vwProduceRecentConsignments.ConsignmentReference1, #vwProduceRecentConsignments.ConsignmentItemQuantityExpected1,
#vwProduceRecentConsignments.ConsignmentReference2, #vwProduceRecentConsignments.ConsignmentItemQuantityExpected3,
#vwProduceRecentConsignments.ConsignmentReference3, #vwProduceRecentConsignments.ConsignmentItemQuantityExpected3
FROM tblProduce INNER JOIN #ProduceQuantityByDepartment ON tblProduce.ProduceID = #ProduceQuantityByDepartment.ProduceID
INNER JOIN #vwProduceRecentTickets ON tblProduce.ProduceID = #vwProduceRecentTickets.ProduceID
INNER JOIN #vwProduceRecentConsignments ON tblProduce.ProduceID = #vwProduceRecentConsignments.ProduceID
select getdate()


/*
SELECT tblProduce.ProduceID, tblProduce.ProduceName, 
vwProduceQuantityByDepartment.DepartmentID, vwProduceQuantityByDepartment.QtyExpected,
vwProduceQuantityByDepartment.QtyRecieved, vwProduceQuantityByDepartment.QtySold,
alTicketItem1.TicketItemQuantity AS TicketItemQuantity1,
alTicketItem2.TicketItemQuantity AS TicketItemQuantity2,
alTicketItem3.TicketItemQuantity AS TicketItemQuantity3,
alCon1.ConsignmentReference AS ConsignmentReference1, alConItem1.QuantityExpected AS ConsignmentItemQuantityExpected1,
alCon2.ConsignmentReference AS ConsignmentReference2, alConItem2.QuantityExpected AS ConsignmentItemQuantityExpected2,
alCon3.ConsignmentReference AS ConsignmentReference3, alConItem3.QuantityExpected AS ConsignmentItemQuantityExpected3
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
INNER JOIN vwProduceQuantityByDepartment ON tblProduce.ProduceID = vwProduceQuantityByDepartment.ProduceID AND tblProduceGroupDepartment.DepartmentID = vwProduceQuantityByDepartment.DepartmentID
INNER JOIN vwProduceRecentTickets ON tblProduce.ProduceID = vwProduceRecentTickets.ProduceID AND tblProduceGroupDepartment.DepartmentID = vwProduceRecentTickets.DepartmentID
LEFT JOIN tblTicketItem AS alTicketItem1 ON vwProduceRecentTickets.TicketItemID1 = alTicketItem1.TicketItemID
LEFT JOIN tblTicketItem AS alTicketItem2 ON vwProduceRecentTickets.TicketItemID2 = alTicketItem2.TicketItemID
LEFT JOIN tblTicketItem AS alTicketItem3 ON vwProduceRecentTickets.TicketItemID3 = alTicketItem3.TicketItemID
INNER JOIN vwProduceRecentConsignments 
--WITH (INDEX(PK_tblProduce_ProduceID))
ON tblProduce.ProduceID = vwProduceRecentConsignments.ProduceID AND tblProduceGroupDepartment.DepartmentID = vwProduceRecentConsignments.DepartmentID
LEFT JOIN tblConsignmentItem AS alConItem1 ON vwProduceRecentConsignments.ConsignmentItemID1 = alConItem1.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon1 ON alConItem1.ConsignmentID = alCon1.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem2 ON vwProduceRecentConsignments.ConsignmentItemID2 = alConItem2.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon2 ON alConItem2.ConsignmentID = alCon2.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem3 ON vwProduceRecentConsignments.ConsignmentItemID3 = alConItem3.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon3 ON alConItem3.ConsignmentID = alCon3.ConsignmentID
WHERE tblProduceGroupDepartment.DepartmentID = '76000400-0000-0070-9204-000068336078' AND tblProduce.ProduceCode = 'APGS'
=======
﻿CREATE PROCEDURE testProduce

AS

CREATE TABLE #Produce (ProduceID uniqueidentifier PRIMARY KEY, DepartmentID uniqueidentifier)

CREATE TABLE #ProduceQuantityByDepartment (ProduceID uniqueidentifier, DepartmentID uniqueidentifier, QtyExpected int, QtyRecieved int, QtySold int,
PRIMARY KEY (ProduceID, DepartmentID))

CREATE TABLE #vwProduceRecentTickets (ProduceID uniqueidentifier, DepartmentID uniqueidentifier, TicketReference1 nvarchar(50), TicketItemQuantity1 numeric(12,4), 
TicketReference2 nvarchar(50), TicketItemQuantity2 numeric(12,4), TicketReference3 nvarchar(50), TicketItemQuantity3 numeric(12,4), PRIMARY KEY (ProduceID, DepartmentID)) 

CREATE TABLE #vwProduceRecentConsignments (ProduceID uniqueidentifier, DepartmentID uniqueidentifier, ConsignmentReference1 nvarchar(50), ConsignmentItemQuantityExpected1 int, 
ConsignmentReference2 nvarchar(50), ConsignmentItemQuantityExpected2 int, ConsignmentReference3 nvarchar(50), ConsignmentItemQuantityExpected3 int,
PRIMARY KEY (ProduceID, DepartmentID)) 

select getdate()
--Get just the correct produce for department AND ProduceCode
INSERT INTO #Produce
SELECT tblProduce.ProduceID, tblProduceGroupDepartment.DepartmentID
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
WHERE tblProduceGroupDepartment.DepartmentID = '76000400-0000-0070-9204-000068336078' --AND tblProduce.ProduceCode = 'APGS'
select getdate()

INSERT INTO #ProduceQuantityByDepartment
SELECT #Produce.ProduceID, #Produce.DepartmentID,
vwProduceQuantityByDepartment.QtyExpected,
vwProduceQuantityByDepartment.QtyRecieved, vwProduceQuantityByDepartment.QtySold
FROM #Produce 
INNER JOIN vwProduceQuantityByDepartment ON #Produce.ProduceID = vwProduceQuantityByDepartment.ProduceID AND #Produce.DepartmentID = vwProduceQuantityByDepartment.DepartmentID
select getdate()

INSERT INTO #vwProduceRecentTickets
SELECT #Produce.ProduceID, #Produce.DepartmentID, alTicket1.TicketReference, alTicketItem1.TicketItemQuantity, alTicket2.TicketReference, alTicketItem2.TicketItemQuantity, 
alTicket3.TicketReference, alTicketItem3.TicketItemQuantity
FROM #Produce
INNER JOIN vwProduceRecentTickets ON #Produce.ProduceID = vwProduceRecentTickets.ProduceID AND #Produce.DepartmentID = vwProduceRecentTickets.DepartmentID
LEFT JOIN tblTicketItem AS alTicketItem1 ON vwProduceRecentTickets.TicketItemID1 = alTicketItem1.TicketItemID
LEFT JOIN tblTicket AS alTicket1 ON alTicketItem1.TicketID = alTicket1.TicketID
LEFT JOIN tblTicketItem AS alTicketItem2 ON vwProduceRecentTickets.TicketItemID2 = alTicketItem2.TicketItemID
LEFT JOIN tblTicket AS alTicket2 ON alTicketItem2.TicketID = alTicket2.TicketID
LEFT JOIN tblTicketItem AS alTicketItem3 ON vwProduceRecentTickets.TicketItemID3 = alTicketItem3.TicketItemID
LEFT JOIN tblTicket AS alTicket3 ON alTicketItem3.TicketID = alTicket3.TicketID
select getdate()

INSERT INTO #vwProduceRecentConsignments
SELECT #Produce.ProduceID, #Produce.DepartmentID,
alCon1.ConsignmentReference, alConItem1.QuantityExpected, alCon2.ConsignmentReference, alConItem2.QuantityExpected, alCon3.ConsignmentReference, alConItem3.QuantityExpected
FROM #Produce INNER JOIN vwProduceRecentConsignments ON #Produce.ProduceID = vwProduceRecentConsignments.ProduceID AND #Produce.DepartmentID = vwProduceRecentConsignments.DepartmentID
LEFT JOIN tblConsignmentItem AS alConItem1 ON vwProduceRecentConsignments.ConsignmentItemID1 = alConItem1.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon1 ON alConItem1.ConsignmentID = alCon1.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem2 ON vwProduceRecentConsignments.ConsignmentItemID2 = alConItem2.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon2 ON alConItem2.ConsignmentID = alCon2.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem3 ON vwProduceRecentConsignments.ConsignmentItemID3 = alConItem3.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon3 ON alConItem3.ConsignmentID = alCon3.ConsignmentID
select getdate()

SELECT tblProduce.ProduceID, tblProduce.ProduceName, 
#ProduceQuantityByDepartment.DepartmentID, #ProduceQuantityByDepartment.QtyExpected,
#ProduceQuantityByDepartment.QtyRecieved, #ProduceQuantityByDepartment.QtySold,
#vwProduceRecentTickets.TicketReference1, #vwProduceRecentTickets.TicketItemQuantity1, #vwProduceRecentTickets.TicketReference2, #vwProduceRecentTickets.TicketItemQuantity2,
#vwProduceRecentTickets.TicketReference3, #vwProduceRecentTickets.TicketItemQuantity3,
#vwProduceRecentConsignments.ConsignmentReference1, #vwProduceRecentConsignments.ConsignmentItemQuantityExpected1,
#vwProduceRecentConsignments.ConsignmentReference2, #vwProduceRecentConsignments.ConsignmentItemQuantityExpected3,
#vwProduceRecentConsignments.ConsignmentReference3, #vwProduceRecentConsignments.ConsignmentItemQuantityExpected3
FROM tblProduce INNER JOIN #ProduceQuantityByDepartment ON tblProduce.ProduceID = #ProduceQuantityByDepartment.ProduceID
INNER JOIN #vwProduceRecentTickets ON tblProduce.ProduceID = #vwProduceRecentTickets.ProduceID
INNER JOIN #vwProduceRecentConsignments ON tblProduce.ProduceID = #vwProduceRecentConsignments.ProduceID
select getdate()


/*
SELECT tblProduce.ProduceID, tblProduce.ProduceName, 
vwProduceQuantityByDepartment.DepartmentID, vwProduceQuantityByDepartment.QtyExpected,
vwProduceQuantityByDepartment.QtyRecieved, vwProduceQuantityByDepartment.QtySold,
alTicketItem1.TicketItemQuantity AS TicketItemQuantity1,
alTicketItem2.TicketItemQuantity AS TicketItemQuantity2,
alTicketItem3.TicketItemQuantity AS TicketItemQuantity3,
alCon1.ConsignmentReference AS ConsignmentReference1, alConItem1.QuantityExpected AS ConsignmentItemQuantityExpected1,
alCon2.ConsignmentReference AS ConsignmentReference2, alConItem2.QuantityExpected AS ConsignmentItemQuantityExpected2,
alCon3.ConsignmentReference AS ConsignmentReference3, alConItem3.QuantityExpected AS ConsignmentItemQuantityExpected3
FROM tblProduce 
INNER JOIN tblProduceGroupDepartment ON tblProduce.ProduceGroupID = tblProduceGroupDepartment.ProduceGroupID
INNER JOIN vwProduceQuantityByDepartment ON tblProduce.ProduceID = vwProduceQuantityByDepartment.ProduceID AND tblProduceGroupDepartment.DepartmentID = vwProduceQuantityByDepartment.DepartmentID
INNER JOIN vwProduceRecentTickets ON tblProduce.ProduceID = vwProduceRecentTickets.ProduceID AND tblProduceGroupDepartment.DepartmentID = vwProduceRecentTickets.DepartmentID
LEFT JOIN tblTicketItem AS alTicketItem1 ON vwProduceRecentTickets.TicketItemID1 = alTicketItem1.TicketItemID
LEFT JOIN tblTicketItem AS alTicketItem2 ON vwProduceRecentTickets.TicketItemID2 = alTicketItem2.TicketItemID
LEFT JOIN tblTicketItem AS alTicketItem3 ON vwProduceRecentTickets.TicketItemID3 = alTicketItem3.TicketItemID
INNER JOIN vwProduceRecentConsignments 
--WITH (INDEX(PK_tblProduce_ProduceID))
ON tblProduce.ProduceID = vwProduceRecentConsignments.ProduceID AND tblProduceGroupDepartment.DepartmentID = vwProduceRecentConsignments.DepartmentID
LEFT JOIN tblConsignmentItem AS alConItem1 ON vwProduceRecentConsignments.ConsignmentItemID1 = alConItem1.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon1 ON alConItem1.ConsignmentID = alCon1.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem2 ON vwProduceRecentConsignments.ConsignmentItemID2 = alConItem2.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon2 ON alConItem2.ConsignmentID = alCon2.ConsignmentID
LEFT JOIN tblConsignmentItem AS alConItem3 ON vwProduceRecentConsignments.ConsignmentItemID3 = alConItem3.ConsignmentItemID
LEFT JOIN tblConsignment AS alCon3 ON alConItem3.ConsignmentID = alCon3.ConsignmentID
WHERE tblProduceGroupDepartment.DepartmentID = '76000400-0000-0070-9204-000068336078' AND tblProduce.ProduceCode = 'APGS'
>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
*/