CREATE TABLE [dbo].[tblRoleContext] (
    [RoleContextID] UNIQUEIDENTIFIER NOT NULL,
    [UserRoleID]    UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]     UNIQUEIDENTIFIER NULL,
    [DepartmentID]  UNIQUEIDENTIFIER NULL,
    [DivisionID]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblRoleContext] PRIMARY KEY CLUSTERED ([RoleContextID] ASC),
    CONSTRAINT [FK_tblRoleContext_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblRoleContext_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblRoleContext_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID])
);

