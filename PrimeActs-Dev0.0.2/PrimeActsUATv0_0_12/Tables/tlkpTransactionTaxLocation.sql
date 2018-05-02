CREATE TABLE [dbo].[tlkpTransactionTaxLocation] (
    [TransactionTaxLocationID]               UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxLocationName]             NVARCHAR (50)    NOT NULL,
    [TransactionTaxLocationNominalAccountID] UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxDisplayName]              NVARCHAR (20)    NOT NULL,
    [TransactionTaxReference]                NVARCHAR (50)    NOT NULL,
    [CreatedByUserID]                        UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                            DATETIME         NULL,
    [UpdatedByUserID]                        UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                            DATETIME         NULL,
    [CompanyID]                              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tlkpTransactionTaxLocation] PRIMARY KEY CLUSTERED ([TransactionTaxLocationID] ASC),
    CONSTRAINT [FK_tlkpTransactionTaxLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpTransactionTaxLocation_tblNominalAccount] FOREIGN KEY ([TransactionTaxLocationNominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tlkpTransactionTaxLocation_tblUser] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpTransactionTaxLocation_tblUser1] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

