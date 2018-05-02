CREATE TABLE [dbo].[tblConsignmentItemReceived]
(
	[ConsignmentItemReceivedID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ConsignmentItemID] UNIQUEIDENTIFIER NOT NULL, 
    [QuantityReceived] INT NOT NULL, 
    [ReceivedDate] DATETIME NOT NULL
	CONSTRAINT [FK_tblConsignmentItemReceived_tblConsignmentItem_ConsignmentItemID] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
)
