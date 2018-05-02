CREATE TABLE [dbo].[tblBankReconciliation] (
    [BankReconciliationID]    UNIQUEIDENTIFIER NOT NULL,
    [BankReconcilitationDate] DATETIME         NOT NULL,
    [NominalLedgerEntryID]    UNIQUEIDENTIFIER NOT NULL,
    [TransactionID]           INT              NOT NULL,
    [TransactionAmount]       FLOAT (53)       NOT NULL,
    [UserID]                  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]               NVARCHAR (25)    NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedBy]               NVARCHAR (25)    NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF__tblBankRe__IsAct__5B78929E] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblBankReconciliation_1] PRIMARY KEY CLUSTERED ([BankReconciliationID] ASC),
    CONSTRAINT [FK_tblBankReconciliation_tblApplicationUser] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblApplicationUser] ([ApplicationUserId]),
    CONSTRAINT [FK_tblBankReconciliation_tblNominalLedgerEntry1] FOREIGN KEY ([NominalLedgerEntryID]) REFERENCES [dbo].[tblNominalLedgerEntry] ([NominalLedgerEntryID])
);

