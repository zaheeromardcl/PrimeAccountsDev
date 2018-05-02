CREATE TABLE [dbo].[tgenPurchaseInvoiceNumber] (
    [PurchaseInvoiceID]     UNIQUEIDENTIFIER NOT NULL,
    [PurchaseInvoiceNumber] INT              IDENTITY (1, 1) NOT NULL, 
    CONSTRAINT [PK_tgenPurchaseInvoiceNumber] PRIMARY KEY ([PurchaseInvoiceID])
);

