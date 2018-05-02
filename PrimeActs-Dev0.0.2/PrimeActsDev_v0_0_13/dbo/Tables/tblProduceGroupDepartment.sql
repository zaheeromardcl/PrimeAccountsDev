<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblProduceGroupDepartment] (
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

=======
﻿CREATE TABLE [dbo].[tblProduceGroupDepartment] (
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

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
