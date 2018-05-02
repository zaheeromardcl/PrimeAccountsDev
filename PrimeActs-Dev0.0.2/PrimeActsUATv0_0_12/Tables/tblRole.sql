CREATE TABLE [dbo].[tblRole] (
    [RoleID]          UNIQUEIDENTIFIER NOT NULL,
    [RoleName]        NVARCHAR (256)   NOT NULL,
    [RoleDescription] NVARCHAR (MAX)   NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblApplicationRole] PRIMARY KEY CLUSTERED ([RoleID] ASC),
    CONSTRAINT [FK_tblRoleCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblroleUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

