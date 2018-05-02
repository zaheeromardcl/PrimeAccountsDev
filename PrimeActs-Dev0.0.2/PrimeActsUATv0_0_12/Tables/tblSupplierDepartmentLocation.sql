CREATE TABLE [dbo].[tblSupplierDepartmentLocation] (
    [SupplierDepartmentLocationID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]              UNIQUEIDENTIFIER NULL,
    [CreatedDate]                  DATETIME         NULL,
    [UpdatedByUserID]              UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                  DATETIME         NULL,
    CONSTRAINT [PK_tblSupplierDepartmentLocation] PRIMARY KEY CLUSTERED ([SupplierDepartmentLocationID] ASC),
    CONSTRAINT [FK_tblSupplierDepartmentLocation_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierDepartmentLocation_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID]),
    CONSTRAINT [FK_tblSupplierDepartmentLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierDepartmentLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

