CREATE TABLE [dbo].[tlkpWarehouseLocation] (
    [WarehouseLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [WarehouseLocationName] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedDate]           DATETIME         NULL,
    [IsActive]              BIT              DEFAULT ((1)) NOT NULL,
    [CompanyID]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblWarehouseLocation] PRIMARY KEY CLUSTERED ([WarehouseLocationID] ASC),
    CONSTRAINT [FK_tlkpWarehouseLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpWarehouseLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpWarehouseLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

