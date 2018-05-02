CREATE TABLE [dbo].[tblDepartmentStockLocation] (
    [DepartmentStockLocationID] UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]              UNIQUEIDENTIFIER NOT NULL,
    [StockLocationID]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblDepartmentStockLocation] PRIMARY KEY CLUSTERED ([DepartmentStockLocationID] ASC),
    CONSTRAINT [FK_tblDepartmentStockLocation_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblDepartmentStockLocation_tlkpStockLocation] FOREIGN KEY ([StockLocationID]) REFERENCES [dbo].[tlkpStockLocation] ([StockLocationID])
);

