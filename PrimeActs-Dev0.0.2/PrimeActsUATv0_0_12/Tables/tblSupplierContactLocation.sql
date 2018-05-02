CREATE TABLE [dbo].[tblSupplierContactLocation] (
    [SupplierContactLocationID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationID]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]               DATETIME         NULL,
    [UpdatedByUserID]           UNIQUEIDENTIFIER NULL,
    [UpdatedDate]               DATETIME         NULL,
    CONSTRAINT [PK_tblSupplierContactLocation] PRIMARY KEY CLUSTERED ([SupplierContactLocationID] ASC),
    CONSTRAINT [FK_tblSupplierContactLocation_tblSupplierContact] FOREIGN KEY ([SupplierContactID]) REFERENCES [dbo].[tblSupplierContact] ([SupplierContactID]),
    CONSTRAINT [FK_tblSupplierContactLocation_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID]),
    CONSTRAINT [FK_tblSupplierContactLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierContactLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

