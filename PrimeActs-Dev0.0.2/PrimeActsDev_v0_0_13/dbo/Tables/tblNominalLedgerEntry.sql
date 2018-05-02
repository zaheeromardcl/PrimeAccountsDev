<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblNominalLedgerEntry] (
    [NominalLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [BatchNumberLogID]              UNIQUEIDENTIFIER NOT NULL,
    [NominalAccountID]              UNIQUEIDENTIFIER NOT NULL,
    [NominalLedgerEntryReference]   NVARCHAR (50)    NOT NULL,
    [NominalLedgerEntryAmount]      NUMERIC (18, 4)  NOT NULL,
    [NominalLedgerEntryDate]        DATETIME         NOT NULL,
    [NominalLedgerEntryDescription] NVARCHAR (400)   NULL,
    [LedgerEntryTypeID]             UNIQUEIDENTIFIER NOT NULL,
    [CurrencyAmount]                NUMERIC (18, 4)  NULL,
    [CurrencyID]                    UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                  NUMERIC (18, 4)  NULL,
    [UpdatedDate]                   DATETIME         NULL,
    [CreatedDate]                   DATETIME         NULL,
    [IsReconciled]                  BIT              CONSTRAINT [DF__tblNomina__IsAct__6D9742D9] DEFAULT ((1)) NOT NULL,
    [AccountingYear]                INT              NULL,
    [CreatedByUserID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]               UNIQUEIDENTIFIER NULL,
    [BankTransactionTypeID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblNominalLedgerEntry] PRIMARY KEY CLUSTERED ([NominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblNominalLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tblNominalAccount] FOREIGN KEY ([NominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tlkpBankTransactionType] FOREIGN KEY ([BankTransactionTypeID]) REFERENCES [dbo].[tlkpBankTransactionType] ([BankTransactionTypeID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblNominalLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblNominalLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblNominalLedgerEntry] (
    [NominalLedgerEntryID]          UNIQUEIDENTIFIER NOT NULL,
    [BatchNumberLogID]              UNIQUEIDENTIFIER NOT NULL,
    [NominalAccountID]              UNIQUEIDENTIFIER NOT NULL,
    [NominalLedgerEntryReference]   NVARCHAR (50)    NOT NULL,
    [NominalLedgerEntryAmount]      NUMERIC (18, 4)  NOT NULL,
    [NominalLedgerEntryDate]        DATETIME         NOT NULL,
    [NominalLedgerEntryDescription] NVARCHAR (400)   NULL,
    [LedgerEntryTypeID]             UNIQUEIDENTIFIER NOT NULL,
    [CurrencyAmount]                NUMERIC (18, 4)  NULL,
    [CurrencyID]                    UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                  NUMERIC (18, 4)  NULL,
    [UpdatedDate]                   DATETIME         NULL,
    [CreatedDate]                   DATETIME         NULL,
    [IsReconciled]                  BIT              CONSTRAINT [DF__tblNomina__IsAct__6D9742D9] DEFAULT ((1)) NOT NULL,
    [AccountingYear]                INT              NULL,
    [CreatedByUserID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]               UNIQUEIDENTIFIER NULL,
    [BankTransactionTypeID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblNominalLedgerEntry] PRIMARY KEY CLUSTERED ([NominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblNominalLedgerEntry_tblBatchNumber] FOREIGN KEY ([BatchNumberLogID]) REFERENCES [dbo].[tblBatchNumberLog] ([BatchNumberLogID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tblNominalAccount] FOREIGN KEY ([NominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tlkpBankTransactionType] FOREIGN KEY ([BankTransactionTypeID]) REFERENCES [dbo].[tlkpBankTransactionType] ([BankTransactionTypeID]),
    CONSTRAINT [FK_tblNominalLedgerEntry_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID]),
    CONSTRAINT [FK_tblNominalLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblNominalLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
