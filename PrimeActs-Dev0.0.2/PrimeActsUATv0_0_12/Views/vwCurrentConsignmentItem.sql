CREATE VIEW vwCurrentConsignmentItem

AS

SELECT tblConsignmentItem.*
FROM tblConsignment INNER JOIN tblConsignmentItem ON tblConsignment.ConsignmentID = tblConsignmentItem.ConsignmentID
WHERE tblConsignment.IsHistory = 0 AND tblConsignment.IsDeleted = 0
