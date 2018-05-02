CREATE VIEW vwConsignmentReturns

AS

SELECT ConsignmentItemID, ReturnUnitPrice, SUM(ReturnQuantity) AS TotalQuantity
FROM tblConsignmentItemPriceReturn
GROUP BY ConsignmentItemID, ReturnUnitPrice