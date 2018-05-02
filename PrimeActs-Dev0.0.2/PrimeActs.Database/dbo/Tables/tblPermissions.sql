CREATE TABLE [dbo].[tblPermission] (
    [PermissionID] UNIQUEIDENTIFIER NOT NULL,
	--[PermissionGroupId] UNIQUEIDENTIFIER NULL,
	[PermissionController]        NVARCHAR (50)    NULL,        
    --[PermissionName]        NVARCHAR (50)    NULL,        
	[PermissionAction]       NVARCHAR (50)    NULL,    
	[PermissionDescription] NVARCHAR (50)    NULL,
	[BitNumber] int    NULL,
    CONSTRAINT [PK_tblPermission] PRIMARY KEY CLUSTERED ([PermissionID] ASC)--,
	--CONSTRAINT [FK_tblPermission_tblPermissionGroup] FOREIGN KEY ([PermissionGroupId]) REFERENCES [dbo].[tblPermissionGroup] ([PermissionGroupId])
);

