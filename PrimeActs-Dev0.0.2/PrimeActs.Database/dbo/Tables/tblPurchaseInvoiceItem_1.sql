CREATE TABLE [dbo].[tblPurchaseInvoiceItem] (
    [PurchaseInvoiceItemID] UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentItemID]     UNIQUEIDENTIFIER NULL,
    [TotalPrice]            NUMERIC(18,4)            NOT NULL,
    [FXTotalPrice]          NUMERIC(18,4)            NULL,
    [CurrencyID]            UNIQUEIDENTIFIER NULL,
    [ExchangeRate]          NUMERIC (16, 4)  NULL,
    [PurchaseInvoiceID]     UNIQUEIDENTIFIER NOT NULL,
	[PurchaseDate]          DATETIME         NULL,
    [Quantity]              DECIMAL (12, 4)  NULL,
    [UpdatedBy]             NVARCHAR (25)    NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedBy]             NVARCHAR (25)    NULL,
    [CreatedDate]           DATETIME         NULL,
    [IsActive]              BIT              CONSTRAINT [DF__tblPurcha__IsAct__29221CFB] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblConsignmentItemPrice] PRIMARY KEY CLUSTERED ([PurchaseInvoiceItemID] ASC),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tblPurchaseInvoice] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID])
);

