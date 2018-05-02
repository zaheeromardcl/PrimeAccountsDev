<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblStockBoardProduceGroup] (
    [StockboardProduceGroupID] UNIQUEIDENTIFIER NOT NULL,
    [StockboardID]             UNIQUEIDENTIFIER NOT NULL,
    [ProduceGroupDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]              DATETIME         NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [UpdatedDate]              DATETIME         NULL,
    CONSTRAINT [PK_tblStockboardProduceGroup] PRIMARY KEY CLUSTERED ([StockboardProduceGroupID] ASC),
    CONSTRAINT [FK_tblStockboardProduceGroup_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblStockboardProduceGroup_tblProduceGroupDepartment] FOREIGN KEY ([ProduceGroupDepartmentID]) REFERENCES [dbo].[tblProduceGroupDepartment] ([ProduceGroupDepartmentID]),
    CONSTRAINT [FK_tblStockboardProduceGroup_tblStockboard] FOREIGN KEY ([StockboardID]) REFERENCES [dbo].[tblStockBoard] ([StockboardID]),
    CONSTRAINT [FK_tblStockboardProduceGroup_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblStockBoardProduceGroup] (
    [StockboardProduceGroupID] UNIQUEIDENTIFIER NOT NULL,
    [StockboardID]             UNIQUEIDENTIFIER NOT NULL,
    [ProduceGroupDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]              DATETIME         NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [UpdatedDate]              DATETIME         NULL,
    CONSTRAINT [PK_tblStockboardProduceGroup] PRIMARY KEY CLUSTERED ([StockboardProduceGroupID] ASC),
    CONSTRAINT [FK_tblStockboardProduceGroup_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblStockboardProduceGroup_tblProduceGroupDepartment] FOREIGN KEY ([ProduceGroupDepartmentID]) REFERENCES [dbo].[tblProduceGroupDepartment] ([ProduceGroupDepartmentID]),
    CONSTRAINT [FK_tblStockboardProduceGroup_tblStockboard] FOREIGN KEY ([StockboardID]) REFERENCES [dbo].[tblStockBoard] ([StockboardID]),
    CONSTRAINT [FK_tblStockboardProduceGroup_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
