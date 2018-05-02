CREATE TABLE [dbo].[tlkpBankTransactionType] (
    [BankTransactionTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [BankTransactionTypeName] NVARCHAR (30)    NOT NULL,
    PRIMARY KEY CLUSTERED ([BankTransactionTypeID] ASC)
);

