CREATE TABLE [dbo].[tblTempCashSalesInvoiceAllocation] (
    [TempCashSalesInvoiceAllocationID] UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]                       UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceID]                   UNIQUEIDENTIFIER NOT NULL,
    [SalesLedgerEntryID]               UNIQUEIDENTIFIER NOT NULL,
    [AllocationAmount]                 NUMERIC (18, 4)  NOT NULL,
    [CreatedByUserID]                  UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                      DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([TempCashSalesInvoiceAllocationID] ASC),
    CONSTRAINT [FK_tblTempCashSalesInvoiceAllocation_DivisionID] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblTempCashSalesInvoiceAllocation_SalesInvoiceID] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]),
    CONSTRAINT [FK_tblTempCashSalesInvoiceAllocation_SalesLedgerEntryID] FOREIGN KEY ([SalesLedgerEntryID]) REFERENCES [dbo].[tblSalesLedgerEntry] ([SalesLedgerEntryID]),
    CONSTRAINT [FK_tblTempCashSalesInvoiceAllocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblTempCashSalesInvoiceAllocation_DivisionID]
    ON [dbo].[tblTempCashSalesInvoiceAllocation]([DivisionID] ASC);

