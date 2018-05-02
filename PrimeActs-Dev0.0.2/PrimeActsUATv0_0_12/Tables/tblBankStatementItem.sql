CREATE TABLE [dbo].[tblBankStatementItem] (
    [BankStatementItemID] UNIQUEIDENTIFIER NOT NULL,
    [BankStatementDate]   DATETIME         NOT NULL,
    [TransactionAmount]   NUMERIC (18, 4)  NOT NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedDate]         DATETIME         NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    [Text1]               NVARCHAR (50)    NULL,
    [Text2]               NVARCHAR (50)    NULL,
    [Text3]               NVARCHAR (50)    NULL,
    [Text4]               NVARCHAR (50)    NULL,
    [TransactionType]     NVARCHAR (10)    NULL,
    [IsReconciled]        BIT              NULL,
    [BankStatementID]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblBankReconciliation_1] PRIMARY KEY CLUSTERED ([BankStatementItemID] ASC),
    CONSTRAINT [FK_tblBankReconciliationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBankReconciliationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblBankStatementItem_tblBankStatement] FOREIGN KEY ([BankStatementID]) REFERENCES [dbo].[tblBankStatement] ([BankStatementID])
);

