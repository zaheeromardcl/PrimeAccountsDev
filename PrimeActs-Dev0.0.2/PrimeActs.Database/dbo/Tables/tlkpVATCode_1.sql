CREATE TABLE [dbo].[tlkpTransactionTaxCode] (
    [TransactionTaxCodeID]          UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxCode]            NVARCHAR (50)    NULL,
    [TransactionTaxCodeDescription] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]          NVARCHAR (25)    NULL,
    [UpdatedDate]        DATETIME         NULL,
    [CreatedBy]          NVARCHAR (25)    NULL,
    [CreatedDate]        DATETIME         NULL,
    [IsActive]           BIT              CONSTRAINT [DF__tlkpVATCo__IsAct__3B40CD36] DEFAULT ((1)) NOT NULL,
    [CompanyID]          UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblVATCode_1] PRIMARY KEY CLUSTERED ([TransactionTaxCodeID] ASC),
    CONSTRAINT [FK_tlkpVATCode_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

