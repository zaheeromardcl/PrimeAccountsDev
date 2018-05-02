CREATE TABLE [dbo].[tlkpTransactionTaxRate] (
    [TransactionTaxRateID]         UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxRatePercentage] NUMERIC (6, 4)   NOT NULL,
    [StartDate]         DATE             NOT NULL,
    [UpdatedBy]         NVARCHAR (25)    NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CreatedBy]         NVARCHAR (25)    NULL,
    [CreatedDate]       DATETIME         NULL,
    [TransactionTaxCodeID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblVATRate_1] PRIMARY KEY CLUSTERED ([TransactionTaxRateID] ASC),
    CONSTRAINT [FK_tlkpVATRate_tlkpVATCode] FOREIGN KEY ([TransactionTaxCodeID]) REFERENCES [dbo].[tlkpTransactionTaxCode] ([TransactionTaxCodeID])
);

