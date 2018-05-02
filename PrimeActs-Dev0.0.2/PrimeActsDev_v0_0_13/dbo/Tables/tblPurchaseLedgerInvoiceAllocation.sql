<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPurchaseLedgerInvoiceAllocation] (
    [PurchaseLedgerInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL,
    [PurchaseLedgerEntryID]             UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceID]                 UNIQUEIDENTIFIER NOT NULL,
    [PurchaseAmount]                    NUMERIC (18, 4)  NOT NULL,
    [FXPurchaseAmount]                  NUMERIC (18, 4)  NULL,
    [CurrencyID]                        UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                      NUMERIC (18, 4)  NULL,
    [NoteID]                            UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                       DATETIME         NULL,
    [CreatedDate]                       DATETIME         NULL,
    [CreatedByUserID]                   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblPurchaseLedgerInvoiceAllocation] PRIMARY KEY CLUSTERED ([PurchaseLedgerInvoiceAllocationID] ASC),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocation_tblPurchaseInvoiceID] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocation_tblPurchaseLedgerEntry] FOREIGN KEY ([PurchaseLedgerEntryID]) REFERENCES [dbo].[tblPurchaseLedgerEntry] ([PurchaseLedgerEntryID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblPurchaseLedgerInvoiceAllocation] (
    [PurchaseLedgerInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL,
    [PurchaseLedgerEntryID]             UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceID]                 UNIQUEIDENTIFIER NOT NULL,
    [PurchaseAmount]                    NUMERIC (18, 4)  NOT NULL,
    [FXPurchaseAmount]                  NUMERIC (18, 4)  NULL,
    [CurrencyID]                        UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                      NUMERIC (18, 4)  NULL,
    [NoteID]                            UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                       DATETIME         NULL,
    [CreatedDate]                       DATETIME         NULL,
    [CreatedByUserID]                   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblPurchaseLedgerInvoiceAllocation] PRIMARY KEY CLUSTERED ([PurchaseLedgerInvoiceAllocationID] ASC),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocation_tblPurchaseInvoiceID] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocation_tblPurchaseLedgerEntry] FOREIGN KEY ([PurchaseLedgerEntryID]) REFERENCES [dbo].[tblPurchaseLedgerEntry] ([PurchaseLedgerEntryID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
