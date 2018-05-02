<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblPurchaseLedgerEntry] (
    [PurchaseLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeID]              UNIQUEIDENTIFIER NOT NULL,
    [PurchaseLedgerEntryDescription] NVARCHAR (100)   NOT NULL,
    [PurchaseAmount]                 NUMERIC (18, 4)  NOT NULL,
    [CurrencyAmount]                 NUMERIC (18, 4)  NOT NULL,
    [CurrencyID]                     UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                   NUMERIC (16, 4)  NULL,
    [BatchNumberLogID]               UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]           UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                    DATETIME         NULL,
    [CreatedDate]                    DATETIME         NULL,
    [AccountingYear]                 INT              NULL,
    [CreatedByUserID]                UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                UNIQUEIDENTIFIER NULL,
    [TransactionTaxAmount]           NUMERIC (18, 4)  NULL,
    [IsAllocated]                    BIT              NOT NULL,
    CONSTRAINT [PK_tblPurchaseLedgerEntry_1] PRIMARY KEY CLUSTERED ([PurchaseLedgerEntryID] ASC),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tlkpLedgerEntryType] FOREIGN KEY ([LedgerEntryTypeID]) REFERENCES [dbo].[tlkpLedgerEntryType] ([LedgerEntryTypeID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblPurchaseLedgerEntry] (
    [PurchaseLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeID]              UNIQUEIDENTIFIER NOT NULL,
    [PurchaseLedgerEntryDescription] NVARCHAR (100)   NOT NULL,
    [PurchaseAmount]                 NUMERIC (18, 4)  NOT NULL,
    [CurrencyAmount]                 NUMERIC (18, 4)  NOT NULL,
    [CurrencyID]                     UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                   NUMERIC (16, 4)  NULL,
    [BatchNumberLogID]               UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]           UNIQUEIDENTIFIER NOT NULL,
    [NoteID]                         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                    DATETIME         NULL,
    [CreatedDate]                    DATETIME         NULL,
    [AccountingYear]                 INT              NULL,
    [CreatedByUserID]                UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                UNIQUEIDENTIFIER NULL,
    [TransactionTaxAmount]           NUMERIC (18, 4)  NULL,
    [IsAllocated]                    BIT              NOT NULL,
    CONSTRAINT [PK_tblPurchaseLedgerEntry_1] PRIMARY KEY CLUSTERED ([PurchaseLedgerEntryID] ASC),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntry_tlkpLedgerEntryType] FOREIGN KEY ([LedgerEntryTypeID]) REFERENCES [dbo].[tlkpLedgerEntryType] ([LedgerEntryTypeID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
