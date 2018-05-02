CREATE TABLE [dbo].[tblRolePermissions]
(
	[ApplicationRolePermissionId] UNIQUEIDENTIFIER NOT NULL,
	[ApplicationRoleId] UNIQUEIDENTIFIER NOT NULL,
	[PermissionGroupId] UNIQUEIDENTIFIER NOT NULL,
	[Value] INT NOT NULL,
	CONSTRAINT [PK_tblRolePermissions] PRIMARY KEY CLUSTERED ([ApplicationRolePermissionId] ASC)
)
