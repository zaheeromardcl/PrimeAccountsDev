<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblTicketItem] (
    [TicketItemID]          UNIQUEIDENTIFIER NOT NULL,
    [TicketID]              UNIQUEIDENTIFIER NOT NULL,
    [TicketItemDescription] NVARCHAR (400)   NOT NULL,
    [TicketItemQuantity]    NUMERIC (12, 4)  NOT NULL,
    [TicketItemTotalPrice]  NUMERIC (18, 4)  NOT NULL,
    [ConsignmentItemID]     UNIQUEIDENTIFIER NULL,
    [HaulierSupplierID]     UNIQUEIDENTIFIER NULL,
    [CurrencyAmount]        NUMERIC (18, 4)  NULL,
    [PorterageID]           UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedDate]           DATETIME         NULL,
    [DepartmentID]          UNIQUEIDENTIFIER NULL,
    [PorterageValue]        NUMERIC (18, 4)  CONSTRAINT [DF_tblTicketItem_PorterageValue] DEFAULT ((0.00)) NOT NULL,
    [OriginalTicketItemID]  UNIQUEIDENTIFIER NULL,
    [IsLatest]              BIT              NULL,
    [TransferTypeID]        UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [TransactionTaxRateID]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblTicketItem_1] PRIMARY KEY CLUSTERED ([TicketItemID] ASC),
    CONSTRAINT [FK_tblTicket_tlkpTransactionTaxRateID] FOREIGN KEY ([TransactionTaxRateID]) REFERENCES [dbo].[tlkpTransactionTaxRate] ([TransactionTaxRateID]),
    CONSTRAINT [FK_tblTicketItem_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblTicketItem_tblSupplier] FOREIGN KEY ([HaulierSupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblTicketItem_tblTicket] FOREIGN KEY ([TicketID]) REFERENCES [dbo].[tblTicket] ([TicketID]),
    CONSTRAINT [FK_tblTicketItem_tlkpPorterage] FOREIGN KEY ([PorterageID]) REFERENCES [dbo].[tlkpPorterage] ([PorterageID]),
    CONSTRAINT [FK_tblTicketItem_tlkpTransferType] FOREIGN KEY ([TransferTypeID]) REFERENCES [dbo].[tlkpTransferType] ([TransferTypeID]),
    CONSTRAINT [FK_tblTicketItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblTicketItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicketItem]
    ON [dbo].[tblTicketItem]([OriginalTicketItemID] ASC, [TicketItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicketItem_ConsignmentItemID]
    ON [dbo].[tblTicketItem]([ConsignmentItemID] ASC);

=======
﻿CREATE TABLE [dbo].[tblTicketItem] (
    [TicketItemID]          UNIQUEIDENTIFIER NOT NULL,
    [TicketID]              UNIQUEIDENTIFIER NOT NULL,
    [TicketItemDescription] NVARCHAR (400)   NOT NULL,
    [TicketItemQuantity]    NUMERIC (12, 4)  NOT NULL,
    [TicketItemTotalPrice]  NUMERIC (18, 4)  NOT NULL,
    [ConsignmentItemID]     UNIQUEIDENTIFIER NULL,
    [HaulierSupplierID]     UNIQUEIDENTIFIER NULL,
    [CurrencyAmount]        NUMERIC (18, 4)  NULL,
    [PorterageID]           UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedDate]           DATETIME         NULL,
    [DepartmentID]          UNIQUEIDENTIFIER NULL,
    [PorterageValue]        NUMERIC (18, 4)  CONSTRAINT [DF_tblTicketItem_PorterageValue] DEFAULT ((0.00)) NOT NULL,
    [OriginalTicketItemID]  UNIQUEIDENTIFIER NULL,
    [IsLatest]              BIT              NULL,
    [TransferTypeID]        UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [TransactionTaxRateID]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblTicketItem_1] PRIMARY KEY CLUSTERED ([TicketItemID] ASC),
    CONSTRAINT [FK_tblTicket_tlkpTransactionTaxRateID] FOREIGN KEY ([TransactionTaxRateID]) REFERENCES [dbo].[tlkpTransactionTaxRate] ([TransactionTaxRateID]),
    CONSTRAINT [FK_tblTicketItem_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblTicketItem_tblSupplier] FOREIGN KEY ([HaulierSupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblTicketItem_tblTicket] FOREIGN KEY ([TicketID]) REFERENCES [dbo].[tblTicket] ([TicketID]),
    CONSTRAINT [FK_tblTicketItem_tlkpPorterage] FOREIGN KEY ([PorterageID]) REFERENCES [dbo].[tlkpPorterage] ([PorterageID]),
    CONSTRAINT [FK_tblTicketItem_tlkpTransferType] FOREIGN KEY ([TransferTypeID]) REFERENCES [dbo].[tlkpTransferType] ([TransferTypeID]),
    CONSTRAINT [FK_tblTicketItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblTicketItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicketItem]
    ON [dbo].[tblTicketItem]([OriginalTicketItemID] ASC, [TicketItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblTicketItem_ConsignmentItemID]
    ON [dbo].[tblTicketItem]([ConsignmentItemID] ASC);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
