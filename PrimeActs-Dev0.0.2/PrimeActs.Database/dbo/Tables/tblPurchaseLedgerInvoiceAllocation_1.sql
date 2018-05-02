CREATE TABLE [dbo].[tblPurchaseLedgerInvoiceAllocation]
(
	[PurchaseLedgerInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL , 
    [PurchaseLedgerEntryID] UNIQUEIDENTIFIER NOT NULL, 
    [PurchaseInvoiceID] UNIQUEIDENTIFIER NOT NULL, 
    [PurchaseAmount] NUMERIC(18, 4) NOT NULL, 
    [FXPurchaseAmount] NUMERIC(18, 4) NULL, 
    [CurrencyID] UNIQUEIDENTIFIER NULL, 
    [ExchangeRate] NUMERIC(18, 4) NULL, 
    [NoteID] UNIQUEIDENTIFIER NULL, 
    [UpdatedBy] NVARCHAR(25) NULL, 
    [UpdatedDate] DATETIME NULL, 
    [CreatedBy] NVARCHAR(25) NULL, 
    [CreatedDate] DATETIME NULL, 
    CONSTRAINT [PK_tblPurchaseLedgerInvoiceAllocation] PRIMARY KEY ([PurchaseLedgerInvoiceAllocationID]),
	 CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocation_tblPurchaseLedgerEntry] FOREIGN KEY ([PurchaseLedgerEntryID]) REFERENCES [dbo].[tblPurchaseLedgerEntry] ([PurchaseLedgerEntryID]),
    CONSTRAINT [FK_tblPurchaseLedgerInvoiceAllocation_tblPurchaseInvoiceID] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID])

)
