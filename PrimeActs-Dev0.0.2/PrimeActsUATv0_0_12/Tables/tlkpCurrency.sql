CREATE TABLE [dbo].[tlkpCurrency] (
    [CurrencyID]          UNIQUEIDENTIFIER NOT NULL,
    [CurrencyName]        NVARCHAR (50)    NOT NULL,
    [CurrencyCode]        NVARCHAR (10)    NOT NULL,
    [DefaultExchangeRate] DECIMAL (16, 4)  NULL,
    [UpdatedDate]         DATETIME         NULL,
    [CreatedDate]         DATETIME         NULL,
    [IsActive]            BIT              CONSTRAINT [DF__tlkpCurre__IsAct__367C1819] DEFAULT ((1)) NOT NULL,
    [CompanyID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]     UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpCurrency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC),
    CONSTRAINT [FK_tlkpCurrency_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpCurrencyCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpCurrencyUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

