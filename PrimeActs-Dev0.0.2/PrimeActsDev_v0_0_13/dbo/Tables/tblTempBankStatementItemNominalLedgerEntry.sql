<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblTempBankStatementItemNominalLedgerEntry] (
    [BankStatementItemNominalLedgerEntryID] UNIQUEIDENTIFIER NOT NULL,
    [BankStatementItemID]                   UNIQUEIDENTIFIER NOT NULL,
    [NominalLedgerEntryID]                  UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([BankStatementItemNominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblTempBankStatementItemNominalLedgerEntry_tblBankStatementItem] FOREIGN KEY ([BankStatementItemID]) REFERENCES [dbo].[tblBankStatementItem] ([BankStatementItemID]),
    CONSTRAINT [FK_tblTempBankStatementItemNominalLedgerEntry_tblNominalLedgerEntry] FOREIGN KEY ([NominalLedgerEntryID]) REFERENCES [dbo].[tblNominalLedgerEntry] ([NominalLedgerEntryID])
);

=======
﻿CREATE TABLE [dbo].[tblTempBankStatementItemNominalLedgerEntry] (
    [BankStatementItemNominalLedgerEntryID] UNIQUEIDENTIFIER NOT NULL,
    [BankStatementItemID]                   UNIQUEIDENTIFIER NOT NULL,
    [NominalLedgerEntryID]                  UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([BankStatementItemNominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblTempBankStatementItemNominalLedgerEntry_tblBankStatementItem] FOREIGN KEY ([BankStatementItemID]) REFERENCES [dbo].[tblBankStatementItem] ([BankStatementItemID]),
    CONSTRAINT [FK_tblTempBankStatementItemNominalLedgerEntry_tblNominalLedgerEntry] FOREIGN KEY ([NominalLedgerEntryID]) REFERENCES [dbo].[tblNominalLedgerEntry] ([NominalLedgerEntryID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
