CREATE TABLE [dbo].[tblSalesLedgerInvoiceAllocation] (
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
    CONSTRAINT [PK_tblSalesLedgerInvoiceAllocation] PRIMARY KEY CLUSTERED ([SalesLedgerInvoiceAllocationID] ASC),
    CONSTRAINT [FK_tblSalesLedgerInvoiceAllocation_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]),
    CONSTRAINT [FK_tblSalesLedgerInvoiceAllocation_tblSalesLedgerEntry] FOREIGN KEY ([SalesLedgerEntryID]) REFERENCES [dbo].[tblSalesLedgerEntry] ([SalesLedgerEntryID]),
    CONSTRAINT [FK_tblSalesLedgerInvoiceAllocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSalesLedgerInvoiceAllocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblSalesLedgerInvoiceAllocation_salesInvoiceID]
    ON [dbo].[tblSalesLedgerInvoiceAllocation]([SalesInvoiceID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblSalesLedgerInvoiceAllocation_SalesLedgerEntryID]
    ON [dbo].[tblSalesLedgerInvoiceAllocation]([SalesLedgerEntryID] ASC);

