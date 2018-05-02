CREATE TABLE [dbo].[tblStockBoardProduceGroupDepartment] (
    [StockBoardProduceGroupDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [StockBoardID]                       UNIQUEIDENTIFIER NOT NULL,
    [ProduceGroupID]                     UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]                       UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]                    UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                        DATETIME         NULL,
    [UpdatedByUserID]                    UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                        DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([StockBoardProduceGroupDepartmentID] ASC),
    CONSTRAINT [FK_tblStockBoardProduceGroupDepartment2_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblStockBoardProduceGroupDepartment2_ProduceGroupID] FOREIGN KEY ([ProduceGroupID]) REFERENCES [dbo].[tblProduceGroup] ([ProduceGroupID]),
    CONSTRAINT [FK_tblStockBoardProduceGroupDepartment2_StockBoardID] FOREIGN KEY ([StockBoardID]) REFERENCES [dbo].[tblStockBoard] ([StockboardID]),
    CONSTRAINT [FK_tblStockBoardProduceGroupDepartment2CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblStockBoardProduceGroupDepartment2UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblStockBoardProduceGroupDepartment2_PGIDDIDSBID]
    ON [dbo].[tblStockBoardProduceGroupDepartment]([ProduceGroupID] ASC, [DepartmentID] ASC, [StockBoardID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblStockBoardProduceGroupDepartment2_DIDPGIDSBID]
    ON [dbo].[tblStockBoardProduceGroupDepartment]([DepartmentID] ASC, [ProduceGroupID] ASC, [StockBoardID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblStockBoardProduceGroupDepartment2_SBIDDIDPGID]
    ON [dbo].[tblStockBoardProduceGroupDepartment]([StockBoardID] ASC, [DepartmentID] ASC, [ProduceGroupID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblStockBoardProduceGroupDepartment2_SBIDPGIDDID]
    ON [dbo].[tblStockBoardProduceGroupDepartment]([StockBoardID] ASC, [ProduceGroupID] ASC, [DepartmentID] ASC);

