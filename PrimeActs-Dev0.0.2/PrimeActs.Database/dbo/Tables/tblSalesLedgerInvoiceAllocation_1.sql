CREATE TABLE [dbo].[tblSalesLedgerInvoiceAllocation]
(
	[SalesLedgerInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL , 
    [SalesLedgerEntryID] UNIQUEIDENTIFIER NOT NULL, 
    [SalesInvoiceID] UNIQUEIDENTIFIER NOT NULL, 
    [SaleAmount] NUMERIC(18, 4) NOT NULL, 
    [FXSaleAmount] NUMERIC(18, 4) NULL, 
    [CurrencyID] UNIQUEIDENTIFIER NULL, 
    [ExchangeRate] NUMERIC(18, 4) NULL, 
    [NoteID] UNIQUEIDENTIFIER NULL, 
    [UpdatedBy] NVARCHAR(25) NULL, 
    [UpdatedDate] DATETIME NULL, 
    [CreatedBy] NVARCHAR(25) NULL, 
    [CreatedDate] DATETIME NULL,

	CONSTRAINT [PK_tblSalesLedgerInvoiceAllocation] PRIMARY KEY ([SalesLedgerInvoiceAllocationID]),
	CONSTRAINT [FK_tblSalesLedgerInvoiceAllocation_tblSalesLedgerEntry] FOREIGN KEY ([SalesLedgerEntryID]) REFERENCES [dbo].[tblSalesLedgerEntry] ([SalesLedgerEntryID]),
    CONSTRAINT [FK_tblSalesLedgerInvoiceAllocation_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]), 
    
)
