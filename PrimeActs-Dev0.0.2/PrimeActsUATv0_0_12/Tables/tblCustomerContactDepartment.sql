CREATE TABLE [dbo].[tblCustomerContactDepartment] (
    [CustomerContactDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerContactID]           UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]        UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                 DATETIME         NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    CONSTRAINT [PK_tblCustomerContactDepartment] PRIMARY KEY CLUSTERED ([CustomerContactDepartmentID] ASC),
    CONSTRAINT [FK_tblCustomerContactDepartment_tblCustomerContact] FOREIGN KEY ([CustomerContactID]) REFERENCES [dbo].[tblCustomerContact] ([CustomerContactID]),
    CONSTRAINT [FK_tblCustomerContactDepartment_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblCustomerContactDepartmentCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerContactDepartmentUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

