<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblConsignmentItemArrival] (
    [ConsignmentItemArrivalID]   UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                     UNIQUEIDENTIFIER NULL,
    [ConsignmentItemID]          UNIQUEIDENTIFIER NULL,
    [ConsignmentItemArrivalDate] DATETIME         NULL,
    [QuantityReceived]           INT              NOT NULL,
    [StockLocationID]            UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedDate]                DATETIME         NULL,
    [CreatedByUserID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblConsignmentItemArrival_1] PRIMARY KEY CLUSTERED ([ConsignmentItemArrivalID] ASC),
    CONSTRAINT [FK_tblConsignmentItemArrival_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblConsignmentItemArrival_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignmentItemArrivalCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignmentItemArrivalUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblConsignmentItemArrival] (
    [ConsignmentItemArrivalID]   UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                     UNIQUEIDENTIFIER NULL,
    [ConsignmentItemID]          UNIQUEIDENTIFIER NULL,
    [ConsignmentItemArrivalDate] DATETIME         NULL,
    [QuantityReceived]           INT              NOT NULL,
    [StockLocationID]            UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedDate]                DATETIME         NULL,
    [CreatedByUserID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblConsignmentItemArrival_1] PRIMARY KEY CLUSTERED ([ConsignmentItemArrivalID] ASC),
    CONSTRAINT [FK_tblConsignmentItemArrival_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblConsignmentItemArrival_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignmentItemArrivalCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignmentItemArrivalUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
