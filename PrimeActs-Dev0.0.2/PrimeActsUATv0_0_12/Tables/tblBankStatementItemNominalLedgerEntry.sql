CREATE TABLE [dbo].[tblBankStatementItemNominalLedgerEntry] (
    [BankStatementItemNominalLedgerEntryID] UNIQUEIDENTIFIER NOT NULL,
    [BankStatementItemID]                   UNIQUEIDENTIFIER NOT NULL,
    [NominalLedgerEntryID]                  UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]                       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                           DATETIME         NULL,
    [UpdatedByUserID]                       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                           DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([BankStatementItemNominalLedgerEntryID] ASC),
    CONSTRAINT [FK_tblBankStatementItemNominalLedgerEntry_tblBankStatementItem] FOREIGN KEY ([BankStatementItemID]) REFERENCES [dbo].[tblBankStatementItem] ([BankStatementItemID]),
    CONSTRAINT [FK_tblBankStatementItemNominalLedgerEntry_tblNominalLedgerEntry] FOREIGN KEY ([NominalLedgerEntryID]) REFERENCES [dbo].[tblNominalLedgerEntry] ([NominalLedgerEntryID]),
    CONSTRAINT [FK_tblBankStatementItemNominalLedgerEntryCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBankStatementItemNominalLedgerEntryUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

