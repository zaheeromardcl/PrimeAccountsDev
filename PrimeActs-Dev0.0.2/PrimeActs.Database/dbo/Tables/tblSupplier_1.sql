CREATE TABLE [dbo].[tblSupplier] (
    [SupplierID]          UNIQUEIDENTIFIER NOT NULL,
    [SupplierCompanyName] NVARCHAR (50)    NOT NULL,
    [ParentSupplierID]    UNIQUEIDENTIFIER NULL,
    [SupplierCode]        NVARCHAR (10)    NOT NULL,
    [IsHaulier]           BIT              CONSTRAINT [DF_tblSupplier_IsHaulier] DEFAULT ((0)) NULL,
    [IsFactor]            BIT              NULL,
    [CompanyID]           UNIQUEIDENTIFIER NULL,
    [NoteID]              UNIQUEIDENTIFIER NULL,
    [TransactionTaxNo]            NVARCHAR (20)    NULL,
    [IsTransactionTaxExempt]       BIT              NULL,
    [UpdatedBy]           NVARCHAR (25)    NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedBy]           NVARCHAR (25)    NULL,
    [CreatedDate]         DATETIME         NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tblSuppli__IsAct__2EDAF651] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSupplier] PRIMARY KEY CLUSTERED ([SupplierID] ASC),
    CONSTRAINT [FK_tblSupplier_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblSupplier_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSupplier_tblSupplier] FOREIGN KEY ([ParentSupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID])
);

