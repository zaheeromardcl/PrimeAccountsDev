CREATE TABLE [dbo].[tblConsignmentItemArrival] (
    [ConsignmentItemArrivalID] UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                   UNIQUEIDENTIFIER NULL,
    [ConsignmentItemID]        UNIQUEIDENTIFIER NULL,
    [ConsignmentArrivalDate]   DATETIME         NOT NULL,
	[Quantity]    INT  not null,
	[IsExpected]	BIT NOT NULL,
	[StockLocationID]          UNIQUEIDENTIFIER NULL,
    [UpdatedBy]                NVARCHAR (25)    NULL,
    [UpdatedDate]              DATETIME         NULL,
    [CreatedBy]                NVARCHAR (25)    NULL,
    [CreatedDate]              DATETIME         NULL,
   
    CONSTRAINT [PK_tblConsignmentItemArrival_1] PRIMARY KEY CLUSTERED ([ConsignmentItemArrivalID] ASC),
    CONSTRAINT [FK_tblConsignmentItemArrival_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblConsignmentItemArrival_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID])
);

