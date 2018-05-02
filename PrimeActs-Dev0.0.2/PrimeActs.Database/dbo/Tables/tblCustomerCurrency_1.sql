CREATE TABLE [dbo].[tblCustomerCurrency] (
    [CustomerCurrencyID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]         UNIQUEIDENTIFIER NOT NULL,
    [CurrencyID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]          INT              NULL,
    CONSTRAINT [PK_tblCustomerCurrency] PRIMARY KEY CLUSTERED ([CustomerCurrencyID] ASC),
    CONSTRAINT [FK_tblCustomerCurrency_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomerCurrency_tlkpCurrency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[tlkpCurrency] ([CurrencyID])
);

