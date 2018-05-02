<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpStockLocation] (
    [StockLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [StockLocationName] NVARCHAR (200)   NOT NULL,
    [StockLocationCode] NVARCHAR (10)    NOT NULL,
    [CompanyID]         UNIQUEIDENTIFIER NOT NULL,
    [AddressID]         UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]       DATETIME         NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    CONSTRAINT [PK_tlkpStockLocation] PRIMARY KEY CLUSTERED ([StockLocationID] ASC),
    CONSTRAINT [FK_tlkpStockLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tlkpStockLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpStockLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpStockLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpStockLocation] (
    [StockLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [StockLocationName] NVARCHAR (200)   NOT NULL,
    [StockLocationCode] NVARCHAR (10)    NOT NULL,
    [CompanyID]         UNIQUEIDENTIFIER NOT NULL,
    [AddressID]         UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]       DATETIME         NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    CONSTRAINT [PK_tlkpStockLocation] PRIMARY KEY CLUSTERED ([StockLocationID] ASC),
    CONSTRAINT [FK_tlkpStockLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tlkpStockLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpStockLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpStockLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
