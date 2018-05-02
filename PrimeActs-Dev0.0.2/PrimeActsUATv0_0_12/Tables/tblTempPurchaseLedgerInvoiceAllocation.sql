CREATE TABLE [dbo].[tblTempPurchaseLedgerInvoiceAllocation] (
    [PurchaseLedgerInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL,
    [PurchaseLedgerEntryID]             UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceID]                 UNIQUEIDENTIFIER NOT NULL,
    [PurchaseAmount]                    NUMERIC (18, 4)  NOT NULL,
    [FXPurchaseAmount]                  NUMERIC (18, 4)  NULL,
    [CurrencyID]                        UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                      NUMERIC (18, 4)  NULL,
    [NoteID]                            UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                       DATETIME         NULL,
    [CreatedDate]                       DATETIME         NULL,
    [CreatedByUserID]                   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblTempPurchaseLedgerInvoiceAllocation] PRIMARY KEY CLUSTERED ([PurchaseLedgerInvoiceAllocationID] ASC),
    CONSTRAINT [FK_tblTempPurchaseLedgerInvoiceAllocation_tblPurchaseInvoiceID] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblTempPurchaseLedgerInvoiceAllocation_tblPurchaseLedgerEntry] FOREIGN KEY ([PurchaseLedgerEntryID]) REFERENCES [dbo].[tblPurchaseLedgerEntry] ([PurchaseLedgerEntryID]),
    CONSTRAINT [FK_tblTempPurchaseLedgerInvoiceAllocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblTempPurchaseLedgerInvoiceAllocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

