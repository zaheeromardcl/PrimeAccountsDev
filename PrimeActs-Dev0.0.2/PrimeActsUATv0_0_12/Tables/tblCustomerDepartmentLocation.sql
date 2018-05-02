CREATE TABLE [dbo].[tblCustomerDepartmentLocation] (
    [CustomerDepartmentLocationID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerLocationID]           UNIQUEIDENTIFIER NULL,
    [CreatedByUserID]              UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                  DATETIME         NULL,
    [UpdatedByUserID]              UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                  DATETIME         NULL,
    CONSTRAINT [PK_tblCustomerDepartmentLocation] PRIMARY KEY CLUSTERED ([CustomerDepartmentLocationID] ASC),
    CONSTRAINT [FK_tblCustomerDepartmentLocation_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblCustomerDepartmentLocation_tblCustomerLocation] FOREIGN KEY ([CustomerLocationID]) REFERENCES [dbo].[tblCustomerLocation] ([CustomerLocationID]),
    CONSTRAINT [FK_tblCustomerDepartmentLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerDepartmentLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

