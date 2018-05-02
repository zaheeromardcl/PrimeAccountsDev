CREATE TABLE [dbo].[tlkpCurrency] (
    [CurrencyID]          UNIQUEIDENTIFIER NOT NULL,
    [CurrencyName]        NVARCHAR (50)    NOT NULL,
    [CurrencyCode]        NVARCHAR (10)    NOT NULL,
    [DefaultExchangeRate] DECIMAL (16, 4)  NULL,
    [UpdatedBy]           NVARCHAR (25)    NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedBy]           NVARCHAR (25)    NULL,
    [CreatedDate]         DATETIME         NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tlkpCurre__IsAct__367C1819] DEFAULT ((1)) NOT NULL,
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tlkpCurrency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC),
    CONSTRAINT [FK_tlkpCurrency_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

