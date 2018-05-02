CREATE VIEW vwConsignmentReceived

AS

SELECT ConsignmentItemID, SUM(QuantityReceived) AS TotalReceived
FROM tblConsignmentItemArrival
GROUP BY ConsignmentItemID