CREATE TABLE [dbo].[tblPermission] (
    [PermissionID]          UNIQUEIDENTIFIER NOT NULL,
    [PermissionController]  NVARCHAR (50)    NULL,
    [PermissionAction]      NVARCHAR (50)    NULL,
    [PermissionDescription] NVARCHAR (50)    NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]           DATETIME         NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    [UpdatedDate]           DATETIME         NULL,
    CONSTRAINT [PK_tblPermission] PRIMARY KEY CLUSTERED ([PermissionID] ASC),
    CONSTRAINT [FK_tblPermissionCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblPermissionUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

