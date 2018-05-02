CREATE TABLE [dbo].[tlkpPurchaseType] (
    [PurchaseTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PurchaseTypeName] NVARCHAR (50)    NOT NULL,
    [PurchaseTypeCode] NVARCHAR (10)    NOT NULL,
    [CompanyID]        UNIQUEIDENTIFIER NOT NULL,
    [IsActive]         BIT              CONSTRAINT [DF_tlkpPurchaseTypeValues_IsActive] DEFAULT ((1)) NOT NULL,
    [UpdatedByUserID]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpPurchaseType] PRIMARY KEY CLUSTERED ([PurchaseTypeID] ASC),
    CONSTRAINT [FK_tlkpPurchaseType_UpdatedByID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpPurchaseTypeValues_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

