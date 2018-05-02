CREATE TABLE [dbo].[tblPurchaseInvoiceItem] (
    [PurchaseInvoiceItemID]          UNIQUEIDENTIFIER NOT NULL,
    [ConsignmentItemID]              UNIQUEIDENTIFIER NULL,
    [TotalPrice]                     NUMERIC (18, 4)  NOT NULL,
    [PurchaseInvoiceItemDescription] NVARCHAR (400)   NULL,
    [PurchaseChargeTypeID]           UNIQUEIDENTIFIER NOT NULL,
    [CurrencyAmount]                 NUMERIC (18, 4)  NULL,
    [PurchaseInvoiceID]              UNIQUEIDENTIFIER NOT NULL,
    [PurchaseDate]                   DATETIME         NULL,
    [PurchaseInvoiceItemQuantity]    NUMERIC (12, 4)  NULL,
    [UpdatedDate]                    DATETIME         NULL,
    [CreatedDate]                    DATETIME         NULL,
    [CreatedByUserID]                UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]                UNIQUEIDENTIFIER NULL,
    [TransactionTaxRateID]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblConsignmentItemPrice] PRIMARY KEY CLUSTERED ([PurchaseInvoiceItemID] ASC),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tblConsignmentItem] FOREIGN KEY ([ConsignmentItemID]) REFERENCES [dbo].[tblConsignmentItem] ([ConsignmentItemID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tblPurchaseInvoice] FOREIGN KEY ([PurchaseInvoiceID]) REFERENCES [dbo].[tblPurchaseInvoice] ([PurchaseInvoiceID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tlkpPurchaseChargeType] FOREIGN KEY ([PurchaseChargeTypeID]) REFERENCES [dbo].[tlkpPurchaseChargeType] ([PurchaseChargeTypeID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItem_tlkpTransactionTaxRate] FOREIGN KEY ([TransactionTaxRateID]) REFERENCES [dbo].[tlkpTransactionTaxRate] ([TransactionTaxRateID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItemCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPurchaseInvoiceItemUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

