<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSalesInvoiceItem] (
    [SalesInvoiceItemID]          UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceItemDescription] NVARCHAR (200)   NOT NULL,
    [SalesInvoiceItemLineTotal]   NUMERIC (18, 4)  NOT NULL,
    [TicketItemID]                UNIQUEIDENTIFIER NOT NULL,
    [CurrencyAmount]              NUMERIC (16, 4)  NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedDate]                 DATETIME         NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [TransactionTaxRateID]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblSalesInvoiceItem] PRIMARY KEY CLUSTERED ([SalesInvoiceItemID] ASC),
    CONSTRAINT [FK_tblSalesInvoiceItem_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]),
    CONSTRAINT [FK_tblSalesInvoiceItem_tblTicketItem] FOREIGN KEY ([TicketItemID]) REFERENCES [dbo].[tblTicketItem] ([TicketItemID]),
    CONSTRAINT [FK_tblSalesInvoiceItem_tlkpTransactionTaxRate] FOREIGN KEY ([TransactionTaxRateID]) REFERENCES [dbo].[tlkpTransactionTaxRate] ([TransactionTaxRateID]),
    CONSTRAINT [FK_tblSalesInvoiceItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesInvoiceItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSalesInvoiceItem] (
    [SalesInvoiceItemID]          UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceItemDescription] NVARCHAR (200)   NOT NULL,
    [SalesInvoiceItemLineTotal]   NUMERIC (18, 4)  NOT NULL,
    [TicketItemID]                UNIQUEIDENTIFIER NOT NULL,
    [CurrencyAmount]              NUMERIC (16, 4)  NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedDate]                 DATETIME         NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [TransactionTaxRateID]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblSalesInvoiceItem] PRIMARY KEY CLUSTERED ([SalesInvoiceItemID] ASC),
    CONSTRAINT [FK_tblSalesInvoiceItem_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]),
    CONSTRAINT [FK_tblSalesInvoiceItem_tblTicketItem] FOREIGN KEY ([TicketItemID]) REFERENCES [dbo].[tblTicketItem] ([TicketItemID]),
    CONSTRAINT [FK_tblSalesInvoiceItem_tlkpTransactionTaxRate] FOREIGN KEY ([TransactionTaxRateID]) REFERENCES [dbo].[tlkpTransactionTaxRate] ([TransactionTaxRateID]),
    CONSTRAINT [FK_tblSalesInvoiceItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesInvoiceItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
