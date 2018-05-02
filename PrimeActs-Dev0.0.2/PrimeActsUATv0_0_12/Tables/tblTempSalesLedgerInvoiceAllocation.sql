CREATE TABLE [dbo].[tblTempSalesLedgerInvoiceAllocation] (
    [SalesLedgerInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL,
    [SalesLedgerEntryID]             UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceID]                 UNIQUEIDENTIFIER NOT NULL,
    [SaleAmount]                     NUMERIC (18, 4)  NOT NULL,
    [FXSaleAmount]                   NUMERIC (18, 4)  NULL,
    [CurrencyID]                     UNIQUEIDENTIFIER NULL,
    [ExchangeRate]                   NUMERIC (18, 4)  NULL,
    [NoteID]                         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                    DATETIME         NULL,
    [CreatedDate]                    DATETIME         NULL,
    [CreatedByUserID]                UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblTempSalesLedgerInvoiceAllocation] PRIMARY KEY CLUSTERED ([SalesLedgerInvoiceAllocationID] ASC),
    CONSTRAINT [FK_tblTempSalesLedgerInvoiceAllocation_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]),
    CONSTRAINT [FK_tblTempSalesLedgerInvoiceAllocation_tblSalesLedgerEntry] FOREIGN KEY ([SalesLedgerEntryID]) REFERENCES [dbo].[tblSalesLedgerEntry] ([SalesLedgerEntryID]),
    CONSTRAINT [FK_tblTempSalesLedgerInvoiceAllocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblTempSalesLedgerInvoiceAllocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblTempSalesLedgerInvoiceAllocation_salesInvoiceID]
    ON [dbo].[tblTempSalesLedgerInvoiceAllocation]([SalesInvoiceID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblTempSalesLedgerInvoiceAllocation_SalesLedgerEntryID]
    ON [dbo].[tblTempSalesLedgerInvoiceAllocation]([SalesLedgerEntryID] ASC);

