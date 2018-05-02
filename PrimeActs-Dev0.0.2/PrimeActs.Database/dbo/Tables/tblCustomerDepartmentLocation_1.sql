CREATE TABLE [dbo].[tblCustomerDepartmentLocation] (
    [CustomerDepartmentLocationID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerLocationID]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblCustomerDepartmentLocation] PRIMARY KEY CLUSTERED ([CustomerDepartmentLocationID] ASC),
    CONSTRAINT [FK_tblCustomerDepartmentLocation_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID]),
    CONSTRAINT [FK_tblCustomerDepartmentLocation_tblCustomerLocation] FOREIGN KEY ([CustomerLocationID]) REFERENCES [dbo].[tblCustomerLocation] ([CustomerLocationID])
);

