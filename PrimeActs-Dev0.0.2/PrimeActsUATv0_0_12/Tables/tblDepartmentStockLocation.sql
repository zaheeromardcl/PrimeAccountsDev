CREATE TABLE [dbo].[tblDepartmentStockLocation] (
    [DepartmentStockLocationID] UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]              UNIQUEIDENTIFIER NOT NULL,
    [StockLocationID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]               DATETIME         NULL,
    [UpdatedByUserID]           UNIQUEIDENTIFIER NULL,
    [UpdatedDate]               DATETIME         NULL,
    CONSTRAINT [PK_tblDepartmentStockLocation] PRIMARY KEY CLUSTERED ([DepartmentStockLocationID] ASC),
    CONSTRAINT [FK_tblDepartmentStockLocation_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblDepartmentStockLocation_tlkpStockLocation] FOREIGN KEY ([StockLocationID]) REFERENCES [dbo].[tlkpStockLocation] ([StockLocationID]),
    CONSTRAINT [FK_tblDepartmentStockLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblDepartmentStockLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

