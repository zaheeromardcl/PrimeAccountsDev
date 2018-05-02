CREATE TABLE [dbo].[tlkpStockLocation] (
    [StockLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [StockLocationName] NVARCHAR (200)   NOT NULL,
    [StockLocationCode] NVARCHAR (10)    NOT NULL,
    [CompanyID]         UNIQUEIDENTIFIER NOT NULL,
    [AddressID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpStockLocation] PRIMARY KEY CLUSTERED ([StockLocationID] ASC),
    CONSTRAINT [FK_tlkpStockLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tlkpStockLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

