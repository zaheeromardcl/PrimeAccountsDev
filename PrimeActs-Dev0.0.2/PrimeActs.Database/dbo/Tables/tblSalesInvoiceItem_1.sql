CREATE TABLE [dbo].[tblSalesInvoiceItem] (
    [SalesInvoiceItemID]          UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceItemDescription] NVARCHAR (200)   NOT NULL,
    [SalesInvoiceItemLineTotal]   NUMERIC(18,4)            NOT NULL,
    [TicketItemID]                UNIQUEIDENTIFIER NOT NULL,
    [TransactionTaxRatePercentage]         DECIMAL (16, 4)  NULL,
	[TransactionTaxCodeID]         UNIQUEIDENTIFIER  NULL,

    [CurrencyAmount]              NUMERIC (16, 4)  NULL,
    [UpdatedBy]                   NVARCHAR (25)    NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedBy]                   NVARCHAR (25)    NULL,
    [CreatedDate]                 DATETIME         NULL,
    CONSTRAINT [PK_tblSalesInvoiceItem] PRIMARY KEY CLUSTERED ([SalesInvoiceItemID] ASC),
    CONSTRAINT [FK_tblSalesInvoiceItem_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES [dbo].[tblSalesInvoice] ([SalesInvoiceID]),
    CONSTRAINT [FK_tblSalesInvoiceItem_tblTicketItem] FOREIGN KEY ([TicketItemID]) REFERENCES [dbo].[tblTicketItem] ([TicketItemID]), 
    CONSTRAINT [FK_tblSalesInvoiceItem_tlkpTransactionTaxCode] FOREIGN KEY (TransactionTaxCodeID) REFERENCES [dbo].tlkpTransactionTaxCode([TransactionTaxCodeID])
);

