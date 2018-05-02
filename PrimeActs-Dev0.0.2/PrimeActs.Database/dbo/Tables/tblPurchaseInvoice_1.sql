
CREATE TABLE [dbo].[tblPurchaseInvoice] (
    [PurchaseInvoiceID]     UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]  UNIQUEIDENTIFIER NOT NULL,
    [AddressID]             UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceNumber] NVARCHAR (20)    NOT NULL,
    [ServerCode]            NVARCHAR (1)     NULL,
    [PurchaseInvoiceDate]   DATETIME         NOT NULL,
    [CurrencyID]            UNIQUEIDENTIFIER NULL,
    [ExchangeRate]          NUMERIC (16, 4)  NULL,
    [NoteID]                UNIQUEIDENTIFIER NULL,
    [DivisionID]            UNIQUEIDENTIFIER NOT NULL,
	[TransactionTaxAmount]	NUMERIC (16,4)   NULL,
    [UpdatedBy]             NVARCHAR (25)    NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedBy]             NVARCHAR (25)    NULL,
    [CreatedDate]           DATETIME         NULL,
    [IsSaved] BIT NOT NULL,
	[Total] NUMERIC(18, 4) NULL, 
    [Status] INT NULL
	
    CONSTRAINT [PK_tblPurchaseInvoice_1] PRIMARY KEY CLUSTERED ([PurchaseInvoiceID] ASC),    
    CONSTRAINT [FK_tblPurchaseInvoice_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblPurchaseInvoice_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblPurchaseInvoice_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblPurchaseInvoice_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID])
);

