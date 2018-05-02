CREATE TABLE [dbo].[tblApplicationRolePermission] (
	[ApplicationRolePermissionID] UNIQUEIDENTIFIER CONSTRAINT [DF_tblApplicationRolePermission_ApplicationRolePermissionID] DEFAULT (newid()) NOT NULL,
    [ApplicationRoleID]       UNIQUEIDENTIFIER NOT NULL,
    [PermissionID] UNIQUEIDENTIFIER NOT NULL,
	[Value] INT NULL,
	CONSTRAINT [PK_tblApplicationRolePermission] PRIMARY KEY CLUSTERED ([ApplicationRolePermissionID] ASC),
    CONSTRAINT [FK_tblApplicationRolePermission_tblApplicationRole] FOREIGN KEY ([ApplicationRoleID]) REFERENCES [dbo].[tblApplicationRole] ([ApplicationRoleID]),
    CONSTRAINT [FK_tblApplicationRolePermission_tblPermission] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[tblPermission] ([PermissionID])
);

