CREATE TABLE [dbo].[tblApplicationUserRole] (
    [ApplicationUserRoleId] UNIQUEIDENTIFIER CONSTRAINT [DF_tblApplicationUserRole_ApplicationUserRoleId] DEFAULT (newid()) NOT NULL,
    [ApplicationUserId]     UNIQUEIDENTIFIER NOT NULL,
    [ApplicationRoleID]     UNIQUEIDENTIFIER NOT NULL,
	[DepartmentId]         UNIQUEIDENTIFIER NULL,
    [CompanyId]            UNIQUEIDENTIFIER NULL,
    [DivisionId]           UNIQUEIDENTIFIER NULL,
	CONSTRAINT [PK_tblApplicationUserRole] PRIMARY KEY CLUSTERED ([ApplicationUserRoleId] ASC),
    CONSTRAINT [FK_tblApplicationUserRole_tblApplicationRole_RoleId] FOREIGN KEY ([ApplicationRoleID]) REFERENCES [dbo].[tblApplicationRole] ([ApplicationRoleID]),
    CONSTRAINT [FK_tblApplicationUserRole_tblApplicationUser_UserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[tblApplicationUser] ([ApplicationUserId])
);

