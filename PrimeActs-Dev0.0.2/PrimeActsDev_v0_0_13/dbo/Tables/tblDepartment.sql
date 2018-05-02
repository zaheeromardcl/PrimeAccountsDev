<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblDepartment] (
    [DepartmentID]           UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]             UNIQUEIDENTIFIER NULL,
    [DepartmentName]         NVARCHAR (50)    NULL,
    [DepartmentCode]         NVARCHAR (2)     NOT NULL,
    [UpdatedDate]            DATETIME         NULL,
    [CreatedDate]            DATETIME         NULL,
    [AddressID]              UNIQUEIDENTIFIER NULL,
    [IsActive]               BIT              CONSTRAINT [DF__tblDepart__IsAct__403A8C7D] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]        UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]        UNIQUEIDENTIFIER NULL,
    [RebateNominalAccountID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblDepartment_1] PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
    CONSTRAINT [FK_tblDepartment_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblDepartment_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblDepartment_tblNominalAccount] FOREIGN KEY ([RebateNominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tblDepartmentCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblDepartmentUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
ALTER TABLE [dbo].[tblDepartment] NOCHECK CONSTRAINT [FK_tblDepartment_tblNominalAccount];


GO
CREATE NONCLUSTERED INDEX [IX_tblDepartment]
    ON [dbo].[tblDepartment]([DepartmentID] ASC);

=======
﻿CREATE TABLE [dbo].[tblDepartment] (
    [DepartmentID]           UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]             UNIQUEIDENTIFIER NULL,
    [DepartmentName]         NVARCHAR (50)    NULL,
    [DepartmentCode]         NVARCHAR (2)     NOT NULL,
    [UpdatedDate]            DATETIME         NULL,
    [CreatedDate]            DATETIME         NULL,
    [AddressID]              UNIQUEIDENTIFIER NULL,
    [IsActive]               BIT              CONSTRAINT [DF__tblDepart__IsAct__403A8C7D] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]        UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]        UNIQUEIDENTIFIER NULL,
    [RebateNominalAccountID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblDepartment_1] PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
    CONSTRAINT [FK_tblDepartment_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblDepartment_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblDepartment_tblNominalAccount] FOREIGN KEY ([RebateNominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tblDepartmentCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblDepartmentUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
ALTER TABLE [dbo].[tblDepartment] NOCHECK CONSTRAINT [FK_tblDepartment_tblNominalAccount];


GO
CREATE NONCLUSTERED INDEX [IX_tblDepartment]
    ON [dbo].[tblDepartment]([DepartmentID] ASC);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
