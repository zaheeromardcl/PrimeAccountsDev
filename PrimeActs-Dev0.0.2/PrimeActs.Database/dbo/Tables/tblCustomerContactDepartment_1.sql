CREATE TABLE [dbo].[tblCustomerContactDepartment] (
    [CustomerContactDepartmentLocationID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerContactID]                   UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentID]                UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblCustomerContactDepartment] PRIMARY KEY CLUSTERED ([CustomerContactDepartmentLocationID] ASC),
    CONSTRAINT [FK_tblCustomerContactDepartment_tblCustomerContact] FOREIGN KEY ([CustomerContactID]) REFERENCES [dbo].[tblCustomerContact] ([CustomerContactID]),
    CONSTRAINT [FK_tblCustomerContactDepartment_tblCustomerDepartment] FOREIGN KEY ([CustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment] ([CustomerDepartmentID])
);

