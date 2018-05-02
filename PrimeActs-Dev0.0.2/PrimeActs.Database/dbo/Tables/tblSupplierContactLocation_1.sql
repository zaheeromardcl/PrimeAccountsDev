CREATE TABLE [dbo].[tblSupplierContactLocation] (
    [SupplierContactLocationID] UNIQUEIDENTIFIER NOT NULL,
    [SupplierContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationID]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblSupplierContactLocation] PRIMARY KEY CLUSTERED ([SupplierContactLocationID] ASC),
    CONSTRAINT [FK_tblSupplierContactLocation_tblSupplierContact] FOREIGN KEY ([SupplierContactID]) REFERENCES [dbo].[tblSupplierContact] ([SupplierContactID]),
    CONSTRAINT [FK_tblSupplierContactLocation_tblSupplierLocation] FOREIGN KEY ([SupplierLocationID]) REFERENCES [dbo].[tblSupplierLocation] ([SupplierLocationID])
);

