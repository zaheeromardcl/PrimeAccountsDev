CREATE TABLE [dbo].[tgenSalesInvoiceNumber] (
    [SalesInvoiceID]     UNIQUEIDENTIFIER NOT NULL,
    [SalesInvoiceNumber] VARCHAR (8)      NOT NULL,
    [Prefix]             VARCHAR (2)      NOT NULL,
    [Suffix]             VARCHAR (2)      NOT NULL,
    [DivisionID]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tgenSalesInvoiceNumber] PRIMARY KEY CLUSTERED ([SalesInvoiceID] ASC)
);

