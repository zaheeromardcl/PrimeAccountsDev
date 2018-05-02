CREATE TABLE [dbo].[tlkpPurchaseChargeType] (
    [PurchaseChargeTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PurchaseChargeTypeCode] NVARCHAR (10)    NOT NULL,
    [PurchaseChargeTypeName] NVARCHAR (100)   NOT NULL,
    [NominalAccountID]       UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]              UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]              NVARCHAR (25)    NULL,
    [UpdatedDate]            DATETIME         NULL,
    [CreatedBy]              NVARCHAR (25)    NULL,
    [CreatedDate]            DATETIME         NULL,
    [IsActive]               BIT              CONSTRAINT [DF_tlkpPurchaseChargeType_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPurchaseChargeType_1] PRIMARY KEY CLUSTERED ([PurchaseChargeTypeID] ASC),
    CONSTRAINT [FK_tlkpPurchaseChargeType_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

