<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblStockBoard] (
    [StockboardID]    UNIQUEIDENTIFIER NOT NULL,
    [StockboardName]  NVARCHAR (30)    NOT NULL,
    [DepartmentID]    UNIQUEIDENTIFIER NULL,
    [CompanyID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblStockboard] PRIMARY KEY CLUSTERED ([StockboardID] ASC),
    CONSTRAINT [FK_tblStockboard_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblStockboard_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblStockboard_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblStockboard_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblStockBoard] (
    [StockboardID]    UNIQUEIDENTIFIER NOT NULL,
    [StockboardName]  NVARCHAR (30)    NOT NULL,
    [DepartmentID]    UNIQUEIDENTIFIER NULL,
    [CompanyID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblStockboard] PRIMARY KEY CLUSTERED ([StockboardID] ASC),
    CONSTRAINT [FK_tblStockboard_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblStockboard_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblStockboard_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblStockboard_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
