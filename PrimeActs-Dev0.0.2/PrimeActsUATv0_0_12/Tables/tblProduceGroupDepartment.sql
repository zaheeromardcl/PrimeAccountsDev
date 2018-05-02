CREATE TABLE [dbo].[tblProduceGroupDepartment] (
    [ProduceGroupDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [ProduceGroupID]           UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]              DATETIME         NULL,
    [UpdatedByUserID]          UNIQUEIDENTIFIER NULL,
    [UpdatedDate]              DATETIME         NULL,
    CONSTRAINT [PK_tblProduceGroupDepartment] PRIMARY KEY CLUSTERED ([ProduceGroupDepartmentID] ASC),
    CONSTRAINT [FK_tblProduceGroupDepartment_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblProduceGroupDepartment_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblProduceGroupDepartment_tblProduceGroup] FOREIGN KEY ([ProduceGroupID]) REFERENCES [dbo].[tblProduceGroup] ([ProduceGroupID]),
    CONSTRAINT [FK_tblProduceGroupDepartment_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblProduceGroupDepartment_DepIDProGrpID]
    ON [dbo].[tblProduceGroupDepartment]([DepartmentID] ASC, [ProduceGroupID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblProduceGroupDepartment_ProdGrpIDDeptID]
    ON [dbo].[tblProduceGroupDepartment]([ProduceGroupID] ASC, [DepartmentID] ASC);

