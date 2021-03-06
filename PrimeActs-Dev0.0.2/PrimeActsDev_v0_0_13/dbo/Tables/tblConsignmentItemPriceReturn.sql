<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblConsignmentItemPriceReturn] (
    [ConsignmentItemPriceReturnID] UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentItemID]            UNIQUEIDENTIFIER NOT NULL,
    [ReturnDate]                   DATETIME         NOT NULL,
    [ReturnQuantity]               INT              NOT NULL,
    [ReturnUnitPrice]              NUMERIC (18, 4)  NOT NULL,
    [NoteID]                       UNIQUEIDENTIFIER NULL,
    [CurrencyID]                   UNIQUEIDENTIFIER NULL,
    [CurrencyReturnUnitPrice]      NUMERIC (18, 4)  NULL,
    [IsNetPrice]                   BIT              NULL,
    [ReducePricesByTransportCost]  BIT              NULL,
    [PurchaseInvoiceItemID]        UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                  DATETIME         NULL,
    [UpdatedByUserID]              UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ConsignmentItemPriceReturnID] ASC),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tblPurchaseInvoiceItem] FOREIGN KEY ([PurchaseInvoiceItemID]) REFERENCES [dbo].[tblPurchaseInvoiceItem] ([PurchaseInvoiceItemID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturnCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturnUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblConsignmentItemPriceReturn] (
    [ConsignmentItemPriceReturnID] UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentItemID]            UNIQUEIDENTIFIER NOT NULL,
    [ReturnDate]                   DATETIME         NOT NULL,
    [ReturnQuantity]               INT              NOT NULL,
    [ReturnUnitPrice]              NUMERIC (18, 4)  NOT NULL,
    [NoteID]                       UNIQUEIDENTIFIER NULL,
    [CurrencyID]                   UNIQUEIDENTIFIER NULL,
    [CurrencyReturnUnitPrice]      NUMERIC (18, 4)  NULL,
    [IsNetPrice]                   BIT              NULL,
    [ReducePricesByTransportCost]  BIT              NULL,
    [PurchaseInvoiceItemID]        UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                  DATETIME         NULL,
    [UpdatedByUserID]              UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ConsignmentItemPriceReturnID] ASC),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tblPurchaseInvoiceItem] FOREIGN KEY ([PurchaseInvoiceItemID]) REFERENCES [dbo].[tblPurchaseInvoiceItem] ([PurchaseInvoiceItemID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturn_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturnCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblConsignmentItemPriceReturnUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
