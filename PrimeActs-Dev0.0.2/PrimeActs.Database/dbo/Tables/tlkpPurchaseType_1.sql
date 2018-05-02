CREATE TABLE [dbo].[tlkpPurchaseType] (
    [PurchaseTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PurchaseTypeName] NVARCHAR (50)    NOT NULL,
    [PurchaseTypeCode] NVARCHAR (10)    NOT NULL,
    [CompanyID]        UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]        NVARCHAR (25)    NULL,
    [UpdatedDate]      DATETIME         NULL,
    [CreatedBy]        NVARCHAR (25)    NULL,
    [CreatedDate]      DATETIME         NULL,
    [IsActive]         BIT              CONSTRAINT [DF_tlkpPurchaseTypeValues_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPurchaseType] PRIMARY KEY CLUSTERED ([PurchaseTypeID] ASC),
    CONSTRAINT [FK_tlkpPurchaseTypeValues_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

