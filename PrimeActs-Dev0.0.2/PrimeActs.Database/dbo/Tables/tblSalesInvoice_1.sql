CREATE TABLE [dbo].[tblSalesInvoice] (
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentAddressID] UNIQUEIDENTIFIER NULL,
    [ServerCode]                  NVARCHAR (1)     NOT NULL,
    [SalesInvoiceReference]       VARCHAR (12)     NOT NULL,
    [SalesInvoiceDate]            DATETIME         NOT NULL,
    [DivisionAddressID]           UNIQUEIDENTIFIER NULL,
    [CurrencyID]                  UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                NUMERIC (16, 4)  NULL,
    [NoteID]                      UNIQUEIDENTIFIER NULL,
	[DivisionID]                  UNIQUEIDENTIFIER NULL,
	[UpdatedBy]                   NVARCHAR (25)    NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedBy]                   NVARCHAR (25)    NULL,
    [CreatedDate]                 DATETIME         NULL,
    
    CONSTRAINT [PK_tblSalesInvoice] PRIMARY KEY CLUSTERED ([SalesInvoiceID] ASC),
    CONSTRAINT [FK_tblSalesInvoice_tblAddress] FOREIGN KEY ([CustomerDepartmentAddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblSalesInvoice_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblSalesInvoice_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblSalesInvoice_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSalesInvoice_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID])
);

