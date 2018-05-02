<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblTempBankNominalLedgerEntry] (
    [TempBankNominalLedgerEntryID] UNIQUEIDENTIFIER NOT NULL,
    [BankStatementID]              UNIQUEIDENTIFIER NOT NULL,
    [BankReconciliationDate]       DATETIME         NOT NULL,
    [NominalLedgerEntryID]         UNIQUEIDENTIFIER NOT NULL,
    [TransactionAmount]            DECIMAL (18, 4)  NOT NULL,
    [TempDescription]              NVARCHAR (MAX)   NULL,
    [TransactionType]              NVARCHAR (10)    NOT NULL,
    [IsReconciled]                 BIT              CONSTRAINT [DF_tblBankReconciliationMatch_IsReconciled] DEFAULT ((0)) NULL,
    [Text1]                        NVARCHAR (50)    NULL,
    [Text2]                        NVARCHAR (50)    NULL,
    [Text3]                        NVARCHAR (50)    NULL,
    [Text4]                        NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([TempBankNominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblTempBankNominalLedgerEntry_tblBankStatement] FOREIGN KEY ([BankStatementID]) REFERENCES [dbo].[tblBankStatement] ([BankStatementID]),
    CONSTRAINT [FK_tblTempBankNominalLedgerEntry_tblNominalLedgerEntry] FOREIGN KEY ([NominalLedgerEntryID]) REFERENCES [dbo].[tblNominalLedgerEntry] ([NominalLedgerEntryID])
);

=======
﻿CREATE TABLE [dbo].[tblTempBankNominalLedgerEntry] (
    [TempBankNominalLedgerEntryID] UNIQUEIDENTIFIER NOT NULL,
    [BankStatementID]              UNIQUEIDENTIFIER NOT NULL,
    [BankReconciliationDate]       DATETIME         NOT NULL,
    [NominalLedgerEntryID]         UNIQUEIDENTIFIER NOT NULL,
    [TransactionAmount]            DECIMAL (18, 4)  NOT NULL,
    [TempDescription]              NVARCHAR (MAX)   NULL,
    [TransactionType]              NVARCHAR (10)    NOT NULL,
    [IsReconciled]                 BIT              CONSTRAINT [DF_tblBankReconciliationMatch_IsReconciled] DEFAULT ((0)) NULL,
    [Text1]                        NVARCHAR (50)    NULL,
    [Text2]                        NVARCHAR (50)    NULL,
    [Text3]                        NVARCHAR (50)    NULL,
    [Text4]                        NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([TempBankNominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblTempBankNominalLedgerEntry_tblBankStatement] FOREIGN KEY ([BankStatementID]) REFERENCES [dbo].[tblBankStatement] ([BankStatementID]),
    CONSTRAINT [FK_tblTempBankNominalLedgerEntry_tblNominalLedgerEntry] FOREIGN KEY ([NominalLedgerEntryID]) REFERENCES [dbo].[tblNominalLedgerEntry] ([NominalLedgerEntryID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
