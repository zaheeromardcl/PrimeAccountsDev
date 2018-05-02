CREATE TABLE [dbo].[tblDivision] (
    [DivisionID]               UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]                UNIQUEIDENTIFIER NOT NULL,
    [DivisionName]             NVARCHAR (50)    NOT NULL,
    [UpdatedDate]              DATETIME         NULL,
    [CreatedDate]              DATETIME         NULL,
    [AddressID]                UNIQUEIDENTIFIER NULL,
    [IsActive]                 BIT              CONSTRAINT [DF__tblDivisi__IsAct__4316F928] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [TransactionTaxLocationID] UNIQUEIDENTIFIER NULL,
    [InvoiceNominalAccountID]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblDivision_1] PRIMARY KEY CLUSTERED ([DivisionID] ASC),
    CONSTRAINT [FK_tblDivision_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblDivision_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblDivisionCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblDivisionInvoiceNominalAccountID] FOREIGN KEY ([InvoiceNominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tblDivisionTransactionTaxLocation] FOREIGN KEY ([TransactionTaxLocationID]) REFERENCES [dbo].[tlkpTransactionTaxLocation] ([TransactionTaxLocationID]),
    CONSTRAINT [FK_tblDivisionUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

