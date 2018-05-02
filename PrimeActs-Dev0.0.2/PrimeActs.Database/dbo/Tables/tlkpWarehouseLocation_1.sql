CREATE TABLE [dbo].[tlkpWarehouseLocation] (
    [WarehouseLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [WarehouseLocationName] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]             NVARCHAR (25)    NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedBy]             NVARCHAR (25)    NULL,
    [CreatedDate]           DATETIME         NULL,
    [IsActive]              BIT              CONSTRAINT [DF__tlkpWareh__IsAct__7DCDAAA2] DEFAULT ((1)) NOT NULL,
    [CompanyID]             UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblWarehouseLocation] PRIMARY KEY CLUSTERED ([WarehouseLocationID] ASC),
    CONSTRAINT [FK_tlkpWarehouseLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

