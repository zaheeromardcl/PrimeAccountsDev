CREATE TABLE [dbo].[tblApplicationRolePermissions]
(
	[ApplicationRolePermissionId] UNIQUEIDENTIFIER NOT NULL,
	[ApplicationRoleId] UNIQUEIDENTIFIER NOT NULL,
	[PermissionGroupId] UNIQUEIDENTIFIER NOT NULL,
	[Value] INT NOT NULL,
	CONSTRAINT [PK_tblApplicationRolePermissions] PRIMARY KEY CLUSTERED ([ApplicationRolePermissionId] ASC)
)
