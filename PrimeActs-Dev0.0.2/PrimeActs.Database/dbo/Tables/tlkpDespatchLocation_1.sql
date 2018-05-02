CREATE TABLE [dbo].[tlkpDespatchLocation] (
    [DespatchLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [DespatchLocationCode] NVARCHAR (10)    NOT NULL,
    [DespatchLocationName] NVARCHAR (100)   NOT NULL,
    [CompanyID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]            NVARCHAR (25)    NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedBy]            NVARCHAR (25)    NULL,
    [CreatedDate]          DATETIME         NULL,
    [IsActive]             BIT              CONSTRAINT [DF_tlkpDespatchLocation_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpDespatch] PRIMARY KEY CLUSTERED ([DespatchLocationID] ASC),
    CONSTRAINT [FK_tlkpDespatchLocation_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

