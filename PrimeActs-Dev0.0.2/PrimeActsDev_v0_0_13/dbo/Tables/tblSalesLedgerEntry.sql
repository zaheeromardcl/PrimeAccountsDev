<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSalesLedgerEntry] (
    [SalesLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeID]           UNIQUEIDENTIFIER NOT NULL,
    [SalesLedgerEntryDescription] NVARCHAR (100)   NOT NULL,
    [SaleAmount]                  NUMERIC (18, 4)  NOT NULL,
    [CurrencyAmount]              NUMERIC (18, 4)  NULL,
    [CurrencyID]                  UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                NUMERIC (16, 4)  NULL,
    [CustomerDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [BatchNumberLogID]            UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxAmount]        NUMERIC (18, 4)  NULL,
    [NoteID]                      UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedDate]                 DATETIME         NULL,
    [AccountingYear]              INT              NULL,
    [SalesPersonUserID]           UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSalesLedgerEntry_1] PRIMARY KEY CLUSTERED ([SalesLedgerEntryID] ASC),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblSalesLedgerEntryType] FOREIGN KEY ([LedgerEntryTypeID]) REFERENCES [dbo].[tlkpLedgerEntryType] ([LedgerEntryTypeID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblSalesLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSalesLedgerEntry] (
    [SalesLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [LedgerEntryTypeID]           UNIQUEIDENTIFIER NOT NULL,
    [SalesLedgerEntryDescription] NVARCHAR (100)   NOT NULL,
    [SaleAmount]                  NUMERIC (18, 4)  NOT NULL,
    [CurrencyAmount]              NUMERIC (18, 4)  NULL,
    [CurrencyID]                  UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                NUMERIC (16, 4)  NULL,
    [CustomerDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [BatchNumberLogID]            UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxAmount]        NUMERIC (18, 4)  NULL,
    [NoteID]                      UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedDate]                 DATETIME         NULL,
    [AccountingYear]              INT              NULL,
    [SalesPersonUserID]           UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSalesLedgerEntry_1] PRIMARY KEY CLUSTERED ([SalesLedgerEntryID] ASC),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tblSalesLedgerEntryType] FOREIGN KEY ([LedgerEntryTypeID]) REFERENCES [dbo].[tlkpLedgerEntryType] ([LedgerEntryTypeID]),
    CONSTRAINT [FK_tblSalesLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblSalesLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
