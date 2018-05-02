<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblRolePermission] (
    [RolePermissionID] UNIQUEIDENTIFIER CONSTRAINT [DF_tblApplicationRolePermission_ApplicationRolePermissionID] DEFAULT (newid()) NOT NULL,
    [RoleID]           UNIQUEIDENTIFIER NOT NULL,
    [PermissionID]     UNIQUEIDENTIFIER NOT NULL,
    [Value]            INT              NULL,
    [CreatedByUserID]  UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]      DATETIME         NULL,
    [UpdatedByUserID]  UNIQUEIDENTIFIER NULL,
    [UpdatedDate]      DATETIME         NULL,
    CONSTRAINT [PK_tblApplicationRolePermission] PRIMARY KEY CLUSTERED ([RolePermissionID] ASC),
    CONSTRAINT [FK_tblApplicationRolePermission_tblApplicationRole] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[tblRole] ([RoleID]),
    CONSTRAINT [FK_tblApplicationRolePermission_tblPermission] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[tblPermission] ([PermissionID]),
    CONSTRAINT [FK_tblRolePermissionCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblRolePermissionUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblRolePermission] (
    [RolePermissionID] UNIQUEIDENTIFIER CONSTRAINT [DF_tblApplicationRolePermission_ApplicationRolePermissionID] DEFAULT (newid()) NOT NULL,
    [RoleID]           UNIQUEIDENTIFIER NOT NULL,
    [PermissionID]     UNIQUEIDENTIFIER NOT NULL,
    [Value]            INT              NULL,
    [CreatedByUserID]  UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]      DATETIME         NULL,
    [UpdatedByUserID]  UNIQUEIDENTIFIER NULL,
    [UpdatedDate]      DATETIME         NULL,
    CONSTRAINT [PK_tblApplicationRolePermission] PRIMARY KEY CLUSTERED ([RolePermissionID] ASC),
    CONSTRAINT [FK_tblApplicationRolePermission_tblApplicationRole] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[tblRole] ([RoleID]),
    CONSTRAINT [FK_tblApplicationRolePermission_tblPermission] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[tblPermission] ([PermissionID]),
    CONSTRAINT [FK_tblRolePermissionCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblRolePermissionUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
