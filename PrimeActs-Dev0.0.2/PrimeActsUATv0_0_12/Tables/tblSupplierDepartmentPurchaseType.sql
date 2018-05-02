CREATE TABLE [dbo].[tblSupplierDepartmentPurchaseType] (
    [SupplierDepartmentPurchaseTypeID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentID]             UNIQUEIDENTIFIER NOT NULL,
    [PurchaseTypeID]                   UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]                  UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]                      DATETIME         NULL,
    [UpdatedByUserID]                  UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                      DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([SupplierDepartmentPurchaseTypeID] ASC),
    CONSTRAINT [tblSupplierDepartmentPurchaseType_tblSupplierDepartment] FOREIGN KEY ([SupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID]),
    CONSTRAINT [tblSupplierDepartmentPurchaseType_tlkpPurchaseType] FOREIGN KEY ([PurchaseTypeID]) REFERENCES [dbo].[tlkpPurchaseType] ([PurchaseTypeID]),
    CONSTRAINT [tblSupplierDepartmentPurchaseTypeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [tblSupplierDepartmentPurchaseTypeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

