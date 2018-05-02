<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSupplierContactDepartment] (
    [SupplierContactDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierContactID]           UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                 DATETIME         NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    CONSTRAINT [PK_tblSupplierContactDepartment] PRIMARY KEY CLUSTERED ([SupplierContactDepartmentID] ASC),
    CONSTRAINT [FK_tblSupplierContactDepartment_tblSupplierContact] FOREIGN KEY ([SupplierContactID]) REFERENCES [dbo].[tblSupplierContact] ([SupplierContactID]),
    CONSTRAINT [FK_tblSupplierContactDepartment_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierContactDepartmentCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierContactDepartmentUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSupplierContactDepartment] (
    [SupplierContactDepartmentID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierContactID]           UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                 DATETIME         NULL,
    [UpdatedByUserID]             UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                 DATETIME         NULL,
    CONSTRAINT [PK_tblSupplierContactDepartment] PRIMARY KEY CLUSTERED ([SupplierContactDepartmentID] ASC),
    CONSTRAINT [FK_tblSupplierContactDepartment_tblSupplierContact] FOREIGN KEY ([SupplierContactID]) REFERENCES [dbo].[tblSupplierContact] ([SupplierContactID]),
    CONSTRAINT [FK_tblSupplierContactDepartment_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [FK_tblSupplierContactDepartmentCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierContactDepartmentUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
