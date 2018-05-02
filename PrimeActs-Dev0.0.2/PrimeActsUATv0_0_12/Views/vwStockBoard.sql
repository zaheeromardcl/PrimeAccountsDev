


CREATE VIEW [dbo].[vwStockBoard]

AS

SELECT NewID() AS vwStockBoardID, vwStockBoardMain.StockboardID, vwStockBoardMain.DepartmentID, tblDepartment.DepartmentName, vwStockBoardMain.ProduceGroupID, 
tblProduceGroup.ProduceGroupName, vwStockBoardMain.ProduceID, vwStockBoardMain.ProduceName, 
vwStockBoardMain.QuantityAvailable, vwStockBoardMain.QuantityExpected, vwStockBoardMain.QuantityReceived, vwStockBoardMain.QuantitySold, vwStockBoardMain.QuantityStock, 
alCon1.ConsignmentItemID AS ConsignmentItemID1, alCon1.ConsignmentReference AS ConsignmentReference1, alCon1.QuantityExpected AS QuantityExpected1,
alCon2.ConsignmentItemID AS ConsignmentItemID2, alCon2.ConsignmentReference AS ConsignmentReference2, alCon2.QuantityExpected AS QuantityExpected2,
alCon3.ConsignmentItemID AS ConsignmentItemID3, alCon3.ConsignmentReference AS ConsignmentReference3, alCon3.QuantityExpected AS QuantityExpected3,
alTicket1.TicketID AS TicketID1, alTicket1.TicketQuantity AS TicketQuantity1, alTicket1.TicketReference AS TicketReference1,
alTicket2.TicketID AS TicketID2, alTicket2.TicketQuantity AS TicketQuantity2, alTicket2.TicketReference AS TicketReference2,
alTicket3.TicketID AS TicketID3, alTicket3.TicketQuantity AS TicketQuantity3, alTicket3.TicketReference AS TicketReference3
FROM vwStockBoardMain
INNER JOIN tblDepartment ON vwStockBoardMain.DepartmentID = tblDepartment.DepartmentID
INNER JOIN tblProduceGroup ON vwStockBoardMain.ProduceGroupID = tblProduceGroup.ProduceGroupID
LEFT JOIN vwStockBoardRecentConsignments as alCon1 ON vwStockBoardMain.DepartmentID = alCon1.DepartmentID AND vwStockBoardMain.ProduceID = alCon1.ProduceID AND vwStockBoardMain.StockBoardID = alCon1.StockboardID
AND alCon1.RowNo = 1
LEFT JOIN vwStockBoardRecentConsignments as alCon2 ON vwStockBoardMain.DepartmentID = alCon2.DepartmentID AND vwStockBoardMain.ProduceID = alCon2.ProduceID AND vwStockBoardMain.StockBoardID = alCon2.StockboardID
AND alCon2.RowNo = 2
LEFT JOIN vwStockBoardRecentConsignments as alCon3 ON vwStockBoardMain.DepartmentID = alCon3.DepartmentID AND vwStockBoardMain.ProduceID = alCon3.ProduceID AND vwStockBoardMain.StockBoardID = alCon3.StockboardID
AND alCon3.RowNo = 3
LEFT JOIN vwStockBoardRecentTickets AS alTicket1 ON vwStockBoardMain.DepartmentID = alTicket1.DepartmentID AND vwStockBoardMain.ProduceID = alTicket1.ProduceID AND vwStockBoardMain.StockBoardID = alTicket1.StockboardID
AND alTicket1.RowNo = 1
LEFT JOIN vwStockBoardRecentTickets AS alTicket2 ON vwStockBoardMain.DepartmentID = alTicket2.DepartmentID AND vwStockBoardMain.ProduceID = alTicket2.ProduceID AND vwStockBoardMain.StockBoardID = alTicket2.StockboardID
AND alTicket2.RowNo = 2
LEFT JOIN vwStockBoardRecentTickets AS alTicket3 ON vwStockBoardMain.DepartmentID = alTicket3.DepartmentID AND vwStockBoardMain.ProduceID = alTicket3.ProduceID AND vwStockBoardMain.StockBoardID = alTicket3.StockboardID
AND alTicket3.RowNo = 3



