CREATE TABLE [dbo].[tblSalesInvoiceSundryItem] (
    [SalesInvoiceSundryItemID]          UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceItemDescription] NVARCHAR (200)   NOT NULL,
	[SalesInvoiceItemQuantity] INT NOT NULL, 
    [SalesInvoiceItemLineTotal]   NUMERIC(18,4)            NOT NULL,
    [SalesInvoiceItemTransactionTax]         DECIMAL (16, 4)  NOT NULL,
    [CurrencyAmount]              NUMERIC (16, 4)  NULL,
    [UpdatedBy]                   NVARCHAR (25)    NULL,
    [UpdatedDate]                 DATETIME         NULL,
    [CreatedBy]                   NVARCHAR (25)    NULL,
    [CreatedDate]                 DATETIME         NULL,
    [IsActive]                    BIT              NOT NULL,
    
    CONSTRAINT [PK_[tblSalesInvoiceSundryItem] PRIMARY KEY CLUSTERED ([SalesInvoiceSundryItemID] ASC), 
    CONSTRAINT [FK_tblSalesInvoiceSundryItem_tblSalesInvoice] FOREIGN KEY ([SalesInvoiceID]) REFERENCES tblsalesInvoice([SalesInvoiceID])
    
);
