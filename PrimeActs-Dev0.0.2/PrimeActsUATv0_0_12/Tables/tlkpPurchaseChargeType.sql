CREATE TABLE [dbo].[tlkpPurchaseChargeType] (
    [PurchaseChargeTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PurchaseChargeTypeCode] NVARCHAR (10)    NOT NULL,
    [PurchaseChargeTypeName] NVARCHAR (100)   NOT NULL,
    [NominalAccountID]       UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]              UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]            DATETIME         NULL,
    [CreatedDate]            DATETIME         NULL,
    [IsActive]               BIT              CONSTRAINT [DF_tlkpPurchaseChargeType_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]        UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]        UNIQUEIDENTIFIER NULL,
    [UseRebate]              BIT              NOT NULL,
    CONSTRAINT [PK_tlkpPurchaseChargeType_1] PRIMARY KEY CLUSTERED ([PurchaseChargeTypeID] ASC),
    CONSTRAINT [FK_tlkpPurchaseChargeType] FOREIGN KEY ([NominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID]),
    CONSTRAINT [FK_tlkpPurchaseChargeType_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpPurchaseChargeTypeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPurchaseChargeTypeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

