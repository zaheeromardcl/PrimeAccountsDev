CREATE TABLE [dbo].[tblUserRole] (
    [UserRoleID]      UNIQUEIDENTIFIER NOT NULL,
    [UserID]          UNIQUEIDENTIFIER NOT NULL,
    [RoleID]          UNIQUEIDENTIFIER NOT NULL,
    [DepartmentID]    UNIQUEIDENTIFIER NULL,
    [CompanyID]       UNIQUEIDENTIFIER NULL,
    [DivisionID]      UNIQUEIDENTIFIER NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]     DATETIME         NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    [UpdatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblUserRole] PRIMARY KEY CLUSTERED ([UserRoleID] ASC),
    CONSTRAINT [FK_tblUserRole_tblRole_RoleId] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[tblRole] ([RoleID]),
    CONSTRAINT [FK_tblUserRole_tblUser_UserId] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblUserRoleCompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblUserRoleCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblUserRoleDepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblUserRoleDivisionID] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblUserRoleUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

