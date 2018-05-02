CREATE TABLE [dbo].[tblSupplierContactDepartment] (
    [SupplierContactDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierContactID]           UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblSupplierContactDepartment] PRIMARY KEY CLUSTERED ([SupplierContactDepartmentID] ASC),
    CONSTRAINT [FK_tblSupplierContactDepartment_tblSupplierContact] FOREIGN KEY ([SupplierContactID]) REFERENCES [dbo].[tblSupplierContact] ([SupplierContactID]),
    CONSTRAINT [FK_tblSupplierContactDepartment_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID])
);

