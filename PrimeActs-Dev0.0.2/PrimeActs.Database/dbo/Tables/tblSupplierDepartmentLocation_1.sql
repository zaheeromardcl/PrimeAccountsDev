CREATE TABLE [dbo].[tblSupplierDepartmentLocation] (
    [SupplierDepartmentLocationID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationID]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblSupplierDepartmentLocation] PRIMARY KEY CLUSTERED ([SupplierDepartmentLocationID] ASC),
    CONSTRAINT [FK_tblSupplierDepartmentLocation_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierDepartmentLocation_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID])
);

